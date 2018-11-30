using LiveCharts;
using LiveCharts.Wpf;
using benais_jWPF_Medecin.BusinessManagement;
using benais_jWPF_Medecin.ViewModel.Pattern;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using LiveCharts.Helpers;

namespace benais_jWPF_Medecin.ViewModel.Usecases.Patient
{
    public class ObservationsViewModel: BaseViewModel
    {
        #region Variables

        private string _currentLogin;
        private int _idPatient;
        private bool _isLoading;
        private bool _isAddView;
        private PatientBM _patientBM;
        private ServicePatientReference.Patient _selectedPatient;
        private ServicePatientReference.Observation _selectedObservation;
        private ObservableCollection<ServicePatientReference.Observation> _observationList;

        private ObservableCollection<string> _datesCollection;
        private SeriesCollection _bloodPressureCollection;
        private SeriesCollection _weightCollection;
            
        #endregion

        #region Getters/Setters

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        public bool IsAddView
        {
            get { return _isAddView; }
            set
            {
                _isAddView = value;
                OnPropertyChanged(nameof(IsAddView));
            }
        }
        public ServicePatientReference.Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }
        public ServicePatientReference.Observation SelectedObservation
        {
            get { return _selectedObservation; }
            set
            {
                _selectedObservation = value;
                OnPropertyChanged(nameof(SelectedObservation));
            }
        }
        public ObservableCollection<ServicePatientReference.Observation> ObservationsList
        {
            get { return _observationList; }
            set
            {
                _observationList = value;
                OnPropertyChanged(nameof(ObservationsList));
            }
        }
        public ObservableCollection<string> DatesCollection
        {
            get { return _datesCollection; }
            set
            {
                _datesCollection = value;
                OnPropertyChanged(nameof(DatesCollection));
            }
        }
        public SeriesCollection BloodPressureCollection
        {
            get { return _bloodPressureCollection; }
            set
            {
                _bloodPressureCollection = value;
                OnPropertyChanged(nameof(BloodPressureCollection));
            }
        }
        public SeriesCollection WeightCollection
        {
            get { return _weightCollection; }
            set
            {
                _weightCollection = value;
                OnPropertyChanged(nameof(WeightCollection));
            }
        }
        
        #endregion

        #region Constructor

        public ObservationsViewModel(string currentLogin, int idPatient)
        {
            _currentLogin = currentLogin;
            _idPatient = idPatient;
            _patientBM = new PatientBM();
            ObservationsList = new ObservableCollection<ServicePatientReference.Observation>();
            IsAddView = false;
            InitializeCommands();
            InitializeGraph();
            InitializePatient(idPatient);
        }

        #endregion

        #region Commands

        private ICommand _addObservationCommand;
        public ICommand AddObservationCommand
        {
            get { return _addObservationCommand; }
            set
            {
                _addObservationCommand = value;
                OnPropertyChanged(nameof(AddObservationCommand));
            }
        }

        private ICommand _cancelAddObservationCommand;
        public ICommand CancelAddObservationCommand
        {
            get { return _cancelAddObservationCommand; }
            set
            {
                _cancelAddObservationCommand = value;
                OnPropertyChanged(nameof(CancelAddObservationCommand));
            }
        }

        private ICommand _confirmObservationCommand;
        public ICommand ConfirmObservationCommand
        {
            get { return _confirmObservationCommand; }
            set
            {
                _confirmObservationCommand = value;
                OnPropertyChanged(nameof(ConfirmObservationCommand));
            }
        }

        /// <summary>
        /// Add observation in database
        /// </summary>
        private void ConfirmObservation()
        {
            //FIXME
        }

        private ICommand _addObservationPictureCommand;
        public ICommand AddObservationPictureCommand
        {
            get { return _addObservationPictureCommand; }
            set
            {
                _addObservationPictureCommand = value;
                OnPropertyChanged(nameof(AddObservationPictureCommand));
            }
        }

        /// <summary>
        /// Add pictures into existing list of picture
        /// </summary>
        private void AddObservationPicture()
        {
            //FIXME
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize all commands of the ViewModel
        /// </summary>
        private void InitializeCommands()
        {
            AddObservationCommand = new RelayCommand(param => IsAddView = true, param => true);
            CancelAddObservationCommand = new RelayCommand(param => IsAddView = false, param => true);
            ConfirmObservationCommand = new RelayCommand(param => ConfirmObservation(), param => true);
            AddObservationPictureCommand = new RelayCommand(param => AddObservationPicture(), param => true);
        }

        /// <summary>
        /// Fetch user data asynchronously
        /// </summary>
        /// <param name="idPatient"></param>
        /// <returns></returns>
        private async Task InitializePatient(int idPatient)
        {
            IsLoading = true;
            try
            {
                await Task.Run(() =>
                {
                    SelectedPatient = _patientBM.GetPatient(idPatient);
                    ObservableCollection<ServicePatientReference.Observation> list = new ObservableCollection<ServicePatientReference.Observation>();
                    List<int> weightList = new List<int>();
                    List<int> pressureList = new List<int>();
                    List<string> dateList = new List<string>();
                    foreach (ServicePatientReference.Observation observation in SelectedPatient.Observations)
                    {
                        list.Add(observation);
                        weightList.Add(observation.Weight);
                        pressureList.Add(observation.Weight);
                        dateList.Add(observation.Date.ToString());
                    }
                    DispatchService.Invoke(() => {
                        ObservationsList = list;
                        UpdateGraph(weightList, pressureList, dateList);
                    });
                });
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// Initialize graph
        /// </summary>
        private void InitializeGraph()
        {
            BloodPressureCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Blood pressure",
                    Values = new ChartValues<double>(),
                }
            };

            DatesCollection = new ObservableCollection<string>();

            WeightCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Weight",
                    Values = new ChartValues<double>(),
                }
            };
        }

        private void UpdateGraph(List<int> weightList, List<int> pressureList, List<string> dateList)
        {
            BloodPressureCollection[0].Values = weightList.AsChartValues();
            WeightCollection[0].Values = pressureList.AsChartValues();
            DatesCollection = new ObservableCollection<string>(dateList);
        }

        #endregion
    }
}
