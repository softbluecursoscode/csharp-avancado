using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Softblue
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Pessoa> pessoas;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            pessoas = new List<Pessoa>();
            pessoas.Add(new Pessoa("José", "Silva"));
            pessoas.Add(new Pessoa("Alberto", "Oliveira"));
            pessoas.Add(new Pessoa("João", "Júnior"));
            pessoas.Add(new Pessoa("Maria", "Vargas"));
            pessoas.Add(new Pessoa("Alice", "Fagundes"));

            listPessoas.ItemsSource = pessoas;
        }
    }

    class Pessoa : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string nome;
        public string sobrenome;

        public Pessoa(string nome, string sobrenome)
        {
            this.nome = nome;
            this.sobrenome = sobrenome;
        }

        public string Nome
        {
            get { return nome; }
            set
            {
                nome = value;
                OnPropertyChanged("Nome");
                OnPropertyChanged("NomeCompleto");
            }
        }

        public string Sobrenome
        {
            get { return sobrenome; }
            set
            {
                sobrenome = value;
                OnPropertyChanged("Sobrenome");
                OnPropertyChanged("NomeCompleto");
            }
        }

        public string NomeCompleto
        {
            get { return nome + " " + sobrenome; }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
