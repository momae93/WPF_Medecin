using benais_jWPF_Medecin.ViewModel;
using System.Windows.Controls;

namespace benais_jWPF_Medecin.View
{
    /// <summary>
    /// Logique d'interaction pour MainUsersUC.xaml
    /// </summary>
    public partial class UsersUC : UserControl
    {
        public UsersUC(string login)
        {
            InitializeComponent();
            this.DataContext = new UsersViewModel(login);
        }
    }
}
