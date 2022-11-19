namespace Homework_7_KravchukSophia
{
//    • неправильний вхідний рядок ‒ нічого не робимо, рядок друкуємо
//Number: 5555-5555-5555-4444
//>>>
//• рядок має правильний формат, але номер картки некоректний
//Number:
//5555555555554445
//INVALID
//>>>
//• все гаразд з номером
//картки Number:
//5555555555554444
//MasterСard
    internal class Program
    {
        static void Main(string[] args)
        {
            Product cherry = new Product("cherry", 14, 2.0);
            Product banana = new Product("banana", 17.61, 0.75);
            Product pineapple = new Product("pineapple", 41.23, 1);
            Product beans = new Product("beans", 30.0, 8.0);

            Meat mutton = new Meat("Mutton", 100, 1.0, MeatTypes.Mutton, MeatClasses.HighClass);
            Meat mutton2 = new Meat("mutton", 100, 1.0, MeatTypes.Mutton, MeatClasses.SecondClass);
            Meat chicken = new Meat("chicken", 100, 1.0, MeatTypes.Chicken, MeatClasses.SecondClass);

            Storage storage1 = new Storage();
            storage1.AddProduct(cherry);
            storage1.AddProduct(banana);
            storage1.AddProduct(beans);
            storage1.AddProduct(mutton);
            storage1.AddProduct(mutton2);
            storage1.AddProduct(chicken);

            Storage storage2 = new Storage();
            storage2.AddProduct(banana);
            storage2.AddProduct(pineapple);
            storage2.AddProduct(chicken);

            Console.WriteLine("\tExclusive Products\n");
            Console.WriteLine(ProductsDictToString.StorageDictString(StoragesComparer.ExclusiveProducts(storage1, storage2)) + "\n");

            Console.WriteLine("\tProducts In Both Storages\n");
            Console.WriteLine(ProductsDictToString.StorageDictString(StoragesComparer.ProductsInBothStorages(storage1, storage2)) + "\n");
            
            Console.WriteLine("\tJoint Products\n");
            Console.WriteLine(ProductsDictToString.StorageDictString(StoragesComparer.JointProducts(storage1, storage2)));

            // American Express
            string cardAmExp1 = "378282246310005";
            string cardAmExp2 = "371449635398431";
            string cardAmExp3 = "378734493671000";

            // MasterCard
            string cardMaster1 = "5555555555554444";
            string cardMaster2 = "5105105105105100";
            string correctNumber = "5555555555554444";

            //Visa

            string cardVisa1 = "4111111111111111";
            string cardVisa2 = "4012888888881881";
            string cardVisa3 = "4003789100205381";

            //False
            string cardFalse1 = "4222222222223";
            string cardFalse2 = "51051051051051001";
            string cardFalse3 = "5705105105105100";
            string cardNumberInvalid = "5555555555554445";

            // Incorrect
            string cardNumberIncorrect1 = "5555 - 5555 - 5555 - 4444";
            string cardNumberIncorrect2 = "5555aaaa - 5555 - 4444";
            string cardNumberIncorrect3 = "5555 5555 5555 4444";
            string cardNumberIncorrect4 = "5555 j-3&&^%-";


            Console.WriteLine("\tAmerican Express Cards\n");
            Console.WriteLine(CardChecker.CheckCard(cardAmExp1));
            Console.WriteLine(CardChecker.CheckCard(cardAmExp2));
            Console.WriteLine(CardChecker.CheckCard(cardAmExp3));
            
            Console.WriteLine("\n\tMasterCard Cards\n");
            Console.WriteLine(CardChecker.CheckCard(cardMaster1));
            Console.WriteLine(CardChecker.CheckCard(cardMaster2));
            Console.WriteLine(CardChecker.CheckCard(correctNumber));


            Console.WriteLine("\n\tVisa Cards\n");
            Console.WriteLine(CardChecker.CheckCard(cardVisa1));
            Console.WriteLine(CardChecker.CheckCard(cardVisa2));
            Console.WriteLine(CardChecker.CheckCard(cardVisa3));

            Console.WriteLine("\n\tFalse Cards\n");
            Console.WriteLine(CardChecker.CheckCard(cardFalse1));
            Console.WriteLine(CardChecker.CheckCard(cardFalse2));
            Console.WriteLine(CardChecker.CheckCard(cardFalse3));
            Console.WriteLine(CardChecker.CheckCard(cardNumberInvalid));

            Console.WriteLine("\n\tIncorrect Format Cards\n");
            Console.WriteLine(CardChecker.CheckCard(cardNumberIncorrect1));
            Console.WriteLine(CardChecker.CheckCard(cardNumberIncorrect2));
            Console.WriteLine(CardChecker.CheckCard(cardNumberIncorrect3));
            Console.WriteLine(CardChecker.CheckCard(cardNumberIncorrect4));



        }
    }
}