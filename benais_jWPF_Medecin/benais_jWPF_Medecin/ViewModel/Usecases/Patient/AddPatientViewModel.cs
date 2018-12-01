using benais_jWPF_Medecin.BusinessManagement;
using benais_jWPF_Medecin.Model.Enum;
using benais_jWPF_Medecin.Resources;
using benais_jWPF_Medecin.ServicePatientReference;
using benais_jWPF_Medecin.View.Usecases.PopupWindows;
using benais_jWPF_Medecin.ViewModel.Pattern;
using benais_jWPF_Medecin.ViewModel.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace benais_jWPF_Medecin.ViewModel
{
    public class AddPatientViewModel: BaseViewModel
    {
        #region Variables

        private string _currentLogin;
        private PatientBM _patientBM;

        private string _name;
        private string _firstname;
        private DateTime _birthday;

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
        public DateTime Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        #endregion

        #region Constructor

        public AddPatientViewModel(string login)
        {
            _currentLogin = login;
            _patientBM = new PatientBM();
            InitializeCommands();
        }

        #endregion

        #region Command

        /// <summary>
        /// Add new patient if fields are corrects
        /// </summary>
        private ICommand _addPatientCommand;
        public ICommand AddPatientCommand
        {
            get { return _addPatientCommand; }
            set { _addPatientCommand = value; }
        }

        /// <summary>
        /// Add asynchronously a patient
        /// </summary>
        /// <returns></returns>
        private async Task AddPatient()
        {
            await Task.Run(() =>
            {
                try
                {
                    if (!Name.IsNullOrWhiteSpace() && !Firstname.IsNullOrWhiteSpace() && Birthday != null)
                    {
                        Patient patient = new Patient() { Name = Name, Firstname = Firstname, Birthday = Birthday, Observations = new List<Observation>().ToArray() };
                        if (_patientBM.AddPatient(patient))
                            DispatchService.Invoke(() => PageMediator.Notify("Change_Main_UC", EUserControl.MAIN_PATIENTS, _currentLogin));
                        else
                            DispatchService.Invoke(() => ShowServerExceptionWindow(ErrorDescription.ADD_PATIENT));
                    }
                    else
                    {
                        DispatchService.Invoke(() => ShowServerExceptionWindow(ErrorDescription.MISSING_FIELDS));
                    }
                }
                catch (Exception)
                {
                    DispatchService.Invoke(() => ShowServerExceptionWindow(ErrorDescription.ADD_PATIENT));
                }
            });
        }

        /// <summary>
        /// Load patients user control
        /// </summary>
        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get { return _backCommand; }
            set { _backCommand = value; }
        }
        private void Back()
        {
            PageMediator.Notify("Change_Main_UC", EUserControl.MAIN_PATIENTS, _currentLogin);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize all commands of the ViewModel
        /// </summary>
        private void InitializeCommands()
        {
            AddPatientCommand = new RelayCommand(param => AddPatient(), param => true);
            BackCommand = new RelayCommand(param => Back(), param => true);
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
