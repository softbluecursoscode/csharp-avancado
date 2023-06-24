using System;
using AutoRun;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main()
    {
        List<Result> results = AutoRunner.Run();

        foreach (Result result in results)
        {
            Console.WriteLine(result);
        }
    }
}

[RunClass]
class A
{
    [RunMethod]
    public static void Execute()
    {
        Console.WriteLine("A.Execute()");
        Thread.Sleep(2000);
    }
}

[RunClass]
class B
{
    [RunMethod]
    public static void Start()
    {
        Console.WriteLine("B.Start()");
        Thread.Sleep(1000);
    }
}