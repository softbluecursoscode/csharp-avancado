using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

// Desabilita o warning 4014, que aparece na linha "Task.Factory.StartNew(MostrarProgresso)"
// Este warning avisa que a chamada pode ser um erro, pois não tem await. Mas neste caso a ideia é continuar a execução mesmo antes da task iniciada terminar.
#pragma warning disable 4014

namespace Softblue
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Objeto para fornecer um lock
        static readonly object sync = new object();

        // Field para controlar se a contagem ainda está sendo feita.
        static bool fazendoContagem;

        // Property que gerencia o valor de 'fazendoContagem'.
        // O acesso ao field é sincronizado, pois 2 threads podem acessá-lo simultaneamente.
        static bool FazendoContagem
        {
            get
            {
                lock (sync)
                {
                    return fazendoContagem;
                }
            }
            set
            {
                lock (sync)
                {
                    fazendoContagem = value;
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            // Coloca o foco na caixa de texto para digitar o diretório.
            txtDir.Focus();
        }

        private async void btnContar_Click(object sender, RoutedEventArgs e)
        {
            // Limpa a caixa de texto.
            txtResult.Text = "";

            // Lê o diretório digitado pelo usuário.
            string dir = txtDir.Text;

            // Se um diretório não foi digitado, mostra um erro.
            if (string.IsNullOrWhiteSpace(dir))
            {
                MessageBox.Show(this, "É preciso fornecer um diretório", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Cria um DirectoryInfo e verifica se o diretório existe.
            DirectoryInfo dirInfo = new DirectoryInfo(dir);

            // Se o diretório não existe. mostra um erro.
            if (!dirInfo.Exists)
            {
                MessageBox.Show(this, "O diretório não existe", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Desabilita os controles.
            btnContar.IsEnabled = false;
            txtDir.IsEnabled = false;

            // Indica que a contagem começou.
            FazendoContagem = true;

            // Inicia a task que fica mostrando o progresso da execução.
            Task.Factory.StartNew(MostrarProgresso);

            // Executa a tarefa de forma assíncrona. A chamada await cuida dos detalhes para fazer o código executar em outra thread.
            Resultado resultado = await Task.Factory.StartNew<Resultado>(() => Contador.ContarArquivosEDiretorios(dirInfo));

            // Indica que a contagem terminou.
            FazendoContagem = false;

            // Mostra o resultado.
            txtResult.Text += "Número de arquivos: " + resultado.Arquivos + "\n";
            txtResult.Text += "Número de diretórios: " + resultado.Diretorios;

            // Habilita os controles.
            btnContar.IsEnabled = true;
            txtDir.IsEnabled = true;
        }

        private void MostrarProgresso()
        {
            // Enquanto a contagem está ativa, mostra a mensagem a cada 1s.
            while (FazendoContagem)
            {
                Dispatcher.Invoke(() => txtResult.Text += "Contando...\n");
                Thread.Sleep(1000);
            }
        }
    }
}

#pragma warning restore 4014