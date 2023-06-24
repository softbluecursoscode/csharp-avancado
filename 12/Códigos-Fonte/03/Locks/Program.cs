using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Counter counter = new Counter();

        Thread[] threads = new Thread[10];

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(() => Run(counter));
        }

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i].Start();
        }

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i].Join();
        }

        Console.WriteLine("Contador: " + counter.Value);
    }

    static void Run(Counter counter)
    {
        counter.Increment();
    }
}

class Counter
{
    private readonly object sync = new object();

    public int Value { get; private set; }

    public void Increment()
    {
        //lock (sync)
        //{
        //    Value++;
        //}

        bool lockTaken = false;
        try
        {
            Monitor.Enter(sync, ref lockTaken);
            Value++;
        }
        finally
        {
            if (lockTaken)
            {
                Monitor.Exit(sync);
            }
        }
    }
}