using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Softblue
{
    // Converte um booleano para TextWrapping.Wrap ou TextWrapping.NoWrap
    [ValueConversion(typeof(bool), typeof(TextWrapping))]
    class BoolToLineWrapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool b = (bool)value;
            return b ? TextWrapping.Wrap : TextWrapping.NoWrap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
