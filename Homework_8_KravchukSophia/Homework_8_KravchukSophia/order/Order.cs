using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Homework_8_KravchukSophia
{
    public class Order
    {
        private string companyName;
        private Dictionary<string, int> productsInOrder;
        private Dictionary<string, List<string>> adjacentProducts;

        public string CompanyName { get { return companyName; } }
        public Dictionary<string, int> ProductsInOrder { get { return this.productsInOrder.ToDictionary(entry => entry.Key, entry => entry.Value); } }

        public Order(string filename, string adjacentFilename = "")
        {
            this.productsInOrder = new Dictionary<string, int>();
            this.adjacentProducts = new Dictionary<string, List<string>>();
            this.GetOrderFromFile(filename);
            if (adjacentFilename != "")
            {
                this.CreateAdjacentProductsDict(adjacentFilename);
            }

        }
        public Order()
        {
            this.productsInOrder = new Dictionary<string, int>();
            this.companyName = "";
        }

        public void AddProductsToOrderFromFile(string pathToFile)
        {
            this.GetOrderFromFile(pathToFile);
        }

        private void GetOrderFromFile(string pathToFile)
        {
            bool firstLine = true;
            string productName;
            int amountOfProducts;
            try
            {
                foreach (string line in System.IO.File.ReadLines(pathToFile))
                {
                    if (line == "")
                    {
                        throw new ArgumentException("FileLine is empty");
                    }

                    if (firstLine)
                    {
                        this.companyName = line;
                        firstLine = false;
                    }
                    else
                    {
                        string[] productsInfo = line.Split(' ');

                        if (productsInfo == null || productsInfo.Count() != 2)
                        {
                            throw new Exception("Fileline is incorrect");
                        }

                        productName = productsInfo[0];
                        amountOfProducts = Convert.ToInt32(productsInfo[1]);

                        if (this.productsInOrder.ContainsKey(productName))
                        {
                            this.productsInOrder[productName] += amountOfProducts;
                        }
                        else
                        {
                            this.productsInOrder.Add(productName, amountOfProducts);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void CreateAdjacentProductsDict(string pathToFile)
        {
            string productName;

            try
            {
                foreach (string line in System.IO.File.ReadLines(pathToFile))
                {
                    List<string> adjacentProds = new List<string>();
                    if (line == "")
                    {
                        throw new ArgumentException("FileLine is empty");
                    }

                    string[] products = line.Split('-');
                    
                    if (products == null || products.Count() != 2)
                    {
                        throw new Exception("Fileline is incorrect");
                    }
                    productName = products[0];
                    string[] adjacent = products[1].Split(',');
                    
                    if (adjacent == null)
                    {
                        throw new Exception("Fileline is incorrect");
                    }

                    foreach (string prod in adjacent)
                    {
                        adjacentProds.Add(prod);
                    }

                    this.adjacentProducts.Add(productName, adjacentProds);
                   
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<string> GetAdjacentProducts(string productName)
        {
            List<string> adjacentProductsList;
            if (this.adjacentProducts.ContainsKey(productName)){
                adjacentProductsList = new List<string>(this.adjacentProducts[productName]);
            }
            else
            {
                adjacentProductsList = new List<string>();
            }

            return adjacentProductsList;
        }

    }
}
