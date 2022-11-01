using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_2_KravchukSophia
{
    internal class Task2
    {

        public Task2() { }


        //Задача 2.
        //Задано матрицю, елементами якої є інформація про колір пікселів,
        //заданий в діапазоні 0..16.
        //Знайти колір найдовшої горизонтальної лінії, вказати індекси її початку та
        //кінця, а також довжину.
        public string GetTheLongestLine(int[,] matrix)
        {
            if (matrix is null)
            {
                Console.WriteLine("The matrix is null");
                return "";
            }

            
            int previousCellColor;
            int[] currentLineStart = new int[2];

            int[] savedLineStart = new int[2];
            int[] savedLineEnd = new int[2];
            int savedColor = matrix[0,0];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                previousCellColor = matrix[i, 0];
                currentLineStart[0] = i;
                currentLineStart[1] = 0;
                for (int j = 1; j <= matrix.GetLength(1); j++)
                {
                    if (j < matrix.GetLength(1) && matrix[i, j] == previousCellColor) // we are on the same color line
                    {
                        continue;
                    }
                    // the color has changed

                    if (savedLineEnd[1] - savedLineStart[1] < j - 1 - currentLineStart[1]) // length of new line is bigger 
                    {
                        //saving position
                        savedLineStart = currentLineStart.Clone() as int[]; 
                        savedLineEnd[0] = i;
                        savedLineEnd[1] = j - 1;
                        savedColor = previousCellColor;
                    }
                    //starting new line
                    currentLineStart[0] = i;
                    currentLineStart[1] = j;
                    previousCellColor = j < matrix.GetLength(1) ? matrix[i, j] : previousCellColor;
                }


            }

            return "Color: " + savedColor + "\nStart: (" + savedLineStart[0] + "," + savedLineStart[1] + ")" +
                "\nEnd: (" + savedLineEnd[0] + "," + savedLineEnd[1] + ")" + "\nLength: " + (savedLineEnd[1] - savedLineStart[1]+1);
        }

    }

}
