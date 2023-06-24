using System;
using System.Collections.Generic;

namespace Softblue
{
    class Program
    {
        static void Main()
        {
            // Lista de números.
            List<int> list = new List<int>();
            list.AddRange(new int[] { 2, 5, 3, 0, 7, 8, 1, 6 });

            // Realiza a contagem dos elementos maiores ou iguais a 5.
            int count = Count(list, e => e >= 5);
            
            Console.WriteLine(count);
        }

        static int Count(List<int> list, Predicate<int> predicate)
        {
            // Contador de elementos.
            int count = 0;

            // Para cada elemento da lista, chama o delegate. Se ele retornar true, incrementa o contador.
            list.ForEach(e =>
                {
                    if (predicate(e))
                    {
                        count++;
                    }
                });

            // Retorna a quantidade de elementos que atendem a condição.
            return count;
        }
    }
}
