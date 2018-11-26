using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace benais_jWPF_Medecin.View.Converters
{
    public class BooleanToColorConverter : IValueConverter
    {
        private const string RESOURCE_GREEN = "ColorGreenEmeraldBrush";
        private const string RESOURCE_RED = "ColorRedAlzarinBrush";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return (bool)value ? Application.Current.Resources[RESOURCE_GREEN] : Application.Current.Resources[RESOURCE_RED];
            }
            catch (Exception)
            {
                return Application.Current.Resources[RESOURCE_RED];
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
