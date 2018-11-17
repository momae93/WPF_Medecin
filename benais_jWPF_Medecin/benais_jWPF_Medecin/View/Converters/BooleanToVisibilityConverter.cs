using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace benais_jWPF_Medecin.View.Converters
{
    public class BooleanToVisibilityConverter: IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                bool isInversed = System.Convert.ToBoolean(parameter);
                value = (isInversed) ? !(bool)value : value; 
                return (bool)value ? Visibility.Visible : Visibility.Hidden;
            }
            catch (Exception)
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
