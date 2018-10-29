using benais_jWPF_Medecin.Model.Enum;
using benais_jWPF_Medecin.View;
using benais_jWPF_Medecin.ViewModel.Pattern;
using System;
using System.Windows;
using System.Windows.Controls;

namespace benais_jWPF_Medecin.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private UserControl _currentUC;
        public UserControl CurrentUC
        {
            get { return _currentUC; }
            set
            {
                _currentUC = value;
                OnPropertyChanged(nameof(CurrentUC));
            }
        }

        public MainWindowViewModel()
        {
            CurrentUC = new LoginUC();
            Mediator.Register("Change_MainWindow_UC", OnConnectView);
        }

        public void OnConnectView(EUserControl userControl, object login)
        {
            try
            {
                switch (userControl)
                {
                    case EUserControl.LOGIN:
                        CurrentUC = new LoginUC();
                        break;
                    case EUserControl.MAIN:
                        CurrentUC = new MainUC((string)login);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed changing view");
                throw;
            }
        }
    }
}
