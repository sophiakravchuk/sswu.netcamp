using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Задача 1.
//Створити клас для роботи з прямокутними матрицями.
//Заповнення матриці має здійснюватися в межах класу та може відбуватись за
//різними законами.
//1. Заповнення матриці у вигляді вертикальної змійки.
//Приклад:
//n = 3, m = 4.
//1 6 7 12
//2 5 8 11
//3 4 9 10

//2.Заповнення матриці у вигляді діагональної змійки для квадратної
//матриці.

//Приклад:
//n = 4, m = 4.
//1  2  6  7
//3  5  8  13
//4  9  12 14
//10 11 15 16

//3.Заповнення матриці у вигляді спіральної змійки.
//n=3, m = 4.
//1 10 9  8
//2 11 12 7
//3 4  5  6
//(Щоб зрозуміти принцип побудови, у всіх випадках рухайтесь в
//прикладах від найменшого до найбільшого значення)
//Продемонструвати виконання заповнення для заданих користувачем
//розмірів матриці.
//Спробуйте в межах заданого закону міняти напрям обходу.


namespace Homework_2_KravchukSophia
{
    internal class Matrix
    {
        private readonly int[,] matrixCanvas;
        private readonly int rowsNumber;
        private readonly int columnsNumber;

        public int RowsNumber {
            get { return this.rowsNumber; }
        }
        public int ColumnsNumber
        {
            get { return this.columnsNumber; }
        }
        public int[,] MatrixCanvas 
        { 
            get
            { 

                int[,]? matrixCopy = this.matrixCanvas.Clone() as int[,];
                return matrixCopy;

            }
        }

        public Matrix()
        {
            this.rowsNumber = 0;
            this.columnsNumber = 0; 

        }
        public Matrix(int n, int m)
        {
            this.matrixCanvas = new int[n,m];
            this.rowsNumber = n;
            this.columnsNumber = m;
        }

        public Matrix(int[,] m)
        {
            this.matrixCanvas = m.Clone() as int[,];
            this.rowsNumber = m.GetLength(0);
            this.columnsNumber = m.GetLength(1);
        }

        public int[,] FillMatrixMethod1()
        {
            int numberToPut = 1;
          
            for (int j = 0; j < this.columnsNumber; j++) // iterate by column
            {
                for (int i = 0; i < this.rowsNumber; i++)
                {
                    int snakeI = j % 2 == 0 ? i : this.rowsNumber - 1 - i; //going up or down

                    this.matrixCanvas[snakeI, j] = numberToPut;
                    numberToPut++;
                }
            }
            return this.MatrixCanvas;
        }

        public int[,] FillMatrixMethod2()

        //Приклад:
        //n = 4, m = 4.
        //1  2  6  7
        //3  5  8  13
        //4  9  12 14
        //10 11 15 16
        {
            if (this.rowsNumber != this.columnsNumber)
            {
                Console.WriteLine("The matrix is not square");
                return this.MatrixCanvas;
            }
           
            int i = 0;
            int j = 0;

            int numberToPut = 1;
            int diagonalNumber = 1;

            int numsInDiag = diagonalNumber;

            while (numberToPut <= this.rowsNumber * this.columnsNumber)
            {
                if (diagonalNumber % 2 == 0)
                {
                    j = diagonalNumber - 1;
                    i = 0;
                    if (j> this.columnsNumber - 1)
                    {
                        i = j - this.columnsNumber + 1;
                        j = this.columnsNumber - 1;
                    }
                }
                else
                {
                    j = 0;
                    i = diagonalNumber - 1;
                    if (i > this.rowsNumber - 1)
                    {
                        j = i - this.rowsNumber + 1;
                        i = this.rowsNumber - 1;
                    }
                }
                for (int numbsInDiagonal = 0; numbsInDiagonal < numsInDiag; numbsInDiagonal++)
                {

                    this.matrixCanvas[i,j] = numberToPut;
                    numberToPut++;
                    if (diagonalNumber % 2 == 0)
                    {
                        i++;
                        j--;

                    }
                    else
                    {
                        i--;
                        j++;
                    }
                }
                if (diagonalNumber >= this.rowsNumber) //after the biggest diagonal
                {
                    numsInDiag--;
                }
                else
                {
                    numsInDiag++;
                }
                diagonalNumber++;

            }

            return this.MatrixCanvas;
        }

        public int[,] FillMatrixMethod3()
        //n=3, m = 4.
        //1 10 9  8
        //2 11 12 7
        //3 4  5  6
        {
            int numberToPut = 1;

            int circle = 0;
            while(numberToPut < this.rowsNumber* this.columnsNumber) { 
                for (int i = circle; i <= this.rowsNumber - 1 - circle; i++)
                {
                    this.matrixCanvas[i, circle] = numberToPut;
                    numberToPut++;
                }//down

                for (int j = circle+1; j <= this.columnsNumber - 1 - circle; j++)
                {
                    this.matrixCanvas[this.rowsNumber - 1 - circle, j] = numberToPut;
                    numberToPut++;
                } //right
                for (int i = this.rowsNumber - 1 - circle - 1; i >= circle; i--)
                {
                    this.matrixCanvas[i, this.columnsNumber - 1 - circle] = numberToPut;
                    numberToPut++;
                }//up
                for (int j = this.columnsNumber - 1 - circle - 1; j > circle; j--)
                {
                    this.matrixCanvas[circle, j] = numberToPut;
                    numberToPut++;
                }//left
                circle++;
            }

            return this.MatrixCanvas;
        }


        public override string ToString()
        {
            string matrixString = "";
            string elementStr = "";
            for (int i = 0; i < this.matrixCanvas.GetLength(0); i++) 
            {
                for (int j = 0; j < this.matrixCanvas.GetLength(1); j++)
                {
                    elementStr = this.matrixCanvas[i, j].ToString();
                    matrixString += elementStr;
                    for (int k = 0; k < (3 - elementStr.Length); k++)
                    {
                        matrixString += " " ;
                    }

                   
                }
                matrixString += "\n";
            }
            return matrixString;
        }
    }
}
