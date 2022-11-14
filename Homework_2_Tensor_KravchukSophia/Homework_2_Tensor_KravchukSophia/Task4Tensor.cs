//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

////Задача 4.
////Описати клас тензор, який може мати довільну розмірність. У випадку 1 –
////це число, 2- це вектор, 3 – це матриця і т.д.
////Продемонструвати створення об’єкта цього типу та його ініціалізацію.

using System;

namespace Homework_2_Tensor_KravchukSophia
{
    public class Task4Tensor
    {
        private int dimension;
        private int length;
        private int[] dims;
        private int[] numbersArray;

        public int[] NumbersArray
        {
            get { return this.numbersArray.Clone() as int[]; }
        }
        public Task4Tensor()
        {
            this.dimension = 0;
            this.length = 1;
            this.numbersArray = new int[1];
            this.dims = new int[1];
        }
        public Task4Tensor(int number)
        {
            this.dimension = 0;
            this.dims = new int[] { 0};
            this.length = 1;
            this.numbersArray = new int[] {number};
        }
        public Task4Tensor(System.Array array)
        {
            if(array == null)
            {
                throw new ArgumentNullException();
            }

            this.dimension = array.Rank;
            this.dims = new int[this.dimension];
            this.length = 1;
            for (int i = 0; i < this.dimension; i++)
            {
                this.dims[i] = array.GetLength(i);
                this.length *= this.dims[i];
            }
            this.numbersArray = new int[this.length];
            int[] coords = { 0 };
            Task4Tensor.NDimArrayToOneDimReccursion(array, ref this.numbersArray, coords, 0, this.dims);
        }

        public int GetValue(int[] coords)
        {
            if(!CheckCoords(coords))
            {
                throw new IndexOutOfRangeException();
            }

            int indexOfValue = Task4Tensor.GetIndexByCoords(coords, this.dims);

            return this.numbersArray[indexOfValue];
        }
        public int GetValue()
        {
            if(this.dimension != 0)
            {
                throw new IndexOutOfRangeException();
            }
            int[] coords = { 0 };
            return this.GetValue(coords);
        }
        public int GetValue(int index)
        {
            if (this.dimension != 1)
            {
                throw new IndexOutOfRangeException();
            }
            int[] coords = { index };
            return this.GetValue(coords);
        }
        public void SetValue(int[] coords, int value)
        {
            if (!CheckCoords(coords))
            {
                throw new IndexOutOfRangeException();
            }

            int indexOfValue = Task4Tensor.GetIndexByCoords(coords, this.dims);

            this.numbersArray[indexOfValue] = value;
        }
        public void SetValue(int value)
        {
            if (this.dimension != 0)
            {
                throw new IndexOutOfRangeException();
            }
            int[] coords = { 0 };
            this.SetValue(coords, value);
        }
        public void SetValue(int index, int value)
        {
            if (this.dimension != 1)
            {
                throw new IndexOutOfRangeException();
            }
            int[] coords = { index };
            this.SetValue(coords, value);
        }

        private bool CheckCoords(int[] coords)
        {
            bool correctCoords = true;
            if (coords.GetLength(0) != this.dims.GetLength(0) || coords.GetLength(0) <= 0)
            {
                correctCoords = false;
            }

            for (int i = 0; i < this.dimension; i++)
            {
                if (coords[i] >= this.dims[i])
                {
                    correctCoords = false;
                }
            }
            return correctCoords;
        }
        private static int GetIndexByCoords(int[] coords, int[] arrayDim)
        {
            int indexOfValue = 0;
            if (coords.GetLength(0) == 1)
            {
                indexOfValue = coords[0];
            }
            else
            {
                int dimsIndex = 1;
                for (int j = 0; j < coords.GetLength(0); j++)
                {
                    int skipValues = coords[j];
                    for (int k = dimsIndex; k < arrayDim.GetLength(0); k++)
                    {
                        skipValues *= arrayDim[k];
                    }
                    indexOfValue += skipValues;
                    dimsIndex++;
                }
            }
            return indexOfValue;

        }
        private static void NDimArrayToOneDimReccursion(System.Array nDimArray, ref int[] copyToArray, int[] coords, int changeIndex, int[] dims)
        {
            if (nDimArray.Rank - coords.GetLength(0) > 0)
            {
                int indexToChange = coords.Length - 1;
                Array.Resize(ref coords, coords.Length + 1);
                for (int i = 0; i < dims[indexToChange]; i++)
                {
                    NDimArrayToOneDimReccursion(nDimArray, ref copyToArray, coords, indexToChange, dims);
                    coords[indexToChange] += 1;
                    for (int j = indexToChange+1; j < coords.GetLength(0); j++)
                    {
                        coords[j] = 0;
                    }
                }
            } 
            else
            {
                
                for(int i = 0; i< nDimArray.GetLength(coords.GetLength(0)-1); i++)
                {
                    coords[^1] = i;
                    int indexToCopyTo = Task4Tensor.GetIndexByCoords(coords, dims);
                    copyToArray[indexToCopyTo] = (int)nDimArray.GetValue(coords);
                }
                coords[^1] = 0;
            }
            
        }

        public override string ToString()
        {
            string numbers = "";
            foreach (int i in this.numbersArray)
            {
                numbers += i + " ";
            }
            return numbers;
        }
    }
}
