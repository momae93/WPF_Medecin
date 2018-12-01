using benais_jWPF_Medecin.ViewModel.Usecases.Popup;
using System.Windows;

namespace benais_jWPF_Medecin.View.Usecases.PopupWindows
{
    /// <summary>
    /// Logique d'interaction pour PictureExceptionWindow.xaml
    /// </summary>
    public partial class PictureExceptionWindow : Window
    {
        public PictureExceptionWindow()
        {
            InitializeComponent();
            this.DataContext = new PictureExceptionViewModel();
        }
    }
}
