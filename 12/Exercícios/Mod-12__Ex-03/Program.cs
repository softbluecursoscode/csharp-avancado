using System;
using System.Threading;

namespace Softblue
{
    class Program
    {
        static void Main()
        {
            // Define o número de leitores e escritores.
            int numLeitores = 10;
            int numEscritores = 5;

            // Cria um semáforo para controlar o acesso de escrita.
            SemaphoreSlim mutexEscrita = new SemaphoreSlim(1);

            // Cria os leitores e escritores.
            Leitor[] leitores = new Leitor[numLeitores];
            Escritor[] escritores = new Escritor[numEscritores];

            // Inicia os leitores.
            for (int i = 0; i < leitores.Length; i++)
            {
                leitores[i] = new Leitor(i + 1, mutexEscrita);
            }

            // Inicia os escritores.
            for (int i = 0; i < escritores.Length; i++)
            {
                escritores[i] = new Escritor(i + 1, mutexEscrita);
            }
        }
    }

    // Representa o leitor.
    class Leitor
    {
        // Id do leitor.
        int id;

        // Semáforo para controle de escrita.
        SemaphoreSlim mutexEscrita;

        // Gerador de números randômicos.
        static Random random = new Random();

        // Contador de leitores ativos.
        static int leitoresAtivos;

        // Objeto que vai fornecer o lock para acesso exclusivo ao contador.
        // É definido como estático para ser compartilhado entre os objetos.
        static object syncContador = new object();

        public Leitor(int id, SemaphoreSlim mutexEscrita)
        {
            this.id = id;
            this.mutexEscrita = mutexEscrita;

            // Inicia o leitor.
            new Thread(Iniciar).Start();
        }

        private void Iniciar()
        {
            // Define o nome da thread.
            Thread.CurrentThread.Name = "Leitor " + id;

            while(true)
            {
                // Usa um lock para evitar acesso concorrente ao contador.
                lock(syncContador)
                {
                    // Incrementa o número de leitores ativos.
                    leitoresAtivos++;

                    // Se for o primeiro leitor ativo, trava o semáforo de escrita, impossibilitando escritores de escreverem.
                    if (leitoresAtivos == 1)
                    {
                        mutexEscrita.Wait();
                    }
                }
                
                // Lê os dados.
                Ler();

                // Usa um lock para evitar acesso concorrente ao contador.
                lock(syncContador)
                {
                    // Decrementa o número de leitores ativos.
                    leitoresAtivos--;

                    // Se for o último leitor ativo, destrava o semáforo de escrita, permitiando que escritores escrevam.
                    if (leitoresAtivos == 0)
                    {
                        mutexEscrita.Release();
                    }
                }

                // Usa os dados lidos. Aqui não é necessário sincronismo.
                UsarDados();
            }
        }

        // Simula uma leitura de dados.
        private void Ler()
        {
            Console.WriteLine("Leitor {0} lendo", id);
            Thread.Sleep(random.Next(2000));
        }

        // Simula o uso de dados.
        private void UsarDados()
        {
            Console.WriteLine("Leitor {0} usando os dados lidos", id);
            Thread.Sleep(random.Next(2000));
        }
    }

    // Representa o escritor.
    class Escritor
    {
        // Id do escritor.
        int id;

        // Semáforo para controle de escrita.
        SemaphoreSlim mutexEscrita;

        // Gerador de números randômicos.
        static Random random = new Random();

        public Escritor(int id, SemaphoreSlim mutexEscrita)
        {
            this.id = id;
            this.mutexEscrita = mutexEscrita;
            
            // Inicia o escritor.
            new Thread(Iniciar).Start();
        }

        private void Iniciar()
        {
            // Dá um nome à thread.
            Thread.CurrentThread.Name = "Escritor " + id;

            while (true)
            {
                // Decide o que vai ser escrito. Não precisa haver sincronismo aqui.
                Decidir();

                try
                {
                    // Obtém o lock exclusivo para escrita. Se não puder escrever, vai ficar bloqueado aqui.
                    mutexEscrita.Wait();

                    // Escreve.
                    Escrever();
                }
                finally
                {
                    // Libera o acesso exclusivo de escrita.
                    mutexEscrita.Release();
                }
            }
        }

        // Simula o processo de decisão sobre o que escrever.
        private void Decidir()
        {
            Console.WriteLine("Escritor {0} decidindo o que escrever", id);
            Thread.Sleep(random.Next(2000));
        }

        // Simula a escrita.
        private void Escrever()
        {
            Console.WriteLine("Escritor {0} escrevendo", id);
            Thread.Sleep(random.Next(2000));
        }
    }
}
