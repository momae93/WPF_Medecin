using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Threading.Tasks;

namespace benais_jWPF_Medecin.ViewModel.Usecases.Patient
{
    public class LivePatientViewModel : BaseViewModel, ServiceLiveReference.IServiceLiveCallback
    {
        #region Variables

        private string _currentLogin;
        private int _idPatient;

        private SeriesCollection _tempatureCollection;
        private ObservableCollection<string> _tempatureDates;
        private SeriesCollection _heartbeatCollection;
        private ObservableCollection<string> _heartbeatDates;
        private bool _isLiveHeatbeat;
        private bool _isLoading;

        #endregion

        #region Getters/Setters

        public SeriesCollection TemperatureCollection
        {
            get { return _tempatureCollection; }
            set
            {
                _tempatureCollection = value;
                OnPropertyChanged(nameof(TemperatureCollection));
            }
        }
        public ObservableCollection<string> TemperatureDates
        {
            get { return _tempatureDates; }
            set
            {
                _tempatureDates = value;
                OnPropertyChanged(nameof(TemperatureDates));
            }
        }
        public SeriesCollection HeartbeatCollection
        {
            get { return _heartbeatCollection; }
            set
            {
                _heartbeatCollection = value;
                OnPropertyChanged(nameof(HeartbeatCollection));
            }
        }
        public ObservableCollection<string> HeartbeatDates
        {
            get { return _heartbeatDates; }
            set
            {
                _heartbeatDates = value;
                OnPropertyChanged(nameof(HeartbeatDates));
            }
        }
        public bool IsLiveHeartbeat
        {
            get { return _isLiveHeatbeat; }
            set
            {
                _isLiveHeatbeat = value;
                OnPropertyChanged(nameof(IsLiveHeartbeat));
                ResetData(value);
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

        public LivePatientViewModel(string login, int idPatient)
        {
            this._currentLogin = login;
            this._idPatient = idPatient;
            IsLoading = true;
            InitializeGraph();
            Task.Run(() =>
            {
                IsLiveHeartbeat = true;
                InstanceContext instanceContext = new InstanceContext(this);
                ServiceLiveReference.ServiceLiveClient client = new ServiceLiveReference.ServiceLiveClient(instanceContext);
                client.Subscribe();
                IsLoading = false;
            });
        }

        #endregion

        #region Methods

        private void InitializeGraph()
        {
            TemperatureCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Temperature",
                    Values = new ChartValues<double>(),
                }
            };
            TemperatureDates = new ObservableCollection<string>();

            HeartbeatCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Heartbeat",
                    Values = new ChartValues<double>(),
                }
            };
            HeartbeatDates = new ObservableCollection<string>();
        }

        public void PushDataHeart(double requestData)
        {
            if (HeartbeatCollection[0].Values.Count > 30)
            {
                HeartbeatCollection[0].Values.Clear();
                HeartbeatDates.Clear();
            }
            HeartbeatCollection[0].Values.Add(requestData);
            HeartbeatDates.Add(DateTime.Now.ToString());
        }

        public void PushDataTemp(double requestData)
        {
            if (TemperatureCollection[0].Values.Count > 10)
            {
                TemperatureCollection[0].Values.Clear();
                TemperatureDates.Clear();
            }
            TemperatureCollection[0].Values.Add(requestData);
            TemperatureDates.Add(DateTime.Now.ToString());
        }

        private void ResetData(bool isLiveHeartbeat)
        {
            if (isLiveHeartbeat)
            {
                HeartbeatCollection[0].Values.Clear();
                HeartbeatDates.Clear();
            }
            else
            {
                TemperatureCollection[0].Values.Clear();
                TemperatureDates.Clear();
            }
        }

        #endregion
    }
}
