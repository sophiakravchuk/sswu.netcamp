using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Створити клас-обгортку для масиву цілих чисел шляхом генерації
//випадкових чисел в заданому діапазоні.
//Визначити частотну таблицю для заданої послідовності.
//(Видрукувати всі різні числа, що зустрічаються в послідовності та їх
//кількість).
//Знайти 2 найдовші підпослідовності, які складаються з простих чисел.
//Визначити індексатор для класу, який дозволить доступатись до елементу
//масиву за номером та змінювати його.

namespace Homework_4_KravchukSophia
{
    public class Task2ArrayCover
    {
        private readonly int[] numbersArray;

        public int[] NumbersArray { get { return this.numbersArray.Clone() as int[]; } }

        public Task2ArrayCover(int length, int rangeStart, int rangeEnd)
        {
            if (length <= 0 || rangeStart <= 0 || rangeEnd <= 0 || rangeEnd < rangeStart)
            {
                throw new ArgumentException();
            }
            this.numbersArray = new int[length];
            this.FillWithRandom(rangeStart, rangeEnd);
        }

        public Task2ArrayCover(int[] arr)
        {
            if (arr != null)
            {
                this.numbersArray = arr.Clone() as int[];
            }
        }

        public Task2ArrayCover()
        {
            this.numbersArray = new int[10];
            this.FillWithRandom(0, 10);
        }

        public int this[int flag]
        {
            get
            {
                return this.numbersArray[flag];
            }

            set
            {
                this.numbersArray[flag] = value;
            }
        }

        public string GetTwoLongestSequences()
        {
            int[,] linesCoords = new int[2, 2];
            int newLineStart = 0;
            bool previousPrime = false;
            for (int i = 0; i < this.numbersArray.GetLength(0); i++)
            {
                if (Task2ArrayCover.IsNumberPrime(this.numbersArray[i]))
                {
                    if (!previousPrime)
                    {
                        newLineStart = i;
                        previousPrime = true;
                    }

                }
                else
                {
                    if (previousPrime)
                    {
                        int[] temp = new int[2] { newLineStart , i-1};
                        for (int j = 0; j < linesCoords.GetLength(0); j++)
                        {
                            if ((linesCoords[j, 1] - linesCoords[j, 0]) < (temp[1] - temp[0]))
                            {
                                int tmp = linesCoords[j, 0];
                                linesCoords[j, 0] = temp[0];
                                temp[0] = tmp;

                                tmp = linesCoords[j, 1];
                                linesCoords[j, 1] = temp[1];
                                temp[1] = tmp;

                            }
                        }

                    }
                    previousPrime = false;
                }
                
            }
            string returnText = "";
            for (int j = 0; j < linesCoords.GetLength(0); j++)
            {
                returnText += String.Format("First longest sequence Start: {0} End: {1}\n", linesCoords[j, 0], linesCoords[j, 1]);
            }
                
            return returnText;
        } 

        public string GetFrequencyTable()
        {
            Dictionary<int, int> frequencyTable = new Dictionary<int, int>();

            for (int i = 0; i < this.numbersArray.GetLength(0); i++)
            {
                if (!frequencyTable.ContainsKey(this.numbersArray[i]))
                {
                    frequencyTable.Add(this.numbersArray[i], 1);
                }
                else //buy already exists
                {
                    frequencyTable[this.numbersArray[i]] += 1;
                }
            }
            string tableString = "";
            foreach (KeyValuePair<int, int> productAmountPair in frequencyTable)
            {
                tableString += productAmountPair.Key + "\t" + productAmountPair.Value + "\n";
            }
            return tableString;
        }
        private void FillWithRandom(int rangeStart, int rangeEnd)
        {
            Random random = new Random();

            for (int i = 0; i < this.numbersArray.GetLength(0); i++)
            {
                this.numbersArray[i] = random.Next(rangeStart, rangeEnd);
            }
        }
        private static bool IsNumberPrime(int num)
        {
            if (num <= 3)
            {
                return num > 1;
            }
            else if (num % 2 == 0 || num % 3 == 0)
            {
                return false;
            }
            else
            {
                for (int i = 5; i * i <= num; i += 6)
                {
                    if (num % i == 0 || num % (i + 2) == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
    
}
