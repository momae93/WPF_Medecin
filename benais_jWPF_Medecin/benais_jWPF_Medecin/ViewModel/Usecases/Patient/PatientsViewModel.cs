using benais_jWPF_Medecin.BusinessManagement;
using benais_jWPF_Medecin.Resources;
using benais_jWPF_Medecin.ServicePatientReference;
using benais_jWPF_Medecin.View.Usecases.PopupWindows;
using benais_jWPF_Medecin.ViewModel.Pattern;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace benais_jWPF_Medecin.ViewModel
{
    public class PatientsViewModel: BaseViewModel
    {
        #region Variables

        private string _currentLogin;

        private PatientBM _patientBM;
        private UserBM _userBM;

        private bool _isReadOnly;
        private ObservableCollection<Patient> _patientList;
        private Patient selectedPatient;

        private bool _isLoading;
        private bool _showPatientsGrid;
        
        #endregion

        #region Getters/Setters

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                _isReadOnly = value;
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }
        public ObservableCollection<Patient> PatientList
        {
            get { return _patientList; }
            set
            {
                _patientList = value;
                OnPropertyChanged(nameof(PatientList));
            }
        }
        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
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
        public bool ShowPatientsGrid
        {
            get { return _showPatientsGrid; }
            set
            {
                _showPatientsGrid = value;
                OnPropertyChanged(nameof(ShowPatientsGrid));
            }
        }

        #endregion

        #region Constructor

        public PatientsViewModel(string login)
        {
            this._currentLogin = login;
            _patientBM = new PatientBM();
            _userBM = new UserBM(login);
            IsReadOnly = _userBM.IsUserReadOnly(login);
            PatientList = new ObservableCollection<Patient>();
            SelectedPatient = null;

            InitializeCommands();
            InitializePatient();
        }

        #endregion

        #region Command

        private ICommand _deletePatientCommand;
        public ICommand DeletePatientCommand
        {
            get { return _deletePatientCommand; }
            set { _deletePatientCommand = value; }
        }
        /// <summary>
        /// Run async task to delete patient
        /// </summary>
        private async void DeletePatient()
        {
            await Task.Run(() =>
            {
                try
                {
                    if (SelectedPatient != null)
                    {
                        bool isDeleted = _patientBM.DeletePatient(SelectedPatient.Id);
                        if (isDeleted)
                        {
                            DispatchService.Invoke(() => {
                                PatientList.Remove(SelectedPatient);
                                SelectedPatient = null;
                            });
                        }
                        else
                            DispatchService.Invoke(() => ShowServerExceptionWindow(ErrorDescription.DELETE_PATIENT));
                    }
                }
                catch (Exception)
                {
                    DispatchService.Invoke(() => ShowServerExceptionWindow(ErrorDescription.DELETE_PATIENT));
                }
            });
        }


        private ICommand _addPatientCommand;
        public ICommand AddPatientCommand
        {
            get { return _addPatientCommand; }
            set { _addPatientCommand = value; }
        }
        /// <summary>
        /// Go to add patient view
        /// </summary>
        private void ChangeView()
        {
            PageMediator.Notify("Change_Main_UC", Model.Enum.EUserControl.MAIN_PATIENTS_ADD, _currentLogin);
        }

        private ICommand _detailsCommand;
        public ICommand DetailsPatientCommand
        {
            get { return _detailsCommand; }
            set { _detailsCommand = value; }
        }

        /// <summary>
        /// Show patient information details if patient selected
        /// </summary>
        private void ShowPatientDetails()
        {
            if (SelectedPatient != null)
                PageMediator.Notify("Change_Main_UC", Model.Enum.EUserControl.MAIN_PATIENTS_SINGLE, SelectedPatient.Id);
        }

        #endregion

        #region Methods

        private void InitializeCommands()
        {
            DeletePatientCommand = new RelayCommand(param => DeletePatient(), param => true);
            AddPatientCommand = new RelayCommand(param => ChangeView(), param => true);
            DetailsPatientCommand = new RelayCommand(param => ShowPatientDetails(), param => true);
        }

        /// <summary>
        /// Fetch asynchronously the patients
        /// </summary>
        /// <returns></returns>
        private async Task InitializePatient()
        {
            IsLoading = true;
            await Task.Run(() =>
            {
                try
                {
                    PatientList = new ObservableCollection<Patient>(_patientBM.GetListPatient());
                    ShowPatientsGrid = PatientList.Count != 0;
                }
                catch (Exception)
                {
                    DispatchService.Invoke(() => ShowServerExceptionWindow(ErrorDescription.GETALL_PATIENT));
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
