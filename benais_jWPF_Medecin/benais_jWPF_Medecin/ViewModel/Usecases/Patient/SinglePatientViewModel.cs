using benais_jWPF_Medecin.Model.Enum;
using benais_jWPF_Medecin.View.Usecases.Patient;
using benais_jWPF_Medecin.ViewModel.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace benais_jWPF_Medecin.ViewModel.Usecases.Patient
{
    public class SinglePatientViewModel : BaseViewModel
    {
        #region Variables

        private string _currentLogin;
        private int _idPatient;

        private UserControl _detailsUC;
        private UserControl _observationsUC;
        private UserControl _graphUC;

        #endregion

        #region Getters/Setters

        public UserControl DetailsUC
        {
            get { return _detailsUC; }
            set
            {
                _detailsUC = value;
                OnPropertyChanged(nameof(DetailsUC));
            }
        }
        public UserControl ObservationsUC
        {
            get { return _observationsUC; }
            set
            {
                _observationsUC = value;
                OnPropertyChanged(nameof(ObservationsUC));
            }
        }
        public UserControl GraphUC
        {
            get { return _graphUC; }
            set
            {
                _graphUC = value;
                OnPropertyChanged(nameof(GraphUC));
            }
        }

        #endregion

        #region Constructor

        public SinglePatientViewModel(string login, int idPatient)
        {
            _currentLogin = login;
            _idPatient = idPatient;
            DetailsUC = new DetailsPatientUC(login, idPatient);

            BackCommand = new RelayCommand(param => Back(), param => true);
        }

        #endregion

        #region Command

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

        #region Method


        #endregion
    }
}
