using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_KravchukSophia
{
    public class Customer
    {
        private string name;
        private int age;
        private CustomerStatuses status;
        private int acceptableWaitingTime;
        private int coordinateY;
        private DateTime gotInTheQueue;

        public string Name { get { return this.name; } }
        public CustomerStatuses Status { get {return status; } }
        public DateTime GotInTheQueue { get { return this.gotInTheQueue; } }

        public Customer(string name, int age, CustomerStatuses status, int acceptableWaitingTime, int coordinateY)
        {
            this.name = name;
            this.age = age;
            this.status = status;
            this.acceptableWaitingTime = acceptableWaitingTime;
            this.coordinateY = coordinateY;
        }

        public int PickAPayDesk(int evoidDesk=-1)
        {
            DesksManager manager = DesksManager.GetInstance();
            int queueId = manager.GetDeskWithTheShortestQueue(evoidDesk);
            queueId = queueId == -1 ? manager.GetClosestDesk(this.coordinateY, evoidDesk) : queueId;
            return queueId;
        }
        public void CustomerGotInTheQueue(int deskCoord)
        {
            this.gotInTheQueue = DateTime.Now;
            this.coordinateY = deskCoord;
        }
        public int WaitedTimeSeconds()
        {
            return (DateTime.Now - this.gotInTheQueue).Seconds;
        }
        public override string ToString()
        {
            return $"Customer {this.name} \n\tAge {this.age} \n\tStatus {this.status} " +
                $"\n\tCan wait {this.acceptableWaitingTime} \n\tCoord {this.coordinateY}" ;
        }
    }
}
