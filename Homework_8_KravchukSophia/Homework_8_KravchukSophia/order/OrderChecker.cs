using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_8_KravchukSophia
{
    public delegate void OrderEventsHander(Order order, Dictionary<string, int> missingProducts, string filname);
    
    public class OrderChecker
    {
        private event OrderEventsHander OrderNotCompleted;

        private string missingProductsFilename;
        private Dictionary<string, int> missingProducts;

        private Dictionary<string, Dictionary<string, int>> missingProductsReplacements;

        private Order orderToCheck;
        private Storage storageToCheckIn;

        public OrderChecker(Order order, Storage storage, string missingProductsFilename = "result.txt")
        {
            this.orderToCheck = order;
            this.storageToCheckIn = storage;
            this.missingProductsFilename = missingProductsFilename;
            this.missingProducts = new Dictionary<string, int>();
            this.OrderNotCompleted += WriteMissingProducts;
            this.missingProductsReplacements = new Dictionary<string, Dictionary<string, int>>();
        }

        public void CheckOrder()
        {
            this.storageToCheckIn.MissingProduct += AddMissingProduct;
            this.storageToCheckIn.MissingProduct += FindReplacementForMissingProduct;
            Dictionary<string, int> poductsNeeded = this.orderToCheck.ProductsInOrder;

            foreach (KeyValuePair<string, int> productAmountPair in poductsNeeded)
            {
                this.storageToCheckIn.ProductIsInStock(productAmountPair.Key, productAmountPair.Value);
            }
            
            if(this.missingProducts.Count() != 0)
            {
                this.OrderNotCompleted?.Invoke(this.orderToCheck, missingProducts, missingProductsFilename);
            }

        }

        public void WriteMissingProducts(Order order, Dictionary<string, int> missingProducts, string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.WriteLine(order.CompanyName);
                sw.WriteLine("Missing products:");
                foreach (KeyValuePair<string, int> productAmountPair in missingProducts)
                {
                    sw.WriteLine(productAmountPair.Key + " => " + productAmountPair.Value);
                    if(this.missingProductsReplacements[productAmountPair.Key].Count() > 0)
                    {
                        sw.WriteLine("\tThis product can be replaced by: ");
                        foreach (KeyValuePair<string, int> relacements in this.missingProductsReplacements[productAmountPair.Key])
                        {
                            sw.WriteLine("\t"+relacements.Key + " => " + relacements.Value + " in stock");
                        }
                    }
                    
                }
            }
        }
        public void AddMissingProduct(string productName, int amount)
        {
            if (this.missingProducts.ContainsKey(productName))
            {
                this.missingProducts[productName] = amount;
            }
            else
            {
                this.missingProducts.Add(productName, amount);
            }
            
        }
        public void FindReplacementForMissingProduct(string productName, int amount)
        {
            Dictionary<string, int> replacements = new Dictionary<string, int>();
            this.missingProductsReplacements.Add(productName, replacements);
            List<string> adjProds = this.orderToCheck.GetAdjacentProducts(productName);

            foreach (string adjProduct in adjProds)
            {
                if(this.storageToCheckIn.ProductIsInStock(adjProduct, 1, false))
                {
                    missingProductsReplacements[productName].Add(adjProduct, this.storageToCheckIn.GetAmountOfProduct(adjProduct));
                }
            }
        }

    }
}
