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
        // CPF para o binding.
        public string Cpf { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            // Usa o próprio objeto window como data context.
            txtCpf.DataContext = this;
        }
    }

    // Validador customizado para CPF.
    public class CpfValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string cpf = (string)value;
            bool valido = Validador.ValidarCpf(cpf);

            return valido ? new ValidationResult(true, null) : new ValidationResult(false, "CPF inválido");
        }
    }
}
