using CadCliente.ApplicationService;
using CadCliente.Model;
using CadCliente.Model.Db;
using Db;
using Softblue.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CadCliente.ViewModel
{
    // ViewModel de Cliente/Endereco.
    class ClienteViewModel : Bindable
    {
        // DAO para acesso a dados.
        ClienteDao dao;

        public IWindowServices WindowServices { get; set; }

        // Lista de clientes.
        private List<Cliente> clientes;
        public List<Cliente> Clientes
        {
            get { return clientes; }
            set { SetValue(ref clientes, value); }
        }

        // Cliente sendo inserido ou alterado.
        private Cliente cliente;
        public Cliente Cliente
        {
            get { return cliente; }
            set { SetValue(ref cliente, value); }
        }

        // Índice que corresponde ao cliente selecionado na lista de clientes.
        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                SetValue(ref selectedIndex, value);

                if (selectedIndex >= 0)
                {
                    // Atribui à propriedade Cliente o objeto cliente selecionado.
                    Cliente = Clientes[selectedIndex];
                }

                // Ajusta o estado dos comandos.
                NovoClienteCommand.CanExecute = IsViewing;
                ExcluirClienteCommand.CanExecute = IsViewing;
                EditarClienteCommand.CanExecute = IsViewing;
            }
        }

        // Indica se o cliente está em modo de edição.
        bool isEditing;
        public bool IsEditing
        {
            get { return isEditing; }
            set
            {
                SetValue(ref isEditing, value);

                // Ajusta o estado dos comandos.
                NovoClienteCommand.CanExecute = IsViewing;
                ExcluirClienteCommand.CanExecute = IsViewing && selectedIndex >= 0;
                EditarClienteCommand.CanExecute = IsViewing && SelectedIndex >= 0;
                CancelarEdicaoClienteCommand.CanExecute = isEditing;
                GravarClienteCommand.CanExecute = isEditing;

                // Notifica as properties IsEditing e IsViewing para serem reavaliadas.
                OnPropertyChanged("IsViewing");
            }
        }

        // Parte do nome para pesquisar.
        private string textoPesquisa;
        public string TextoPesquisa
        {
            get { return textoPesquisa; }
            set { SetValue(ref textoPesquisa, value); }
        }
        
        // Indica se o cliente está em modo de visualização.
        public bool IsViewing
        {
            get { return !IsEditing; }
        }

        // Comandos.
        public Command NovoClienteCommand { get; set; }
        public Command ExcluirClienteCommand { get; set; }
        public Command EditarClienteCommand { get; set; }
        public Command GravarClienteCommand { get; set; }
        public Command CancelarEdicaoClienteCommand { get; set; }
        public Command SairCommand { get; set; }
        public Command PesquisarCommand { get; set; }

        // Construtor.
        public ClienteViewModel()
        {
            try
            {
                NovoClienteCommand = new Command(NovoCliente);
                ExcluirClienteCommand = new Command(ExcluirCliente);
                EditarClienteCommand = new Command(EditarCliente);
                GravarClienteCommand = new Command(GravarCliente);
                CancelarEdicaoClienteCommand = new Command(CancelarEdicaoCliente);
                SairCommand = new Command(Sair);
                PesquisarCommand = new Command(Pesquisar);

                // Cria o DAO.
                dao = DaoFactory.CreateDao<ClienteDao>();

                IsEditing = false;
                SelectedIndex = -1;
                
                // Obtém a lista inicial de clientes.
                Clientes = dao.ListarClientes(null);
            }
            catch (Exception e)
            {
                // Este catch é necessário para evitar erro no processo de criação do XAML.
                Debug.WriteLine("Erro: " + e.Message);
            }
        }

        // Um novo cliente está sendo adicionado.
        void NovoCliente()
        {
            // Inicia edição.
            IsEditing = true;
            
            // Não seleciona nenhum cliente da lista.
            SelectedIndex = -1;

            // Cria um novo cliente.
            Cliente = new Cliente();

            // Registra o interesse nas mudanças dos erros de validação.
            Cliente.ErrorsChanged += OnErrorsChanged;
            Cliente.Endereco.ErrorsChanged += OnErrorsChanged;

            if (WindowServices != null)
            {
                // Atualiza os bindings.
                WindowServices.UpdateBindings();
                
                // Coloca o foco no formulário.
                WindowServices.PutFocusOnForm();
            }
        }

        void OnErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            // Se os erros de validação mudarem, verifica se a gravação é ativada ou não.
            GravarClienteCommand.CanExecute = !Cliente.HasErrors && !Cliente.Endereco.HasErrors;
        }

        // Exclui um cliente.
        void ExcluirCliente()
        {
            bool confirm = true;
            if (WindowServices != null)
            {
                // Pede confirmação.
                confirm = WindowServices.ConfirmDelete();
            }

            if (confirm)
            {
                // Exclui do banco de dados.
                dao.Excluir(Cliente.Id.Value);
                
                // Atualiza a lista de clientes.
                Clientes = dao.ListarClientes(TextoPesquisa);
                
                // Cria um novo cliente para limpar os dados do formulário.
                Cliente = new Cliente();
            }
        }

        // Edita um cliente existente.
        void EditarCliente()
        {
            // Entra em modo de edição.
            IsEditing = true;

            if (WindowServices != null)
            {
                // Coloca o foco no formulário.
                WindowServices.PutFocusOnForm();
            }
        }

        // Grava as alterações de um cliente.
        void GravarCliente()
        {
            if (Cliente.Id == null)
            {
                // Se o cliente não tiver um ID, é uma inserção.
                dao.Inserir(Cliente);
            }
            else
            {
                // Se o cliente tiver um ID é uma alteração.
                dao.Alterar(cliente);
            }

            // Entra em modo de visualização.
            IsEditing = false;

            // Atualiza a lista de clientes.
            Clientes = dao.ListarClientes(TextoPesquisa);

            // Cria um novo cliente para limpar os dados do formulário.
            Cliente = new Cliente();
        }

        // Cancela a edição de um cliente.
        void CancelarEdicaoCliente()
        {
            // Entra em modo de visualização.
            IsEditing = false;

            if (Cliente.Id != null)
            {
                // Se o cliente tem ID, é porque ele existe no banco de dados. Então lê seus dados do banco de dados novamente.
                dao.LerCliente(Cliente.Id.Value, Cliente);
                
                // Remove o interesse na mudança dos erros de validação.
                Cliente.ErrorsChanged -= OnErrorsChanged;
                Cliente.Endereco.ErrorsChanged -= OnErrorsChanged;
            }
            else
            {
                // Se o cliente não tem ID, é porque não existe no banco de dados. Então basta descartar os dados.
                // Remove o interesse na mudança dos erros de validação.
                Cliente.ErrorsChanged -= OnErrorsChanged;
                Cliente.Endereco.ErrorsChanged -= OnErrorsChanged;
                
                Cliente = new Cliente();
            }
        }

        // Verifica se é preciso salvar os dados antes de sair.
        public void ProcessarSaida()
        {
            if (IsEditing && GravarClienteCommand.CanExecute)
            {
                // Se está em modo de edição e a gravação está habilitada, pergunta se os dados serão salvos.
                bool confirm = true;
                if (WindowServices != null)
                {
                    confirm = WindowServices.ConfirmSave();
                }

                if (confirm)
                {
                    // Grava os dados do cliente.
                    GravarCliente();
                }
            }
        }

        // Inicia a saída da aplicação.
        void Sair()
        {
            // Faz o processamento final, gravando os dados se necessário.
            ProcessarSaida();

            if (WindowServices != null)
            {
                // Fecha a janela.
                WindowServices.CloseWindow();
            }
        }

        // Pesquisa clientes de acordo com um padrão de nome.
        void Pesquisar()
        {
            // Atualiza a lista de acordo com o texto fornecido.
            Clientes = dao.ListarClientes(TextoPesquisa);
        }
    }
}
