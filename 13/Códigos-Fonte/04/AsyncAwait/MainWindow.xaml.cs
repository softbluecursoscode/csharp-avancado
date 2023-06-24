using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace AsyncAwait
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

        private async void btnExecutar_Click(object sender, RoutedEventArgs e)
        {
            btnExecutar.IsEnabled = false;

            int result = await Task<int>.Factory.StartNew(Execute);

            txtResult.Text += "Execução finalizada! " + result + " iterações executadas";
            btnExecutar.IsEnabled = true;
        }

        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            txtResult.Text = "";
        }

        private int Execute()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                txtResult.Dispatcher.Invoke(() => txtResult.Text += "Iteração " + i + "\n");
            }

            return 10;
        }
    }
}
