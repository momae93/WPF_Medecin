using benais_jWPF_Medecin.BusinessManagement;
using benais_jWPF_Medecin.Model.Enum;
using benais_jWPF_Medecin.ViewModel.Pattern;
using System.Windows;
using System.Windows.Input;

namespace benais_jWPF_Medecin.ViewModel
{
    public class MainViewModel: BaseViewModel
    {
        #region Variables
        private SessionBM _sessionBM;
        private string _login;
        #endregion

        #region Getters/Setters
        public ICommand LogoutCommand
        {
            get { return _logoutCommand; }
            set { _logoutCommand = value; }
        }
        #endregion

        #region Constructor
        public MainViewModel(string login)
        {
            _sessionBM = new SessionBM(login);
            _login = login;
            LogoutCommand = new RelayCommand(param => LogoutSession(), param => true);
        }
        #endregion

        #region Command

        private ICommand _logoutCommand;
        private void LogoutSession()
        {
            if (_sessionBM.Disconnect())
            {
                MessageBox.Show("Success logout");
                ConnexionMediator.Notify("Change_Main_UC", EUserControl.LOGIN, _login);
            }
            else
            {
                MessageBox.Show("Fail logout");
            }
        }
        #endregion
    }
}
