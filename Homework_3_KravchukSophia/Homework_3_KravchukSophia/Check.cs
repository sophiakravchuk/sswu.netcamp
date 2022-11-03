using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Homework_3_KravchukSophia
{
    public class Check
    {
        private Buy[] buys;

        public Buy[] Buys { get { return buys; } }

        public Check()
        {
            this.buys = new Buy[1];
        }
        public Check(Buy buy)
        {
            this.buys = new Buy[1];
            this.buys[0] = buy;
        }

        public Check(Buy[] buys)
        {
            this.buys = new Buy[buys.Length];
            buys.CopyTo(this.buys, 0);
        }
        public override string ToString()
        {
            if (this.buys.Length >= 1)
            {
                string checkText = "";
                double finalPrice = 0;
                for (int i = 0; i < this.buys.Length; i++)
                {
                    if(this.buys[i] != null) 
                    {
                        finalPrice += this.buys[i].getFinalPrice();
                        checkText += this.buys[i].ToString();
                    }
                }
                return checkText + "\n\nFinal price: " + Math.Round(finalPrice, 2);
            }
            else 
            { 
                return "\nFinal price: 0";
            }
           
        }

    }
}
