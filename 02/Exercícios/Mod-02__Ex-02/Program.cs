using System;
using System.Threading;

namespace Softblue
{
    class Program
    {
        static void Main()
        {
            // Cria o cronômetro.
            Clock c = new Clock();

            // Registra o método OnSecond para ser chamado a cada segundo.
            c.RegisterCallback(OnSecond);
            
            // Inicia a contagem.
            c.Start();
        }

        // Método referenciado pelo delegate.
        static void OnSecond(long secs)
        {
            Console.WriteLine(secs);
        }
    }

    class Clock
    {
        // Armazena os callbacks.
        private SecondsHandler callbacks;

        // Inicia a contagem.
        public void Start()
        {
            int count = 0;
            while (true)
            {
                // Aguarda 1s antes de continuar.
                Thread.Sleep(1000);
                
                // Incrementa o contador de segundos.
                count++;

                if (callbacks != null)
                {
                    // Chama os callbacks registrados.
                    callbacks(count);
                }
            }
        }

        // Permite que um callback seja registrado.
        public void RegisterCallback(SecondsHandler handler)
        {
            callbacks += handler;
        }
    }

    // Delegate utilizado na invocação a cada segundo.
    delegate void SecondsHandler(long secs);
}
