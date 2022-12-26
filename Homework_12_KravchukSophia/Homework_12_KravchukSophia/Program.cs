using System.Threading;

namespace Homework_12_KravchukSophia
{
    internal class Program
    {

        public static void CustomerServing()
        {
            DesksManager manager = DesksManager.GetInstance();
            bool nooneArrived = false;    
            while(true)
            {
                Thread.Sleep(2000); // time to serve a client 2 seconds
                int customersServed = manager.ServeOneCustomerPerDesk();
                if(customersServed == 0)
                {
                    if (nooneArrived)
                    {
                        break;
                    }
                    nooneArrived = true;
                    Thread.Sleep(1000); // waiting until customers come 1 second
                }
                else
                {
                    nooneArrived = false;
                }

            }
        }

        static void Main(string[] args)
        {

            CustomersGenerator generator = new CustomersGenerator("../../../Customers1.txt");
            DesksManager manager = DesksManager.GetInstance();
            Thread customerServingThread = new Thread(new ThreadStart(Program.CustomerServing));
            customerServingThread.Start();
            while (true) 
            {
                Customer? customer = generator.GetNewCustomer();
                Thread.Sleep(100); //time until new customer arrives 1 millisecond
                if (customer == null)
                {
                    break;
                }
                
                int deskId = customer.PickAPayDesk();
                manager.AssignToADesk(deskId, customer);

            }
            manager.CloseADesk(0); // closing desk 0
            
            customerServingThread.Join();

        }
    }
}