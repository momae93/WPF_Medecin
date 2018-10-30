using benais_jWPF_Medecin.ViewModel.Usecases.Patient;
using System.Windows.Controls;

namespace benais_jWPF_Medecin.View.Usecases.Patient
{
    /// <summary>
    /// Logique d'interaction pour DetailsPatientUC.xaml
    /// </summary>
    public partial class DetailsPatientUC : UserControl
    {
        public DetailsPatientUC(string login, int idPatient)
        {
            InitializeComponent();
            this.DataContext = new DetailsPatientViewModel(login, idPatient);
        }
    }
}
