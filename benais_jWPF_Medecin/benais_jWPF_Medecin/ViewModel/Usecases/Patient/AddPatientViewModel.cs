using benais_jWPF_Medecin.BusinessManagement;
using benais_jWPF_Medecin.Model.Enum;
using benais_jWPF_Medecin.ServicePatientReference;
using benais_jWPF_Medecin.ViewModel.Pattern;
using benais_jWPF_Medecin.ViewModel.Utils;
using System;
using System.Collections.Generic;
using System.Windows;
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

            AddPatientCommand = new RelayCommand(param => AddPatient(), param => true);
            BackCommand = new RelayCommand(param => Back(), param => true);
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
        private void AddPatient()
        {
            if (!Name.IsNullOrWhiteSpace() && !Firstname.IsNullOrWhiteSpace() && Birthday != null)
            {
                Patient patient = new Patient() { Name = Name, Firstname = Firstname, Birthday = Birthday, Observations = new List<Observation>().ToArray() };
                if (_patientBM.AddPatient(patient))
                    Mediator.Notify("Change_Main_UC", EUserControl.MAIN_PATIENTS, _currentLogin);
                else
                    MessageBox.Show("Fail add user");
            }
            else
            {
                MessageBox.Show("Fields are empty");
            }
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
            Mediator.Notify("Change_Main_UC", EUserControl.MAIN_PATIENTS, _currentLogin);
        }

        #endregion
    }
}
