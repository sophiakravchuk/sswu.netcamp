namespace Homework_2_Tensor_KravchukSophia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number = 10;
            int[] oneDimArray = { 5, 8, 71, 35 };
            int[,,,,] fiveDimArray = new int[2, 2, 2, 3, 4]
                {//2
                    {
                        {// 2 matrices [3, 4]:
                            { { 0, 1, 2, 3}, { 4, 5, 6, 7}, { 8, 9, 10, 11} },
                            { { 12, 13, 14, 15}, {16, 17, 18, 19 }, { 20, 21, 22, 23} }
                        },
                        {// 2 matrices [3, 4]:
                            { { 24, 25, 26, 27 }, { 28, 29, 30, 31 }, {32, 33, 34, 35} },
                            { {36, 37, 38, 39}, {40, 41, 42, 43}, {44, 45, 46, 47} }
                        }
                    },
                    {
                        {// 2 matrices [3, 4]:
                            { { 48, 49 ,50, 51}, { 52, 53, 54, 55}, { 56, 57, 58, 59} },
                            { { 60, 61, 62, 63}, { 64, 65, 66, 67}, { 68, 69, 70, 71} }
                        },
                        {// 2 matrices [3, 4]:
                            { { 72, 73, 74, 75 }, { 76, 77, 78, 79 }, { 80, 81, 82, 83} },
                            { { 84, 85, 86, 87 }, { 88, 89, 90, 91}, { 92, 93, 94, 95} }
                        }
                    }
                };


            Task4Tensor tensorNumber = new Task4Tensor(number);
            Task4Tensor tensorOneDim = new Task4Tensor(oneDimArray);
            Task4Tensor tensorFiveDim = new Task4Tensor(fiveDimArray);

            int[] coordsFiveDim = { 1, 0, 1 ,1, 2};

            //GetValue
            Console.WriteLine(tensorNumber.GetValue());
            Console.WriteLine(tensorOneDim.GetValue(2));
            Console.WriteLine(tensorFiveDim.GetValue(coordsFiveDim));

            //SetValue
            tensorNumber.SetValue(11);
            tensorOneDim.SetValue(2, 5);
            tensorFiveDim.SetValue(coordsFiveDim, 1);

            //GetValue
            Console.WriteLine("After SetValue");

            Console.WriteLine(tensorNumber.GetValue());
            Console.WriteLine(tensorOneDim.GetValue(2));
            Console.WriteLine(tensorFiveDim.GetValue(coordsFiveDim));

            Console.WriteLine(tensorNumber);
            Console.WriteLine(tensorOneDim);
            Console.WriteLine(tensorFiveDim);

            //Exceptions
            try
            {
                Console.WriteLine(tensorNumber.GetValue(1));
            } catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Exception caught");
            }
            try
            {
                Console.WriteLine(tensorOneDim.GetValue());
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Exception caught");
            }
            try
            {
                Console.WriteLine(tensorFiveDim.GetValue(1));
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Exception caught");
            }


            
            try
            {
                Console.WriteLine(tensorOneDim.GetValue(10));
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Exception caught");
            }
            try
            {
                int[] coords = { 1, 1, 1, 2, 5};
                Console.WriteLine(tensorFiveDim.GetValue(coords));
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Exception caught");
            }



        }
    }
}