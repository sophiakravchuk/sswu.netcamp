using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_KravchukSophia
{
    public sealed class DesksManager
    {
        private Dictionary<int, PayDesk> desks;
        private int numberOfDesks;

        private static readonly object _lock = new object();
        private static DesksManager _instance;

        public static DesksManager GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DesksManager();
                    }
                }
            }
            return _instance;
        }
        private DesksManager(int numberOfDesks = 10)
        {
            this.numberOfDesks = numberOfDesks;
            this.CreateDesks();
        }

        public void FilterVIPs(int deskId, int peopleInQueue)
        {
            lock (_lock)
            {
                PayDesk desk = this.desks[deskId];
                if (desk.QueueWasOverloaded)
                {
                    desk.CloseDeskWithoutRearranging();
                    return;
                }
                for (int i = 0; i <= desk.PeopleInTheQueue; i++)
                {
                    Customer customer;

                    customer = desk.CustomerServed();

                    if (customer == null)
                    {
                        break;
                    }
                    if (customer.Status == CustomerStatuses.VIP)
                    {
                        this.AssignToADesk(deskId, customer, false);
                    }
                    else
                    {
                        int newDeskId = customer.PickAPayDesk(desk.Id);
                        this.AssignToADesk(newDeskId, customer, false);

                        ActivityReport.WriteACustomerMoved(customer, desk.Id, newDeskId, (DateTime.Now - customer.GotInTheQueue).Seconds);
                    }
                }
                desk.QueueWasOverloaded = true;
                desk.CheckQueueLength();
            }
        }
        public int GetDeskWithTheShortestQueue(int evoidDesk = -1)
        {
            int shortestQueueId = -1;
            int smallestAmount = int.MaxValue;
            foreach (KeyValuePair<int, PayDesk> deskPair in this.desks)
            {

                if (deskPair.Value.IsOpened && deskPair.Value.Id != evoidDesk)
                {
                    shortestQueueId = deskPair.Value.PeopleInTheQueue < smallestAmount ? deskPair.Value.Id : shortestQueueId;
                    smallestAmount = deskPair.Value.PeopleInTheQueue < smallestAmount ? deskPair.Value.PeopleInTheQueue : smallestAmount;
                }

            }
            return shortestQueueId;
        }
        public int GetClosestDesk(int customerCoord, int evoidDesk = -1)
        {
            int closestDeskId = -1;
            int closestDeskDistance = int.MaxValue;
            foreach (KeyValuePair<int, PayDesk> deskPair in this.desks)
            {
                if (deskPair.Value.IsOpened && deskPair.Value.Id != evoidDesk)
                {
                    closestDeskId = Math.Abs(deskPair.Value.LocationY - customerCoord) < closestDeskDistance ? deskPair.Value.Id : closestDeskId;
                    closestDeskDistance = Math.Abs(deskPair.Value.LocationY - customerCoord) < closestDeskDistance ? Math.Abs(deskPair.Value.LocationY - customerCoord) : closestDeskDistance;
                }
            }
            return closestDeskId;
        }
        public void AssignToADesk(int deskId, Customer customer, bool checkLength = true)
        {
            this.desks[deskId].AddCustomer(customer, checkLength);
            customer.CustomerGotInTheQueue(this.desks[deskId].LocationY);
        }
        public int PeopleInTheQueue(int deskId)
        {
            return this.desks[deskId].PeopleInTheQueue;
        }
        public void CloseADesk(int deskId)
        {
            PayDesk desk = this.desks[deskId];
            if (!desk.IsOpened)
            {
                return;
            }
            this.RearrangeQueue(desk);
            try
            {
                desk.CloseDesk();
            }
            catch (Exception e)
            {
                this.CloseADesk(deskId);

            }
        }
        public int ServeOneCustomerPerDesk()
        {
            lock (_lock)
            {
                int customersServed = 0;
                foreach (KeyValuePair<int, PayDesk> deskPair in this.desks)
                {
                    if (deskPair.Value.IsOpened)
                    {

                        Customer customer = deskPair.Value.CustomerServed();
                        if (customer != null)
                        {
                            ActivityReport.WriteACustomerServed(customer, deskPair.Key, customer.WaitedTimeSeconds());
                            customersServed++;
                        }

                    }
                }
                return customersServed;
            }
        }

        private void RearrangeQueue(PayDesk desk)
        {
            lock (_lock)
            {
                while (true)
                {
                    Customer customer = desk.CustomerServed();
                    if (customer == null)
                    {
                        break;
                    }
                    int newDeskId = customer.PickAPayDesk(desk.Id);
                    this.AssignToADesk(newDeskId, customer);

                    ActivityReport.WriteACustomerMoved(customer, desk.Id, newDeskId, (DateTime.Now - customer.GotInTheQueue).Seconds);
                }
            }
        }
        private void CreateDesks()
        {
            int deskCoordinate = 0;
            this.desks = new Dictionary<int, PayDesk>();
            for (int i = 0; i < this.numberOfDesks; i++)
            {
                PayDesk desk = new PayDesk(i, deskCoordinate);
                desk.QueueOverloaded += this.FilterVIPs;
                this.desks[i] = desk;
                deskCoordinate += 2;
            }
        }
    }
}

