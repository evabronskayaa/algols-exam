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

        public static void InsertionSort(this int[] array) 
        {
            for (var i = 1; i < array.Length; i++) 
            {
                var key = array[i];
                var j = i;
                while (j > 0 && array[j - 1] > key) 
                {
                    Swap(ref array[j - 1], ref array[j]);
                    j--;
                }

                array[j] = key;
            }
        }
    }
}