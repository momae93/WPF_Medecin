using benais_jWPF_Medecin.BusinessManagement;
using benais_jWPF_Medecin.Common.Exceptions;
using benais_jWPF_Medecin.Model.Enum;
using benais_jWPF_Medecin.View.Usecases.PopupWindows;
using benais_jWPF_Medecin.ViewModel.Pattern;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace benais_jWPF_Medecin.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        #region Variables

        private string _login;
        private string _password;
        private LoginBM _loginBM;
        private string _message;
        private bool _isLoadingSession;
        private bool _isConnecting;

        #endregion

        #region Getters/Setters

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }
        public bool IsLoadingSession
        {
            get { return _isLoadingSession; }
            set
            {
                _isLoadingSession = value;
                OnPropertyChanged(nameof(IsLoadingSession));
            }
        }
        
        #endregion

        #region Constructor

        public LoginViewModel()
        {
            _loginBM = new LoginBM();
            LoginCommand = new RelayCommand(param => LoginSession(), param => true);
            Message = "";
            IsLoadingSession = false;
            _isConnecting = false;
            Mediator.Register("Retry_Login_UC", OnRetryListener);
        }

        #endregion

        #region Command

        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get { return _loginCommand; }
            set { _loginCommand = value; }
        }
        private void LoginSession()
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
                Message = "Some fields are empty";
            else
                Connect();
        }

        #endregion

        #region Methods

        private async void Connect()
        {
            IsLoadingSession = true;
            await Task.Run(() =>
            {
                try
                {
                    bool connect = _loginBM.Connect(Login, Password);
                    if (connect)
                        DispatchService.Invoke(() => PageMediator.Notify("Change_MainWindow_UC", EUserControl.MAIN, Login));
                    else
                        Message = "Wrong username or password";
                }
                catch (System.Exception e)
                {
                    if (e is CustomServerException)
                        DispatchService.Invoke(() => ShowRetryWindow());
                }
                finally
                {
                    IsLoadingSession = false;
                }
            });
        }

        private void ShowRetryWindow()
        {
            RetryLoginWindow retryLoginWindow = new RetryLoginWindow();
            retryLoginWindow.Show();
        }

        private void OnRetryListener(object obj)
        {
            if (!IsLoadingSession)
                Connect();
        }

        #endregion
    }
}
