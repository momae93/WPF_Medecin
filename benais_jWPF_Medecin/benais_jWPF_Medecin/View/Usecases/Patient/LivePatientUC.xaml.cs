using benais_jWPF_Medecin.ViewModel.Usecases.Patient;
using System.Windows.Controls;

namespace benais_jWPF_Medecin.View.Usecases.Patient
{
    /// <summary>
    /// Logique d'interaction pour LivePatientUC.xaml
    /// </summary>
    public partial class LivePatientUC : UserControl
    {
        public LivePatientUC(string login, int idPatient)
        {
            InitializeComponent();
            this.DataContext = new LivePatientViewModel(login, idPatient);
        }
    }
}
