using benais_jWPF_Medecin.BusinessManagement;
using benais_jWPF_Medecin.ServicePatientReference;
using benais_jWPF_Medecin.ViewModel.Pattern;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace benais_jWPF_Medecin.ViewModel.Usecases.Patient
{
    public class ChartPatientViewModel: BaseViewModel
    {
        #region Variables

        private string _currentLogin;
        private int _idPatient;
        private PatientBM _patientBM;
        private SeriesCollection _seriesCollection; 
        private List<string> _dates;
        private bool _isLoading;

        #endregion

        #region Getters/Setters

        public SeriesCollection SeriesCollection
        {
            get { return _seriesCollection; }
            set
            {
                _seriesCollection = value;
                OnPropertyChanged(nameof(SeriesCollection));
            }
        }
        public List<string> Dates
        {
            get { return _dates; }
            set
            {
                _dates = value;
                OnPropertyChanged(nameof(Dates));
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

        #endregion

        #region Constructor

        public ChartPatientViewModel(string login, int idPatient)
        {
            _currentLogin = login;
            _idPatient = idPatient;
            _patientBM = new PatientBM();
            IsLoading = true;
            InitializeGraph();
            FetchChartData(idPatient);
        }

        #endregion

        #region Method

        private void InitializeGraph()
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Weight",
                    Values = new ChartValues<int>()
                },
                new LineSeries
                {
                    Title = "Blood pressure",
                    Values = new ChartValues<int>()
                }
            };
        }

        private async Task FetchChartData(int idPatient)
        {
            IsLoading = true;
            try
            {
                await Task.Run(() =>
                {
                    ServicePatientReference.Patient _selectedPatient = _patientBM.GetPatient(idPatient);
                    List<Observation> observations = _selectedPatient.Observations.OrderBy(x => x.Date).ToList();
                    List<int> weightList = observations.Select(x => x.Weight).ToList();
                    List<int> pressureList = observations.Select(x => x.BloodPressure).ToList();
                    List<string> dateList = observations.Select(x => x.Date.ToString()).ToList();

                    DispatchService.Invoke(() => UpdateGraph(weightList, pressureList, dateList));
                });
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void UpdateGraph(List<int> weightList, List<int> pressureList, List<string> dateList)
        {
            SeriesCollection[0].Values = weightList.AsChartValues();
            SeriesCollection[1].Values = pressureList.AsChartValues();
            Dates = dateList;
        }

        #endregion
    }
}
