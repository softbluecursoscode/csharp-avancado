using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Server
{
    class ChatServer
    {
        // Eventos de conectado e desconectado.
        public event EventHandler Connected;
        public event EventHandler Disconnected;

        readonly object syncRunning = new object();
        bool running;

        // Servidor de socket.
        TcpListener tcpListener;

        // Lista de clientes conectados no chat.
        public List<Member> Members { get; set; }

        public ChatServer()
        {
            Members = new List<Member>();
        }

        // Conecta na porta especificada.
        public void Connect(int port)
        {
            // A conexão é feita usando uma thread a parte.
            Task.Factory.StartNew(() => HandleConnection(port));
        }

        private void HandleConnection(int port)
        {
            // Abre o servidor do scoket.
            tcpListener = new TcpListener(IPAddress.Loopback, port);
            tcpListener.Start();

            lock (syncRunning)
            {
                running = true;
            }

            if (Connected != null)
            {
                // Dispara o evento dizendo que o servidor foi conectado.
                Connected(this, EventArgs.Empty);
            }

            while (true)
            {
                if (tcpListener.Pending())
                {
                    // Se houver um pedido de conexão pendente, conecta e cria um objeto Member para responder a requisição.
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    Member member = new Member(this, tcpClient);
                    member.HandleMemberInteraction();
                }
                else
                {
                    // Se não houver um pedido de conexão pendente, verifica se é para terminar o loop e aguarda 1s.
                    lock (syncRunning)
                    {
                        if (!running)
                        {
                            break;
                        }
                    }

                    Thread.Sleep(1000);
                }
            }

            // O loop terminou, então para o servidor do socket.
            tcpListener.Stop();

            if (Disconnected != null)
            {
                // Dispara o evento informando que o servidor foi desconectado.
                Disconnected(this, EventArgs.Empty);
            }
        }

        // Desconecta o servidor.
        public void Disconnect()
        {
            // Envia um comando para os membros informando que o servidor vai se desconectar.
            lock (Members)
            {
                foreach (Member member in Members)
                {
                    member.SendServerDisconnectingCommand();
                }

            }

            // Fica em um loop aguardando até que todos os clientes se desconectem do servidor.
            while (true)
            {
                lock (Members)
                {
                    if (Members.Count == 0)
                    {
                        break;
                    }
                }
                Thread.Sleep(500);
            }

            // Agora que não há mais membros, o servidor pode ser encerrado.
            lock (syncRunning)
            {
                running = false;
            }
        }
    }
}
