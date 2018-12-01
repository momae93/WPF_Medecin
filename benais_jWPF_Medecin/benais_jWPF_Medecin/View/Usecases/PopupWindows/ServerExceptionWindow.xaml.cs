using benais_jWPF_Medecin.ViewModel.Usecases.Popup;
using System.Windows;

namespace benais_jWPF_Medecin.View.Usecases.PopupWindows
{
    /// <summary>
    /// Logique d'interaction pour ServerExceptionWindow.xaml
    /// </summary>
    public partial class ServerExceptionWindow : Window
    {
        public ServerExceptionWindow(string description)
        {
            InitializeComponent();
            this.DataContext = new ServerExceptionViewModel(description);
        }
    }
}
