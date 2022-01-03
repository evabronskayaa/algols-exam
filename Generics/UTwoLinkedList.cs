using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
    /// <summary>
    /// Represents a two-way linked List. Made by Un1ver5e!
    /// </summary>
    /// <typeparam name="T">Type of items in the List.</typeparam>
    public abstract partial class UTwoLinkedList<T> : System.Collections.Generic.IEnumerable<T>
    {
        /// <summary>
        /// Represents en empty Node, located both in the beginning and the end of the list, making it loop.
        /// </summary>
        internal Node<T> endpoint = new Node<T>(default);

        /// <summary>
        /// Represents the first element of the list.
        /// </summary>
        internal Node<T> first
        {
            get => endpoint.Next;
        }

        /// <summary>
        /// Represents the last element of the list.
        /// </summary>
        internal Node<T> last
        {
            get => endpoint.Previous;
        }

        /// <summary>
        /// Gets the first item in the List.
        /// </summary>
        /// <returns>The first item in the List.</returns>
        public T GetFirst() => first.Content;
        /// <summary>
        /// Gets the last item in the List.
        /// </summary>
        /// <returns>The last item in the List.</returns>
        public T GetLast() => last.Content;

        /// <summary>
        /// Takes the first item in the List and removes it.
        /// </summary>
        /// <returns>The first item in the List.</returns>
        public T TakeFirst() => first.SafeTake().Content;
        /// <summary>
        /// Takes the last item in the List and removes it.
        /// </summary>
        /// <returns>The last item in the List.</returns>
        public T TakeLast() => last.SafeTake().Content;

        /// <summary>
        /// Places the item in the first position in the List.
        /// </summary>
        /// <param name="item">The item to place.</param>
        public void PlaceFirst(T item) => new Node<T>(item).PlaceBetween(endpoint, first);
        /// <summary>
        /// Places the item in the last position in the List.
        /// </summary>
        /// <param name="item">The item to place.</param>
        public void PlaceLast(T item) => new Node<T>(item).PlaceBetween(last, endpoint);

        /// <summary>
        /// Defines whether the List contains any elements.
        /// </summary>
        /// <returns>True is the List is empty, otherwise false.</returns>
        public virtual bool IsEmpty() => endpoint.Next == endpoint;

        /// <summary>
        /// Builds a string representation of this List.
        /// </summary>
        /// <returns>All the items in the List from first to the last.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Node<T> item = first;
            while (item != endpoint)
            {
                sb.Append(item.Content.ToString() + "; ");
                item = item.Next;
            }
            return sb.ToString();
        }

        public IEnumerator<T> GetEnumerator() => new UTwoLinkedListEnumerator<T>(this);

        IEnumerator IEnumerable.GetEnumerator() => new UTwoLinkedListEnumerator<T>(this);

        internal class Node<I>
        {
            internal Node<I> Previous { get; private set; }
            internal readonly I Content;
            internal Node<I> Next { get; private set; }

            /// <summary>
            /// Takes the element and redefines it's neighbours' references.
            /// </summary>
            /// <returns></returns>
            internal Node<I> SafeTake()
            {
                Previous.Next = Next;
                Next.Previous = Previous;
                return this;
            }

            /// <summary>
            /// Places the element between two other elements.
            /// </summary>
            /// <param name="previous"></param>
            /// <param name="next"></param>
            internal void PlaceBetween(Node<I> previous, Node<I> next)
            {
                Next = next;
                Previous = previous;
                Previous.Next = this;
                Next.Previous = this;
            }

            /// <summary>
            /// Places the element after a specified element.
            /// </summary>
            /// <param name="element"></param>
            internal void PlaceAfter(Node<I> element)
            {
                Next = element.Next;
                Previous = element;
                Previous.Next = this;
                Next.Previous = this;
            }

            /// <summary>
            /// Places the element before a specified element.
            /// </summary>
            /// <param name="element"></param>
            internal void PlaceBefore(Node<I> element)
            {
                Next = element;
                Previous = element.Previous;
                Previous.Next = this;
                Next.Previous = this;
            }

            /// <summary>
            /// Creates a Node with a specified content, referencing itself as its' neighbours.
            /// </summary>
            /// <param name="content"></param>
            internal Node(I content)
            {
                Content = content;
                Next = this;
                Previous = this;
            }
        }
    }
}