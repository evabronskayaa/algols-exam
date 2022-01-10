namespace SortingAlgorithms
{
    public static class CocktailSorter
    {
        private static void Swap(ref int e1, ref int e2) {
            var temp = e1;
            e1 = e2;
            e2 = temp;
        }

        public static int[] CocktailSort(this int[] array) {
            for (var i = 0; i < array.Length / 2; i++) {
                
                var swapFlag = false;
                for (var j = i; j < array.Length - i - 1; j++) {
                    
                    if (array[j] > array[j + 1]) {
                        Swap(ref array[j], ref array[j + 1]);
                        swapFlag = true;
                    }
                }

                for (var j = array.Length - 2 - i; j > i; j--) {
                    
                    if (array[j - 1] > array[j]) {
                        Swap(ref array[j - 1], ref array[j]);
                        swapFlag = true;
                    }
                }

                if (!swapFlag) break;
            }

            return array;
        }
        
    }
}