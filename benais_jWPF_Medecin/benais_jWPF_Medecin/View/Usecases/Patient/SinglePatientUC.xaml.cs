using benais_jWPF_Medecin.ViewModel.Usecases.Patient;
using System.Windows.Controls;

namespace benais_jWPF_Medecin.View.Usecases.Patient
{
    /// <summary>
    /// Logique d'interaction pour SinglePatientUC.xaml
    /// </summary>
    public partial class SinglePatientUC : UserControl
    {
        public SinglePatientUC(string login, int idPatient)
        {
            InitializeComponent();
            this.DataContext = new SinglePatientViewModel(login, idPatient);
        }
    }
}
