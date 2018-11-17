using benais_jWPF_Medecin.ViewModel;
using System.Windows.Controls;

namespace benais_jWPF_Medecin.View
{
    public class BaseUC<VM>: UserControl 
        where VM: BaseViewModel, new()
    {
        private VM _viewModel;

        public VM ViewModel
        {
            get { return _viewModel; }
            set
            {
                if (ViewModel == value)
                    return;
                _viewModel = value;

                this.DataContext = _viewModel;
            }
        }

        public BaseUC()
        {
            this._viewModel = new VM();
        }
    }
}
