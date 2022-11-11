using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Homework_6_KravchukSophia.FileReader;
using static System.Net.Mime.MediaTypeNames;

namespace Homework_6_KravchukSophia
{
    public class AllUsersData
    {
        private Dictionary<string, User> users;
        public Dictionary<string, User> Users { get { return users.ToDictionary(entry => entry.Key, entry => entry.Value); } }
        public List<string> AllUsers { get { return this.users.Keys.ToList(); } }
        public AllUsersData()
        {
            this.users = new Dictionary<string, User>();
        }

        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User is null");
            }
            else if (this.UserAlreadyExists(user.UserSurname))
            {
                this.users[user.UserSurname] = user.Clone() as User;
            }
            else
            {
                this.users.Add(user.UserSurname, user);
            }
           
        }

        public User? GetUserByName(string userSurname)
        {
            return this.users[userSurname].Clone() as User;
        }
        public bool UserAlreadyExists(string userSurname) 
        { 
            return this.users.ContainsKey(userSurname);
        }

        public DateTime GetLastMeasurementDate()
        {
            DateTime lastMeasurementDate = DateTime.MinValue;
            foreach (KeyValuePair<string, User> userPair in this.Users)
            {
                DateTime currentUserDate = userPair.Value.GetLastEditedDate();
                lastMeasurementDate = (currentUserDate - lastMeasurementDate).Days > 0 ? currentUserDate : lastMeasurementDate;
            }
            return lastMeasurementDate;
        }

        public User GetTheBiggestDebtor()
        {
            string debtorName = "";
            foreach (KeyValuePair<string, User> userPair in this.Users)
            {
                if (debtorName == "")
                {
                    debtorName = userPair.Key;
                }
                else
                {
                    debtorName = userPair.Value.CompareTo(this.users[debtorName]) > 0 ? userPair.Key : debtorName;
                }
            }
            return this.users[debtorName].Clone() as User;
        }

        public override string ToString()
        {
            string text = "";
            foreach (KeyValuePair<string, User> userPair in this.Users)
            {
                text += userPair.Value.ToString() + "\n";
            }
            return text;
        }
    }
}
