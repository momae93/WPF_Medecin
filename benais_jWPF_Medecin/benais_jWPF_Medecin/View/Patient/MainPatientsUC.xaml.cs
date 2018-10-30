using benais_jWPF_Medecin.ViewModel;
using System.Windows.Controls;

namespace benais_jWPF_Medecin.View
{
    /// <summary>
    /// Logique d'interaction pour MainPatientsUC.xaml
    /// </summary>
    public partial class MainPatientsUC : UserControl
    {
        public MainPatientsUC(string login)
        {
            InitializeComponent();
            this.DataContext = new PatientsViewModel(login);
        }
    }
}
