using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Homework_5_KravchukSophia
{
    public class DairyProducts : Product
    {
        private readonly DateTime expirationDate;
        private readonly int daysTillExpiration;

        public DateTime ExpirationDate { get { return expirationDate; } }
        public int DaysTillExpiration { get { return daysTillExpiration; } }

        public DairyProducts() : base()
        {
            this.expirationDate = DateTime.Now;
            this.daysTillExpiration = 0;
        }

        public DairyProducts(string name, double price, double weight) : base(name, price, weight)
        {
            this.expirationDate = DateTime.Now;
            this.daysTillExpiration = 0;
        }

        public DairyProducts(string name, double price, double weight, DateTime expirationDate) : base(name, price, weight)
        {
            this.expirationDate = expirationDate;
            this.daysTillExpiration = (int)Math.Round((expirationDate - DateTime.Now).TotalDays);
        }

        public override double ChangePrice(int percentage)
        {
            if (percentage < 0)
            {
                throw new Exception(String.Format("Percentage should be positive value. Given percentage: {0}", percentage));
            }
            int discountCoef = 0;
            if (this.daysTillExpiration < 100)
            {
                discountCoef = (100 - this.daysTillExpiration)/10;
            }
            return base.ChangePrice(percentage + discountCoef);
        }


        public override string ToString()
        {
            return base.ToString() + String.Format("\nExpiration date: {0}\nDays till expiration: {1}", this.expirationDate, this.daysTillExpiration);
        }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                DairyProducts p = (DairyProducts)obj;
                return base.Equals(obj) && (this.ExpirationDate == p.ExpirationDate) 
                    && (this.DaysTillExpiration == p.DaysTillExpiration);
            }
        }
    }
}
