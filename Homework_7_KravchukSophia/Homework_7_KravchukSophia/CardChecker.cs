using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_7_KravchukSophia
{
    public static class CardChecker
    {
        private static readonly Dictionary<int, string> cardStart = new()
        {
            {34,  "AmericanExpress"},
            {37,  "AmericanExpress"},
            {51,  "MasterCard"},
            {52,  "MasterCard"},
            {53,  "MasterCard"},
            {54,  "MasterCard"},
            {55,  "MasterCard"},
            {4,  "Visa"}
        };
        private static int[] americanExpressLength = {15 };
        private static int[] masterCardLength = { 16 };
        private static int[] visaLength = {13, 16 };

        private static readonly Dictionary<string, int[]> cardLength = new()
        {
            {"AmericanExpress", CardChecker.americanExpressLength},
            {"MasterCard", CardChecker.masterCardLength},
            {"Visa", CardChecker.visaLength}
        };

        public static string GetCardType(string cardNumber)
        {
            string cardType;
            int[] cardNumberArr;
            CardChecker.StringNumberToArray(cardNumber, out cardNumberArr);

            cardType = CardChecker.cardStart.ContainsKey(cardNumberArr[0]) ? CardChecker.cardStart[cardNumberArr[0]] : 
                (CardChecker.cardStart.ContainsKey(cardNumberArr[0]*10 + cardNumberArr[1]) ? CardChecker.cardStart[cardNumberArr[0] * 10 + cardNumberArr[1]] : "");
            bool correctLength = false;
            if (cardType != "")
            {
                int[] cardPossibleLength = CardChecker.cardLength[cardType];
                for (int i = 0; i < cardPossibleLength.Length; i++)
                {
                    if(cardPossibleLength[i] == cardNumberArr.Length)
                    {
                        correctLength = true;
                        break;
                    }
                }
            }

            return correctLength ? cardType : "Card number is incorrect";
        }

        private static void StringNumberToArray (string cardNumber, out int[] arrCardNumber)
        {
            arrCardNumber = new int[cardNumber.Length];
            for(int i = 0; i < cardNumber.Length; i++)
            {
                int cardNumberVal = (int)Char.GetNumericValue(cardNumber[i]);
                if (cardNumberVal < 0)
                {
                    throw new ArgumentException();
                }
                else
                {
                    arrCardNumber[i] = cardNumberVal;
                }                             
            }
        }

        private static bool CheckLuhnAlgorithm(int[] numbersArray)
        {
            int checkSum = 0;
            for(int i = numbersArray.Length - 1; i >= 0; i--)
            {
                if ((numbersArray.Length - i) % 2 == 0)
                {
                    int add = numbersArray[i] * 2;
                    checkSum += add - 9 > 0 ? ((add % 10) + (add / 10)) : add;
                }
                else
                {
                    checkSum += numbersArray[i];
                }
            }
            return (checkSum % 10) == 0;
        }

        public static string CheckCard(string cardNumber)
        {
            string response = "Number: " + cardNumber + "\n";
            int[] cardNumberArr;
            bool correctCard;
            
            try
            {
                CardChecker.StringNumberToArray(cardNumber, out cardNumberArr);
            } catch(ArgumentException e)
            {
                return response;
            }
            if (!CardChecker.CheckLuhnAlgorithm(cardNumberArr))
            {
                return response + "INVALID";
            }
            
            return response + CardChecker.GetCardType(cardNumber);
        } 
    }

}
