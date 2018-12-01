using benais_jWPF_Medecin.BusinessManagement;
using benais_jWPF_Medecin.Resources;
using benais_jWPF_Medecin.View.Usecases.PopupWindows;
using benais_jWPF_Medecin.ViewModel.Pattern;
using System.Threading;
using System.Threading.Tasks;

namespace benais_jWPF_Medecin.ViewModel.Usecases.Patient
{
    public class DetailsPatientViewModel : BaseViewModel
    {
        #region Variables

        private string _currentLogin;
        private int _idPatient;
        private PatientBM _patientBM;
        private ServicePatientReference.Patient _selectedPatient;
        private int _observationsNb;
        private bool _isLoading;

        #endregion

        #region Getters/Setters

        public ServicePatientReference.Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }
        public int ObservationsNb
        {
            get { return _observationsNb; }
            set
            {
                _observationsNb = value;
                OnPropertyChanged(nameof(ObservationsNb));
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

        public object DispatcherService { get; private set; }

        #endregion

        #region Constructor

        public DetailsPatientViewModel(string login, int idPatient)
        {
            _currentLogin = login;
            _idPatient = idPatient;
            _patientBM = new PatientBM();
            InitializePatient(idPatient);
        }

        #endregion

        #region Method

        /// <summary>
        /// Initialize patient infos
        /// </summary>
        /// <param name="idPatient"></param>
        /// <returns></returns>
        private async Task InitializePatient(int idPatient)
        {
            IsLoading = true;

            await Task.Run(() =>
                {
                    try
                    {
                        SelectedPatient = _patientBM.GetPatient(idPatient);
                        if (SelectedPatient.Observations != null)
                            ObservationsNb = SelectedPatient.Observations.Length;
                    }
                    catch
                    {
                        DispatchService.Invoke(() => ShowServerExceptionWindow(ErrorDescription.GETSINGLE_PATIENT));
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
