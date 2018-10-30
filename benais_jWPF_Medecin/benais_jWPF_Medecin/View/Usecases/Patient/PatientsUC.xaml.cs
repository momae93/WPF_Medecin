using benais_jWPF_Medecin.ViewModel;
using System.Windows.Controls;

namespace benais_jWPF_Medecin.View
{
    /// <summary>
    /// Logique d'interaction pour MainPatientsUC.xaml
    /// </summary>
    public partial class PatientsUC : UserControl
    {
        public PatientsUC(string login)
        {
            InitializeComponent();
            this.DataContext = new PatientsViewModel(login);
        }
    }
}
