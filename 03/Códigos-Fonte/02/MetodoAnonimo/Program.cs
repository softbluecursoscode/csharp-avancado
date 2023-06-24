using System;
using System.Threading;

class Program
{
    static void Main()
    {
        NumberGenerator g = new NumberGenerator();
        g.OnGenerated += delegate(object sender, NumberEventArgs args)
        {
            Console.WriteLine("Número gerado: " + args.Number);
        };
        g.Start();
    }
}

public delegate void NumberHandler(object sender, NumberEventArgs args);

class NumberGenerator
{
    public event NumberHandler OnGenerated;

    Random r = new Random();

    public void Start()
    {
        while (true)
        {
            int n = r.Next(100);

            if (OnGenerated != null)
            {
                NumberEventArgs args = new NumberEventArgs() { Number = n };
                OnGenerated(this, args);
            }

            Thread.Sleep(1000);
        }
    }
}

public class NumberEventArgs : EventArgs
{
    public int Number { get; set; }
}