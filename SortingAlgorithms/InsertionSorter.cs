namespace SortingAlgorithms
{
    public static class InsertionSorter
    {
        private static void Swap(ref int e1, ref int e2)
        {
            var temp = e1;
            e1 = e2;
            e2 = temp;
        }

        public static int[] InsertionSort(this int[] array)
        {
            for (var i = 1; i < array.Length; i++)
            {
                for (var j = i; j > 0 && array[j - 1] > array[j]; j--)
                {
                    Swap(ref array[j - 1], ref array[j]);
                }
            }

            return array;
        }
    }
}

// 12 11 13 5 6 7 112
// 11 12 13 5 6 7 112
// 11 12 13 5 6 7 112
// 5 11 12 13 6 7 112
// 5 6 11 12 13 7 112