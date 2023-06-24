using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        //Task t = new Task(MostrarMensagem);
        //t.Start();

        //Task t = Task.Factory.StartNew(MostrarMensagem);
        //t.Wait();

        Task<long> t = Task.Factory.StartNew(() => CalcularFatorial(20));

        try
        {
            long fatorial = t.Result;
            Console.WriteLine(fatorial);
        }
        catch (AggregateException e)
        {
            Exception e1 = e.InnerException;
            Console.WriteLine(e1.Message);
            Console.WriteLine(e1.GetType());
        }

    }

    static void MostrarMensagem()
    {
        Thread.Sleep(2000);
        Console.WriteLine("Aprendendo a usar tasks!");
    }

    static long CalcularFatorial(int n)
    {
        checked
        {
            if (n == 0)
            {
                return 1;
            }
            return n * CalcularFatorial(n - 1);
        }
    }
}
