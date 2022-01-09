using System;

namespace ExternalSortingAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectMergeSort.MakeFile("file.txt", 10);
            DirectMergeSort.SortFile("file.txt");
            Console.WriteLine("Hello World!");
        }
    }
}