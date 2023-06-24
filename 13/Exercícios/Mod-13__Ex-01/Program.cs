using System;
using System.IO;
using System.Threading;

namespace Softblue
{
    class Program
    {
        // Objeto para fornecer um lock
        static readonly object sync = new object();

        // Field para controlar se a contagem ainda está sendo feita.
        static bool fazendoContagem;

        // Property que gerencia o valor de 'fazendoContagem'.
        // O acesso ao field é sincronizado, pois 2 threads podem acessá-lo simultaneamente.
        static bool FazendoContagem
        {
            get
            {
                lock (sync)
                {
                    return fazendoContagem;
                }
            }
            set
            {
                lock (sync)
                {
                    fazendoContagem = value;
                }
            }
        }

        static void Main()
        {
            // Solicita o diretório.
            Console.Write("Diretório: ");
            string dir = Console.ReadLine();

            // Cria um DirectoryInfo e verifica se o diretório existe.
            DirectoryInfo dirInfo = new DirectoryInfo(dir);

            if (!dirInfo.Exists)
            {
                Console.WriteLine("O diretório {0} não existe.", dir);
                return;
            }

            // Cria o delegate.
            ContarElementosHandler handler = Contador.ContarArquivosEDiretorios;

            // Indica que a contagem começou.
            FazendoContagem = true;

            // Inicia a chamada assíncrona ao delegate.
            handler.BeginInvoke(dirInfo, ContagemTerminou, handler);

            // Enquanto a contagem está ativa, mostra a mensagem a cada 1s.
            while (FazendoContagem)
            {
                Console.WriteLine("Contando...");
                Thread.Sleep(1000);
            }
        }     

        // Método de callback, chamado ao final da contagem.
        static void ContagemTerminou(IAsyncResult ar)
        {
            // Obtém o resultado da contagem.
            ContarElementosHandler handler = (ContarElementosHandler)ar.AsyncState;
            Resultado resultado = handler.EndInvoke(ar);
            
            // Indica que a contagem terminou. Isto vai fazer com que a thread principal para de executar na próxima iteração do loop.
            FazendoContagem = false;
            
            // Mostra o resultado na tela.
            Console.WriteLine("Quantidade de arquivos: {0}\nQuantidade de diretórios: {1}", resultado.Arquivos, resultado.Diretorios);
        }
    }
    
    // Delegate usado na contagem.
    delegate Resultado ContarElementosHandler(DirectoryInfo dirInfo);
}