using System;
using System.Collections.Generic;
using System.Threading;

namespace Softblue
{
    class TaskQueue
    {
        // Fila de armazenamento das tarefas.
        Queue<Action> queue = new Queue<Action>();
        
        // Thread da fila.
        Thread thread;

        // AutoResetEvent para bloquear a fila enquanto não houver tarefa.
        AutoResetEvent are = new AutoResetEvent(false);

        public TaskQueue()
        {
            // Inicia a thread.
            thread = new Thread(Run);
            thread.Start();
        }

        // Enfileira uma tarefa.
        public void Enqueue(Action task)
        {
            // O lock é necessário porque a fila (queue) é acessada por mais de uma thread.
            lock (queue)
            {
                // Enfileira a tarefa.
                queue.Enqueue(task);
            }

            // Avisa que existe uma nova tarefa enfileirada.
            are.Set();
        }

        // Método executado pela thread.
        void Run()
        {
            while (true)
            {
                Action task = null;
                lock (queue)
                {
                    // Verifica se existe alguma tarefa pendente.
                    if (queue.Count > 0)
                    {
                        // Se existir, desenfileira.
                        task = queue.Dequeue();

                        // Se a task for null, significa que a fila deve terminar.
                        if (task == null)
                        {
                            break;
                        }
                    }
                }

                if (task != null)
                {
                    // Se uma tarefa foi encontrada, processa.
                    task();

                    // Aguarda um pouco.
                    Thread.Sleep(500);
                }
                else
                {
                    // Se uma tarefa não foi processada, bloqueia a fila e aguarda até que uma tarefa esteja disponível.
                    are.WaitOne();
                }
            }
        }

        // Finaliza a fila de tarefas.
        public void Close()
        {
            // Enfileira a tarefa null, para que a thread da fila pare de executar.
            Enqueue(null);
        }
    }
}
