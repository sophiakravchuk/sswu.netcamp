using System;
using System.IO;
using System.Text;

namespace Homework_9_KravchukSophia
{
    public class PortionFileStream
    {

        private int maxBytes;
        private StreamReader numbersFileReader;
        private bool didJustSeek = false;

        private int lastPortionOverdo = 0;

        public int ArrayMaxLength { get { return this.maxBytes; } }
        public PortionFileStream(string path)
        {
            this.numbersFileReader = new StreamReader(new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite));
            this.maxBytes = 1000;
            

        }

        public PortionFileStream(string path, int seekIndex, int arrayMaxLength=1000)
        {
            if (seekIndex != 0) 
            {
                this.didJustSeek = true;
            }
            this.numbersFileReader = new StreamReader(new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite));
            this.numbersFileReader.BaseStream.Seek(seekIndex, SeekOrigin.Begin);
            this.maxBytes = arrayMaxLength;

        }
        public int[]? GetNewPortion(int portionSize=0)
        {
            portionSize = portionSize == 0 ? this.maxBytes : portionSize - this.lastPortionOverdo;
            if (this.numbersFileReader.EndOfStream)
            {
                return null;
            }
            string numb = "";
            int[] numbersPortion = new int[portionSize];
            int numbersAdded = 0;
            int bytesRead = 0;
            
            bool additionalNumberAdded = false;
            bool allBytesRead = false;

            bool skippedFirstNumber = false;
            char ch;
            do
            {
                ch = (char)this.numbersFileReader.Read();
                if (ch == ',')
                {
                    if (!skippedFirstNumber && this.didJustSeek)
                    {
                        numb = "";
                        skippedFirstNumber = true;
                        this.didJustSeek = false;
                    }
                    else
                    {
                        if (numb != "")
                        {
                            numbersPortion[numbersAdded] = Convert.ToInt32(numb);
                            numb = "";
                            numbersAdded++;
                        }
                    }   

                    if (allBytesRead)
                    {
                        additionalNumberAdded = true;
                    }
                    
                }
                else
                {
                    numb += ch;
                }
                bytesRead++;
                if(bytesRead >= portionSize)
                {
                    allBytesRead = true;
                    if (ch == ',')
                    {
                        additionalNumberAdded = true;
                    }
                }

            } while (!this.numbersFileReader.EndOfStream && !additionalNumberAdded);

            this.lastPortionOverdo = bytesRead - portionSize;

            if (numbersAdded < portionSize)
            {
                numbersPortion = numbersPortion[..numbersAdded];
            }
            return numbersPortion;
        }

        public void CloseFile()
        {
            this.numbersFileReader.Close();
        }

    }
}
