using System;
using System.Threading;

class Program
{
    static void Main()
    {
        //Mutex mutex = new Mutex(false, "AppMutex");

        //bool acquired = mutex.WaitOne(1000);

        //if (!acquired)
        //{
        //    Console.WriteLine("Aplicação já está executando");
        //    return;
        //}

        //Console.WriteLine("Aplicação executando");
        //Console.ReadLine();

        //mutex.ReleaseMutex();

        Printers printers = new Printers();

        for (int i = 0; i < 10; i++)
        {
            Thread t = new Thread(() => Run(printers));
            t.Name = "Thread #" + i;
            t.Start();
        }
    }

    static void Run(Printers printers)
    {
        while (true)
        {
            printers.Print(Thread.CurrentThread.Name);
            Thread.Sleep(500);
        }
    }
}

class Printers
{
    private const int Count = 3;

    private bool[] usedPrinters = new bool[Count];
    private Random random = new Random();
    private SemaphoreSlim semaphore = new SemaphoreSlim(Count);

    public void Print(string name)
    {
        semaphore.Wait();
        try
        {
            int printer = GetPrinter();
            Console.WriteLine("Impressora {0} iniciando impressão para {1}", printer, name);
            Thread.Sleep(random.Next(3000));
            Console.WriteLine("Impressora {0} finalizou impressão para {1}", printer, name);
            SetPrinterFree(printer);
        }
        finally
        {
            semaphore.Release();
        }
    }

    int GetPrinter()
    {
        for (int i = 0; i < usedPrinters.Length; i++)
        {
            if (!usedPrinters[i])
            {
                usedPrinters[i] = true;
                return i;
            }
        }

        throw new Exception("Não pode chegar aqui");
    }

    void SetPrinterFree(int printer)
    {
        usedPrinters[printer] = false;
    }
}