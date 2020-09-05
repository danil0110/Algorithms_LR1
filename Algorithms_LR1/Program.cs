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
            string filename = "test.bin";
            // FileGeneration(filename);
            FileForTest();
            DirectMerge dm = new DirectMerge(filename);
            dm.Sort();
        }

        public static void FileForTest()
        {
            int[] arr = {8, 23, 5, 65, 44, 33, 1, 6};
            BinaryWriter bw = new BinaryWriter(File.Create("test.bin"));
            foreach (var el in arr)
            {
                bw.Write(el);
            }
            
            bw.Close();
        }
        
        public static void FileGeneration(string file)
        {
            Stopwatch s = new Stopwatch();
            BinaryWriter bw = new BinaryWriter(File.Create(file));
            Random rnd = new Random();
            s.Start();
            while (s.Elapsed < TimeSpan.FromSeconds(30))
            {
                bw.Write(rnd.Next(-500, 500));
            }
            s.Stop();
            bw.Close();
        }
        
    }
    
    
    
}