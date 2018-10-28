using benais_jWPF_Medecin.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace benais_jWPF_Medecin.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private UserControl _currentUC;
        public UserControl CurrentUC
        {
            get { return _currentUC; }
            set
            {
                _currentUC = value;
                OnPropertyChanged(nameof(CurrentUC));
            }
        }

        public MainWindowViewModel()
        {
            CurrentUC = new LoginUC();
        }
    }
}
