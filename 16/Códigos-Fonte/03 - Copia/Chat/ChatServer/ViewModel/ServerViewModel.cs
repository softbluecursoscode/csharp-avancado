using Softblue.Mvvm;

namespace Chat.Server
{
    class ServerViewModel : Bindable
    {
        // Servidor do chat.
        ChatServer server;

        // Comandos chamados pelos botões 'Conectar' e 'Desconectar'
        public Command ConnectCommand { get; set; }
        public Command DisconnectCommand { get; set; }

        // Porta do servidor.
        // A property é do tipo string para evitar que uma porta não especificada mostre o valor 0.
        string port;
        public string Port
        {
            get { return port; }
            set
            {
                port = value;
                int portInt = int.Parse(port);

                if (portInt < Settings.Default.MinPort)
                {
                    // A porta não por ser menor do que MinPort
                    AddError("A porta não pode ser menor que " + Settings.Default.MinPort);
                }
                else if (portInt > Settings.Default.MaxPort)
                {
                    // A porta não pode ser maior do que MaxPort
                    AddError("A porta não pode ser maior que " + Settings.Default.MaxPort);
                }
                else
                {
                    // Tudo certo com a validação.
                    RemoveErrors();
                    Settings.Default.Port = value;
                }
            }
        }

        // Indica se o servidor está conectado.
        private bool connected;
        public bool Connected
        {
            get { return connected; }
            set
            {
                SetValue(ref connected, value);
                OnPropertyChanged("NotConnected");
                OnPropertyChanged("CanConnect");
            }
        }

        // Indica se é possível conectar.
        public bool CanConnect
        {
            get
            {
                // Só é possível conectar se não houver erros de validação e se a conexão já não tiver sido feita.
                return !HasErrors && !Connected;
            }
        }

        // Indica se o servidor não está conectado.
        public bool NotConnected
        {
            get { return !Connected; }
        }

        // Implementação do padrão singleton.
        private static ServerViewModel current;
        public static ServerViewModel Current
        {
            get
            {
                if (current == null)
                {
                    current = new ServerViewModel();
                }
                return current;
            }
        }

        // Construtor.
        private ServerViewModel()
        {
            // Cria o servidor do chat.
            server = new ChatServer();

            // Atribui a porta a partir dos settings.
            Port = Settings.Default.Port;

            // Cria os comandos.
            ConnectCommand = new Command(Connect);
            DisconnectCommand = new Command(Disconnect);

            // Registro no evento disparado quando há mudança nos erros de validação.
            ErrorsChanged += (s, e) => OnPropertyChanged("CanConnect");

            // Registro nos eventos de servidor conectado e desconectado.
            server.Connected += (s, e) => Connected = true;
            server.Disconnected += (s, e) => Connected = false;
        }

        private void Connect()
        {
            // Conecta o servidor.
            server.Connect(int.Parse(Port));
        }

        public void Disconnect()
        {
            // Desconecta o servidor.
            server.Disconnect();
        }
    }
}
