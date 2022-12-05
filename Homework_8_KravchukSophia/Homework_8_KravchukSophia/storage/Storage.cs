using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Homework_8_KravchukSophia
{
    public delegate void StorageEventsHander(string productName, int amount);


    public class Storage : IComparable
    {
        public event StorageEventsHander MissingProduct;
        private class SortHeavierHelper : IComparer
        {
            int IComparer.Compare(object a, object b)
            {
                Storage s1 = (Storage)a;
                Storage s2 = (Storage)b;
                if (s1 == null || s2 == null) return 1;

                int compare = (int)((s1.GetWholeWeight() - s2.GetWholeWeight()) * 100);

                return compare;
            }
        }

        private class SortMoreExpensiveHelper : IComparer
        {
            int IComparer.Compare(object a, object b)
            {
                Storage s1 = (Storage)a;
                Storage s2 = (Storage)b;
                if (s1 == null || s2 == null) return 1;

                return (int)((s1.GetWholeCost() - s2.GetWholeCost()) * 100);
            }
        }

        private Dictionary<Product, int> productsInStorage;
        private Dictionary<string, Product> productNames;

        public Dictionary<Product, int> ProductsInStorage {
            get
            {
                return this.productsInStorage.ToDictionary(entry => entry.Key, entry => entry.Value);
            }
            set 
            {
                this.productsInStorage = value.ToDictionary(entry => entry.Key, entry => entry.Value);
            }
        }

        public Storage()
        {
            this.productsInStorage = new Dictionary<Product, int>();
            this.productNames = new Dictionary<string, Product>();
        }
        public Storage(Product product)
        {
            this.productsInStorage = new Dictionary<Product, int>();
            this.productNames = new Dictionary<string, Product>();
            if (product != null)
            {
                this.AddProduct(product);
            }
            
        }

        public Storage(Product[] products)
        {
            this.productsInStorage = new Dictionary<Product, int>();
            this.productNames = new Dictionary<string, Product>();
            if (products != null)
            {
                for(int i = 0; i < products.Length; i++)
                {
                    if (products[i] != null)
                    {
                        this.AddProduct(products[i]);
                    }   
                }
            }
        }

        public Storage(Dictionary<Product, int> products)
        {
            this.productsInStorage = products.ToDictionary(entry => entry.Key, entry => entry.Value);
            this.productNames = products.ToDictionary(entry => entry.Key.Name, entry => entry.Key);
        }

        public void AddProduct(Product newProduct, int amountOfItems=1)
        {
            Product copyNewProduct = newProduct.Clone() as Product;
            if (!this.productsInStorage.ContainsKey(copyNewProduct))
            {
                if (copyNewProduct is null)
                {
                    throw new ArgumentNullException();
                }
                if (amountOfItems <= 0)
                {
                    throw new ArgumentOutOfRangeException("Amount of products has to be greater than 0");
                }
                this.productsInStorage.Add(copyNewProduct, amountOfItems);
                this.productNames.Add(copyNewProduct.Name, copyNewProduct);

            }
            else //buy already exists
            {
                this.productsInStorage[newProduct] += amountOfItems;
            }
        }
        public Dictionary<Product, int> GetAllMeat()
        {
            Dictionary<Product, int> allMeat = new Dictionary<Product, int>();
            foreach (KeyValuePair<Product, int> productAmountPair in this.ProductsInStorage)
            {
                if (productAmountPair.Key is Meat)
                {
                    allMeat[productAmountPair.Key.Clone() as Product] = productAmountPair.Value;
                }
            }
            return allMeat;
        }

        public void ChangePriceForAllProducts(int percentage)
        {
            foreach (KeyValuePair<Product, int> productAmountPair in this.ProductsInStorage)
            {
                if (productAmountPair.Key != null)
                {
                    productAmountPair.Key.ChangePrice(percentage);
                }

            }
        }

        public double GetWholeCost()
        {
            double wholeCost = 0;
            foreach (KeyValuePair<Product, int> productAmountPair in this.productsInStorage)
            {
                wholeCost += productAmountPair.Key.Price * productAmountPair.Value;

            }
            return Math.Round(wholeCost, 2);
        }
        public double GetWholeWeight()
        {
            double wholeWeight = 0;
            foreach (KeyValuePair<Product, int> productAmountPair in this.productsInStorage)
            {
                wholeWeight += productAmountPair.Key.Weight * productAmountPair.Value;

            }
            return Math.Round(wholeWeight, 2);
        }

        public bool ProductIsInStock(Product product, int amount=1, bool invokeEvent = true)
        {
            bool isInStock = this.productsInStorage.ContainsKey(product) && this.productsInStorage[product] >= amount;
            if (invokeEvent && !isInStock)
            {
                int currentAmount = this.productsInStorage.ContainsKey(product) ? this.productsInStorage[product] : 0;
                MissingProduct?.Invoke(product.Name, amount - currentAmount);
            }

            return isInStock;
        }
        public bool ProductIsInStock(string product, int amount = 1, bool invokeEvent=true)
        {
            if (!this.productNames.ContainsKey(product))
            {
                if (invokeEvent)
                {
                    MissingProduct?.Invoke(product, amount);
                }
                return false;
            }
            else
            {
                Product product1 = this.productNames[product];
                return this.ProductIsInStock(product1, amount, invokeEvent);
            }
        }

        public int GetAmountOfProduct(Product product)
        {
            return this.productsInStorage.ContainsKey(product)? this.productsInStorage[product] : 0;
        }

        public int GetAmountOfProduct(string product)
        {
            if (!this.productNames.ContainsKey(product))
            {
                return 0;
            }
            else
            {
                Product product1 = this.productNames[product];
                return this.GetAmountOfProduct(product1);
            }
        }
        public override string ToString()
        {
            if (this.productsInStorage.Count >= 1)
            {
                string storageText = "";
                foreach (KeyValuePair<Product, int> productAmountPair in this.ProductsInStorage) 
                {
                    if (productAmountPair.Key != null)
                    {
                        storageText += String.Format("{0} \t {1}", productAmountPair.Key.Name, productAmountPair.Value)  + "\n";
                    }
                }
                return storageText;
            }
            else
            {
                return "\nStorage is empty";
            }

        }

        public override bool Equals(object? obj)
        {
            Storage other = obj as Storage;
            if (other == null) return false;


            foreach (KeyValuePair<Product, int> productAmountPair in this.productsInStorage)
            {
                if (!other.productsInStorage.ContainsKey(productAmountPair.Key))
                {
                    return false;
                }
                if (productAmountPair.Value != other.productsInStorage[productAmountPair.Key])
                {
                    return false;
                }

            }
            return true;
        }
        public int CompareTo(object? obj)
        {
            Storage other = obj as Storage;
            if (other == null) return 1;

            return this.productsInStorage.Count - other.productsInStorage.Count;
        }

        public static IComparer SortMoreExpensive()
        {
            return (IComparer)new SortMoreExpensiveHelper();
        }
        public static IComparer SortHeavier()
        {
            return (IComparer)new SortHeavierHelper();
        }
    }
}
