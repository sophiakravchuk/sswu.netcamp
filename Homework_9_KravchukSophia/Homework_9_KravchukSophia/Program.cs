using System;
using System.Diagnostics;
using System.IO;
using System.Reflection.PortableExecutable;

namespace Homework_9_KravchukSophia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string path = "../../../numbers.txt";
            //FileSorter fileSorter = new FileSorter(path);
            //fileSorter.SortFile("../../../RESULT.txt");


            List<Product> products = new List<Product>();
            products.Add(new Product("cherry", 14));
            products.Add(new Product("banana", 17.61));
            products.Add(new Product("pineapple", 41.23));
            products.Add(new Product("beans", 30.0));
            products.Add(new Product("milk", 1545));
            products.Add(new Product("soy", 1545));
            products.Add(new Product("water", 4));
            products.Add(new Product("apple", 551));
            products.Add(new Product("pear", 44));
            products.Add(new Product("orange", 20));
            products.Add(new Product("chocolate", 45));
            products.Add(new Product("tea", 96));
            products.Add(new Product("coffee", 12.39));
            products.Add(new Product("soup", 52.01));
            products.Add(new Product("pepper", 75.20));
            products.Add(new Product("eggs", 964.02));
            products.Add(new Product("cheese", 45.02));
            products.Add(new Product("meat", 7));
            products.Add(new Product("salt", 5620));
            products.Add(new Product("cream", 41.2));
            products.Add(new Product("tomatoes", 980));
            products.Add(new Product("cucumber", 10.0));

            List<Product> products2 = products.ConvertAll(product => new Product(product.Name, product.Price));
            List<Product> products3 = products.ConvertAll(product => new Product(product.Name, product.Price));

            DateTime start = DateTime.Now;
            QuickSorter.QuickSort(products,0, products.Count()-1, SortTypes.LastPivot);
            double lastPivotTime = (DateTime.Now - start).TotalMilliseconds;

            DateTime start2 = DateTime.Now;
            QuickSorter.QuickSort(products2, 0, products.Count() - 1, SortTypes.FirstPivot);
            double firstPivotTime = (DateTime.Now - start2).TotalMilliseconds;

            DateTime start3 = DateTime.Now;
            QuickSorter.QuickSort(products3, 0, products.Count() - 1, SortTypes.RandomPivot);
            double randomPivotTime = (DateTime.Now - start3).TotalMilliseconds;

            Console.WriteLine("Last Pivot Total Milliseconds: " + lastPivotTime);
            Console.WriteLine("First Pivot Total Milliseconds: " + firstPivotTime);
            Console.WriteLine("Random Pivot Total Milliseconds: " + randomPivotTime);

        }
    }
}