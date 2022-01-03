namespace Generics
{
    /// <summary>
    /// Represents a custom-made Stack object. Made by Un1ver5e!
    /// </summary>
    /// <typeparam name="T">Type of items in this Stack.</typeparam>
    public sealed class UStack<T> : UTwoLinkedList<T>
    {

        /// <summary>
        /// Return the top of the stack without removing it.
        /// </summary>
        /// <returns>The top of the stack.</returns>
        public T Peek() => GetLast();

        /// <summary>
        /// Takes the top of the stack.
        /// </summary>
        /// <returns>the top of the stack.</returns>
        public T Pop() => TakeLast();

        /// <summary>
        /// Places given object on the top of the stack.
        /// </summary>
        /// <param name="item">The object to place.</param>
        public void Push(T item) => PlaceLast(item);

        /// <summary>
        /// Checks whether the stack contains elements.
        /// </summary>
        /// <returns>True if the stack contains no elements, otherwise false.</returns>
        public override bool IsEmpty() => base.IsEmpty();

        /// <summary>
        /// Returns the string representation of the stack, starting with the deepest lying elements.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => base.ToString();

        public UStack(params T[] collection)
        {
            foreach (T item in collection)
            {
                PlaceLast(item);
            }
        }

        public UStack(System.Collections.Generic.IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                PlaceLast(item);
            }
        }
    }
}