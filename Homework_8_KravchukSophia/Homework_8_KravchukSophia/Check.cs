using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Homework_8_KravchukSophia
{
    public static class Check
    {
        public static string GetCheckString(Cart cart)
        {
            if (cart.AmountOfBuys >= 1)
            {
                string checkText = "";
                List<Product> allProducts = cart.AllProducts;

                foreach (Product product in allProducts)
                {
                    if(product is not null) 
                    {
                        checkText += product.Name + "\tx" + cart.GetAmountOfItems(product) + "\t" + cart.GetElementWeight(product) + " " +cart.CurrentWeightUnit+ "\t" + cart.GetElementPrice(product) + "\n";
                    }
                }
                return checkText + "\n\nFinal price: " + cart.GetFinalPrice() + " " + cart.CurrentCurrencie;
            }
            else 
            { 
                return "\nFinal price: 0";
            }
           
        }

    }
}
