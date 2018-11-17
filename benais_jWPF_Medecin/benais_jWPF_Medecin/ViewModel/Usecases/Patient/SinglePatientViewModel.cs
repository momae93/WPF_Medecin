﻿using benais_jWPF_Medecin.Model.Enum;
using benais_jWPF_Medecin.View.Usecases.Patient;
using benais_jWPF_Medecin.ViewModel.Pattern;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace benais_jWPF_Medecin.ViewModel.Usecases.Patient
{
    public class SinglePatientViewModel : BaseViewModel
    {
        #region Variables

        private string _currentLogin;
        private int _idPatient;
        private int _selectedIndex;

        private UserControl _detailsUC;
        private UserControl _observationsUC;
        private UserControl _chartUC;
        private UserControl _liveUC;

        #endregion

        #region Getters/Setters

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
                ChangeUC(value);
            }
        }

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
        public UserControl ChartUC
        {
            get { return _chartUC; }
            set
            {
                _chartUC = value;
                OnPropertyChanged(nameof(ChartUC));
            }
        }
        public UserControl LiveUC
        {
            get { return _liveUC; }
            set
            {
                _liveUC = value;
                OnPropertyChanged(nameof(LiveUC));
            }
        }

        #endregion

        #region Constructor

        public SinglePatientViewModel(string login, int idPatient)
        {
            _currentLogin = login;
            _idPatient = idPatient;
            DetailsUC = new DetailsPatientUC(_currentLogin, _idPatient);
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

        private void ChangeUC(int index)
        {
            try
            {
                EPatientUserControl userControl = (EPatientUserControl)index;
                switch (userControl)
                {
                    case EPatientUserControl.DETAILS:
                        DetailsUC = new DetailsPatientUC(_currentLogin, _idPatient);
                        DetailsUC.UpdateDefaultStyle();
                        break;
                    case EPatientUserControl.OBSERVATIONS:
                        break;
                    case EPatientUserControl.GRAPH:
                        ChartUC = new ChartPatientUC(_currentLogin, _idPatient);
                        DetailsUC.UpdateDefaultStyle();
                        break;
                    case EPatientUserControl.LIVE:
                        LiveUC = new LivePatientUC(_currentLogin, _idPatient);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region Method


        #endregion
    }
}
