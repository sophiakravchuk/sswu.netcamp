using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12_KravchukSophia
{
    public enum CustomerStatuses
    {
        Com = 1,
        VIP = 0
    }
    public class CustomersGenerator
    {
        private StreamReader customersFileReader;
        private int coordMin;
        private int coordMax;

        public CustomersGenerator(string filepath, int coordMin = 0, int coordMax = 20)
        {
            this.customersFileReader = new StreamReader(filepath);
            this.coordMin = coordMin;
            this.coordMax = coordMax;
        }

        public Customer? GetNewCustomer()
        {
            if (this.customersFileReader.EndOfStream)
            {
                return null;
            }
            Random random = new Random();
            string? line = this.customersFileReader.ReadLine();
            Customer customer;
            try
            {
                string[] elements = line.Split(',');
                Enum.TryParse(elements[2].Trim(), out CustomerStatuses status);
                customer = new Customer(elements[0].Trim(), Convert.ToInt32(elements[1]),
                    status, Convert.ToInt32(elements[3]), random.Next(this.coordMin, this.coordMax));
            }
            catch (Exception e)
            {
                return null;
            }
            return customer;
        }
    }
}
