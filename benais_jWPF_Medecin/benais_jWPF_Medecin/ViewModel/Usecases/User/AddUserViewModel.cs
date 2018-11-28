using benais_jWPF_Medecin.BusinessManagement;
using benais_jWPF_Medecin.Model.Enum;
using benais_jWPF_Medecin.ServiceUserReference;
using benais_jWPF_Medecin.View.Converters;
using benais_jWPF_Medecin.ViewModel.Pattern;
using benais_jWPF_Medecin.ViewModel.Utils;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace benais_jWPF_Medecin.ViewModel
{
    public class MainAddUserViewModel: BaseViewModel
    {
        #region Variables

        private string _currentLogin;
        private UserBM _sessionBM;

        private string _name;
        private string _firstname;
        private string _password;
        private string _login;
        private string _role;
        private byte[] _image;

        #endregion

        #region Getters/Setters

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Firstname
        {
            get { return _firstname; }
            set
            {
                _firstname = value;
                OnPropertyChanged(nameof(Firstname));
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
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }
        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }
        public byte[] Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        #endregion

        #region Constructor

        public MainAddUserViewModel(string login)
        {
            _currentLogin = login;
            _sessionBM = new UserBM(login);
            InitDefaultPicture();
            AddUserCommand = new RelayCommand(param => AddUser(), param => true);
            LoadImageCommand = new RelayCommand(param => LoadImage(), param => true);
            BackCommand = new RelayCommand(param => Back(), param => true);
            Firstname = "test";
        }

        private void InitDefaultPicture()
        {
            try
            {
                string filename = "default-picture.png";
                string resourceFolderPath = Path.GetFullPath(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\Images\")) + filename;
                Uri uri = new Uri(resourceFolderPath);
                BitmapImage bitmapImage = new BitmapImage(uri);
                Image = ImageToByteArray.Convert(bitmapImage);
            }
            catch (Exception)
            { }
        }

        #endregion

        #region Command

        /// <summary>
        /// Add new user if fields are corrects
        /// </summary>
        private ICommand _addUserCommand;
        public ICommand AddUserCommand
        {
            get { return _addUserCommand; }
            set { _addUserCommand = value; }
        }
        private void AddUser()
        {
            if (!Name.IsNullOrWhiteSpace() && !Firstname.IsNullOrWhiteSpace() &&
                !Password.IsNullOrWhiteSpace() && !Login.IsNullOrWhiteSpace() &&
                !Role.IsNullOrWhiteSpace())
            {
                User user = new User() { Name = Name, Firstname = Firstname, Pwd = Password, Login = Login, Role = Role, Picture = Image, Connected = false };
                if (_sessionBM.AddUser(user))
                    PageMediator.Notify("Change_Main_UC", EUserControl.MAIN_USERS, _currentLogin);
                else
                    MessageBox.Show("Fail add user");
            }
            else
            {
                MessageBox.Show("Fields are empty");
            }
        }

        /// <summary>
        /// Open a dialog to pick a file
        /// </summary>
        private ICommand _loadImageCommand;
        public ICommand LoadImageCommand
        {
            get { return _loadImageCommand; }
            set { _loadImageCommand = value; }
        }
        private void LoadImage()
        {
            var dialog = new System.Windows.Forms.OpenFileDialog();

            dialog.Title = "Select a picture";
            dialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            dialog.ShowDialog();

            if (!dialog.FileName.IsNullOrWhiteSpace())
            {
                BitmapImage img = new BitmapImage(new Uri(dialog.FileName));
                Image = ImageToByteArray.Convert(img);
            }
        }

        /// <summary>
        /// Load users user control
        /// </summary>
        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get { return _backCommand; }
            set { _backCommand = value; }
        }
        private void Back()
        {
            PageMediator.Notify("Change_Main_UC", EUserControl.MAIN_USERS, _currentLogin);
        }

        #endregion
    }
}
