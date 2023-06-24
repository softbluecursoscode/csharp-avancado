using System;
using System.IO;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main()
        {
            Client c = new Client("localhost", 3500);

            // Solicita o nome do cliente.
            Console.Write("Qual o seu nome? ");
            string name = Console.ReadLine();
            c.Start(name);
        }
    }

    class Client
    {
        string host;
        int port;

        public Client(string host, int port)
        {
            this.host = host;
            this.port = port;
        }

        public void Start(string name)
        {
            // Inicia o cliente no host e porta especificados.
            TcpClient tcpClient = new TcpClient(host, port);

            // Cria as streams para leitura e escrita de dados.
            StreamReader reader = new StreamReader(tcpClient.GetStream());
            StreamWriter writer = new StreamWriter(tcpClient.GetStream());
            writer.AutoFlush = true;

            // Escreve o nome do cliente no canal.
            writer.WriteLine(name);

            while (true)
            {
                // Fica em um loop lendo e mostrando mensagens enviadas pelo servidor.
                string msg = reader.ReadLine();
                Console.WriteLine("Mensagem do servidor: " + msg);
            }
        }
    }
}
