using System;

namespace SortingAlgorithms
{
    public static class ShellSorter
    {
        private static void Swap(ref int x, ref int y)
        {
            var t = x;
            x = y;
            y = t;
        }
        
        public static int[] ShellSort(this int[] array)
        {
            var d = array.Length / 2;
            while (d >= 1)
            {
                for (var i = d; i < array.Length; i++)
                {
                    var j = i;
                    while ((j >= d) && (array[j - d] > array[j]))
                    {
                        Swap(ref array[j], ref array[j - d]);
                        j -= d;
                    }
                }

                d /= 2;
            }

            return array;
        }

        private static int Increment(long[] inc, long size)
        {
            int p1 = 1, p2 = 1, p3 = 1, s = -1;

            do
            {
                if (++s % 2 != 0)
                {
                    inc[s] = 8 * p1 - 6 * p2 + 1;
                }
                else
                {
                    inc[s] = 9 * p1 - 9 * p3 + 1;
                    p2 *= 2;
                    p3 *= 2;
                }

                p1 *= 2;
            } while (3 * inc[s] < size);

            return s > 0 ? --s : 0;
        }

        public static void SedgwickSort<T>(this T[] a)
            where T : IComparable
        {
            var size = a.Length;
            long j;
            var seq = new long[40];

            // вычисление последовательности приращений
            int s = Increment(seq, size);
            while (s >= 0)
            {
                // сортировка вставками с инкрементами inc[] 
                var inc = seq[s--];
                for (var i = inc; i < size; i++)
                {
                    var temp = a[i];
                    for (j = i - inc; (j >= 0) && (a[j].CompareTo(temp) > 0); j -= inc)
                        a[j + inc] = a[j];
                    a[j + inc] = temp;
                }
            }
        }
    }
}