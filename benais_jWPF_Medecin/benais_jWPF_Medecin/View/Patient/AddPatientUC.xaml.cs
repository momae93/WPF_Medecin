using benais_jWPF_Medecin.ViewModel;
using System.Windows.Controls;

namespace benais_jWPF_Medecin.View.Patient
{
    /// <summary>
    /// Logique d'interaction pour AddPatientUC.xaml
    /// </summary>
    public partial class AddPatientUC : UserControl
    {
        public AddPatientUC(string login)
        {
            InitializeComponent();
            this.DataContext = new AddPatientViewModel(login);
        }
    }
}
