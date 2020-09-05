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
            FileGeneration("data.bin");
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