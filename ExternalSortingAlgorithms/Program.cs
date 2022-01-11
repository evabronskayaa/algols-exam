using System;
using System.IO;

namespace ExternalSortingAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            MakeFile("file.txt", 10);
            NaturalMergeSort.DoPolypathNaturalSort("file.txt", 2);
            Console.WriteLine("Hello World!");
        }

        private static void MakeFile(string filePath, int length)
        {
            if (File.Exists(filePath)) File.Delete(filePath);
            var rnd = new Random();
            var file = new StreamWriter(filePath);
            var a = new int[length];

            for (var i = 0; i < length; i++) file.WriteLine(rnd.Next(100));

            file.Close();
        }
    }
}