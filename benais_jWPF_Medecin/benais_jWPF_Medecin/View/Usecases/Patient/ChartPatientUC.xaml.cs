using benais_jWPF_Medecin.ViewModel.Usecases.Patient;
using System.Windows.Controls;

namespace benais_jWPF_Medecin.View.Usecases.Patient
{
    /// <summary>
    /// Logique d'interaction pour ChartPatientUC.xaml
    /// </summary>
    public partial class ChartPatientUC : UserControl
    {
        public ChartPatientUC(string login, int idPatient)
        {
            InitializeComponent();
            this.DataContext = new ChartPatientViewModel(login, idPatient);
        }
    }
}
