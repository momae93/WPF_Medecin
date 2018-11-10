using benais_jWPF_Medecin.BusinessManagement;
using benais_jWPF_Medecin.ServicePatientReference;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.Linq;

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

        #endregion

        #region Constructor

        public ChartPatientViewModel(string login, int idPatient)
        {
            _currentLogin = login;
            _idPatient = idPatient;
            _patientBM = new PatientBM();
            InitializeGraph(idPatient);
        }

        #endregion

        #region Method

        private void InitializeGraph(int idPatient)
        {
            ServicePatientReference.Patient _selectedPatient = _patientBM.GetPatient(idPatient);
            List<Observation> observations = _selectedPatient.Observations.OrderBy(x => x.Date).ToList();
            List<int> weightList = observations.Select(x => x.Weight).ToList();
            List<int> pressureList = observations.Select(x => x.BloodPressure).ToList();
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Weight",
                    Values = weightList.AsChartValues<int>()
                },
                new LineSeries
                {
                    Title = "Blood pressure",
                    Values = pressureList.AsChartValues<int>()
                }
            };
            Dates = observations.Select(x => x.Date.ToString()).ToList();
        }

        #endregion
    }
}
