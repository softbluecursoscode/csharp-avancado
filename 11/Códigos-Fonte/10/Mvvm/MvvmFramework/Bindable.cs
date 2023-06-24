using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Softblue.Mvvm
{
    public abstract class Bindable : INotifyDataErrorInfo, INotifyPropertyChanged
    {
        Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return errors.Values;
            }

            if (!errors.ContainsKey(propertyName))
            {
                return Enumerable.Empty<List<string>>();
            }

            return errors[propertyName];
        }

        public bool HasErrors
        {
            get { return errors.Count > 0; }
        }

        public void AddError(String error, [CallerMemberName] string propertyName = "")
        {
            RemoveErrors(propertyName);
            AddErrors(new List<string> { error }, propertyName);
        }

        public void AddErrors(List<string> error, [CallerMemberName] string propertyName = "")
        {
            errors[propertyName] = error;
            OnErrorsChanged(propertyName);
        }

        public void RemoveErrors([CallerMemberName] string propertyName = "")
        {
            errors.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        protected void SetValue<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(property, value))
            {
                return;
            }

            property = value;
            OnPropertyChanged(propertyName);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
