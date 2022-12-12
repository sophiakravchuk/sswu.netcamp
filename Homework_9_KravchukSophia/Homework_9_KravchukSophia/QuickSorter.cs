using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_9_KravchukSophia
{
    public enum SortTypes
    {
        LastPivot = -1,
        FirstPivot = 0,
        RandomPivot = 1
    }
    public class QuickSorter
    {

        private static void SwapTwoElements(List<Product> array, int i, int j)
        {
            Product temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        private static int MakePartitionLastPivot(List<Product> array, int startIndex, int endIndex)
        {
            Product pivot = array[endIndex];
            int pivotNewPlaceIndex = startIndex - 1;

            for (int i = startIndex; i <= endIndex-1; i++)
            {
                if (pivot.IsMoreExpensive(array[i]))
                {
                    pivotNewPlaceIndex++;
                    QuickSorter.SwapTwoElements(array, pivotNewPlaceIndex, i);
                }
            }
            QuickSorter.SwapTwoElements(array, pivotNewPlaceIndex + 1, endIndex);
            return pivotNewPlaceIndex + 1;
        }

        private static int MakePartitionFirstPivot(List<Product> array, int startIndex, int endIndex)
        {
            Product pivot = array[startIndex];
            int pivotNewPlaceIndex = endIndex+1;

            for (int i = endIndex; i > startIndex; i--)
            {
                if (array[i].IsMoreExpensive(pivot))
                {
                    pivotNewPlaceIndex--;
                    QuickSorter.SwapTwoElements(array, pivotNewPlaceIndex, i);
                }
            }
            QuickSorter.SwapTwoElements(array, pivotNewPlaceIndex-1, startIndex);
            return pivotNewPlaceIndex-1;
        }
        private static int MakePartitionRandomPivot(List<Product> array, int startIndex, int endIndex)
        {
            var rand = new Random();
            int pivotIndex = rand.Next(startIndex, endIndex-1);
            QuickSorter.SwapTwoElements(array, pivotIndex, endIndex);
            return QuickSorter.MakePartitionLastPivot(array, startIndex, endIndex);
        }

        public static void QuickSort(List<Product> array, int startIndex, int endIndex, SortTypes pivotType=SortTypes.LastPivot)
        {
            if (startIndex < endIndex)
            {
                int partitionPivot = 0;
                switch (pivotType)
                {
                    case SortTypes.LastPivot:
                        partitionPivot = QuickSorter.MakePartitionLastPivot(array, startIndex, endIndex);
                        break;
                    case SortTypes.FirstPivot:
                        partitionPivot = QuickSorter.MakePartitionFirstPivot(array, startIndex, endIndex);
                        break;
                    case SortTypes.RandomPivot:
                        partitionPivot = QuickSorter.MakePartitionRandomPivot(array, startIndex, endIndex);
                        break;
                }

               
                QuickSorter.QuickSort(array, startIndex, partitionPivot - 1, pivotType);
                QuickSorter.QuickSort(array, partitionPivot + 1, endIndex, pivotType);
            }
        }

    }
}
