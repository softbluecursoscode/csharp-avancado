using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Thread[] threads = new Thread[3];

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(Run);
            threads[i].Name = "Thread " + i;
            threads[i].Start();
        }
    }

    static void Run()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("{0} => A", Thread.CurrentThread.Name);
            Console.WriteLine("{0} => B", Thread.CurrentThread.Name);
            Console.WriteLine("{0} => C", Thread.CurrentThread.Name);
            Console.WriteLine("{0} => D", Thread.CurrentThread.Name);
            Console.WriteLine("{0} => E", Thread.CurrentThread.Name);
            Console.WriteLine("{0} => F", Thread.CurrentThread.Name);
        }
    }
}
