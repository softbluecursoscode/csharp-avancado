using System;
using System.Threading;

namespace Softblue
{
    class Program
    {
        static void Main()
        {
            // Cria a fila de tarefas.
            TaskQueue tq = new TaskQueue();

            // Enfileira 100 tarefas.
            for (int i = 0; i < 100; i++)
            {
                int j = i;
                Console.WriteLine("Enfileirando tarefa: " + j);
                tq.Enqueue(() => Console.WriteLine("Tarefa: " + j));
                Thread.Sleep(300);
            }

            // Finaliza a fila.
            tq.Close();

            Console.WriteLine("Aguardando o processamento das tarefas pendentes");
        }
    }
}
