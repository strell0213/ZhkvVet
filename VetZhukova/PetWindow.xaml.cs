using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VetZhukova.DB;
using VetZhukova.ServiceFolder;

namespace VetZhukova
{
    /// <summary>
    /// Логика взаимодействия для PetWindow.xaml
    /// </summary>
    public partial class PetWindow : Window
    {
        private Patient _patient;
        private VisitService _visitService;
        private PatientService _patientService;
        public PetWindow(Patient patient)
        {
            InitializeComponent();
            _patient = patient;
            _visitService = new VisitService();
            _patientService = new PatientService();

            GetInfo();
        }

        public void GetInfo()
        {
            TBName.Text = _patient.name;
            TBAge.Text = _patient.Age.ToString();
            TBBreed.Text = _patient.breed;
            UpdateImage();
            UpdateDoneList();
        }

        public void UpdateDoneList()
        {
            var list = _visitService.GetDoneVisitPatient();
            LBDoneVisit.ItemsSource = list;
        }

        public void UpdateImage()
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string targetFolder = System.IO.Path.Combine(appPath, "Patient");

            if (!Directory.Exists(targetFolder)) return;
            if (_patient.imagePath is null) return;

            string imagePath = System.IO.Path.Combine(targetFolder, _patient.imagePath);
            if (File.Exists(imagePath))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                ImPhoto.Source = bitmap;
            }
        }

        private void BOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void BSave_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _patient.name = TBName.Text;
            _patient.Age = Convert.ToInt32(TBAge.Text);
            _patient.breed = TBBreed.Text;

            if(!_patientService.UpdatePatient(_patient))
            {
                MessageBox.Show("Ошибка! Что-то пошло не так..", GlobalSettings.NameApplication, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void ImPhoto_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Right) return;

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Выберите фото пациента",
                Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (openFileDialog.ShowDialog() == false) return;

            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string targetFolder = System.IO.Path.Combine(appPath, "Patient");

            if (!Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder);

            string fileExt= System.IO.Path.GetExtension(openFileDialog.FileName); // ".jpg"(openFileDialog.FileName);
            string fileName = $"{_patient.PatientID.ToString()}{fileExt}";
            targetFolder = System.IO.Path.Combine(targetFolder, fileName);

            File.Copy(openFileDialog.FileName, targetFolder, overwrite: true);

            _patient.imagePath = openFileDialog.FileName;
            _patientService.UpdateImage(_patient);
            UpdateImage();
        }
    }
}
