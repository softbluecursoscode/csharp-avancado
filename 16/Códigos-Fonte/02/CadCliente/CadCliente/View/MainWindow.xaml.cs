using CadCliente.ApplicationService;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CadCliente
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IWindowServices
    {
        // Indica se a janela deve ser fechada.
        bool closeWindow;

        public MainWindow()
        {
            InitializeComponent();

            // Faz com que o ViewModel referencie a janela como um IWindowServices
            viewModel.WindowServices = this;
        }

        public void PutFocusOnForm()
        {
            // Coloca o foco no campo de nome do formulário.
            txtNome.Focus();
        }

        public bool ConfirmSave()
        {
            // Exibe um dialog perguntando se é preciso salvar.
            MessageBoxResult result = MessageBox.Show(this, "Deseja salvar as alterações no cliente?", "Salvar alterações?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }

        public bool ConfirmDelete()
        {
            // Exibe um dialog perguntando se realmente é para excluir o cliente.
            MessageBoxResult result = MessageBox.Show(this, "Deseja excluir o cliente?", "Excluir?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }

        public void CloseWindow()
        {
            // Fecha a janela.
            closeWindow = true;
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!closeWindow)
            {
                // Se a janela está fechando, primeiro dá a chance do ViewModel de fazer as últimas checagens.
                viewModel.ProcessarSaida();
            }
        }

        public void UpdateBindings()
        {
            // Atualiza os bindings do formulário manualmente.
            // Isto é necessário para que os erros de validação já apareçam assim que o formulário é ativado.

            txtNome.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtEmail.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtTelefone.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtLogradouro.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtNumero.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtComplemento.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtBairro.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtCep.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void HandleSpecialChars(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Trata a digitação de caracteres especiais em alguns campos do formulário.

            // Converte o objeto que gerou o evento para um TextBox.
            TextBox textBox = sender as TextBox;

            if (e.Text.Length > 0)
            {
                bool allowed = false;

                if (textBox == txtNumero)
                {
                    // Permite apenas números no campo "número".
                    if (char.IsDigit(e.Text, e.Text.Length - 1))
                    {
                        allowed = true;
                    }
                }
                else if (textBox == txtCep)
                {
                    // Permite apenas números e um "-" no campo "CEP"
                    char c = e.Text[e.Text.Length - 1];

                    if (char.IsDigit(c))
                    {
                        allowed = true;
                    }
                    else if (c == '-' && !txtCep.Text.Contains("-"))
                    {
                        allowed = true;
                    }
                }
                else if (textBox == txtTelefone)
                {
                    // Permite apenas números e um "(" e ")" no campo "telefone".
                    char c = e.Text[e.Text.Length - 1];

                    if (char.IsDigit(c))
                    {
                        allowed = true;
                    }
                    else if (c == '-' && !txtTelefone.Text.Contains("-"))
                    {
                        allowed = true;
                    }
                    else if (c == '(' && !txtTelefone.Text.Contains("("))
                    {
                        allowed = true;
                    }
                    else if (c == ')' && !txtTelefone.Text.Contains(")"))
                    {
                        allowed = true;
                    }
                }

                // Indica se o evento foi tratado ou não. Se o evento for tratado, o caractere não aparece no campo do formulário.
                e.Handled = !allowed;
            }
        }

        private void HandleSpaceChar(object sender, KeyEventArgs e)
        {
            // Não permite espaços em branco nos campos "telefone", "CEP" e "número";
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
