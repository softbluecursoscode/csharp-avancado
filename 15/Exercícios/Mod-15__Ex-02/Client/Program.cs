using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Softblue
{
    class Program
    {
        static void Main()
        {
            Client client = new Client("localhost", 3000);
            client.Start();
        }
    }

    class Client
    {
        string host;
        int port;
        UdpClient udpClient;

        public Client(string host, int port)
        {
            this.host = host;
            this.port = port;
        }

        public void Start()
        {
            // Solicita o nome ao usuário.
            Console.Write("Qual o seu nome? ");
            string name = Console.ReadLine();

            // Clia o cliente UDP.
            udpClient = new UdpClient();

            // Converte o nome de usuário para um array de bytes.
            byte[] bytes = Encoding.ASCII.GetBytes(name);

            // Envia o nome para o servidor
            udpClient.Send(bytes, bytes.Length, host, port);

            // Fica em um loop aguardando mensagens do servidor.
            while (true)
            {
                IPEndPoint endpoint = new IPEndPoint(0, 0);

                // Recebe uma mensagem do servidor e converte para string.
                bytes = udpClient.Receive(ref endpoint);
                string message = Encoding.ASCII.GetString(bytes);

                // Mostra na tela.
                Console.WriteLine("Mensagem do servidor: " + message);
            }
        }
    }
}
