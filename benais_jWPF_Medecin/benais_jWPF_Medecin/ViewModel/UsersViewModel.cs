﻿using benais_jWPF_Medecin.BusinessManagement;
using benais_jWPF_Medecin.ServiceUserReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace benais_jWPF_Medecin.ViewModel
{
    public class UsersViewModel: BaseViewModel
    {
        #region Variables

        private SessionBM _sessionBM;

        private string _login;
        private ObservableCollection<User> _userList;
        private User _selectedUser;
        
        #endregion

        #region Getters/Setters

        public ObservableCollection<User> UserList
        {
            get { return _userList; }
            set
            {
                _userList = value;
                OnPropertyChanged(nameof(UserList));
            }
        }
        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        #endregion

        #region Constructor

        public UsersViewModel(string login)
        {
            _login = login;
            _sessionBM = new SessionBM(login);
            UserList = new ObservableCollection<User>(_sessionBM.GetListUser());
            DeleteUserCommand = new RelayCommand(param => DeleteUser(), param => true);
            AddUserCommand = new RelayCommand(param => DeleteUser(), param => true);
            SelectedUser = null;
        }

        #endregion

        #region Command

        private ICommand _deleteUserCommand;
        public ICommand DeleteUserCommand
        {
            get { return _deleteUserCommand; }
            set { _deleteUserCommand = value; }
        }
        private void DeleteUser()
        {
            if (SelectedUser != null)
            {
                bool isDeleted = _sessionBM.DeleteUser(SelectedUser.Login);
                if (isDeleted)
                {
                    UserList.Remove(SelectedUser);
                    SelectedUser = null;
                    MessageBox.Show("Deleted");
                }
                else
                    MessageBox.Show("Fail delete user");
            }
        }

        private ICommand _addUserCommand;
        public ICommand AddUserCommand
        {
            get { return _addUserCommand; }
            set { _addUserCommand = value; }
        }
        private void AddUser()
        {

        }

        #endregion
    }
}