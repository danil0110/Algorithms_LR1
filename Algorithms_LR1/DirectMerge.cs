using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Algorithms_LR1
{
    public class DirectMerge
    {
        public string FileInput { get; set; }
        private ulong iterations, segments;

        public DirectMerge(string input)
        {
            FileInput = input;
            iterations = 1;
        }
        
        public void Sort()
        {
            while (true)
            {
                SplitToFiles();
                if (segments == 1)
                {
                    break;
                }
                MergePairs();
            }
        }

        private void SplitToFiles()
        {
            segments = 1;
            BinaryReader br = new BinaryReader(File.Open(FileInput, FileMode.Open));
            BinaryWriter writerA = new BinaryWriter(File.Create("a.bin"));
            BinaryWriter writerB = new BinaryWriter(File.Create("b.bin"));
            ulong counter = 0;
            bool flag = true;
            while (br.BaseStream.Position != br.BaseStream.Length)
            {
                if (counter == iterations)
                {
                    flag = !flag;
                    counter = 0;
                    segments++;
                }
                
                if (flag)
                {
                    writerA.Write(br.ReadInt32());
                    counter++;
                }
                else
                {
                    writerB.Write(br.ReadInt32());
                    counter++;
                }
            }
            br.Close();
            writerA.Close();
            writerB.Close();
        }

        private void MergePairs()
        {
            BinaryReader readerA = new BinaryReader(File.Open("a.bin", FileMode.Open));
            BinaryReader readerB = new BinaryReader(File.Open("b.bin", FileMode.Open));
            BinaryWriter bw = new BinaryWriter(File.Create(FileInput));
            ulong counterA = iterations, counterB = iterations;
            int elementA = 0, elementB = 0;
            bool pickedA = false, pickedB = false, endA = false, endB = false;

            while (true)
            {
                if (endA && endB)
                {
                    break;
                }
                
                if (counterA == 0 && counterB == 0)
                {
                    counterA = iterations;
                    counterB = iterations;
                }

                if (readerA.BaseStream.Position != readerA.BaseStream.Length)
                {
                    if (counterA > 0)
                    {
                        if (!pickedA)
                        {
                            elementA = readerA.ReadInt32();
                            pickedA = true;
                        }
                    }
                }
                else
                {
                    endA = true;
                }

                if (readerB.BaseStream.Position != readerB.BaseStream.Length)
                {
                    if (counterB > 0)
                    {
                        if (!pickedB)
                        {
                            elementB = readerB.ReadInt32();
                            pickedB = true;
                        }
                    }
                }
                else
                {
                    endB = true;
                }

                if (endA && endB && pickedA == false && pickedB == false)
                {
                    break;
                }
                if (pickedA)
                {
                    if (pickedB)
                    {
                        if (elementA < elementB)
                        {
                            bw.Write(elementA);
                            counterA--;
                            pickedA = false;
                        }
                        else
                        {
                            bw.Write(elementB);
                            counterB--;
                            pickedB = false;
                        }
                    }
                    else
                    {
                        bw.Write(elementA);
                        counterA--;
                        pickedA = false;
                    }
                }
                else if (pickedB)
                {
                    bw.Write(elementB);
                    counterB--;
                    pickedB = false;
                }

            }

            iterations *= 2;

            bw.Close();
            readerA.Close();
            readerB.Close();
        }
        
    }
}