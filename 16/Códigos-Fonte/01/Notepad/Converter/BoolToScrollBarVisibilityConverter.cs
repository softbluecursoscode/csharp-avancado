using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace Softblue
{
    // Converte um booleano para ScrollBarVisibility.Visible ou ScrollBarVisibility.Disabled
    [ValueConversion(typeof(bool), typeof(ScrollBarVisibility))]
    class BoolToScrollBarVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool b = (bool)value;
            return b ? ScrollBarVisibility.Visible : ScrollBarVisibility.Disabled;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
