using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_8_KravchukSophia
{
    public static class StoragesComparer
    {
        public static Dictionary<Product, int> ExclusiveProducts(object a, object b)
        {
            Storage s1 = (Storage)a;
            Storage s2 = (Storage)b;

            if(s1 == null || s2 == null)
            {
                throw new ArgumentNullException();
            }

            Dictionary<Product, int> exclusiveProducts = new();
            Dictionary<Product, int> s1Products = s1.ProductsInStorage;
            foreach (KeyValuePair<Product, int> productAmountPair in s1Products)
            {
                if (!s2.ProductIsInStock(productAmountPair.Key))
                {
                    exclusiveProducts.Add(productAmountPair.Key, productAmountPair.Value);
                }
            }
            return exclusiveProducts;
        }

        public static Dictionary<Product, int> ProductsInBothStorages(object a, object b)
        {
            Storage s1 = (Storage)a;
            Storage s2 = (Storage)b;

            if (s1 == null || s2 == null)
            {
                throw new ArgumentNullException();
            }

            Dictionary<Product, int> commonProducts = new();
            Dictionary<Product, int> s1Products = s1.ProductsInStorage;
            foreach (KeyValuePair<Product, int> productAmountPair in s1Products)
            {
                if (s2.ProductIsInStock(productAmountPair.Key))
                {
                    commonProducts.Add(productAmountPair.Key, productAmountPair.Value);
                }
            }
            return commonProducts;
        }

        public static Dictionary<Product, int> JointProducts(object a, object b)
        {
            Storage s1 = (Storage)a;
            Storage s2 = (Storage)b;

            if (s1 == null || s2 == null)
            {
                throw new ArgumentNullException();
            }
            Dictionary<Product, int> s1Products = s1.ProductsInStorage;
            Storage jointStorage = new Storage(s1Products);
            Dictionary<Product, int> s2Products = s2.ProductsInStorage;
            foreach (KeyValuePair<Product, int> productAmountPair in s2Products)
            {
                jointStorage.AddProduct(productAmountPair.Key, productAmountPair.Value);
            }
            return jointStorage.ProductsInStorage;
        }
    }
}
