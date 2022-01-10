namespace SortingAlgorithms
{
    public static class SelectionSorter
    {
        private static void Swap(ref int x, ref int y)
        {
            var t = x;
            x = y;
            y = t;
        }

        private static int IndexOfMin(int[] array, int n) 
        {
            int result = n;
            for (var i = n; i < array.Length; ++i) 
            {
                if (array[i] < array[result])
                {
                    result = i;
                }
            }

            return result;
        }

        public static int[] SelectionSort(this int[] array, int currentIndex = 0) 
        {
            if (currentIndex == array.Length) return array;

            var index = IndexOfMin(array, currentIndex);
            if (index != currentIndex) 
            {
                Swap(ref array[index], ref array[currentIndex]);
            }

            return SelectionSort(array, currentIndex + 1);
        }
    }
}