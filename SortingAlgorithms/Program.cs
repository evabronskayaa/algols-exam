using System;

namespace SortingAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] strArray = {"hello", "world", "bye", "plum", "banana"};
            var aa = new ABC_Sorter(strArray);
            var result = aa.ABCSort();
            PrintArray(result);

            int[] arr = {12, 11, 13, 5, 6, 7, 112};
            // arr.BubbleSort();
            // arr.CocktailSort();
            // arr.InsertionSort();
            arr.QuickSort();
            // arr.RadixSort();
            // arr.SedgwickSort();
            // arr.SelectionSort();
            // arr.ShellSort();
            // arr = arr.TreeSort();
            //
            // Console.WriteLine("Sorted array is");
            PrintArray(arr);
            //
            // Console.WriteLine( 12 / 10 % 10);
            
            
            while (true)
            {
                Console.Write("Введите искомое значение или -777 для выхода: ");
                var k = Convert.ToInt32(Console.ReadLine());
                if (k == -777) break;
            
                var searchResult = arr.BinarySearchRecursive(k, 0, arr.Length - 1);
                //var searchResult = arr.BinarySearchIterative(k, 0, arr.Length - 1);
                if (searchResult < 0) 
                {
                    Console.WriteLine("Элемент со значением {0} не найден", k);
                }
                else 
                {
                    Console.WriteLine("Элемент найден. Индекс элемента со значением {0} равен {1}", k, searchResult);
                }
            }
        }

        private static void PrintArray<T>(T[] arr) {
            var n = arr.Length;
            for (var i = 0; i < n; ++i) Console.Write(arr[i]+" ");
        }
    }
}