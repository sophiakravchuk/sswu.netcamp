using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Homework_9_KravchukSophia
{
    public class Product : ICloneable, IComparable
    {
        private readonly string name;
        private double priceInUAH;
        private readonly double weightInKG;

        public virtual string Name { get { return name; } }
        public double Price
        {
            get { return priceInUAH; }
            set
            {
                if (priceInUAH >= 0)
                {
                    this.priceInUAH = value;
                }
                else
                {
                    throw new ArgumentException(String.Format("Price should be positive values. Price: {0}", value));
                }

            }
        }
        public double Weight { get { return weightInKG; } }

        public Product(string name, double price, double weight=1)
        {
            this.name = name;
            if (price < 0 || weight < 0)
            {
                throw new ArgumentException(String.Format("Price and weight should be positive values. Price: {0}, Weight: {1}", price, weight));
            }
            this.priceInUAH = price;
            this.weightInKG = weight;
        }

        public Product()
        {
            name = "Empty product";
            priceInUAH = 0;
            weightInKG = 0;
        }

        public virtual double ChangePrice(int percentage)
        {
            if (percentage < 0)
            {
                throw new Exception(String.Format("Percentageshould be positive value. Given percentage: {0}", percentage));
            }

            this.priceInUAH = Math.Round((this.priceInUAH * (100 - percentage)) / 100, 2);
            return this.priceInUAH;
        }

        public override string ToString()
        {
            return "Product name: " + this.name + "\nPrice per piece: " + this.priceInUAH + "\nWeight: " + this.weightInKG;
        }

        public object Clone()
        {
            return (Product)MemberwiseClone();
        }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Product p = (Product)obj;
                return (this.Name == p.Name) && (this.Price == p.Price)
                    && (this.Weight == p.Weight);
            }
        }

        public virtual int CompareTo(Object? obj)
        {
            Product other = obj as Product;
            if (other == null) return 1;

            return String.Compare(this.Name, other.Name);
        }

        public bool IsMoreExpensive(Object? obj)
        {
            Product other = obj as Product;
            if (other == null) return false;

            return this.Price >= other.Price;
        }

    }
}




