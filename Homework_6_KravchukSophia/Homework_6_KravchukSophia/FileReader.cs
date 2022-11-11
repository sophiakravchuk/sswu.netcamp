using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Homework_6_KravchukSophia
{
    public static class FileReader
    {
        public enum Quarters
        {
            Unknown = 0,
            I = 1,
            II = 2,
            III = 3,
            IV = 4
        }
        public static void ReadFileIntoSystem(string path, AllUsersData allUsersData)
        {
            int counter = 0;
            int amountOfUsers = 0;
            Quarters quarter = Quarters.Unknown;
            string userSurname = "";
            int apartmentNuber;
            string address;

            foreach (string line in System.IO.File.ReadLines(path))
            {
                if (line == "")
                {
                    throw new ArgumentException("FileLine is empty");
                }
                try
                {
                    string[] elements = line.Split('|');
                    if (counter == 0)
                    {
                        amountOfUsers = Convert.ToInt32(elements[0]);
                        quarter = Enum.TryParse(elements[1], true, out quarter) ? quarter : 0;
                    }
                    else
                    {
                        //Daniels | 168 | Charles Street Howell, NJ 07731 | 01.01.2022 | 134 | 01.02.2022 | 148 | 01.03.2022 | 167
                        if (elements == null || (elements.Count()-3) %2 != 0)
                        {
                            throw new Exception("Fileline is incorrect");
                        }



                        userSurname = elements[0];
                        User newUser;
                        if (allUsersData.UserAlreadyExists(userSurname))
                        {
                            newUser = allUsersData.GetUserByName(userSurname);
                        }
                        else
                        {
                            apartmentNuber = Convert.ToInt32(elements[1]);
                            address = elements[2];
                            newUser = new User(userSurname, apartmentNuber, address);
                        }

                        

                        Dictionary<Quarters, Dictionary<DateTime, int>> userAllQuarter = new Dictionary<Quarters, Dictionary<DateTime, int>>();
                        Dictionary<DateTime, int> userMonthMeasurement = new Dictionary<DateTime, int>();

                        for (int i = 3; i < elements.Count(); i += 2)
                        {
                            userMonthMeasurement.Add(Convert.ToDateTime(elements[i]), Convert.ToInt32(elements[i + 1]));
                        }
                        userAllQuarter.Add(quarter, userMonthMeasurement);

                        newUser.AddMeasurments(userAllQuarter);

                        allUsersData.AddUser(newUser);
                    }
                    counter++;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            if(amountOfUsers != counter-1)
            {
                throw new Exception("Amount of users is not correct");
            }
        }
    }
}
