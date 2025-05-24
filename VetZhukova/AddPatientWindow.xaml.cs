using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AddPatientWindow.xaml
    /// </summary>
    public partial class AddPatientWindow : Window
    {
        private readonly OwnerService ownerService;
        private readonly PatientService patientService;
        List<Owner> ownerList;
        public AddPatientWindow()
        {
            InitializeComponent();
            ownerService = new OwnerService();
            patientService = new PatientService();
        }

        public int AddOwner()
        {
            if (TBFIOOwner.Text.Length == 0 || TBPhone.Text.Length == 0 || TBLogin.Text.Length == 0 || PBPass.Password.Length == 0)
            {
                MessageBox.Show("Есть пустые поля у пациента! Пожалуйста заполните их.", GlobalSettings.NameApplication, MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }

            return ownerService.AddOwner(TBFIOOwner.Text, TBPhone.Text, TBLogin.Text, PBPass.Password);
        }

        public int AddPatient(int ownerID)
        {
            if (TBNamePatient.Text.Length == 0 || TBBreed.Text.Length == 0)
            {
                MessageBox.Show("Есть пустые поля у питомца! Пожалуйста заполните их.", GlobalSettings.NameApplication, MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }

            return patientService.AddPatient(TBNamePatient.Text, TBBreed.Text, ownerID);
        }

        public void UpdateOwner()
        {
            CBChooseOwner.Items.Clear();
            ownerList = ownerService.GetOwners();

            foreach (var owner in ownerList)
            {
                CBChooseOwner.Items.Add(owner.fio);
            }
        }

        private void CBHaveOwner_Click(object sender, RoutedEventArgs e)
        {
            if (CBHaveOwner.IsChecked == true)
            {
                GAddOwner.Visibility = Visibility.Hidden;
                GChooseOwner.Visibility = Visibility.Visible;
                UpdateOwner();
            }
            else
            {
                GAddOwner.Visibility = Visibility.Visible;
                GChooseOwner.Visibility = Visibility.Hidden;
            }
        }

        private void BOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int ownerID;
            if (CBHaveOwner.IsChecked == true) ownerID = ownerList[CBChooseOwner.SelectedIndex].OwnerID;
            else ownerID = AddOwner();

            if (ownerID != -1)
            {
                if (AddPatient(ownerID)==-1) return;

                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Не выбран пациент!", GlobalSettings.NameApplication, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
