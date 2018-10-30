using benais_jWPF_Medecin.BusinessManagement;
using benais_jWPF_Medecin.ServicePatientReference;
using benais_jWPF_Medecin.ViewModel.Pattern;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace benais_jWPF_Medecin.ViewModel
{
    public class PatientsViewModel: BaseViewModel
    {
        #region Variables

        private string _login;
        private PatientBM _patientBM;
        private ObservableCollection<Patient> _patientList;
        private Patient selectedPatient;

        #endregion

        #region Getters/Setters

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

        #endregion

        #region Constructor

        public PatientsViewModel(string login)
        {
            _patientBM = new PatientBM();
            PatientList = new ObservableCollection<Patient>(_patientBM.GetListPatient());
            //DeletePatientCommand = new RelayCommand(param => DeletePatient(), param => true);
            AddPatientCommand = new RelayCommand(param => ChangeView(), param => true);
            SelectedPatient = null;
        }

        #endregion

        #region Command

        //private ICommand _deletePatientCommand;
        //public ICommand DeletePatientCommand
        //{
        //    get { return _deletePatientCommand; }
        //    set { _deletePatientCommand = value; }
        //}
        //private void DeletePatient()
        //{
        //    if (SelectedPatient != null)
        //    {
        //        bool isDeleted = _patientBM.DeleteUser(SelectedPatient.Login);
        //        if (isDeleted)
        //        {
        //            UserList.Remove(SelectedPatient);
        //            SelectedPatient = null;
        //            MessageBox.Show("Deleted");
        //        }
        //        else
        //            MessageBox.Show("Fail delete user");
        //    }
        //}

        private ICommand _addPatientCommand;
        public ICommand AddPatientCommand
        {
            get { return _addPatientCommand; }
            set { _addPatientCommand = value; }
        }
        private void ChangeView()
        {
            Mediator.Notify("Change_Main_UC", Model.Enum.EUserControl.MAIN_PATIENTS_ADD, _login);
        }

        #endregion
    }
}
