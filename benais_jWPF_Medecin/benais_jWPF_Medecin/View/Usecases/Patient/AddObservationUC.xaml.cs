using benais_jWPF_Medecin.ViewModel.Usecases.Patient;
using System.Windows.Controls;

namespace benais_jWPF_Medecin.View.Usecases.Patient
{
    /// <summary>
    /// Logique d'interaction pour AddObservationUC.xaml
    /// </summary>
    public partial class AddObservationUC : UserControl
    {
        public AddObservationUC()
        {
            InitializeComponent();
            this.DataContext = new AddObservationViewModel();
        }
    }
}
