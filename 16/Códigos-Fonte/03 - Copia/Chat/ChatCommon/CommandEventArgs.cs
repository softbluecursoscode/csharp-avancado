using System;
using System.Collections.Generic;

namespace Chat.Common
{
    // Classes que representam parâmetros para eventos.

    public class BaseEventArgs : EventArgs
    {
        public bool StopInputHandler { get; set; }
    }

    public class MemberEventArgs : BaseEventArgs
    {
        public string Name { get; private set; }

        public MemberEventArgs(string name)
        {
            this.Name = name;
        }
    }

    public class EnterRoomResponseEventArgs : BaseEventArgs
    {
        public bool Valid { get; private set; }
        public string Error { get; private set; }

        public EnterRoomResponseEventArgs(bool valid, string error)
        {
            this.Valid = valid;
            this.Error = error;
        }
    }

    public class MembersEventArgs : BaseEventArgs
    {
        public List<string> Members { get; private set; }

        public MembersEventArgs(List<string> members)
        {
            this.Members = members;
        }
    }

    public class MessageEventArgs : BaseEventArgs
    {
        public string Message { get; private set; }

        public MessageEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
