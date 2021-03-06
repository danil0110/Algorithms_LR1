﻿using System;
using System.Diagnostics;
using System.IO;

namespace Algorithms_LR1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            string filename = "data.bin", input;
            LargeFileGeneration(filename);
            //Generator(filename, 10000, -5000, 5000);
            
            Console.WriteLine("Before sort: ");
            OutputData(filename);

            DirectMerge dm = new DirectMerge(filename);
            Console.Write("Choose method (1 - original / 2 - modified)\n---> ");
            while (true)
            {
                input = Console.ReadLine();
                if (input == "1")
                {
                    sw.Start();
                    dm.Sort();
                    sw.Stop();
                    break;
                }
                else if (input == "2")
                {
                    sw.Start();
                    dm.SortModified();
                    sw.Stop();
                    break;
                }
                else
                {
                    Console.Write("Wrong input. Try again.\n---> ");
                }
            }

            Console.WriteLine("After sort: ");
            OutputData(filename);
            Console.WriteLine($"Elapsed: {(double)sw.ElapsedMilliseconds / 1000} seconds");
        }

        public static void LargeFileGeneration(string file)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Create(file, 65536)))
            {
                Random rnd = new Random();
                for (int i = 0; i < 270000000; i++)
                {
                    bw.Write(rnd.Next(0, 256000000));
                }
            }
        }

        public static void Generator(string file, int count, int start, int end) // для тестов
        {
            using (BinaryWriter bw = new BinaryWriter(File.Create(file, 65536)))
            {
                Random rnd = new Random();
                for (int i = 0; i < count; i++)
                {
                    bw.Write(rnd.Next(start, end));
                }
            }
        }
        
        public static void OutputData(string file) // вывод первых 100 чисел для проверки
        {
            using (BinaryReader br = new BinaryReader(File.OpenRead(file)))
            {
                long length = br.BaseStream.Length;
                long position = 0;
                for (int i = 0; i < 100; i++)
                {
                    if (position == length)
                    {
                        break;
                    }
                    else
                    {
                        Console.Write($"{br.ReadInt32()} ");
                        position += 4;
                    }
                }
                Console.WriteLine();
            }
        }
    }
}