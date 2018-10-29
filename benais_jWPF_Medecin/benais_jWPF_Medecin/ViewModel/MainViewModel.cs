using benais_jWPF_Medecin.BusinessManagement;
using benais_jWPF_Medecin.Model.Enum;
using benais_jWPF_Medecin.ServiceUserReference;
using benais_jWPF_Medecin.View;
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

        private SessionBM _sessionBM;

        private string _login;
        private User _currentUser;
        private UserControl _currentUC;
        
        #endregion

        #region Getters/Setters

        public ICommand LogoutCommand
        {
            get { return _logoutCommand; }
            set { _logoutCommand = value; }
        }
        public ICommand UsersViewCommand
        {
            get { return _usersViewCommand; }
            set { _usersViewCommand = value; }
        }

        public ICommand PatientsViewCommand
        {
            get { return _patientsViewCommand; }
            set { _patientsViewCommand = value; }
        }


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
            _sessionBM = new SessionBM(login);
            LogoutCommand = new RelayCommand(param => LogoutSession(), param => true);
            UsersViewCommand = new RelayCommand(param => LoadUsersView(), param => true);
            PatientsViewCommand = new RelayCommand(param => LoadPatientsView(), param => true);

            InitializeUser();
            LoadUsersView();
        }

        #endregion

        #region Command
        private ICommand _logoutCommand;
        private ICommand _usersViewCommand;
        private ICommand _patientsViewCommand;

        private void LogoutSession()
        {
            if (_sessionBM.Disconnect())
            {
                MessageBox.Show("Success logout");
                Mediator.Notify("Change_Main_UC", EUserControl.LOGIN, _login);
            }
            else
            {
                MessageBox.Show("Fail logout");
            }
        }
        private void LoadUsersView()
        {
            CurrentUC = new MainUsersUC(_login);
        }
        private void LoadPatientsView()
        {
            CurrentUC = new MainPatientsUC();
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

        #endregion
    }
}
