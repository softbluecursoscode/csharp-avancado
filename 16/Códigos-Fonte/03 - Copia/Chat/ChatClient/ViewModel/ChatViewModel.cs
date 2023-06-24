using Chat.Common;
using Softblue.Mvvm;
using System.Collections.ObjectModel;

namespace Chat.Client
{
    class ChatViewModel : Bindable
    {
        // Cliente do chat.
        public ChatClient chatClient;

        // Serviços de janela.
        public IChatWindowServices WindowServices { get; set; }

        // Lista de nomes de membros conectados.
        // É do tipo ObservableCollection para que as mudanças nos elementos sejam refletidas na interface gráfica.
        private ObservableCollection<string> names;
        public ObservableCollection<string> Names
        {
            get { return names; }
            set { SetValue(ref names, value); }
        }

        // Mensagem a ser enviada.
        private string message;
        public string Message
        {
            get { return message; }
            set { SetValue(ref message, value); }
        }

        // Mensagens do chat.
        private string messages;
        public string Messages
        {
            get { return messages; }
            set { SetValue(ref messages, value); }
        }

        // Título da janela.
        private string title;
        public string Title
        {
            get { return title; }
            set { SetValue(ref title, value); }
        }

        // Comando de envio de mensagem.
        public Command SendCommand { get; set; }

        // ViewModel implementa o padrão singleton.
        private static ChatViewModel current;
        public static ChatViewModel Current
        {
            get
            {
                if (current == null)
                {
                    current = new ChatViewModel();
                }
                return current;
            }
        }

        // Construtor privado.
        private ChatViewModel()
        {
            Names = new ObservableCollection<string>();
            SendCommand = new Command(Send);
        }

        // Inicializa o ViewModel.
        public void Initialize(ChatClient chatClient)
        {
            this.chatClient = chatClient;

            // Customiza o título com o nome de quem se conectou.
            Title = string.Format("Chat - {0}", chatClient.Name);

            // Registro nos eventos necessários.
            chatClient.InputHandler.GetMembersResponse += OnGetMembersResponse;
            chatClient.InputHandler.MessageReceived += OnMessageReceived;
            chatClient.InputHandler.MemberEntered += OnMemberEntered;
            chatClient.InputHandler.MemberLeft += OnMemberLeft;
            chatClient.InputHandler.MemberCanLeave += OnMemberCanLeave;
            chatClient.InputHandler.ServerDisconnecting += OnServerDisconnecting;

            // Solicita a lista de membros.
            chatClient.OutputHandler.SendGetMembersCommand();
        }

        // Algum membro mandou uma mensagem.
        void OnMessageReceived(object sender, MessageEventArgs e)
        {
            if (WindowServices != null)
            {
                WindowServices.RunOnUIThread(() =>
                {
                    // Mostra a mensagem na tela.
                    ShowMessage(e.Message);
                });
            }
        }

        // O servidor enviou a lista de membros.
        void OnGetMembersResponse(object sender, MembersEventArgs e)
        {
            if (WindowServices != null)
            {
                WindowServices.RunOnUIThread(() =>
                {
                    // Atribui a lista à coleção 'Nomes'.
                    Names = new ObservableCollection<string>(e.Members);
                });
            }
        }

        // Um novo membro entrou.
        void OnMemberEntered(object sender, MemberEventArgs e)
        {
            if (WindowServices != null)
            {
                WindowServices.RunOnUIThread(() =>
                {
                    // Adiciona o nome na lista de nomes.
                    Names.Add(e.Name);

                    // Mostra mensagem avisando.
                    ShowMessage(e.Name + " entrou no chat");
                });
            }
        }

        // Um membro saiu.
        void OnMemberLeft(object sender, MemberEventArgs e)
        {
            if (WindowServices != null)
            {
                WindowServices.RunOnUIThread(() =>
                {
                    // Remove o nome da lista de nomes.
                    Names.Remove(e.Name);

                    // Mostra mensagem avisando.
                    ShowMessage(e.Name + " saiu do chat");
                });
            }
        }

        // Indica que o servidor autorizou este cliente a sair do chat.
        private void OnMemberCanLeave(object sender, MemberEventArgs e)
        {
            // Para a thread que lê dados da stream.
            e.StopInputHandler = true;

            // Desconecta o cliente.
            chatClient.Disconnect();

            if (WindowServices != null)
            {
                // Fecha a janela do chat.
                WindowServices.RunOnUIThread(() => WindowServices.CloseWindow());
            }
        }

        // O servidor está se deconectando.
        private void OnServerDisconnecting(object sender, BaseEventArgs e)
        {
            // Inicia o processo de desconexão do cliente.
            // Quando o servidor avisa que vai se desconectar, todos os cliente se desconectam antes.
            LeaveRoom();
        }

        // O cliente está iniciando o processo de sair do chat.
        public void LeaveRoom()
        {
            // Avisa o servidor que o cliente está saindo.
            chatClient.OutputHandler.SendMemberLeavingCommand(chatClient.Name);
        }

        // Envia uma mensagem.
        public void Send()
        {
            if (!string.IsNullOrEmpty(Message))
            {
                // Envia a mensagem ao servidor.
                chatClient.OutputHandler.SendMessageCommand(Message);
                
                // Limpa o texto depois de enviado.
                Message = "";
            }
        }
        
        // Mostra a mensagem na tela.
        void ShowMessage(string message)
        {
            if (string.IsNullOrEmpty(messages))
            {
                // Se for a primeira mensagem, apenas mostra.
                Messages = message;
            }
            else
            {
                // Se não for a primeira mensagem, coloca uma quebra de linha no início.
                Messages += "\n" + message;
            }

            // Faz o scroll até o final das mensagens.
            WindowServices.ScrollMessagesToEnd();
        }
    }
}
