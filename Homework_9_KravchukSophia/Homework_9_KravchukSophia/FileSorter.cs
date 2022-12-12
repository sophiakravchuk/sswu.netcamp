using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Homework_9_KravchukSophia
{
    public class FileSorter
    { 
        private readonly PortionFileStream numbersFileManager;

        private readonly string fileToReadFrom = "../../../temp1.txt";
        private readonly string fileToWriteInto = "../../../temp2.txt";

        public FileSorter(string numbersFilePath)
        {
            this.numbersFileManager = new PortionFileStream(numbersFilePath);
        }

        public void SortFile(string pathToResulFile)
        {
            this.SortFirstPortions();
            this.MergeHalfsIntoFile(this.fileToReadFrom, this.fileToWriteInto, 1, pathToResulFile);
        }

        private void SortFirstPortions()
        {
            using StreamWriter tempFile1 = new(this.fileToReadFrom);

            int[]? portion = this.numbersFileManager.GetNewPortion();
            while (portion != null)
            {
                SplitMergeSorter.SortArray(ref portion, 0, portion.Count() - 1);
                foreach (var item in portion)
                {
                    tempFile1.Write(item + ",");
                }
                portion = this.numbersFileManager.GetNewPortion(this.numbersFileManager.ArrayMaxLength);
            }

            tempFile1.Close();
        }
        private void MergeHalfsIntoFile(string readFileName, string writeFileName, int mergeRun, string pathToResulFile)
        {
            using StreamWriter tempFile = new(writeFileName);
            int halfAllowedPortionLength = this.numbersFileManager.ArrayMaxLength / 2;
            int secondPartStart = this.numbersFileManager.ArrayMaxLength * mergeRun;
            int stepsAllowed = secondPartStart / halfAllowedPortionLength;

            PortionFileStream firstPartStream = new PortionFileStream(readFileName);
            PortionFileStream secondPartStream = new PortionFileStream(readFileName, secondPartStart);



            int[]? firstPartLeft = firstPartStream.GetNewPortion(halfAllowedPortionLength);
            int[]? secondPartLeft = secondPartStream.GetNewPortion(halfAllowedPortionLength);
            if (secondPartLeft == null)
            {
                firstPartStream.CloseFile();
                secondPartStream.CloseFile();
                tempFile.Close();
                System.IO.File.Delete(writeFileName);

                System.IO.File.Move(readFileName, pathToResulFile);
                return;
            }
            while (firstPartLeft != null || secondPartLeft != null)
            {
                int firstPartLength = firstPartLeft == null ? 0: firstPartLeft.Length;
                int secondPartLength = secondPartLeft == null ? 0: secondPartLeft.Length;

                int firstArrayIndex = 0;
                int secondArrayIndex = 0;

                int leftPartSteps = 1;
                int rightPartSteps = 1;

                while (true)
                {
                    while (firstArrayIndex < firstPartLength && secondArrayIndex < secondPartLength)
                    {
                        if (firstPartLeft[firstArrayIndex] <= secondPartLeft[secondArrayIndex])
                        {
                            tempFile.Write(firstPartLeft[firstArrayIndex] + ",");
                            firstArrayIndex++;
                        }
                        else
                        {
                            tempFile.Write(secondPartLeft[secondArrayIndex] + ",");
                            secondArrayIndex++;
                        }
                    }
                    if (firstArrayIndex >= firstPartLength)
                    {
                        if (leftPartSteps < stepsAllowed)
                        {
                            firstPartLeft = firstPartStream.GetNewPortion(halfAllowedPortionLength);
                            leftPartSteps++;
                            firstArrayIndex = 0;
                            firstPartLength = firstPartLeft == null ? 0 : firstPartLeft.Length;
                            
                        }
                        else
                        {
                            break;
                        }
                        
                    }
                    else if (secondArrayIndex >= secondPartLength)
                    {
                        if (rightPartSteps < stepsAllowed)
                        {
                            secondPartLeft = secondPartStream.GetNewPortion(halfAllowedPortionLength);
                            rightPartSteps++;
                            secondArrayIndex = 0;
                            secondPartLength = secondPartLeft == null ? 0 : secondPartLeft.Length;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                while (true)
                {
                    while (firstArrayIndex < firstPartLength)
                    {
                        tempFile.Write(firstPartLeft[firstArrayIndex] + ",");
                        firstArrayIndex++;
                    }

                    while (secondArrayIndex < secondPartLength)
                    {
                        tempFile.Write(secondPartLeft[secondArrayIndex] + ",");
                        secondArrayIndex++;
                    }
                    if (rightPartSteps < stepsAllowed)
                    {
                        secondPartLeft = secondPartStream.GetNewPortion(halfAllowedPortionLength);
                        rightPartSteps++;
                        secondArrayIndex = 0;
                        secondPartLength = secondPartLeft == null? 0: secondPartLeft.Length;
                    }
                    else if (leftPartSteps < stepsAllowed)
                    {
                        firstPartLeft = firstPartStream.GetNewPortion(halfAllowedPortionLength);
                        leftPartSteps++;
                        firstArrayIndex = 0;
                        firstPartLength = firstPartLeft == null ? 0 : firstPartLeft.Length;
                    }
                    else
                    {
                        break;
                    }
                }
                

                secondPartStart += 2 * secondPartStart;
                firstPartStream.CloseFile();
                firstPartStream = secondPartStream;
                secondPartStream = new PortionFileStream(readFileName, secondPartStart);
                firstPartLeft = firstPartStream.GetNewPortion(halfAllowedPortionLength);
                secondPartLeft = secondPartStream.GetNewPortion(halfAllowedPortionLength);

            }
            tempFile.Close();
            firstPartStream.CloseFile();
            secondPartStream.CloseFile();
            System.IO.File.Delete(readFileName);

            this.MergeHalfsIntoFile(writeFileName, readFileName, mergeRun*2, pathToResulFile);

        }

    }
}
