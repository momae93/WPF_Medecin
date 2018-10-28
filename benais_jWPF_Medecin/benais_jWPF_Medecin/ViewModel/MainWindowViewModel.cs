using benais_jWPF_Medecin.Model.Enum;
using benais_jWPF_Medecin.View;
using benais_jWPF_Medecin.ViewModel.Pattern;
using System;
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
            ConnexionMediator.Register("Change_Main_UC", OnConnectView);
        }

        public void OnConnectView(EUserControl userControl, string login)
        {
            try
            {
                switch (userControl)
                {
                    case EUserControl.LOGIN:
                        CurrentUC = new LoginUC();
                        break;
                    case EUserControl.MAIN:
                        CurrentUC = new MainUC(login);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
