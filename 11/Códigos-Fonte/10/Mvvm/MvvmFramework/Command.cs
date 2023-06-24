using System;
using System.Windows.Input;

namespace Softblue.Mvvm
{
    public class Command : ICommand
    {
        Action commandAction;
        bool canExecute;

        public Command(Action commandAction, bool canExecute = true)
        {
            this.commandAction = commandAction;
            this.canExecute = canExecute;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return canExecute;
        }

        public bool CanExecute
        {
            get { return canExecute; }
            set
            {
                if (canExecute != value)
                {
                    canExecute = value;

                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public event EventHandler CanExecuteChanged;

        void ICommand.Execute(object parameter)
        {
            if (commandAction != null)
            {
                commandAction();
            }
        }
    }
}
