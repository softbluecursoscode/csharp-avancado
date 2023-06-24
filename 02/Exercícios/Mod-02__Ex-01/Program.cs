using System.Collections.Generic;
namespace Softblue
{
    class Program
    {
        static void Main()
        {
            // Lista com elementos de 0 a 10.
            List<int> l = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Retorna apenas os números maiores que 5.
            List<int> l1 = FilterList(l, FilterGreaterThan5);
            Print(l1);

            // Retorna apenas o números ímpares.
            List<int> l2 = FilterList(l, FilterOdd);
            Print(l2);
        }

        static bool FilterGreaterThan5(int n)
        {
            return n > 5;
        }

        static bool FilterOdd(int n)
        {
            return n % 2 != 0;
        }
        
        // Imprime na tela os elementos da lista.
        static void Print(List<int> list)
        {
            foreach (int n in list)
            {
                System.Console.Write(n + " ");
            }
            System.Console.WriteLine();
        }

        // Filtra os elementos da lista com base no delegate.
        static List<int> FilterList(List<int> list, Filter filter)
        {
            if (list == null)
            {
                return null;
            }

            // Cria uma nova lista.
            List<int> newList = new List<int>();

            // Itera sobre os elementos da lista.
            foreach (int n in list)
            {
                if (filter(n))
                {
                    // Se a chamada ao delegate no elemento n retornar true, insere na lista o elemento.
                    newList.Add(n);
                }
            }

            // Retorna a lista filtrada.
            return newList;
        }
    }

    // Delegate para filtragem de elementos.
    delegate bool Filter(int n);
}
