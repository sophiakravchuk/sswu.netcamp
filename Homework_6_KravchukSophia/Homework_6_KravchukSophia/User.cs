using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Homework_6_KravchukSophia.FileReader;

namespace Homework_6_KravchukSophia
{
    public class User : ICloneable, IComparable
    {
        private string userSurname;
        private int userApartmentNumber;
        private string userAddress;
        private Dictionary<Quarters, Dictionary<DateTime, int>> electricityUsage;

        public string UserSurname { get { return userSurname; } }
        public int UserApartmentNumber { get { return this.userApartmentNumber; } }
        public string UserAddress { get { return userAddress; } }
        public User(string surname, int apartmentNumber, string address)
        {
            this.userApartmentNumber = apartmentNumber;
            this.userAddress = address;
            this.userSurname = surname;
            this.electricityUsage = new Dictionary<Quarters, Dictionary<DateTime, int>>();
        }

        public User()
        {
            this.userApartmentNumber = 0;
            this.userAddress = "";
            this.userSurname = "";
            this.electricityUsage = new Dictionary<Quarters, Dictionary<DateTime, int>>();
        }

        public void AddMeasurments(Dictionary<Quarters, Dictionary<DateTime, int>> newMeasurements)
        {
            foreach (KeyValuePair<Quarters, Dictionary<DateTime, int>> quarterPair in newMeasurements)
            {
                if (!this.electricityUsage.ContainsKey(quarterPair.Key))
                {
                    if (quarterPair.Value is null)
                    {
                        throw new ArgumentNullException();
                    }
                    if (this.MeasurementsAreCorrect(quarterPair.Value))
                    {
                        this.electricityUsage.Add(quarterPair.Key, quarterPair.Value.ToDictionary(entry => entry.Key, entry => entry.Value));

                    }
                    else
                    {
                        throw new ArgumentException("Measurments are not correct");
                    }
                }
                else //Quarter already exists
                {
                    throw new ArgumentException("Measurments of this quarter already exists");
                }

            }
            
        }

        public Quarters GetLastEditedQuarter()
        {
            Quarters prevQuarter = Quarters.Unknown;
            foreach (Quarters quarter in this.electricityUsage.Keys)
            {
                prevQuarter = quarter > prevQuarter? quarter : prevQuarter;
            }
            return prevQuarter;
        }
        public DateTime GetLastEditedDate(Quarters lastEditedQuarter = Quarters.Unknown)
        {
            Quarters lastQuarter = lastEditedQuarter == Quarters.Unknown ? this.GetLastEditedQuarter() : lastEditedQuarter;
            DateTime prevDate = DateTime.MinValue;
            foreach (DateTime date in this.electricityUsage[lastQuarter].Keys)
            {
                prevDate = date > prevDate ? date : prevDate;
            }
            return prevDate;
        }
        public int GetLastMeasurement()
        {
            Quarters lastQuarter = this.GetLastEditedQuarter();
            DateTime lastDate = this.GetLastEditedDate(lastQuarter);

            return this.electricityUsage[lastQuarter][lastDate];
        }

        public int GetWholeAmountOfEnergy()
        {
            int amountOfEnergy = 0;
            foreach (KeyValuePair<Quarters, Dictionary<DateTime, int>> quarter in this.electricityUsage)
            {
                amountOfEnergy += this.GetAmountOfEnergyForQuarter(quarter.Key);
            }
            return amountOfEnergy;
        }
        public int GetAmountOfEnergyForQuarter(Quarters quarter, int prevMeas = 0)
        {
            int previousMeasurement = prevMeas;
            int amountOfEnergy = 0;
            foreach (KeyValuePair<DateTime, int> measurement in this.electricityUsage[quarter])
            {
                amountOfEnergy += previousMeasurement == 0? 0 : (measurement.Value - previousMeasurement);
                previousMeasurement = measurement.Value;
            }
            return amountOfEnergy;
        }
        public double GetWholePrice(double price)
        {
            return price*this.GetWholeAmountOfEnergy();
        }

        public string GetLongReport()
        {
            string userText = "";
            foreach (KeyValuePair<Quarters, Dictionary<DateTime, int>> quarter in this.electricityUsage)
            {
                userText += $"Quarter {quarter.Key} => {this.GetAmountOfEnergyForQuarter(quarter.Key)} kWt" + "\n";
                foreach (KeyValuePair<DateTime, int> measurement in quarter.Value)
                {
                    userText += "\t" + measurement.Key.ToString("MMMM", CultureInfo.CreateSpecificCulture("en")) + "\n";
                    userText += "\t\t" + measurement.Key.ToString("dd.MM.yy") + " => " + measurement.Value + "\n";
                }
            }

            userText += $"Whole amount of used energy: {this.GetWholeAmountOfEnergy()} kWt\n";
            return userText;
        }
        public string GetShortReport()
        {
            string userText =$"Report for {this.UserSurname}, apartment {this.UserApartmentNumber} => {this.GetWholeAmountOfEnergy()} kWt\n";
            foreach (KeyValuePair<Quarters, Dictionary<DateTime, int>> quarter in this.electricityUsage)
            {
                userText += $"Quarter {quarter.Key} => {this.GetAmountOfEnergyForQuarter(quarter.Key)} kWt\n";
                foreach (KeyValuePair<DateTime, int> measurement in quarter.Value)
                {
                    userText += "\t" + measurement.Key.ToString("MMMM", CultureInfo.CreateSpecificCulture("en"));
                    userText += "  => " + measurement.Value + "\t";
                }
                userText += "\n";
            }

            return userText;
        }

        private bool MeasurementsAreCorrect(Dictionary<DateTime, int> measurements)
        {
            int lastMeasurement = this.electricityUsage.Count == 0 ? 0 : this.GetLastMeasurement();
            DateTime lastDate = DateTime.MinValue;

            foreach (KeyValuePair<DateTime, int> measurement in measurements)
            {
                if (((measurement.Key - lastDate).TotalDays <= 0) || ((measurement.Value - lastMeasurement) < 0))
                {
                    return false;
                }
                if (measurement.Value < 0)
                {
                    return false;
                }
                lastMeasurement = measurement.Value;
            }
            return true;
        }
        public int CompareTo(object? obj)
        {
            User other = obj as User;
            if (other == null) return 1;

            return this.GetWholeAmountOfEnergy() - other.GetWholeAmountOfEnergy();
        }
        public object Clone()
        {
            return (User)MemberwiseClone();
        }
        public override string ToString()
        {
            string userText = String.Format($"{this.userSurname} | {this.userApartmentNumber} | {this.userAddress}\n");
            foreach (KeyValuePair<Quarters, Dictionary<DateTime, int>> quarter in this.electricityUsage)
            {
                userText += quarter.Key + "\n";
                foreach (KeyValuePair<DateTime, int> measurement in quarter.Value)
                {
                    userText += "\t" + measurement.Key.ToString("dd.MM.yy") + " => " + measurement.Value + "\n";
                }
            }

            return userText;
        }
    }
    
}
