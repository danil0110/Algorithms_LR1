using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;

namespace Algorithms_LR1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string filename = "data.bin";
            Generator(filename, 500);
            // LargeFileGeneration(filename);
            Console.WriteLine("Before sort: ");
            OutputData(filename);
            DirectMerge dm = new DirectMerge(filename);
            dm.Sort();
            Console.WriteLine("\nAfter sort: ");
            OutputData(filename);
        }
        
        public static void LargeFileGeneration(string file)
        {
            //Stopwatch s = new Stopwatch();
            BinaryWriter bw = new BinaryWriter(File.Create(file));
            Random rnd = new Random();
            //s.Start();
            for (int i = 0; i < 256000000; i++)
            {
                bw.Write(rnd.Next(-500, 500));
            }
            //s.Stop();
            bw.Close();
        }

        public static void OutputData(string file)
        {
            BinaryReader br = new BinaryReader(File.Open(file, FileMode.Open));
            for (int i = 0; i < 100; i++)
            {
                if (br.BaseStream.Position == br.BaseStream.Length)
                {
                    break;
                }
                else
                {
                    Console.Write($"{br.ReadInt32()} ");
                }
            }
            
            br.Close();
        }

        public static void Generator(string file, int count)
        {
            Random rnd = new Random();
            BinaryWriter bw = new BinaryWriter(File.Create(file));
            for (int i = 0; i < count; i++)
            {
                bw.Write(rnd.Next(-500, 500));
            }
            bw.Close();
        }
        
    }
}