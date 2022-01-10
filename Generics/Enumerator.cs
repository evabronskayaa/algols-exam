using System.Collections;
using System.Collections.Generic;

namespace Generics
{
    class UTwoLinkedListEnumerator<T> : IEnumerator<T>
    {
        private UTwoLinkedList<T> body;
        private UTwoLinkedList<T>.Node<T> currentElement;

        public T Current => currentElement.Content;

        object IEnumerator.Current => currentElement.Content;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            currentElement = currentElement.Next;
            return currentElement != body.endpoint;
        }

        public void Reset() => currentElement = body.endpoint;

        public UTwoLinkedListEnumerator(UTwoLinkedList<T> body)
        {
            this.body = body;
            currentElement = body.endpoint;
        }
    }
}