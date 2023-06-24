using System;

namespace Chat.Common
{
    // Comandos possíveis de serem enviados pelo socket.
    public enum ChatCommandType
    {
        EnterRoom,                  // Cliente quer entrar na sala.
        EnterRoomResponse,          // Resposta do servidor, informando se o cliente pode entrar.
        GetMembers,                 // Obter lista de membros conectados.
        GetMembersResponse,         // Resposta com a lista de membros conectados.
        SendMessage,                // Enviar uma mensagem para todos no chat.
        MessageReceived,            // O servidor recebeu um pedido de envio de mensagem de um membro.
        MemberEntered,              // Um novo membro entrou no chat.
        MemberLeft,                 // Um membro saiu do chat.
        MemberLeaving,              // Um membro está informando que deseja sair.
        MemberCanLeave,             // Resposta do servidor indicando que o membro pode sair.
        ServerDisconnecting         // O servidor está em processo de desconexão.
    }

    // Representa um comando específico.
    public class ChatCommand
    {
        // Tipo de comando.
        public ChatCommandType Type { get; set; }

        // Parâmetro do comando.
        public string Param { get; set; }

        public ChatCommand(ChatCommandType type, string param)
        {
            this.Type = type;
            this.Param = param;
        }

        // Cria um comando a partir de uma string (o formato é "<tipo>|<parâmetro>")
        public static ChatCommand Parse(string commandStr)
        {
            int delimiterPos = commandStr.IndexOf('|');
            string typeStr = commandStr.Substring(0, delimiterPos);
            ChatCommandType type = (ChatCommandType)Enum.Parse(typeof(ChatCommandType), typeStr);
            string param = commandStr.Substring(delimiterPos + 1);

            return new ChatCommand(type, param);
        }

        // Converte o comando para um formato de string.
        public override string ToString()
        {
            return Type + "|" + Param;
        }
    }
}
