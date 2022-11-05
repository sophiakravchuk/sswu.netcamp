

namespace Homework_4_KravchukSophia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime date1 = new DateTime(2022, 12, 1);
            DateTime date2 = new DateTime(2023, 12, 1);

            Product product1 = new Product("cherry", 14, 2.0);
            Product product2= new Product("banana", 17.61, 0.75);
            Product product3 = new Product("banana", 17.61, 0.75);
            Product productHeavyExpensive = new Product("beans", 30.0, 8.0);

            Console.WriteLine(product2.CompareTo(product3));

            Meat meat = new Meat("meat", 100, 1.0, MeatTypes.Mutton, MeatClasses.HighClass);
            Meat meat2 = new Meat("meat", 100, 1.0, MeatTypes.Mutton, MeatClasses.SecondClass);
            Meat meat3 = new Meat("meat", 80, 1.0, MeatTypes.Chicken, MeatClasses.SecondClass);

            Console.WriteLine(meat.CompareTo(null));

            DairyProducts dairy1 = new DairyProducts("milk", 100, 0.75, DateTime.Now);
            DairyProducts dairy2 = new DairyProducts("milk", 100, 0.75, date1);
            DairyProducts dairy3 = new DairyProducts("milk", 100, 0.75, date2);

            Console.WriteLine(dairy1.CompareTo(dairy1));

            Cart cart = new Cart();
            cart.AddBuy(product1, 3);
            cart.AddBuy(dairy3, 1);
            cart.AddBuy(dairy3, 1);
            cart.AddBuy(meat3, 2);
            cart.AddBuy(product2, 1);

            Cart cart2 = new Cart();
            cart2.AddBuy(product1, 3);

            Console.WriteLine(cart.CompareTo(cart2));



            Storage storage = new Storage();
            storage.AddProduct(product2);
            storage.AddProduct(meat3);
            storage.AddProduct(dairy3);
            storage.AddProduct(meat2);
            
            Dictionary<Product, int> meats = storage.GetAllMeat();

            Storage storage2 = new Storage(meats);
            Storage storage3 = new Storage(productHeavyExpensive);

            Console.WriteLine(storage.CompareTo(storage2));

            Storage[] storageArray =
            {
                storage3,
                storage,
                storage2
            };

            Array.Sort(storageArray, Storage.SortMoreExpensive());

            foreach (Storage c in storageArray)
                Console.WriteLine(c.GetWholeCost() + "\t\t");

            Array.Sort(storageArray, Storage.SortHeavier());

            foreach (Storage c in storageArray)
                Console.WriteLine(c.GetWholeWeight() + "\t\t");

            Array.Sort(storageArray);

            foreach (Storage c in storageArray)
                Console.WriteLine(c.productsInStorage.Count() + "\t\t");


            int[] arr =
            {
                0, 1,
                2, 3, 5, 7, 11, 13, 17, 19, 23, //9
                9, 45,
                29, 31, 37, 41, 43, 47, 53, 59,//8
                8,
                61, 67, 71, 73, 79, 83, 89, 97, 101, 149,//10
                0, 103, 107, 109,
                1,
                113 , 127, 131, 137, 139, 149
            };

            Task2ArrayCover arrayNumbers = new Task2ArrayCover(arr);

            Console.WriteLine("Frequency Table: " + arrayNumbers.GetFrequencyTable()+"\n");
            Console.WriteLine(arrayNumbers.GetTwoLongestSequences());
        }
    }
}