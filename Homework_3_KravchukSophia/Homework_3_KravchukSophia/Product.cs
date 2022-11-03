using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Створити ієрархію класів, на основі сутностей, описаних в завданні 1.
//1. Описати похідні класи від класу Product:
//a.Клас Meat, що має поля «Категорія» з переліку (Вищий сорт 1
//сорт та 2 сорт), та «Вид»(баранина, телятина, свинина,
//курятина).
//b.Клас Dairy_products, що має поле термін придатності,
//визначений в днях.

//2. Визначити необхідні конструктори та властивості, а також
//перевантажити методи класу Object для всіх сутностей.
//Для всієї ієрархії передбачити метод зміни ціни.
//Для класу Product цей метод має змінювати ціну на задану кількість
//відсотків.
//Для класу Meat метод має змінювати ціну на задану кількість відсотків
//та додатково на відсотки, визначені як сталі нормативи складу,
//відповідно до категорії м’яса.
//Аналогічно для класу Product метод має змінювати ціну на задану
//кількість відсотків та додатково на відсотки, визначені як сталі
//нормативи складу, відповідно до терміну придатності.

namespace Homework_3_KravchukSophia
{
    public class Product : ICloneable
    {
        private readonly string name;
        private double price;
        private readonly double weight;

        public string Name { get { return name; } }
        public double Price { 
            get { return price; }  
            set 
            {
                if (price >= 0)
                {
                    this.price = value; 
                } 
                else
                {
                    throw new ArgumentException(String.Format("Price should be positive values. Price: {0}", value));
                }
               
            } }
        public double Weight { get { return weight; } }

        public Product(string name, double price, double weight)
        {
            this.name = name;
            if (price < 0 || weight < 0)
            {
                throw new ArgumentException(String.Format("Price and weight should be positive values. Price: {0}, Weight: {1}", price, weight));
            }
            this.price = price;
            this.weight = weight;
        }

        public Product()
        {
            name = "Empty product";
            price = 0;
            weight = 0;
        }

        public virtual double ChangePrice(int percentage) 
        {
            if (percentage < 0)
            {
                throw new Exception(String.Format("Percentageshould be positive value. Given percentage: {0}", percentage));
            }

            this.price = Math.Round((this.price * (100 - percentage)) /100, 2);
            return this.price;
        }

        public override string ToString()
        {
            return "Product name: " + this.name + "\nPrice per piece: " + this.price + "\nWeight: " + this.weight;
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
    }
}




