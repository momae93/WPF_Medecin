using benais_jWPF_Medecin.ViewModel;
using System.Windows.Controls;

namespace benais_jWPF_Medecin.View
{
    /// <summary>
    /// Logique d'interaction pour MainUC.xaml
    /// </summary>
    public partial class MainUC : UserControl
    {
        public MainUC(string login)
        {
            InitializeComponent();
            this.DataContext = new MainViewModel(login);
        }
    }
}
