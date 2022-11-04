using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Homework_5_KravchukSophia
{
    public class Storage
    {
        public List<Product> productsInStorage;


        public List<Product> ProductsInStorage { 
            get { return productsInStorage; }
            set 
            {
                foreach (Product product in value)
                    {
                        this.AddProduct(product);
                    }
                
            }
        }

        public Storage()
        {
            this.productsInStorage = new List<Product>();
        }
        public Storage(Product product)
        {
            this.productsInStorage = new List<Product>();
            this.productsInStorage.Add(product);
        }

        public Storage(Product[] products)
        {

            this.productsInStorage = new List <Product>(products);
        }

        public Storage(List<Product> products)
        {
           this.productsInStorage = new List<Product>(products.Count);
            foreach (Product product in products)
            {
                if ((product != null) && product is Product)
                {
                    this.productsInStorage.Add(product);
                }
                
            }
            
        }

        public Product this[int flag]
        {
            get
            {
                Product product = this.ProductsInStorage[flag];
                return product.Clone() as Product;
            }

            set
            {
                this.ProductsInStorage[flag] = value;
            }
        }
        public void AddProduct(Product product)
        {
            if ((product != null) && product is Product)
            {
                productsInStorage.Add(product);
            }
            else
            {
                throw new Exception("Object is not a product");
            }
                
        }
        public List<Product> GetAllMeat()
        {
            List<Product> allMeat = new List<Product>();
            foreach (Product product in this.ProductsInStorage)
            {
                if ((product != null) && product is Meat)
                {
                    allMeat.Add((Product)product.Clone());
                }

            }
            return allMeat;
        }

        public void ChangePriceForAllProducts(int percentage)
        {
            foreach (Product product in this.ProductsInStorage)
            {
                if (product != null)
                {
                    product.ChangePrice(percentage);
                }

            }
        }

        public override string ToString()
        {
            if (this.productsInStorage.Count >= 1)
            {
                string storageText = "";
                foreach (Product product in this.ProductsInStorage)
                {
                    if (product != null)
                    {
                        storageText += product.ToString() +"\n\n";
                    }
                }
                return storageText;
            }
            else
            {
                return "\nStorage is empty";
            }

        }
    }
}
