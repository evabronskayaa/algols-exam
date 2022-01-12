namespace SortingAlgorithms
{
    public static class BubbleSorter
    {
        private static void Swap(ref int e1, ref int e2) 
        {
            var temp = e1;
            e1 = e2;
            e2 = temp;
        }

        public static int[] BubbleSort(this int[] array) 
        {
            for (var i = 1; i < array.Length; i++)
            {
                for (var j = 0; j < array.Length - i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    }
                }
            }

            return array;
        }
    }
}