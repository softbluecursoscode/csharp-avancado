using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Command
{
    class ProcessarCommand : ICommand
    {
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            MessageBox.Show("Comando executado!");
        }

        private bool canExecute;
        public bool CanExecute
        {
            get { return canExecute; }
            set
            {
                canExecute = value;
                if (CanExecuteChanged != null)
                {
                    CanExecuteChanged(this, EventArgs.Empty);
                }
            }
        }

    }
}
