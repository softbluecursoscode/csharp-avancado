using System;
using System.Collections.Generic;
using System.IO;

namespace Chat.Common
{
    // Envia comandos para a stream de saída.
    public class OutputHandler
    {
        // Stream de saída.
        StreamWriter output;

        public OutputHandler(Stream stream)
        {
            output = new StreamWriter(stream);
            output.AutoFlush = true;
        }

        public void SendEnterRoomCommand(string name)
        {
            ChatCommand command = new ChatCommand(ChatCommandType.EnterRoom, name);
            output.WriteLine(command);
        }

        public void SendEnterRoomResponseCommand(bool valid, string error)
        {
            string param;
            if (valid)
            {
                param = "OK";
            }
            else
            {
                param = error;
            }

            ChatCommand command = new ChatCommand(ChatCommandType.EnterRoomResponse, param);
            output.WriteLine(command);
        }

        public void SendGetMembersCommand()
        {
            ChatCommand command = new ChatCommand(ChatCommandType.GetMembers, null);
            output.WriteLine(command);
        }

        public void SendGetMembersResponseCommand(List<string> members)
        {
            string membersStr = "";
            bool first = true;

            foreach (string member in members)
            {
                if (!first)
                {
                    membersStr += ",";
                }
                membersStr += member;
                first = false;
            }

            ChatCommand command = new ChatCommand(ChatCommandType.GetMembersResponse, membersStr);
            output.WriteLine(command);
        }

        public void SendMessageCommand(string message)
        {
            ChatCommand command = new ChatCommand(ChatCommandType.SendMessage, message);
            output.WriteLine(command);
        }

        public void SendMessageReceivedCommand(string message)
        {
            ChatCommand command = new ChatCommand(ChatCommandType.MessageReceived, message);
            output.WriteLine(command);
        }

        public void SendMemberEnteredCommand(string name)
        {
            ChatCommand command = new ChatCommand(ChatCommandType.MemberEntered, name);
            output.WriteLine(command);
        }

        public void SendMemberLeftCommand(string name)
        {
            ChatCommand command = new ChatCommand(ChatCommandType.MemberLeft, name);
            output.WriteLine(command);
        }

        public void SendMemberLeavingCommand(string name)
        {
            ChatCommand command = new ChatCommand(ChatCommandType.MemberLeaving, name);
            output.WriteLine(command);
        }

        public void SendMemberCanLeaveCommand(string name)
        {
            ChatCommand command = new ChatCommand(ChatCommandType.MemberCanLeave, name);
            output.WriteLine(command);
        }

        public void SendServerDisconnectingCommand()
        {
            ChatCommand command = new ChatCommand(ChatCommandType.ServerDisconnecting, "");
            output.WriteLine(command);
        }
    }
}
