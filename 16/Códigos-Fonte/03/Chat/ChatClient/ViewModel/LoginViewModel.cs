using Chat.Common;
using Softblue.Mvvm;
using System.Threading.Tasks;

namespace Chat.Client
{
    class LoginViewModel : Bindable
    {
        // Cliente do chat.
        ChatClient chatClient;

        // Nome do servidor.
        private string server;
        public string Server
        {
            get { return server; }
            set
            {
                SetValue(ref server, value);

                if (string.IsNullOrWhiteSpace(value))
                {
                    // O servidor não pode ser vazio.
                    AddError("Forneça um servidor");
                }
                else
                {
                    // Tudo ok com a validação.
                    RemoveErrors();
                    Settings.Default.Server = value;
                }
            }
        }

        // Porta para se conectar.
        private string port;
        public string Port
        {
            get { return port; }
            set
            {
                SetValue(ref port, value);
                if (string.IsNullOrWhiteSpace(value))
                {
                    // A porta não pode ser vazia.
                    AddError("Forneça uma porta");
                }
                else
                {
                    // Tudo ok com a validação.
                    RemoveErrors();
                    Settings.Default.Port = value;
                }
            }
        }

        // Nome do cliente no chat.
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                SetValue(ref name, value);

                if (string.IsNullOrWhiteSpace(value))
                {
                    // O nome não pode ser vazio.
                    AddError("Forneça um nome");
                }
                else
                {
                    // Tudo ok com a validação.
                    RemoveErrors();
                    Settings.Default.Name = value;
                }
            }
        }

        // Comando de conexão.
        public Command ConnectCommand { get; set; }

        // Indica se a conexão pode ser feita (apenas se não há erros de validação e se a conexão já não está sendo feita)
        public bool CanConnect { get { return !HasErrors && NotTryingConnect; } }

        // Indica se a conexão não está sendo feita.
        private bool notTryingConnect;
        public bool NotTryingConnect
        {
            get { return notTryingConnect; }
            set
            {
                SetValue(ref notTryingConnect, value);
                OnPropertyChanged("CanConnect");
            }
        }

        // Serviços de janela.
        public ILoginWindowServices WindowServices { get; set; }

        // ViewModel implementa padrão singleton.
        static LoginViewModel current;
        public static LoginViewModel Current
        {
            get
            {
                if (current == null)
                {
                    current = new LoginViewModel();
                }
                return current;
            }
        }

        // Construtor privado.
        private LoginViewModel()
        {
            // Instancia o cliente do chat.
            chatClient = new ChatClient();

            // Lê os dados dos settings.
            Server = Settings.Default.Server;
            Port = Settings.Default.Port;
            Name = Settings.Default.Name;

            NotTryingConnect = true;

            // Registra o comando de conexão.
            ConnectCommand = new Command(ConnectAsync);

            // Registro no evento de mudança nos erros de validação.
            ErrorsChanged += (s, e) => OnPropertyChanged("CanConnect");
        }

        // Inicia a conexão de forma assíncrona.
        private async void ConnectAsync()
        {
            NotTryingConnect = false;

            // Faz a conexão no servidor. A presenta do await faz com que esta chamada seja assíncrona.
            bool connected = await Task<bool>.Factory.StartNew(() => chatClient.Connect(Server, int.Parse(Port), Name));

            if (!connected)
            {
                // Não foi possível conectar. Mostra o erro para o usuário.
                if (WindowServices != null)
                {
                    WindowServices.ShowErrorConnectionDialog("Servidor não encontrado");
                }

                NotTryingConnect = true;
            }
            else
            {
                // A conexão foi feita. Envia o pedido de entrada na sala e aguarda resposta.
                chatClient.InputHandler.EnterRoomResponse += OnEnterRoomResponse;
                chatClient.OutputHandler.SendEnterRoomCommand(Name);
            }
        }

        private void OnEnterRoomResponse(object sender, EnterRoomResponseEventArgs e)
        {
            // O servidor mandou uma resposta sobre o pedido de entrada na sala.
            if (WindowServices != null)
            {
                WindowServices.RunOnUIThread(() =>
                {
                    if (e.Valid )
                    {
                        // A entrada foi autorizada. Abre a janela do chat.
                        WindowServices.OpenChatWindow(chatClient);
                    }
                    else
                    {
                        // A entrada foi negada. Mostra o erro.
                        WindowServices.ShowErrorConnectionDialog(e.Error);
                        
                        // Desconecta o cliente.
                        chatClient.Disconnect();
                        NotTryingConnect = true;

                        // Para a thread que lê dados do canal.
                        e.StopInputHandler = true;
                    }
                });
            }
        }
    }
}
