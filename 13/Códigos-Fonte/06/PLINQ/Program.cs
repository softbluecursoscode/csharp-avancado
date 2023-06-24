using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        IEnumerable<int> numbers = Enumerable.Range(0, 100);

        var q = from n in numbers.AsParallel().AsOrdered()
                where n >= 50 && n <= 70
                select n;

        foreach (var n in q)
        {
            Console.WriteLine(n);
        }
    }
}
