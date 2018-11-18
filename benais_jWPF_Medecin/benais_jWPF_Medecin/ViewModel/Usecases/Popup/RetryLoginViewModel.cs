using benais_jWPF_Medecin.View.Usecases.PopupWindows;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace benais_jWPF_Medecin.ViewModel.Usecases.Popup
{
    public class RetryLoginViewModel: BaseViewModel
    {
        #region Variables

        private BackgroundWorker _backgroundWorker;
        private int _time;
        
        #endregion

        #region Getter / Setter

        public int Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        #endregion

        #region Constructor

        public RetryLoginViewModel()
        {
            CancelCommand = new RelayCommand((param) => CloseWindows());
            RetryCommand = new RelayCommand((param) => CloseWindows());
            StartAyncTimer();
        }

        #endregion

        #region Method

        private void StartAyncTimer()
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.ProgressChanged += _backgroundWorker_ProgressChanged;
            _backgroundWorker.RunWorkerAsync();
        }

        private void CloseWindows()
        {
            foreach (Window item in Application.Current.Windows)
                if (item.DataContext == this) item.Close();
        }

        #endregion

        #region Worker

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            for (int i = 0; i < 11; i++)
            {
                worker.ReportProgress(i);
                Thread.Sleep(1000);
            }
        }

        private void _backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Time = e.ProgressPercentage;
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CloseWindows();
        }

        #endregion

        #region Commands

        private ICommand _cancelCommand;

        public ICommand CancelCommand
        {
            get { return _cancelCommand; }
            set { _cancelCommand = value; }
        }

        private ICommand _retryCommand;

        public ICommand RetryCommand
        {
            get { return _retryCommand; }
            set { _retryCommand = value; }
        }
        #endregion
    }
}
