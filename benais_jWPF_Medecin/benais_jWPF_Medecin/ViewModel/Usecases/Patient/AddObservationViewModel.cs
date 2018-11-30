using benais_jWPF_Medecin.Model;
using benais_jWPF_Medecin.View.Converters;
using benais_jWPF_Medecin.ViewModel.Pattern;
using benais_jWPF_Medecin.ViewModel.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows;
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
        private ObservableCollection<Picture> _picturesCollection;
        private string _prescription;
        private int _selectedPrescriptionIndex;

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
        public ObservableCollection<Picture> PicturesCollection
        {
            get { return _picturesCollection; }
            set
            {
                _picturesCollection = value;
                OnPropertyChanged(nameof(PicturesCollection));
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
        public int SelectedPrescriptionIndex
        {
            get { return _selectedPrescriptionIndex; }
            set
            {
                _selectedPrescriptionIndex = value;
                OnPropertyChanged(nameof(SelectedPrescriptionIndex));
            }
        }

        #endregion

        #region Constructor

        public AddObservationViewModel()
        {
            PrescriptionCollection = new ObservableCollection<Prescription>();
            PicturesCollection = new ObservableCollection<Picture>();
            AddObservationCommand = new RelayCommand(param => AddObservation(), param => true);
            AddPrescriptionCommand = new RelayCommand(param => AddPrescription(), param => true);
            DeletePrescriptionCommand = new RelayCommand(param => DeletePrescription(param), param => true);
            AddPictureCommand = new RelayCommand(param => AddPicture(), param => true);
            DeletePictureCommand = new RelayCommand(param => DeletePicture(param), param => true);
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
        private void AddObservation()
        {
            if (Weight <= 0 || BloodPressure <= 0)
                MessageBox.Show("Incorrect input");
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

        private void AddPrescription()
        {
            if (!string.IsNullOrWhiteSpace(Prescription))
                PrescriptionCollection.Add(new Prescription(Prescription));
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
        private void DeletePrescription(object id)
        {
            try
            {
                PrescriptionCollection.RemoveAll(x => x.Id == Convert.ToString(id));
            }
            catch (Exception)
            { }
        }

        private ICommand _addPictureCommand;
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
            }
        }


        private ICommand _deletePicctureCommand;
        public ICommand DeletePictureCommand
        {
            get { return _deletePicctureCommand; }
            set
            {
                _deletePicctureCommand = value;
                OnPropertyChanged(nameof(DeletePictureCommand));
            }
        }
        private void DeletePicture(object id)
        {
            try
            {
                PicturesCollection.RemoveAll(x => x.Id == Convert.ToString(id));
            }
            catch (Exception)
            { }
        }

        #endregion

        #region Methods

        #endregion
    }
}
