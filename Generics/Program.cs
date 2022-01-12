using System;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue1 queue1 = new Queue1();
            queue1.Enqueue(4);
            queue1.Enqueue(2);
            queue1.Enqueue(3);

            Stack1 stack1 = new Stack1();
            stack1.Push(1);
            stack1.Push(2);
            stack1.Push(3);
            
            stack1.Print();
            queue1.Print();
            Console.WriteLine("Hello World!");
        }
    }
}