using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Algorithms_LR1
{
    public class DirectMerge
    {
        public string FileInput { get; set; }
        private int iterations, segments;

        public DirectMerge(string input)
        {
            FileInput = input;
            iterations = 1;
        }
        
        public void Sort()
        {
            SplitToFiles();
            BinaryReader br1 = new BinaryReader(File.Open("a.bin", FileMode.Open));
            BinaryReader br2 = new BinaryReader(File.Open("b.bin", FileMode.Open));
            for (int i = 0; i < 4; i++)
            {
                Console.Write($"{br1.ReadInt32()} ");
            }
            Console.WriteLine();
            for (int i = 0; i < 4; i++)
            {
                Console.Write($"{br2.ReadInt32()} ");
            }
        }

        private void SplitToFiles()
        {
            BinaryReader br = new BinaryReader(File.Open(FileInput, FileMode.Open));
            BinaryWriter writerA = new BinaryWriter(File.Create("a.bin"));
            BinaryWriter writerB = new BinaryWriter(File.Create("b.bin"));
            int counter = 0;
            bool flag = true;
            while (br.BaseStream.Position != br.BaseStream.Length)
            {
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

                if (counter == iterations)
                {
                    flag = !flag;
                    counter = 0;
                }
            }
            br.Close();
            writerA.Close();
            writerB.Close();
        }
        
    }
}