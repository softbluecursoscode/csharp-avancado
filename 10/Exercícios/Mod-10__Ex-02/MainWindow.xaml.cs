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

namespace Softblue
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Brush statusBarOriginalColor;
        FontWeight statusBarOriginalWeight;

        public MainWindow()
        {
            InitializeComponent();

            // Grava o aspecto original do texto, para ser restaurado depois.
            statusBarOriginalColor = statusBar.Foreground;
            statusBarOriginalWeight = statusBar.FontWeight;
        }

        private void txt1_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt2.Text = txt1.Text;
        }

        private void mnuSair_Click(object sender, RoutedEventArgs e)
        {
            // Sai da aplicação.
            Application.Current.Shutdown();
        }

        private void statusBar_MouseEnter(object sender, MouseEventArgs e)
        {
            // Muda o aspecto do texto.
            statusBar.Foreground = Brushes.Blue;
            statusBar.FontWeight = FontWeights.Bold;
        }

        private void statusBar_MouseLeave(object sender, MouseEventArgs e)
        {
            // Retorna para o aspecto original.

            statusBar.Foreground = statusBarOriginalColor;
            statusBar.FontWeight = statusBarOriginalWeight;
        }
    }
}
