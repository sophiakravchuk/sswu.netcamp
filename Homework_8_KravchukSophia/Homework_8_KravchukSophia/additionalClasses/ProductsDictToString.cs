using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_8_KravchukSophia
{
    public static class ProductsDictToString
    {
        public static string StorageDictString(Dictionary<Product, int> dict)
        {
            if (dict.Count >= 1)
            {
                string storageText = "";
                foreach (KeyValuePair<Product, int> productAmountPair in dict)
                {
                    if (productAmountPair.Key != null)
                    {
                        storageText += String.Format("{0} \t=> {1}", productAmountPair.Key.Name, productAmountPair.Value) + "\n";
                    }
                }
                return storageText;
            }
            else
            {
                return "\nDictionary is empty";
            }

        }
    }
}
