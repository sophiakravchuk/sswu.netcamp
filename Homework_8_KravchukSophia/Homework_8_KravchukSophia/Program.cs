namespace Homework_8_KravchukSophia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order("../../../Order/orderFiles/Order2.txt", "../../../Order/adjacentProducts.txt");
            Storage storage = new Storage();
            Product product1 = new Product("Chicken", 1, 100);

            //Product product2 = new Product("Milk", 1, 100);
            Product product3 = new Product("Cherry", 1, 100);
            Product product4 = new Product("Banana", 1, 100);
            DairyProducts dairy1 = new DairyProducts("Milk", 1, 100, DateTime.Today);

            storage.AddProduct(product1, 1);
            //storage.AddProduct(product2, 100);
            storage.AddProduct(product3, 1);
            storage.AddProduct(product4);
            storage.AddProduct(dairy1, 1);

            OrderChecker orderChecker = new OrderChecker(order, storage, "../../../result.txt");
            orderChecker.CheckOrder();

        }
    }
}