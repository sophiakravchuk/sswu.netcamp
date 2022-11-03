//Створити 3класи:
//1.Клас Product, який має три елемент-даних – назва, ціна і вага
//2. Клас Buy, містить дані про товар, кількість одиниць товару, що
//купується в штуках, вміє обчислювати ціну за весь куплений товар.
//3. Клас Check, що не містить ніяких елементів-даних. Цей клас повинен
//виводити на екран інформацію про товар і про покупку;
//Створити конструктори класів, визначити властивості з різними
//модифікаторами. Перевизначити метод ToString.
//Продемонструвати створення екземплярів класів.

namespace Homework_3_KravchukSophia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Product product1 = new Product("cherry", 14, 2.0);
            Console.WriteLine(product1 + "\n");
            product1.ChangePrice(10);
            Console.WriteLine(product1 + "\n");

            Product product2= new Product("banana", 17.61, 0.75);
            Console.WriteLine(product2 + "\n");
            product2.ChangePrice(10);
            Console.WriteLine(product2 + "\n");

            Meat meat = new Meat("meat", 100, 1.0, MeatTypes.Mutton, MeatClasses.HighClass);
            Console.WriteLine(meat + "\n");
            meat.ChangePrice(10);
            Console.WriteLine(meat + "\n");

            Meat meat2 = new Meat("meat", 100, 1.0, MeatTypes.Mutton, MeatClasses.SecondClass);
            Console.WriteLine(meat2 + "\n");
            meat2.ChangePrice(10);
            Console.WriteLine(meat2 + "\n");

            Meat meat3 = new Meat("meat", 100, 1.0, MeatTypes.Chicken, MeatClasses.SecondClass);
            Console.WriteLine(meat3 + "\n");
            meat3.ChangePrice(10);
            Console.WriteLine(meat3 + "\n");


            DateTime date1 = new DateTime(2022, 12, 1);
            DateTime date2 = new DateTime(2023, 12, 1);


            DairyProducts dairy1 = new DairyProducts("milk", 100, 0.75, DateTime.Now);
            Console.WriteLine(dairy1 + "\n");
            dairy1.ChangePrice(10);
            Console.WriteLine(dairy1 + "\n");

            DairyProducts dairy2 = new DairyProducts("milk", 100, 0.75, date1);
            Console.WriteLine(dairy2 + "\n");
            dairy2.ChangePrice(10);
            Console.WriteLine(dairy2 + "\n");

            DairyProducts dairy3 = new DairyProducts("milk", 100, 0.75, date2);
            Console.WriteLine(dairy3 + "\n");
            dairy3.ChangePrice(10);
            Console.WriteLine(dairy3 + "\n");

            Storage storage = new Storage();
            storage.AddProduct(product2);
            storage.AddProduct(meat3);
            storage.AddProduct(dairy3);
            storage.AddProduct(meat2);
            Console.WriteLine("___________________________________\n" + storage + "\n");
            List<Product> meats = storage.GetAllMeat();

            Storage storage2 = new Storage(meats);
            Console.WriteLine("___________________________________\n" + storage2 + "\n");
            storage2.ChangePriceForAllProducts(10);
            Console.WriteLine("___________________________________\n" + storage2 + "\n");


            //Buy buy1 = new Buy(product1, 5);
            //Buy buy2 = new Buy(product2, 3);
            //Buy[] buys = new Buy[] { buy1, buy2 };
            //Check firstCheck = new Check(buy1);
            //Check secondCheck = new Check(buys);


            //Console.WriteLine(firstCheck);
            //Console.WriteLine(secondCheck);
        }
    }
}