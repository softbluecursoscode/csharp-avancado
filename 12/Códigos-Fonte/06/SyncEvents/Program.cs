using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Cancela cancela = new Cancela();

        for (int i = 0; i < 5; i++)
        {
            Thread t = new Thread(() => Run(cancela));
            t.Name = "Thread #" + i;
            t.Start();
        }

        new Thread(() => RunGuarda(cancela)).Start();
    }

    static void Run(Cancela cancela)
    {
        while (true)
        {
            cancela.Passar(Thread.CurrentThread.Name);
            Thread.Sleep(300);
        }
    }

    static void RunGuarda(Cancela cancela)
    {
        while (true)
        {
            cancela.Abrir();
            Thread.Sleep(3000);
            cancela.Fechar();
            Thread.Sleep(3000);
        }
    }
}

class Cancela
{
    //AutoResetEvent h = new AutoResetEvent(false);
    ManualResetEventSlim h = new ManualResetEventSlim(false);

    public void Abrir()
    {
        h.Set();
    }

    public void Fechar()
    {
        h.Reset();
    }

    public void Passar(string name)
    {
        //h.WaitOne();
        h.Wait();
        Console.WriteLine(name + " passou pela cancela");
    }
}