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

namespace Conversion
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
            Cliente cliente = new Cliente
            {
                Nome = "Sérgio Junior",
                Altura = 1.8,
                Cpf = new Cpf()
                {
                    Base = 23456987,
                    Digito = 5
                }
            };

            gridCliente.DataContext = cliente;
        }
    }

    class Cliente
    {
        public string Nome { get; set; }
        public double Altura { get; set; }
        public Cpf Cpf { get; set; }
    }

    class Cpf
    {
        public int Base { get; set; }
        public int Digito { get; set; }
    }

    [ValueConversion(typeof(Cpf), typeof(string))]
    class CpfConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Cpf cpf = value as Cpf;
            return string.Format("{0:000\\.000\\.000}-{1:00}", cpf.Base, cpf.Digito);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string cpfStr = value as string;

            string baseStr = cpfStr.Substring(0, 11).Replace(".", "");
            string digitoStr = cpfStr.Substring(12);

            Cpf cpf = new Cpf();
            cpf.Base = int.Parse(baseStr);
            cpf.Digito = int.Parse(digitoStr);

            return cpf;
        }
    }
}
