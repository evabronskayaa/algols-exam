namespace SortingAlgorithms
{
    public static class BinarySearcher
    {
        public static int BinarySearchRecursive(this int[] array, int searchedValue, int first, int last) {
            if (first > last) return -1;

            var middle = (first + last) / 2;
            var middleValue = array[middle];

            if (middleValue == searchedValue) {
                return middle;
            }
            else
            {
                if (middleValue > searchedValue) {
                    return BinarySearchRecursive(array, searchedValue, first, middle - 1);
                }
                else {
                    return BinarySearchRecursive(array, searchedValue, middle + 1, last);
                }
            }
        }
        
        public static int BinarySearchIterative(this int[] array, int searchedValue, int left, int right) {
            while (left <= right) {
                var middle = (left + right) / 2;

                if (searchedValue == array[middle]) {
                    return middle;
                }
                else if (searchedValue < array[middle]) {
                    right = middle - 1;
                }
                else {
                    left = middle + 1;
                }
            }
            return -1;
        }
    }
}