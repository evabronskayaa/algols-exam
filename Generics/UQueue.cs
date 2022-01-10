namespace Generics
{
    /// <summary>
    /// Represents a custom-made Queue object. Made by Un1ver5e!
    /// </summary>
    /// <typeparam name="T">Type of items in this Stack.</typeparam>
    public sealed class UQueue<T> : UTwoLinkedList<T>
    {

        /// <summary>
        /// Return the end of the stack without removing it.
        /// </summary>
        /// <returns>The top of the stack.</returns>
        public T End() => GetLast();

        /// <summary>
        /// Takes the End of the queue.
        /// </summary>
        public T Dequeue() => TakeFirst();

        /// <summary>
        /// Places given object to the beginning of the stack.
        /// </summary>
        public void Enqueue(T item) => PlaceLast(item);

        public UQueue(params T[] collection)
        {
            foreach (T item in collection)
            {
                PlaceLast(item);
            }
        }

        public UQueue(System.Collections.Generic.IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                PlaceLast(item);
            }
        }
    }
}