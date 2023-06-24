using System;
using System.Collections.Generic;
using System.Threading;

namespace Softblue
{
    class Program
    {
        static void Main()
        {
            // Número de threads.
            int numThreads = 5;

            // Lista onde os números serão inseridos.
            List<int> lista = new List<int>();

            // CountdownEvent para controlar o término das threads.
            CountdownEvent countdown = new CountdownEvent(numThreads);

            // Inicia as threads.
            for (int i = 0; i < numThreads; i++)
            {
                new Thread(() => Inserir(lista, countdown)).Start();
            }

            // Aguarda as threads terminarem de executar.
            countdown.Wait();

            // Mostra os números da lista.
            foreach (int numero in lista)
            {
                Console.WriteLine(numero);
            }
        }

        // Método executado por cada uma das threads.
        static void Inserir(List<int> lista, CountdownEvent countdown)
        {
            // Gerador de números randômicos.
            Random r = new Random();

            for (int i = 0; i < 10; i++)
            {
                // Usa o lock da lista para sincronizar. Assim garante que apenas uma thread vai inserir na lista por vez.
                // Não é preciso sincronizar mais do que isso.
                lock (lista)
                {
                    // Adiciona um número randômico entre 0 e 100 na lista.
                    lista.Add(r.Next(101));
                }
            }

            // Avisa o CountdownEvent que a thread terminou.
            countdown.Signal();
        }
    }
}
