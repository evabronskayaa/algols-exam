namespace SortingAlgorithms
{
    public static class QuickSorter
    {
        private static void Swap(ref int x, ref int y) {
            var t = x;
            x = y;
            y = t;
        }

        private static int Partition(int[] array, int minIndex, int maxIndex) {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++) {
                
                if (array[i] < array[maxIndex]) {
                    pivot++;
                    Swap(ref array[pivot], ref array[i]);
                }
            }

            pivot++;
            Swap(ref array[pivot], ref array[maxIndex]);
            return pivot;
        }

        private static int[] QuickSort(int[] array, int minIndex, int maxIndex) {
            if (minIndex >= maxIndex) return array;

            var pivotIndex = Partition(array, minIndex, maxIndex);
            QuickSort(array, minIndex, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, maxIndex);

            return array;
        }

        public static int[] QuickSort(this int[] array) => QuickSort(array, 0, array.Length - 1);
    }
}