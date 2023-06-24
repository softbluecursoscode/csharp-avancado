using System;
using System.Collections.Generic;
using System.IO;
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
            Server server = new Server(3000);
            server.Start();
        }
    }

    class Server
    {
        int port;
        List<KnownClient> clients = new List<KnownClient>();
        UdpClient udpServer;

        public Server(int port)
        {
            this.port = port;
        }

        public void Start()
        {
            // Inicia o servidor UDP na porta especificada.
            udpServer = new UdpClient(port);


            // Cria uma thread para aguardar conexões de clientes.
            new Thread(HandleClients).Start();

            // Fica em um loop enviando mensagens.
            while (true)
            {
                // Lê a mensagem do teclado.
                string message = Console.ReadLine();

                // O lock é necessário porque a lista de clientes está sendo manipulada por 2 threads.
                lock (clients)
                {
                    // Manda a mensagem para cada cliente da lista.
                    foreach (KnownClient client in clients)
                    {
                        client.SendMessage(message, udpServer);
                    }
                }
            }
        }

        public void HandleClients()
        {
            while (true)
            {
                // Recebe um novo cliente.
                IPEndPoint endpoint = new IPEndPoint(0, 0);
                
                // Lê o nome do cliente.
                byte[] bytes = udpServer.Receive(ref endpoint);
                string name = Encoding.ASCII.GetString(bytes);

                // Cria um objeto para representar este cliente.
                KnownClient client = new KnownClient(name, endpoint);

                // O lock é necessário porque a lista de clientes está sendo manipulada por 2 threads.
                lock (clients)
                {
                    // Adiciona o cliente na lista.
                    clients.Add(client);
                }

                Console.WriteLine("Cliente " + name + " entrou em contato");
            }
        }
    }

    class KnownClient
    {
        public string Name { get; private set; }
        public IPEndPoint EndPoint { get; private set; }

        public KnownClient(string name, IPEndPoint endpoint)
        {
            Name = name;
            EndPoint = endpoint;
        }

        // Envia uma mensagem para este cliente.
        public void SendMessage(string message, UdpClient udpServer)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            udpServer.Send(bytes, bytes.Length, EndPoint);
        }
    }
}
