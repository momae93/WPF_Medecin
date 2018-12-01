using benais_jWPF_Medecin.BusinessManagement;
using benais_jWPF_Medecin.Model.Enum;
using benais_jWPF_Medecin.Resources;
using benais_jWPF_Medecin.ServiceUserReference;
using benais_jWPF_Medecin.View.Usecases.PopupWindows;
using benais_jWPF_Medecin.ViewModel.Pattern;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace benais_jWPF_Medecin.ViewModel
{
    public class UsersViewModel : BaseViewModel
    {
        #region Variables

        private UserBM _sessionBM;

        private string _currentLogin;
        private bool _isReadOnly;
        private ObservableCollection<User> _userList;
        private User _selectedUser;
        private bool _isLoading;
        private bool _showUsersGrid;

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
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                _isReadOnly = value;
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        public bool ShowUsersGrid
        {
            get { return _showUsersGrid; }
            set
            {
                _showUsersGrid = value;
                OnPropertyChanged(nameof(ShowUsersGrid));
            }
        }

        #endregion

        #region Constructor

        public UsersViewModel(string login)
        {
            _currentLogin = login;
            _sessionBM = new UserBM(login);
            IsLoading = false;
            IsReadOnly = _sessionBM.IsUserReadOnly(login);
            UserList = new ObservableCollection<User>();
            SelectedUser = null;

            InitializeCommands();
            InitializeUsers();
        }

        #endregion

        #region Command

        private ICommand _deleteUserCommand;
        public ICommand DeleteUserCommand
        {
            get { return _deleteUserCommand; }
            set { _deleteUserCommand = value; }
        }
        /// <summary>
        /// Run async task to delete user
        /// </summary>
        private async void DeleteUser()
        {
            await Task.Run(() =>
            {
                try
                {
                    if (SelectedUser != null)
                    {
                        bool isDeleted = _sessionBM.DeleteUser(SelectedUser.Login);
                        if (isDeleted)
                        {
                            DispatchService.Invoke(() => {
                                UserList.Remove(SelectedUser);
                                SelectedUser = null;
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    DispatchService.Invoke(() => ShowServerExceptionWindow(ErrorDescription.DELETE_USER));
                }
            });
        }

        private ICommand _addUserCommand;
        public ICommand AddUserCommand
        {
            get { return _addUserCommand; }
            set { _addUserCommand = value; }
        }

        /// <summary>
        /// Go to add user view
        /// </summary>
        private void ChangeView()
        {
            PageMediator.Notify("Change_Main_UC", EUserControl.MAIN_USERS_ADD, _currentLogin);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize all commands
        /// </summary>
        private void InitializeCommands()
        {
            DeleteUserCommand = new RelayCommand(param => DeleteUser(), param => true);
            AddUserCommand = new RelayCommand(param => ChangeView(), param => true);
        }

        /// <summary>
        /// Fetch asynchronously the users
        /// </summary>
        /// <returns></returns>
        private async Task InitializeUsers()
        {
            IsLoading = true;
            await Task.Run(() =>
            {
                try
                {
                    UserList = new ObservableCollection<User>(_sessionBM.GetListUser());
                    ShowUsersGrid = UserList.Count != 0;
                }
                catch (Exception)
                {
                    DispatchService.Invoke(() => ShowServerExceptionWindow(ErrorDescription.GETALL_USER));
                }
                finally
                {
                    IsLoading = false;
                }
            });
        }

        /// <summary>
        /// Show pop up with custom message
        /// </summary>
        /// <param name="description"></param>
        private void ShowServerExceptionWindow(string description)
        {
            ServerExceptionWindow serverExceptionWindow = new ServerExceptionWindow(description);
            serverExceptionWindow.Show();
        }

        #endregion
    }
}