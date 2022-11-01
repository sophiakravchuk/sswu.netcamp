using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Задачі 2 рівня.( Додаткові бали)
//Задача 3.
//Описати уявний тривимірний куб, в якому в довільному місці існують
//порожнини. Перевірити, чи є наскрізний отвір в кубі.
//Задача 4.
//Описати клас тензор, який може мати довільну розмірність. У випадку 1 –
//це число, 2- це вектор, 3 – це матриця і т.д.
//Продемонструвати створення об’єкта цього типу та його ініціалізацію.

namespace Homework_2_KravchukSophia
{
    internal class Task3Cube
    {
        private int[,,] cube;
        private readonly int height;
        private const int amountOfSides = 6;

        private readonly int[,] pairs = new int[3, 2] { { 0, 2 }, { 1, 3 }, { 4, 5 } };

        public int Height
        {
            get { return this.height; }
        }
        public int[,,] Cube
        {
            get
            {

                int[,,]? cubeCopy = this.cube.Clone() as int[,,];
                return cubeCopy;

            }
        }

        public Task3Cube(int n)
        {
            this.cube = new int[amountOfSides, n, n];
            this.height = n;
        }

        public string CheckForHoles() 
        {
            string holes = "";
            for (int pairNumber = 0; pairNumber < this.pairs.GetLength(0); pairNumber++)
            {
                int firstSide = this.pairs[pairNumber, 0];
                int secondSide = this.pairs[pairNumber, 1];
                for (int rowNumber = 0; rowNumber < this.cube.GetLength(1); rowNumber++)
                {
                    for (int cell = 0; cell < this.cube.GetLength(2); cell++)
                    {
                        if (pairNumber == 2)
                        {
                            if (this.cube[firstSide, rowNumber, cell] == 0 && this.cube[secondSide, this.cube.GetLength(1) - 1 - rowNumber, cell] == 0)
                            {
                                holes += "Hole: (" + firstSide + "," + rowNumber + "," + cell + ") -> (" + secondSide + "," + (this.cube.GetLength(1) - 1 - rowNumber) + "," + cell + ")\n";
                            }
                        }
                        else
                        {
                            if (this.cube[firstSide, rowNumber, cell] == 0 && this.cube[secondSide, rowNumber, this.cube.GetLength(2) - 1 -cell] == 0)
                            {
                                holes += "Hole: (" + firstSide + ","+ rowNumber + "," + cell + ") -> ("+ secondSide + "," + rowNumber + "," + (this.cube.GetLength(2) - 1 - cell) + ")\n";
                            }
                        }

                    }
                }
            }

            return holes != "" ? holes : "There is no holes";
        }


        public void FillCubeRandomly(int rangeStart, int rangeEnd) 
        {
            Random rnd = new Random(59);
            for (int i = 0; i < amountOfSides; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    for (int k = 0; k < this.height; k++)
                    {
                        this.cube[i, j, k] = rnd.Next(rangeStart, rangeEnd);
                    }
                }
            }
        }
        public override string ToString()
        {
            string cubeString = "";
            string elementStr = "";
            for (int i = 0; i < this.cube.GetLength(1); i++)
            {
                for (int side = 0; side < this.cube.GetLength(0); side++)
                {
                    for (int j = 0; j < this.cube.GetLength(2); j++)
                    {
                        elementStr = this.cube[side, i, j].ToString();
                        cubeString += elementStr;
                        for (int l = 0; l < (3 - elementStr.Length); l++)
                        {
                            cubeString += " ";
                        }
                    }
                    cubeString += "\t";

                }
                cubeString += "\n";
            }

            cubeString += "Front Side\t" + "Right Side\t" + "Back Side\t" + "Left Side\t" + "Upper Side\t" + "Bottom Side\t";

            return cubeString;
        }

    }
}
