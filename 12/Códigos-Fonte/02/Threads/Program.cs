using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Thread.CurrentThread.Name = "Main()";

        Thread t1 = new Thread(Run);
        t1.Name = "Run() 1";
        t1.Start("p1");

        Thread t2 = new Thread(Run);
        t2.Name = "Run() 2";
        t2.Start("p2");

        t1.Join();
        t2.Join();

        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine(i + " => " + Thread.CurrentThread.Name);
            //Thread.Sleep(500);
        }
    }

    static void Run(object param)
    {
        string p = (string)param;

        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine(i + " => " + Thread.CurrentThread.Name + " => " + p);
            //Thread.Sleep(500);
        }
    }
}
