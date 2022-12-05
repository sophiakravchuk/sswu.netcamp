using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Homework_8_KravchukSophia
{
    public enum Currencies
    {
        UAH = 100000,
        USD = 2686,
        GBP = 2383,
        EUR = 2747,
        PLN = 12910
    }

    public enum WeightUnits
    {
        KG = 1,
        G = 1000
    }
    public class Cart // Я вирішила не використовувати клас Buy, бо вже не бачу в ньому сенсу. Використовую Dictionary замість нього
    {
        private Dictionary<Product, int> allBuys;
        private Currencies currentCurrency = Currencies.UAH;
        private WeightUnits currentWeightUnit = WeightUnits.KG;

        public Currencies CurrentCurrencie { get { return this.currentCurrency; } }
        public WeightUnits CurrentWeightUnit { get { return this.currentWeightUnit; } }
        public int AmountOfBuys { get { return this.allBuys.Count; } }
        public Dictionary<Product, int> AllBuys { 
            get 
            {
                return this.allBuys.ToDictionary(entry => entry.Key, entry => entry.Value);
            } 
        }
        public List<Product> AllProducts
        {
            get
            {
                return this.allBuys.Keys.ToList();
            }
        }
        public Cart()
        {
            this.allBuys = new Dictionary<Product, int>();

        }

        public Cart(Product product, int amountOfProducts)
        {
            if (amountOfProducts <= 0)
            {
                throw new ArgumentOutOfRangeException("Amount of products has to be greater than 0");
            }
            if (product is null)
            {
                throw new ArgumentNullException("Product is null");
            }
            this.allBuys = new Dictionary<Product, int>();
            this.allBuys.Add(product, amountOfProducts);

        }

        public void AddBuy(Product newProduct, int amountOfItems)
        {

            if (!this.allBuys.ContainsKey(newProduct))
            {
                if (newProduct is null)
                {
                    throw new ArgumentNullException();
                }
                if (amountOfItems <= 0)
                {
                    throw new ArgumentOutOfRangeException("Amount of products has to be greater than 0");
                }
                this.allBuys.Add(newProduct, amountOfItems);
                  
            }
            else //buy already exists
            {
                this.allBuys[newProduct] += amountOfItems;
            }
        }
        public void DeleteBuy(Product newProduct)
        {

            if (!this.allBuys.ContainsKey(newProduct))
            {
                throw new Exception("A Buy does not exist");
            }
            else //buy already exists
            {
                this.allBuys.Remove(newProduct);
            }
        }

        public void AddOneMoreItem(Product product)
        {
            if (!this.allBuys.ContainsKey(product))
            {
                throw new ArgumentException();
            }
            else
            {
                this.allBuys[product] += 1;
            }
        }

        public void SubstractOneItem(Product product)
        {
            if (!this.allBuys.ContainsKey(product))
            {
                throw new ArgumentException();
            }
            else
            {
                this.allBuys[product] -= 1;
                if (this.allBuys[product] <= 0)
                {
                    this.allBuys.Remove(product);
                }
            }
        }

        public double GetFinalPrice()
        {
            double finalPrice = 0;
            foreach (KeyValuePair<Product, int> productAmountPair in this.allBuys)
            {
                finalPrice += this.GetElementPrice(productAmountPair.Key);
                    
            }
            return Math.Round(finalPrice, 2);
        }

        public double GetElementPrice(Product product)
        {
            if (!this.allBuys.ContainsKey(product))
            {
                throw new Exception("There is no such element in the cart");
            }
            return Math.Round(this.allBuys[product] * product.Price * (int)this.currentCurrency / (int)Currencies.UAH, 2);

        }

        public double GetFinalWeight()
        {
            double finalWeight = 0;
            foreach (KeyValuePair<Product, int> productAmountPair in this.allBuys)
            {
                finalWeight += this.GetElementWeight(productAmountPair.Key);

            }
            return finalWeight;
        }

        public double GetElementWeight(Product product)
        {
            if (!this.allBuys.ContainsKey(product))
            {
                throw new Exception("There is no such element in the cart");
            }
            return Math.Round(this.allBuys[product] * product.Weight * (int)this.currentWeightUnit, 2);

        }

        public Currencies[] GetAllCurrecies()
        {
            return (Currencies[])Enum.GetValues(typeof(Currencies));
        }
        public WeightUnits[] GetAllWeightUnits()
        {
            return (WeightUnits[])Enum.GetValues(typeof(WeightUnits));
        }
        public int GetAmountOfItems(Product product)
        {
            return this.allBuys[product];
        }

        public void ChangeCurrency(Currencies currency) 
        {
            this.currentCurrency = currency;
        }
        public void ChangeWeightUnit(WeightUnits unit)
        {
            this.currentWeightUnit = unit;
        }

        public override bool Equals(object? obj)
        {
            Cart other = obj as Cart;
            if (other == null) return false;

            if (this.currentCurrency != other.currentCurrency && this.CurrentWeightUnit != other.CurrentWeightUnit
                && this.allBuys.Count != other.allBuys.Count)
            {
                return false;
            }

            foreach (KeyValuePair<Product, int> productAmountPair in this.allBuys)
            {
                if (!other.allBuys.ContainsKey(productAmountPair.Key))
                {
                    return false;
                }
                if (productAmountPair.Value != other.allBuys[productAmountPair.Key])
                {
                    return false;
                }

            }
            return true;
        }

        //public int CompareTo(object? obj)
        //{
        //    Cart other = obj as Cart;
        //    if (other == null) return 1;

        //    if (this.GetFinalPrice() > other.GetFinalPrice())
        //    {
        //        return 1;
        //    } else if(this.GetFinalPrice() < other.GetFinalPrice())
        //    {
        //        return -1;
        //    }
        //    else
        //    {
        //        return 0;
        //    }


        public int CompareTo(object? obj)
        {
            Cart other = obj as Cart;
            if (other == null) return 1;

            return this.allBuys.Count - other.allBuys.Count;
        }

        }
    }
