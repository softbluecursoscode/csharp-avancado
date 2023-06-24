using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.IO;

namespace Softblue
{
    class Program
    {
        static void Main()
        {
            Server s = new Server(3500);
            s.Start();
        }
    }

    class Server
    {
        TcpListener listener;
        int port;
        List<ConnectedClient> clients = new List<ConnectedClient>();

        public Server(int port)
        {
            this.port = port;
        }

        public void Start()
        {
            // Inicia o servidor na porta especificada.
            listener = new TcpListener(IPAddress.Loopback, port);
            listener.Start();

            // Cria uma nova thread para gerenciar as conexões de clientes.
            new Thread(ConnectionHandler).Start();

            while (true)
            {
                // Fica em um loop enviando mensagens para os clientes conectados.
                Console.Write("Mensagem para enviar: ");
                string msg = Console.ReadLine();
                if (!string.IsNullOrEmpty(msg))
                {
                    SendMsgToClients(msg);
                }
            }
        }

        // Método chamado por uma nova thread.
        private void ConnectionHandler()
        {
            while (true)
            {
                // Aguarda a conexão de um cliente.
                TcpClient client = listener.AcceptTcpClient();

                // Cria o cliente conectado e coloca na lista.
                ConnectedClient connectedClient = new ConnectedClient(client);
                
                // O lock é necessário porque a lista de clientes está sendo manipulada por 2 threads.
                lock (clients)
                {
                    clients.Add(connectedClient);
                    Console.WriteLine("Cliente " + connectedClient.Name + " conectado");
                }
            }
        }

        // Envia uma mensagem para os clientes conectados.
        private void SendMsgToClients(string msg)
        {
            // O lock é necessário porque a lista de clientes está sendo manipulada por 2 threads.
            lock (clients)
            {
                // Usa o StreamWriter de cada cliente para enviar uma mensagem.
                foreach (ConnectedClient client in clients)
                {
                    client.Out.WriteLine(msg);
                }
            }
        }
    }

    // Representa um cliente conectado.
    class ConnectedClient
    {
        public TcpClient TcpClient { get; private set; }
        public string Name { get; private set; }
        public StreamReader In { get; private set; }
        public StreamWriter Out { get; private set; }

        public ConnectedClient(TcpClient tcpClient)
        {
            TcpClient = tcpClient;

            // Cria o StreamReader e StreamWriter.
            In = new StreamReader(TcpClient.GetStream());
            Out = new StreamWriter(TcpClient.GetStream());
            Out.AutoFlush = true;

            // Lê o nome enviado pelo cliente.
            Name = In.ReadLine();
        }
    }
}
