using System;

namespace Generics
{
    public class List1
    {
        public int Count { get; private set; } = 0;
        private Node _first;
        private Node _last;

        public void AddLast(Object value)
        {
            Node newEl = new Node(_last, null, value);
            if (_last != null)
                _last.Next = newEl;
            _last = newEl;
            if (_first == null)
                _first = newEl;

            Count++;
        }
        public void AddFirst(Object value)
        {
            Node newEl = new Node(null, _first, value);
            if (_first != null)
                _first.Previous = newEl;
            _first = newEl;
            if (_last == null)
                _last = newEl;

            Count++;
        }

        public void Remove(object value)
        {
            Node node = _first;
            while (node != null)
            {
                if (node.Value.Equals(value))
                {
                    DeleteNode(node);
                    return;
                }
                node = node.Next;
            }
        }
        public void RemoveAt(int index)
        {
            Node node = _first;
            for (int i = 0; i < Count; i++)
            {
                if (i == index)
                {
                    DeleteNode(node);
                    return;
                }
                node = node.Next;
            }
        }
        public void RemoveFirst()
        {
            DeleteNode(_first);
        }
        public void RemoveLast()
        {
            DeleteNode(_last);
        }
        private void DeleteNode(Node node)
        {
            if (_first == _last)
            {
                _first = null;
                _last = null;
            }
            else if (node == _first)
            {
                _first = node.Next;
                node.Next.Previous = null;
            }
            else if (node == _last)
            {
                _last = node.Previous;
                node.Previous.Next = null;
            }
            else
            {
                node.Next.Previous = node.Previous;
                node.Previous.Next = node.Next;
            }

            Count--;
        }

        private void InsertBefore(Node node, Object value)
        {
            if (node.Previous == null)
            {
                AddFirst(value);
            }
            else
            {
                Node newNode = new Node(node.Previous, node, value);
                newNode.Next.Previous = newNode;
                newNode.Previous.Next = newNode;
                Count++;
            }
        }

        public Object this[int index]
        {
            get
            {
                Node node = _first;
                for (int i = 0; i < Count; i++)
                {
                    if (i == index)
                        return node.Value;
                    else
                        node = node.Next;
                }
                return null;
            }
            set
            {
                Node node = _first;
                for (int i = 0; i < Count; i++)
                {
                    if (i == index)
                        node.Value = value;
                    else
                        node = node.Next;
                }
            }
        }

        public Object GetFirst()
        {
            return _first != null ? _first.Value : null;
        }
        public Object GetLast()
        {
            return _last != null ? _last.Value : null;
        }

        public void PrintToConsole()
        {
            Node node = _first;
            Console.Write("[");
            for (int i = 0; i < Count; i++)
            {
                Console.Write(node.Value + ",");
                node = node.Next;
            }
            if (Count > 0)
                Console.Write("\b]");
            else
                Console.Write("]");
            Console.WriteLine();
        }

        public bool Contains(Object value)
        {
            Node curNode = _first;
            while(curNode != null)
            {
                if (curNode.Value.Equals(value))
                    return true;

                curNode = curNode.Next;
            }
            return false;
        }
        
    }
    
    class Node
    {
        public Node Next { get; set; }
        public Node Previous { get; set; }
        public Object Value { get; set; }
        public Node(Node previous, Node next, Object value)
        {
            Previous = previous;
            Next = next;
            Value = value;
        }
    }
}