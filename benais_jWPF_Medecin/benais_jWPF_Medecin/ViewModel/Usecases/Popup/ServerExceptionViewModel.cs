using System.Windows;
using System.Windows.Input;

namespace benais_jWPF_Medecin.ViewModel.Usecases.Popup
{
    public class ServerExceptionViewModel: BaseViewModel
    {
        #region Constructor

        public ServerExceptionViewModel()
        {
            CloseWindowCommand = new RelayCommand(param => CloseWindows(), param => true);
        }

        #endregion

        #region Commands

        private ICommand _closeWindowCommand;

        public ICommand CloseWindowCommand
        {
            get { return _closeWindowCommand; }
            set
            {
                _closeWindowCommand = value;
                OnPropertyChanged(nameof(CloseWindowCommand));
            }
        }

        private void CloseWindows()
        {
            foreach (Window item in Application.Current.Windows)
                if (item.DataContext == this) item.Close();
        }

        #endregion
    }
}
