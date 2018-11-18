using benais_jWPF_Medecin.ViewModel.Usecases.Popup;
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
using System.Windows.Shapes;

namespace benais_jWPF_Medecin.View.Usecases.PopupWindows
{
    /// <summary>
    /// Logique d'interaction pour RetryLoginWindow.xaml
    /// </summary>
    public partial class RetryLoginWindow : Window
    {
        public RetryLoginWindow()
        {
            InitializeComponent();
            this.DataContext = new RetryLoginViewModel();
        }
    }
}
