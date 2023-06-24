using System;
using System.Timers;


class Program
{
    static void Main()
    {
        //Timer timer = new Timer(OnTimer, null, 3000, 2000);
        //Timer timer = new Timer(OnTimer, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(1));
        //Console.ReadLine();
        //timer.Dispose();

        Timer timer = new Timer();
        timer.Interval = 2000;
        timer.Elapsed += OnTimer;

        timer.Start();
        Console.ReadLine();
    }

    static void OnTimer(object param)
    {
        Console.WriteLine("Timer disparado!");
    }

    static void OnTimer(object sender, ElapsedEventArgs args)
    {
        Console.WriteLine("Timer disparado");
    }
}
