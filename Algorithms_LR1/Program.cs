using System;
using System.Collections.Specialized;
using System.IO;

namespace Algorithms_LR1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            
        }

        public static void FileGeneration()
        {
            StreamWriter sw = new StreamWriter("data.txt");
            Random rnd = new Random();
            sw.Write(Convert.ToString(rnd.Next(-200, 200)));
            while (true)
            {
                sw.Write(" " + Convert.ToString(rnd.Next(-200, 200)));
            }
        }
        
    }
}