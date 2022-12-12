using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Homework_9_KravchukSophia
{
    public class SplitMergeSorter
    {
        public static void SortArray(ref int[] array, int leftIndex, int rightIndex)
        {
            
            if (leftIndex < rightIndex)
            {
                int middleIndex = leftIndex + (rightIndex - leftIndex) / 2;

                SplitMergeSorter.SortArray(ref array, leftIndex, middleIndex);
                SplitMergeSorter.SortArray(ref array, middleIndex + 1, rightIndex);

                SplitMergeSorter.MergeArrays(ref array, leftIndex, middleIndex, rightIndex);
            }
            
        }
        private static void MergeArrays(ref int[] array, int leftIndex, int middleIndex, int rightIndex)
        {
            int firstPartLength = middleIndex - leftIndex + 1;
            int secondPartLength = rightIndex - middleIndex;

            int[] leftArray = new int[firstPartLength];
            int[] rightArray = new int[secondPartLength];
            
            for (int i = 0; i < firstPartLength; ++i)
            {
                leftArray[i] = array[leftIndex + i];
            }
            for (int i = 0; i < secondPartLength; ++i)
            {
                rightArray[i] = array[middleIndex + 1 + i];
            }

            int firstArrayIndex = 0;
            int secondArrayIndex = 0;


            int mergedArrayIndex = leftIndex;
            while (firstArrayIndex < firstPartLength && secondArrayIndex < secondPartLength)
            {
                if (leftArray[firstArrayIndex] <= rightArray[secondArrayIndex])
                {
                    array[mergedArrayIndex] = leftArray[firstArrayIndex];
                    firstArrayIndex++;
                }
                else
                {
                    array[mergedArrayIndex] = rightArray[secondArrayIndex];
                    secondArrayIndex++;
                }
                mergedArrayIndex++;
            }

            while (firstArrayIndex < firstPartLength)
            {
                array[mergedArrayIndex] = leftArray[firstArrayIndex];
                firstArrayIndex++;
                mergedArrayIndex++;
            }

            while (secondArrayIndex < secondPartLength)
            {
                array[mergedArrayIndex] = rightArray[secondArrayIndex];
                secondArrayIndex++;
                mergedArrayIndex++;
            }
        }
    }
}
