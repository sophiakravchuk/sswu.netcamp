using Homework_8_KravchukSophia;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Homework_8_KravchukSophia
{

    public enum MeatTypes
    {
        Unknown,
        Mutton = 1,
        Veal = 3,
        Pork = 5,
        Chicken = 8
    }

    public enum MeatClasses
    {
        Unknown,
        HighClass = 1,
        SecondClass = 5
    }

    public class Meat : Product, IComparable
    {
        private readonly MeatTypes meatType;
        public readonly MeatClasses meatClass;

        public MeatTypes MeatType { get { return meatType; } }
        public override string Name { get { return String.Format("{0} {1} {2}", base.Name, this.meatType, this.meatClass); } }
        public MeatClasses MeatClass { get { return meatClass; } }

        public Meat() : base()
        {
            this.meatType = MeatTypes.Unknown;
            this.meatClass = MeatClasses.Unknown;
        }

        public Meat(string name, double price, double weight) : base(name, price, weight)
        {
            this.meatType = MeatTypes.Unknown;
            this.meatClass = MeatClasses.Unknown;
        }
        public Meat(string name, double price, double weight, MeatTypes meatType, MeatClasses meatClass) : base(name, price, weight)
        {
            this.meatType = meatType;
            this.meatClass = meatClass;
        }

        public override double ChangePrice(int percentage)
        {
            if (percentage < 0)
            {
                throw new Exception(String.Format("Percentageshould be positive value. Given percentage: {0}", percentage));
            }
            return base.ChangePrice(percentage + ((int)this.meatClass + (int)this.meatType));
        }

        public override string ToString()
        {
            return base.ToString() + String.Format("\nMeat type: {0}\nMeat class: {1}", this.meatType, this.meatClass);
        }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Meat p = (Meat)obj;
                return base.Equals(obj) && (this.MeatClass == p.MeatClass)
                    && (this.MeatType == p.MeatType);
            }
        }

    }
}
