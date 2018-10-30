using benais_jWPF_Medecin.ViewModel;
using System.Windows.Controls;

namespace benais_jWPF_Medecin.View
{
    /// <summary>
    /// Logique d'interaction pour MainAddUser.xaml
    /// </summary>
    public partial class AddUserUC : UserControl
    {
        public AddUserUC(string login)
        {
            InitializeComponent();
            this.DataContext = new MainAddUserViewModel(login);
        }
    }
}
