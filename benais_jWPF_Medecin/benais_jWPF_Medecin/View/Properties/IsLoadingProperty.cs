using System.Windows;

namespace benais_jWPF_Medecin.View.Properties
{
    public class IsLoadingProperty: DependencyObject
    {
        private const string PROPERTY_NAME = "Value";
        public static DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(PROPERTY_NAME, typeof(object), typeof(IsLoadingProperty), new PropertyMetadata(null, OnValuePropertyChanged));

        public static bool GetValue(DependencyObject d) => (bool)d.GetValue(ValueProperty);
        public static void SetValue(DependencyObject d, bool value) => d.SetValue(ValueProperty, value);

        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e){ }
    }
}
