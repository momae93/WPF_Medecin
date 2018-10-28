﻿using benais_jWPF_Medecin.BusinessManagement;
using benais_jWPF_Medecin.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace benais_jWPF_Medecin.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        #region Variables
        private string _login;
        private string _password;
        private LoginBM _loginBM;
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
        public ICommand LoginCommand
        {
            get { return _loginCommand; }
            set { _loginCommand = value; }
        }
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            _loginBM = new LoginBM();
            LoginCommand = new RelayCommand(param => LoginSession(), param => true);
        }
        #endregion

        #region Command

        private ICommand _loginCommand;
        private void LoginSession()
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
                MessageBox.Show("Fields are empty");
            else
            {
                string message = (_loginBM.Connect(Login, Password)) ? "Success login" : "Fail login";
                MessageBox.Show(message);
            }
        }
        #endregion
    }
}