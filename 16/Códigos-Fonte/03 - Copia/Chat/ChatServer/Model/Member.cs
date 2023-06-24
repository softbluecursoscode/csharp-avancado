using System;
using System.Net.Sockets;
using Chat.Common;
using System.Collections.Generic;

namespace Chat.Server
{
    // Representa um cliente conectado no chat.
    class Member
    {
        TcpClient tcpClient;
        ChatServer chatServer;

        // Nome do membro
        string name;

        // Handler para gerenciar os comandos que chegam ao membro e são enviados ao cliente do chat.
        InputHandler inputHandler;
        OutputHandler outputHandler;

        public Member(ChatServer chatServer, TcpClient tcpClient)
        {
            this.chatServer = chatServer;
            this.tcpClient = tcpClient;

            inputHandler = new InputHandler(tcpClient.GetStream());
            outputHandler = new OutputHandler(tcpClient.GetStream());
        }

        public void HandleMemberInteraction()
        {
            // Registra os eventos necessários.
            inputHandler.EnterRoom += OnEnterRoom;
            inputHandler.GetMembers += OnGetMembers;
            inputHandler.SendMessage += OnSendMessage;
            inputHandler.MemberLeaving += OnMemberLeaving;

            // Inicia o input handler, que fica aguardando a chegada de comandos do cliente na stream.
            inputHandler.Start();
        }

        // Um cliente quer entrar na sala.
        void OnEnterRoom(object sender, MemberEventArgs e)
        {
            // Define o seu nome.
            name = e.Name;

            bool valid = true;
            string error = null;

            lock (chatServer.Members)
            {
                // Verifica se já não existe um membro com o mesmo nome.
                foreach (Member member in chatServer.Members)
                {
                    if (member != this && member.name == name)
                    {
                        valid = false;
                        error = "O nome " + name + " já existe no chat";
                        break;
                    }
                }
            }

            // Envia uma resposta ao cliente, dizendo se ele pode ou não entrar. Em caso negativo, envia o erro.
            outputHandler.SendEnterRoomResponseCommand(valid, error);

            if (valid)
            {
                // Se o cliente pode se conectar, avisa todos os membros existentes que um novo membro está entrando.
                lock (chatServer.Members)
                {
                    foreach (Member member in chatServer.Members)
                    {
                        member.outputHandler.SendMemberEnteredCommand(name);
                    }
                    
                    // Adiciona o cliente na lista de membros.
                    chatServer.Members.Add(this);
                }
            }
            else
            {
                // Se o cliente não pode se conectar, indica que a thread de leitura da stream deve ser interrompida.
                e.StopInputHandler = true;
            }
        }

        void OnGetMembers(object sender, BaseEventArgs e)
        {
            // Um novo membro solicita a lista de membros conectados.

            List<string> names = new List<string>();

            lock (chatServer.Members)
            {
                // Cria uma lista com os nomes dos membros.
                foreach (Member member in chatServer.Members)
                {
                    names.Add(member.name);
                }
            }

            // Envia a lista para o membro que pediu.
            outputHandler.SendGetMembersResponseCommand(names);
        }

        void OnSendMessage(object sender, MessageEventArgs e)
        {
            // Um membro está mandando uma mensagem.

            // Decora a mensagem com a hora atual e o nome do membro.
            string time = DateTime.Now.ToString("HH:mm:ss");
            string message = string.Format("[{0}] {1} - {2}", time, name, e.Message);

            lock (chatServer.Members)
            {
                // Envia a mensagem para todos os membros conectados.
                foreach (Member member in chatServer.Members)
                {
                    member.outputHandler.SendMessageReceivedCommand(message);
                }
            }
        }

        void OnMemberLeaving(object sender, MemberEventArgs e)
        {
            // Um membro está saindo do chat.

            lock (chatServer.Members)
            {
                // Remove o membro da lista de membros.
                chatServer.Members.Remove(this);

                // Avisa os outros membros a respeito da saída.
                foreach (Member member in chatServer.Members)
                {
                    member.outputHandler.SendMemberLeftCommand(name);
                }
            }

            // Avisa o membro que agora ele está autorizado a se desconectar.
            outputHandler.SendMemberCanLeaveCommand(name);
            
            // Indica que a thread de leitura da stream deve ser interrompida.
            e.StopInputHandler = true;
        }

        public void SendServerDisconnectingCommand()
        {
            // O servidor está sendo desconectado, então avisa o membro.
            outputHandler.SendServerDisconnectingCommand();
        }
    }
}
