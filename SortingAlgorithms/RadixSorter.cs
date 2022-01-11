namespace SortingAlgorithms
{
    public static class RadixSorter
    {
        // боже, срань господня, помогите
        private static int GetMax(int[] arr, int n)
        {
            var mx = arr[0];
            for (var i = 1; i < n; i++)
                if (arr[i] > mx) mx = arr[i];
            return mx;
        }

        private static void CountSort(int[] arr, int n, int exp)
        {
            var output = new int[n];
            // массив с количеством чисел с конкретной цифрой в нужном разряде
            var count = new int[10];

            // экспонента на первом проходе 1
            for (var i = 0; i < n; i++)
                count[(arr[i] / exp) % 10]++; 

            for (var i = 1; i < 10; i++)
                count[i] += count[i - 1];
            
            for (var i = n - 1; i >= 0; i--)
            {
                output[count[(arr[i] / exp) % 10] - 1] = arr[i];
                count[(arr[i] / exp) % 10]--;
            }

            for (var i = 0; i < n; i++)
                arr[i] = output[i];
        }

        public static void RadixSort(this int[] arr)
        {
            var n = arr.Length;
            var m = GetMax(arr, n); 

            // сортируем по разряду
            for (var exp = 1; m / exp > 0; exp *= 10)
                CountSort(arr, n, exp);
        }
    }
}