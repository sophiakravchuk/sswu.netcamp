

namespace Homework_5_KravchukSophia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime date1 = new DateTime(2022, 12, 1);
            DateTime date2 = new DateTime(2023, 12, 1);

            Product product1 = new Product("cherry", 14, 2.0);
            Product product2= new Product("banana", 17.61, 0.75);
            
            Meat meat = new Meat("meat", 100, 1.0, MeatTypes.Mutton, MeatClasses.HighClass);
            Meat meat2 = new Meat("meat", 100, 1.0, MeatTypes.Mutton, MeatClasses.SecondClass);
            Meat meat3 = new Meat("meat", 100, 1.0, MeatTypes.Chicken, MeatClasses.SecondClass);
            
            DairyProducts dairy1 = new DairyProducts("milk", 100, 0.75, DateTime.Now);
            DairyProducts dairy2 = new DairyProducts("milk", 100, 0.75, date1);
            DairyProducts dairy3 = new DairyProducts("milk", 100, 0.75, date2);
            
            Cart cart = new Cart();
            cart.AddBuy(product1, 3);
            cart.AddBuy(dairy3, 1);
            cart.AddBuy(dairy3, 1);
            cart.AddBuy(meat3, 2);
            cart.AddBuy(product2, 1);

            Currencies[] a = cart.GetAllCurrecies();

            cart.ChangeCurrency(a[1]);
            cart.ChangeCurrency(Currencies.UAH);
            cart.ChangeCurrency(Currencies.EUR);
            cart.ChangeWeightUnit(WeightUnits.G);
            cart.ChangeWeightUnit(WeightUnits.KG);
            
            cart.DeleteBuy(meat3);
            Console.WriteLine(Check.GetCheckString(cart));
            cart.AddOneMoreItem(meat3);
            Console.WriteLine(Check.GetCheckString(cart));
            cart.SubstractOneItem(product1);
            Console.WriteLine(Check.GetCheckString(cart));
            cart.SubstractOneItem(product2);
            Console.WriteLine(Check.GetCheckString(cart));

        }
    }
}