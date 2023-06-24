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

            // Registra interesse no evento.
            c.OnSecondElapsed += OnSecond;

            // Inicia a contagem.
            c.Start();
        }

        // Método chamado no disparo do evento.
        static void OnSecond(object sender, SecondElapsedEventArgs args)
        {
            Console.WriteLine(args.Seconds);
        }
    }

    class Clock
    {
        // Armazena os callbacks.
        public event SecondsHandler OnSecondElapsed;

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

                if (OnSecondElapsed != null)
                {
                    // Chama os callbacks registrados.
                    OnSecondElapsed(this, new SecondElapsedEventArgs(count));
                }
            }
        }
    }

    // Classe com dados do evento.
    public class SecondElapsedEventArgs : EventArgs
    {
        public long Seconds { get; set; }

        public SecondElapsedEventArgs(int seconds)
        {
            this.Seconds = seconds;
        }
    }

    // Delegate utilizado na invocação a cada segundo.
    delegate void SecondsHandler(object sender, SecondElapsedEventArgs args);
}
