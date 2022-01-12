namespace Generics
{
    public class Stack1
    {
        private readonly List1 _list = new List1();
        public void Push(object temp)
        {
            _list.AddLast(temp);
        }

        public object Pop()
        {
            object temp = _list.GetLast();
            _list.RemoveLast();
            return temp;
        }

        public object Top()
        {
            return _list.GetLast();
        }

        public bool IsEmpty()
        {
            return _list.Count == 0;
        }

        public void Print()
        {
            _list.PrintToConsole();
        }
    }
}