using benais_jWPF_Medecin.ViewModel.Usecases.Patient;
using System.Windows.Controls;

namespace benais_jWPF_Medecin.View.Usecases.Patient
{
    /// <summary>
    /// Logique d'interaction pour ObservationsPatientUC.xaml
    /// </summary>
    public partial class ObservationsPatientUC : UserControl
    {
        public ObservationsPatientUC(string login, int idPatient)
        {
            InitializeComponent();
            this.DataContext = new ObservationsViewModel(login, idPatient);
        }
    }
}
