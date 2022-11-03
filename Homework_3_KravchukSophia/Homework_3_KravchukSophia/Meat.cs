using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

    public class Meat: Product
    {
        private readonly MeatTypes meatType;
        public readonly MeatClasses meatClass;

        public MeatTypes MeatType { get { return meatType; } }
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
            return base.ChangePrice(percentage+ ((int)this.meatClass + (int)this.meatType));
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
