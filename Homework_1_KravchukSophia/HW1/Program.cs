//Створити 3класи:
//1.Клас Product, який має три елемент-даних – назва, ціна і вага
//2. Клас Buy, містить дані про товар, кількість одиниць товару, що
//купується в штуках, вміє обчислювати ціну за весь куплений товар.
//3. Клас Check, що не містить ніяких елементів-даних. Цей клас повинен
//виводити на екран інформацію про товар і про покупку;
//Створити конструктори класів, визначити властивості з різними
//модифікаторами. Перевизначити метод ToString.
//Продемонструвати створення екземплярів класів.

namespace HW1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Product product1 = new Product("cherry", 14.57, 1.0);
            Product product2= new Product("banana", 17.61, 0.75);
            Buy buy1 = new Buy(product1, 5);
            Buy buy2 = new Buy(product2, 3);
            Buy[] buys = new Buy[] { buy1, buy2 };
            Check firstCheck = new Check(buy1);
            Check secondCheck = new Check(buys);

            Console.WriteLine(firstCheck);

            Console.WriteLine(secondCheck);
        }
    }
}