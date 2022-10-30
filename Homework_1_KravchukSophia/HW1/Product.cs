using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HW1
{
    internal class Product
    {
        private readonly string name;
        private readonly double price;
        private readonly double weight;

        public string Name { get { return name; } }
        public double Price { get { return price; } }
        public double Weight { get { return weight; } }

        public Product(string name, double price, double weight)
        {
            this.name = name;
            this.price = price;
            this.weight = weight;
        }

        public Product()
        {
            name = "Empty product";
            price = 0;
            weight = 0;
        }

        public override string ToString()
        {
            return "Product name: " + this.name + "\nPrice per piece: " + this.price + "\nWeight: " + this.weight;
        }
    }
}




