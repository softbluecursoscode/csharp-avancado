using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BindingObject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Pessoa pessoa;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            pessoa = new Pessoa();
            pessoa.Nome = "José";
            pessoa.Idade = 35;
            pessoa.Brasileiro = true;

            //txtNome.DataContext = pessoa;
            //txtIdade.DataContext = pessoa;
            //chkBrasileiro.DataContext = pessoa;

            gridPessoa.DataContext = pessoa;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (pessoa != null)
            {
                MessageBox.Show(this, pessoa.ToString(), "Dados do Objeto", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

    class Pessoa
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public bool Brasileiro { get; set; }

        public override string ToString()
        {
            return string.Format("Nome: {0}\nIdade: {1}\nBrasileiro: {2}", Nome, Idade, Brasileiro);
        }
    }
}
