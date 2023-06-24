using System;
using System.Linq;

class Program
{
    static void Main()
    {
        //var c = Enumerable.Range(1, 10);
        //var c = Enumerable.Empty<double>();
        var c = Enumerable.Repeat("B", 10);

        foreach (var e in c)
        {
            Console.WriteLine(e);
        }
    }
}
