using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_KravchukSophia
{
    public delegate void QueueOverloadedEventsHandler(int deskId, int peopleInQueue);

    public class PayDesk
    {
        private int locationY;
        private int id;
        private bool isOpened;
        private PriorityQueue<Customer, CustomerStatuses> queue;
        private static readonly object _lock = new object();
        private bool queueWasOverloaded = false;
        public bool QueueWasOverloaded { get { return this.queueWasOverloaded; }
            set { this.queueWasOverloaded = value; } }

        private int maxLengthOfQueue;

        public event QueueOverloadedEventsHandler QueueOverloaded;

        public int PeopleInTheQueue { get { return this.queue.Count; } }
        public int Id { get { return this.id; } }
        public int LocationY { get { return this.locationY; } }
        public bool IsOpened { get { return this.isOpened; } }

        public PayDesk(int deskId, int deskCoordinate, int maxLengthOfQueue=2)
        {
            this.id = deskId;
            this.locationY = deskCoordinate;
            this.isOpened = true;

            this.queue = new PriorityQueue<Customer, CustomerStatuses>();
            this.maxLengthOfQueue = maxLengthOfQueue;
        }

        public void AddCustomer(Customer customer, bool checkLength=true)
        {
            if (this.isOpened)
            {
                lock (_lock)
                {
                    this.queue.Enqueue(customer, customer.Status);
                }
                if (checkLength)
                {
                    this.CheckQueueLength();
                }
            } else
            {
                throw new Exception("Pay desk is closed");
            }
        }
        public void CheckQueueLength()
        {
            lock (_lock)
            {
                if (this.maxLengthOfQueue < this.queue.Count)
                {
                    this.QueueOverloaded?.Invoke(this.id, this.queue.Count);
                }
            }
        }
        public Customer CustomerServed()
        {
            lock (_lock)
            {
                this.queue.TryDequeue(out Customer customer, out CustomerStatuses status);

                if (this.queue.Count <= this.maxLengthOfQueue / 2)
                {
                    this.isOpened = true;
                }
                return customer;
            }
        }
        public void CloseDesk()
        {
            lock (_lock)
            {
                if (this.queue.Count > 0)
                {
                    throw new Exception("Queue is not empty");
                }
                this.isOpened = false;
            }
        }
        public void CloseDeskWithoutRearranging()
        {
            lock (_lock)
            {
                this.isOpened = false;

            }
        }
    }
}
