using benais_jWPF_Medecin.Model;
using benais_jWPF_Medecin.Resources;
using benais_jWPF_Medecin.View.Converters;
using benais_jWPF_Medecin.View.Usecases.PopupWindows;
using benais_jWPF_Medecin.ViewModel.Pattern;
using benais_jWPF_Medecin.ViewModel.Utils;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace benais_jWPF_Medecin.ViewModel.Usecases.Patient
{
    public class AddObservationViewModel: BaseViewModel
    {
        #region Variables

        private int _weight;
        private int _bloodPressure;
        private string _comment;
        private ObservableCollection<Prescription> _prescriptionCollection;
        private string _prescription;
        private bool _isPrescriptionsEmpty;
        private ObservableCollection<Picture> _picturesCollection;
        private bool _isPicturesEmpty;

        #endregion

        #region Getters/Setters

        public int Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));

            }
        }
        public int BloodPressure
        {
            get { return _bloodPressure; }
            set
            {
                _bloodPressure = value;
                OnPropertyChanged(nameof(BloodPressure));

            }
        }
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }
        public ObservableCollection<Prescription> PrescriptionCollection
        {
            get { return _prescriptionCollection; }
            set
            {
                _prescriptionCollection = value;
                OnPropertyChanged(nameof(PrescriptionCollection));
            }
        }
        public string Prescription
        {
            get { return _prescription; }
            set
            {
                _prescription = value;
                OnPropertyChanged(nameof(Prescription));
            }
        }
        public bool IsPrescriptionEmpty
        {
            get { return _isPrescriptionsEmpty; }
            set
            {
                _isPrescriptionsEmpty = value;
                OnPropertyChanged(nameof(IsPrescriptionEmpty));
            }
        }
        public ObservableCollection<Picture> PicturesCollection
        {
            get { return _picturesCollection; }
            set
            {
                _picturesCollection = value;
                OnPropertyChanged(nameof(PicturesCollection));
            }
        }
        public bool IsPicturesEmpty
        {
            get { return _isPicturesEmpty; }
            set
            {
                _isPicturesEmpty = value;
                OnPropertyChanged(nameof(IsPicturesEmpty));
            }
        }

        #endregion

        #region Constructor

        public AddObservationViewModel()
        {
            PrescriptionCollection = new ObservableCollection<Prescription>();
            PicturesCollection = new ObservableCollection<Picture>();
            IsPicturesEmpty = true;
            IsPrescriptionEmpty = true;
            InitializeCommands();
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
        /// <summary>
        /// Add the observation and raises a popup if missing fields
        /// </summary>
        private void AddObservation()
        {
            if (Weight <= 0 || BloodPressure <= 0)
                ShowServerExceptionWindow(ErrorDescription.MISSING_FIELDS);
            else
            {
                ServiceObservationReference.Observation observation = new ServiceObservationReference.Observation()
                {
                    BloodPressure = BloodPressure,
                    Weight = Weight,
                    Comment = Comment,
                    Date = DateTime.Now,
                    Pictures = PicturesCollection.Select(x => x.Content).ToArray(),
                    Prescription = PrescriptionCollection.Select(x => x.Content).ToArray()
                };
                Mediator.Notify("Observations_UC", observation);
            }
        }

        private ICommand _addPrescriptionCommand;
        public ICommand AddPrescriptionCommand
        {
            get { return _addPrescriptionCommand; }
            set
            {
                _addPrescriptionCommand = value;
                OnPropertyChanged(nameof(AddPrescriptionCommand));
            }
        }
        /// <summary>
        /// Add prescription
        /// </summary>
        private void AddPrescription()
        {
            if (!string.IsNullOrWhiteSpace(Prescription))
            {
                PrescriptionCollection.Add(new Prescription(Prescription));
                IsPrescriptionEmpty = false;
            }
            Prescription = "";
        }

        private ICommand _deletePrescriptionCommand;
        public ICommand DeletePrescriptionCommand
        {
            get { return _deletePrescriptionCommand; }
            set
            {
                _deletePrescriptionCommand = value;
                OnPropertyChanged(nameof(DeletePrescriptionCommand));
            }
        }
        /// <summary>
        /// Delete selected prescription
        /// </summary>
        /// <param name="id"></param>
        private void DeletePrescription(object id)
        {
            try
            {
                PrescriptionCollection.RemoveAll(x => x.Id == Convert.ToString(id));
                if (PrescriptionCollection.Count == 0)
                    IsPrescriptionEmpty = true;
            }
            catch (Exception)
            { }
        }

        private ICommand _addPictureCommand;
        /// <summary>
        /// Add a picture
        /// </summary>
        public ICommand AddPictureCommand
        {
            get { return _addPictureCommand; }
            set
            {
                _addPictureCommand = value;
                OnPropertyChanged(nameof(AddPictureCommand));
            }
        }
        private void AddPicture()
        {
            var dialog = new System.Windows.Forms.OpenFileDialog();

            dialog.Title = "Select a picture";
            dialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            dialog.ShowDialog();

            if (!dialog.FileName.IsNullOrWhiteSpace())
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(dialog.FileName));
                byte[] picture = ImageToByteArray.Convert(bitmapImage);
                PicturesCollection.Add(new Picture(picture));
                IsPicturesEmpty = false;
            }
        }


        private ICommand _deletePictureCommand;
        /// <summary>
        /// Delete selected picture
        /// </summary>
        public ICommand DeletePictureCommand
        {
            get { return _deletePictureCommand; }
            set
            {
                _deletePictureCommand = value;
                OnPropertyChanged(nameof(DeletePictureCommand));
            }
        }
        private void DeletePicture(object id)
        {
            try
            {
                PicturesCollection.RemoveAll(x => x.Id == Convert.ToString(id));
                if (PicturesCollection.Count == 0)
                    IsPicturesEmpty = true;
            }
            catch (Exception)
            { }
        }

        #endregion

        #region Methods

        private void InitializeCommands()
        {
            AddObservationCommand = new RelayCommand(param => AddObservation(), param => true);
            AddPrescriptionCommand = new RelayCommand(param => AddPrescription(), param => true);
            DeletePrescriptionCommand = new RelayCommand(param => DeletePrescription(param), param => true);
            AddPictureCommand = new RelayCommand(param => AddPicture(), param => true);
            DeletePictureCommand = new RelayCommand(param => DeletePicture(param), param => true);
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
