using System;
using System.Collections.Generic;

namespace Softblue
{
    class Program
    {
        static void Main()
        {
            // Cria a lista original.
            List<double> valores = new List<double>() { 3, 7, 2, 4, 6 };
            
            // Gera uma nova lista, dividindo cada um dos elementos por 2.
            List<double> v = valores.ConvertAll(e => e / 2);
            
            // Imprime cada um dos elementos da nova lista.
            v.ForEach(e => Console.WriteLine(e + " "));
        }
    }
}
