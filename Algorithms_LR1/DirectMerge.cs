using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Algorithms_LR1
{
    public class DirectMerge
    {
        public string FileInput { get; set; }
        private long iterations, segments;

        public DirectMerge(string input)
        {
            FileInput = input;
            segments = 1;
        }

        public void Sort()
        {
            iterations = 1; // степень двойки, количество элементов в каждой серии
            // суть сортировки заключается в распределении на
            // отсортированные серии. Если после распределения
            // на 2 вспомогательных файла и слияния высясняется,
            // что серий было две, значит на следующей итерации
            // серия будет одна - файл отсортирован, завершаем работу.
            while (segments != 2)
            {
                Console.Write(".");
                SplitToFiles();
                MergePairs();
            }
            File.Delete("a.bin");
            File.Delete("b.bin");
            Console.WriteLine();
        }

        public void SortModified()
        {
            using (BinaryReader br = new BinaryReader(File.OpenRead(FileInput)))
            {
                iterations = br.BaseStream.Length / 4 / 4;
            }

            while (segments != 2)
            {
                Console.Write(".");
                SplitToFilesModified();
                MergePairs();
            }
            File.Delete("a.bin");
            File.Delete("b.bin");
            Console.WriteLine();
        }

        private void SplitToFiles() // разделение на 2 вспом. файла
        {
            using (BinaryReader br = new BinaryReader(File.OpenRead(FileInput)))
            using (BinaryWriter writerA = new BinaryWriter(File.Create("a.bin", 65536)))
            using (BinaryWriter writerB = new BinaryWriter(File.Create("b.bin", 65536)))
            {
                segments = 1;
                long counter = 0;
                bool flag = true; // запись либо в 1-ый, либо во 2-ой файл

                long length = br.BaseStream.Length;
                long position = 0;
                while (position != length)
                {
                    // если достигли количества элементов в последовательности -
                    // меняем флаг для след. файла и обнуляем счетчик количества
                    if (counter == iterations)
                    {
                        flag = !flag;
                        counter = 0;
                        segments++;
                    }

                    int element = br.ReadInt32();
                    position += 4;
                    if (flag)
                    {
                        writerA.Write(element);
                    }
                    else
                    {
                        writerB.Write(element);
                    }
                    counter++;
                }
            }
        }

        [SuppressMessage("ReSharper.DPA", "DPA0003: Excessive memory allocations in LOH")]
        private void SplitToFilesModified()
        {
            using (BinaryReader br = new BinaryReader(File.OpenRead(FileInput)))
            using (BinaryWriter writerA = new BinaryWriter(File.Create("a.bin", 65536)))
            using (BinaryWriter writerB = new BinaryWriter(File.Create("b.bin", 65536)))
            {
                segments = 1;
                List<int> array = new List<int>();
                long counter = 0;
                bool picked = false;
                bool flag = true; // запись либо в 1-ый, либо во 2-ой файл

                long length = br.BaseStream.Length;
                long position = 0;
                while (position != length)
                {
                    // если достигли количества элементов в последовательности -
                    // меняем флаг для след. файла и обнуляем счетчик количества
                    if (counter == iterations)
                    {
                        array.Sort();
                        if (flag)
                        {
                            foreach (var el in array)
                            {
                                writerA.Write(el);
                            }
                        }
                        else
                        {
                            foreach (var el in array)
                            {
                                writerB.Write(el);
                            }
                        }

                        picked = false;
                        flag = !flag;
                        counter = 0;
                        segments++;
                        array.Clear();
                    }

                    array.Add(br.ReadInt32());
                    picked = true;
                    position += 4;
                    counter++;
                }

                if (picked)
                {
                    if (flag)
                    {
                        foreach (var el in array)
                        {
                            writerA.Write(el);
                        }
                    }
                    else
                    {
                        foreach (var el in array)
                        {
                            writerB.Write(el);
                        }
                    }
                }
                array.Clear();
            }
        }

        private void MergePairs() // слияние отсорт. последовательностей обратно в файл
        {
            using (BinaryReader readerA = new BinaryReader(File.OpenRead("a.bin")))
            using (BinaryReader readerB = new BinaryReader(File.OpenRead("b.bin")))
            using (BinaryWriter bw = new BinaryWriter(File.Create(FileInput, 65536)))
            {
                long counterA = iterations, counterB = iterations;
                int elementA = 0, elementB = 0;
                bool pickedA = false, pickedB = false, endA = false, endB = false;
                long lengthA = readerA.BaseStream.Length;
                long lengthB = readerB.BaseStream.Length;
                long positionA = 0;
                long positionB = 0;
                while (!endA || !endB)
                {
                    if (counterA == 0 && counterB == 0)
                    {
                        counterA = iterations;
                        counterB = iterations;
                    }

                    if (positionA != lengthA)
                    {
                        if (counterA > 0 && !pickedA)
                        {
                            elementA = readerA.ReadInt32();
                            positionA += 4;
                            pickedA = true;
                        }
                    }
                    else
                    {
                        endA = true;
                    }

                    if (positionB != lengthB)
                    {
                        if (counterB > 0 && !pickedB)
                        {
                            elementB = readerB.ReadInt32();
                            positionB += 4;
                            pickedB = true;
                        }
                    }
                    else
                    {
                        endB = true;
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

                iterations *= 2; // увеличиваем размер серии в 2 раза
            }
        }
    }
}