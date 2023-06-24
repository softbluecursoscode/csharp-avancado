using Mvvm.ApplicationServices;
using System.Windows;

namespace Mvvm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IWindowServices
    {
        public MainWindow()
        {
            InitializeComponent();

            viewModel.WindowServices = this;
        }

        public void SetFocus()
        {
            txtItem.Focus();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetFocus();
        }
    }
}
