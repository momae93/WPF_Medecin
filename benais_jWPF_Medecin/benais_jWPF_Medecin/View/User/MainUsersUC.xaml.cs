using benais_jWPF_Medecin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace benais_jWPF_Medecin.View
{
    /// <summary>
    /// Logique d'interaction pour MainUsersUC.xaml
    /// </summary>
    public partial class MainUsersUC : UserControl
    {
        public MainUsersUC(string login)
        {
            InitializeComponent();
            this.DataContext = new UsersViewModel(login);
        }
    }
}
