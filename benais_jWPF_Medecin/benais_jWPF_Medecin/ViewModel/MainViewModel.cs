using benais_jWPF_Medecin.BusinessManagement;
using benais_jWPF_Medecin.Model.Enum;
using benais_jWPF_Medecin.Resources;
using benais_jWPF_Medecin.ServiceUserReference;
using benais_jWPF_Medecin.View;
using benais_jWPF_Medecin.View.Patient;
using benais_jWPF_Medecin.View.Usecases.Patient;
using benais_jWPF_Medecin.View.Usecases.PopupWindows;
using benais_jWPF_Medecin.ViewModel.Pattern;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace benais_jWPF_Medecin.ViewModel
{
    public class MainViewModel: BaseViewModel
    {
        #region Variables

        private UserBM _sessionBM;

        private string _login;
        private User _currentUser;
        private UserControl _currentUC;
        
        #endregion

        #region Getters/Setters

        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }
        public UserControl CurrentUC
        {
            get { return _currentUC; }
            set
            {
                _currentUC = value;
                OnPropertyChanged(nameof(CurrentUC));
            }
        }

        #endregion

        #region Constructor

        public MainViewModel(string login)
        {
            _login = login;
            _sessionBM = new UserBM(login);
            LogoutCommand = new RelayCommand(param => LogoutSession(), param => true);
            UsersViewCommand = new RelayCommand(param => LoadUsersView(), param => true);
            PatientsViewCommand = new RelayCommand(param => LoadPatientsView(), param => true);

            InitializeUser();
            LoadUsersView();

            PageMediator.Register("Change_Main_UC", OnChangeView);
        }

        #endregion

        #region Command

        private ICommand _logoutCommand;
        public ICommand LogoutCommand
        {
            get { return _logoutCommand; }
            set { _logoutCommand = value; }
        }
        private void LogoutSession()
        {
            if (_sessionBM.Disconnect())
                PageMediator.Notify("Change_MainWindow_UC", EUserControl.LOGIN, _login);
            else
                ShowServerExceptionWindow();
        }

        private ICommand _usersViewCommand;
        public ICommand UsersViewCommand
        {
            get { return _usersViewCommand; }
            set { _usersViewCommand = value; }
        }
        private void LoadUsersView()
        {
            CurrentUC = new UsersUC(_login);
        }

        private ICommand _patientsViewCommand;
        public ICommand PatientsViewCommand
        {
            get { return _patientsViewCommand; }
            set { _patientsViewCommand = value; }
        }
        private void LoadPatientsView()
        {
            CurrentUC = new PatientsUC(_login);
        }

        private void ShowServerExceptionWindow()
        {
            ServerExceptionWindow window = new ServerExceptionWindow(ErrorDescription.DISCONNECT);
            window.Show();
        }

        #endregion

        #region Method

        private void InitializeUser()
        {
            try
            {
                _currentUser = _sessionBM.GetUser();
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed get user");
            }
        }

        public void OnChangeView(EUserControl userControl, object param)
        {
            try
            {
                switch (userControl)
                {
                    case EUserControl.MAIN_USERS:
                        CurrentUC = new UsersUC(_login);
                        break;
                    case EUserControl.MAIN_USERS_ADD:
                        CurrentUC = new AddUserUC(_login);
                        break;
                    case EUserControl.MAIN_PATIENTS:
                        CurrentUC = new PatientsUC(_login);
                        break;
                    case EUserControl.MAIN_PATIENTS_ADD:
                        CurrentUC = new AddPatientUC(_login);
                        break;
                    case EUserControl.MAIN_PATIENTS_SINGLE:
                        CurrentUC = new SinglePatientUC(_login, (int) param);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
