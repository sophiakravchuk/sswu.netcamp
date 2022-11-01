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

//Задача 2.
//Задано матрицю, елементами якої є інформація про колір пікселів,
//заданий в діапазоні 0..16.
//Знайти колір найдовшої горизонтальної лінії, вказати індекси її початку та
//кінця, а також довжину.


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
    internal class Program
    {
        static void Main(string[] args)
        {
            Matrix m = new Matrix(3, 4);
            Matrix m2 = new Matrix(5, 5);
            Matrix m3 = new Matrix(5, 4);


            Console.WriteLine("Task 1.1\n");
            m.FillMatrixMethod1();
            Console.WriteLine(m + "\n");

            Console.WriteLine("Task 1.2\n");
            m2.FillMatrixMethod2();
            Console.WriteLine(m2 + "\n");

            Console.WriteLine("Task 1.3\n");
            m3.FillMatrixMethod3();
            Console.WriteLine(m3 + "\n");



            int n = 8;
            int k = 9;
            //int[,] matrix1 = new int[6,6] { { 2, 2, 14, 2, 9, 0},
            //                                {10, 0, 5, 11, 10, 14},
            //                                {15, 1, 11, 3, 12, 11},
            //                                {15, 11, 12, 11, 11, 11},
            //                                {11, 1, 8, 6, 4, 11},
            //                                {3, 13, 9, 1, 14, 7} };

            int[,] matrix1 = new int[n, k];
            Random rnd = new Random();
            string matrixString = "";
            string elementStr;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    int el = rnd.Next(0, 16);
                    matrix1[i, j] = el;
                    elementStr = el.ToString();
                    matrixString += elementStr;
                    for (int l = 0; l < (3 - elementStr.Length); l++)
                    {
                        matrixString += " ";
                    }

                }
                matrixString += "\n";
            }

            Console.WriteLine("Task 2\n");

            Console.WriteLine(matrixString + "\n");

            Task2 t = new Task2();
            Console.WriteLine(t.GetTheLongestLine(matrix1)+"\n");

            Console.WriteLine("Task 3*\n");
            Task3Cube cube = new Task3Cube(3);
            cube.FillCubeRandomly(0, 5);
            Console.WriteLine(cube.ToString()+"\n");
            Console.WriteLine(cube.CheckForHoles());

        }
    }
}