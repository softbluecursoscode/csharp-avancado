using System;
using System.Collections.Generic;
using System.Collections;
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

namespace Validation
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
            Funcionario f = new Funcionario();
            txtId.DataContext = f;

            f.ErrorsChanged += (s, a) =>
                {
                    INotifyDataErrorInfo info = s as INotifyDataErrorInfo;
                    btnProcurar.IsEnabled = !info.HasErrors;
                };
        }
    }


    class Funcionario : INotifyDataErrorInfo
    {
        string id;
        Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

        public string Id
        {
            get { return id; }
            set
            {
                id = value;

                if (id.Contains(" "))
                {
                    List<string> list = new List<string>();
                    list.Add("O texto contém espaços em branco");
                    AddErrors("Id", list);
                }
                else
                {
                    ClearErrors("Id");
                }
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return errors.Values;
            }
            else
            {
                List<string> propertyErrors;
                errors.TryGetValue(propertyName, out propertyErrors);
                return propertyErrors;
            }
        }

        public bool HasErrors
        {
            get { return errors.Count > 0; }
        }

        private void AddErrors(string propertyName, List<string> propertyErrors)
        {
            errors[propertyName] = propertyErrors;

            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        private void ClearErrors(string propertyName)
        {
            errors.Remove(propertyName);
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }
    }

    class SizeValidator : ValidationRule
    {
        public int MaxSize { get; set; }

        public SizeValidator()
        {
            MaxSize = 1000;
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string str = value.ToString();
            int size = str.Length;

            if (size <= MaxSize)
            {
                return new ValidationResult(true, null);
            }

            return new ValidationResult(false, "O texto excedeu o tamanho máximo de " + MaxSize);
        }
    }
}
