using benais_jWPF_Medecin.BusinessManagement;
using benais_jWPF_Medecin.Model.Enum;
using benais_jWPF_Medecin.ServiceUserReference;
using benais_jWPF_Medecin.ViewModel.Pattern;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace benais_jWPF_Medecin.ViewModel
{
    public class UsersViewModel: BaseViewModel
    {
        #region Variables

        private UserBM _sessionBM;

        private string _currentLogin;
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
            _currentLogin = login;
            _sessionBM = new UserBM(login);
            UserList = new ObservableCollection<User>(_sessionBM.GetListUser());
            DeleteUserCommand = new RelayCommand(param => DeleteUser(), param => true);
            AddUserCommand = new RelayCommand(param => ChangeView(), param => true);
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
        private void ChangeView()
        {
            PageMediator.Notify("Change_Main_UC", EUserControl.MAIN_USERS_ADD, _currentLogin);
        }

        #endregion
    }
}