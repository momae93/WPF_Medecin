using benais_jWPF_Medecin.BusinessManagement;

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

        private void InitializePatient(int idPatient)
        {
            SelectedPatient = _patientBM.GetPatient(idPatient);
            if (SelectedPatient.Observations != null)
                ObservationsNb = SelectedPatient.Observations.Length;
        }

        #endregion
    }
}
