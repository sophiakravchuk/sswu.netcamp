using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_6_KravchukSophia
{
    public static class SystemReport
    {
        public static string GetReportForUser(AllUsersData allUsersData, string surname, double price)
        {
            string reportText = "";
            if(allUsersData == null || !allUsersData.UserAlreadyExists(surname))
            {
                throw new Exception("There is no such user");
            }
            User user = allUsersData.GetUserByName(surname);
            reportText += String.Format($"Report for {user.UserSurname}, apartment {user.UserApartmentNumber}\n");
            reportText += user.GetLongReport();
            reportText += String.Format("Whole price: {0} UAH\n", Math.Round(user.GetWholePrice(price), 2));
            return reportText;
        }

        public static async Task GetReportForAllUsersAsync(AllUsersData allUsersData, double price, string path)
        {
            List<string> users = allUsersData.AllUsers;
            using StreamWriter file = new(path, append: true);

            foreach (string user in users)
            {
                string reportText = "";
                User userData = allUsersData.GetUserByName(user);
                reportText += userData.GetShortReport();
                reportText += String.Format("Whole price: {0} UAH\n\n\n", Math.Round(userData.GetWholePrice(price)));
                await file.WriteLineAsync(reportText);
            }
        }

        public static string GetApartmentWithZeroElectricityUsage(AllUsersData allUsersData)
        {
            List<string> users = allUsersData.AllUsers;
            //int apNumber = 0;
            string apNums = "";
            foreach (string user in users)
            {
                User userData = allUsersData.GetUserByName(user);
                //apNumber = userData.GetWholeAmountOfEnergy() == 0 ? userData.UserApartmentNumber : apNumber;
                apNums += userData.GetWholeAmountOfEnergy() == 0 ? " #" + userData.UserApartmentNumber.ToString() : "";

            }
            return apNums.Length == 0 ? "There are no such apartment" : $"Apartment {apNums} has zero usage of electricity";
        }

        public static string GetTheBiggestDebtor(AllUsersData allUsersData)
        {
            return allUsersData.GetTheBiggestDebtor().UserSurname;

        }
        public static string DaysTillLastMeasurement(AllUsersData allUsersData)
        {
            int days = (DateTime.Now - allUsersData.GetLastMeasurementDate()).Days;

            return $"{days} days untill last measurement";

        }
    }
}
