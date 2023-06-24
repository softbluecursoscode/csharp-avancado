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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            List<Pessoa> pessoas = new List<Pessoa>()
            {
                new Pessoa() { Nome = "José", Idade = 35, Brasileiro = true },
                new Pessoa() { Nome = "Mariana", Idade = 22, Brasileiro = true },
                new Pessoa() { Nome = "Joseph", Idade = 40, Brasileiro = false },
                new Pessoa() { Nome = "William", Idade = 19, Brasileiro = false },
                new Pessoa() { Nome = "Mary", Idade = 30, Brasileiro = false },
            };

            list.ItemsSource = pessoas;
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
