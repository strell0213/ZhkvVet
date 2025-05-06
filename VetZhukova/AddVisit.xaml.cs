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
    /// Логика взаимодействия для AddVisit.xaml
    /// </summary>
    public partial class AddVisit : Window
    {
        private readonly OwnerService ownerService;
        private readonly PatientService patientService;
        private readonly ServiceService serviceService;
        private readonly EmployeeService employeeService;
        private readonly VisitService visitService;

        List<Service> servicesList;
        List<Employee> employeesList;
        List<Owner> ownerList;
        List<Patient> patientsList;
        public AddVisit()
        {
            InitializeComponent();
            ownerService = new OwnerService();
            patientService = new PatientService();
            serviceService = new ServiceService();
            employeeService = new EmployeeService();
            visitService = new VisitService();

            UpdateService();
            UpdateEmployee();
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
        public int SelectOwner(int idOwner)
        {
            UpdateOwner();

            int index = 0;
            foreach(var owner in ownerList)
            {
                if (owner.OwnerID == idOwner) { return index; }
                else index++;
            }
            return -1;
        }
        public void UpdatePatient(int ownerID)
        {
            CBChoosePatient.Items.Clear();
            patientsList = patientService.GetPatients(ownerID);

            foreach (var patient in patientsList)
            {
                CBChoosePatient.Items.Add(patient.name);
            }
        }
        public int SelectPatient(int patientID, int ownerID)
        {
            UpdatePatient(ownerID);

            int index = 0;
            foreach (var patient in patientsList)
            {
                if (patient.PatientID == patientID) { return index; }
                else index++;
            }
            return -1;
        }
        public void UpdateService()
        {
            CBService.Items.Clear();
            servicesList = serviceService.GetServices();

            foreach (var ser in servicesList)
            {
                CBService.Items.Add(ser.serviceName);
            }
        }
        public void UpdateEmployee()
        {
            CBEmployee.Items.Clear();
            employeesList = employeeService.GetEmployees();

            foreach (var emp in employeesList)
            {
                CBEmployee.Items.Add(emp.fullName);
            }
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
            if(TBNamePatient.Text.Length == 0 || TBBreed.Text.Length == 0)
            {
                MessageBox.Show("Есть пустые поля у питомца! Пожалуйста заполните их.", GlobalSettings.NameApplication, MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }

            return patientService.AddPatient(TBNamePatient.Text, TBBreed.Text, ownerID);
        }

        public bool AddVisitDB(int patientID)
        {
            if(DPDateVisit.Text.Length == 0 || CBService.SelectedIndex == -1 || CBEmployee.SelectedIndex == -1)
            {
                MessageBox.Show("Есть пустые поля в записи! Пожалуйста заполните их.", GlobalSettings.NameApplication, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return visitService.AddVisit(employeesList[CBEmployee.SelectedIndex].EmployeeID, patientID, servicesList[CBService.SelectedIndex].ServiceID, DPDateVisit.Text, TBNotes.Text);
        }
        private void BOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int ownerID;
            int patientID;

            if (CBHaveOwner.IsChecked == true) ownerID = ownerList[CBChooseOwner.SelectedIndex].OwnerID;
            else ownerID = AddOwner();

            if (ownerID != -1)
            {
                if (CBHavePatient.IsChecked == true) patientID = patientsList[CBChoosePatient.SelectedIndex].PatientID;
                else patientID = AddPatient(ownerID);

                if(patientID != -1)
                {
                    bool res = AddVisitDB(patientID);

                    if (res == true) DialogResult = true;
                    else
                    {
                        MessageBox.Show("Ошибка при записи", GlobalSettings.NameApplication, MessageBoxButton.OK, MessageBoxImage.Error);
                        CBHaveOwner.IsChecked = true;
                        CBChooseOwner.SelectedIndex = SelectOwner(ownerID);
                        CBHavePatient.IsChecked = true;
                        CBChoosePatient.SelectedIndex = SelectPatient(patientID, ownerID);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Не выбран питомец!", GlobalSettings.NameApplication, MessageBoxButton.OK, MessageBoxImage.Error);
                    CBHaveOwner.IsChecked = true;
                    CBChooseOwner.SelectedIndex = SelectOwner(ownerID);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Не выбран пациент!", GlobalSettings.NameApplication, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void BCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult = false;
        }

        private void CBHaveOwner_Click(object sender, RoutedEventArgs e)
        {
            if(CBHaveOwner.IsChecked==true)
            {
                GAddOwner.Visibility = Visibility.Hidden;
                GChooseOwner.Visibility = Visibility.Visible;
                CBHavePatient.IsEnabled = true;
                UpdateOwner();
            }
            else
            {
                GAddOwner.Visibility = Visibility.Visible;
                GChooseOwner.Visibility = Visibility.Hidden;
                CBHavePatient.IsEnabled = false;
            }
        }

        private void CBHavePatient_Click(object sender, RoutedEventArgs e)
        {
            if(CBHavePatient.IsChecked == true)
            {
                if(CBChooseOwner.SelectedIndex == -1)
                {
                    MessageBox.Show("Не выбран пациент!", GlobalSettings.NameApplication, MessageBoxButton.OK, MessageBoxImage.Error);
                    CBHavePatient.IsChecked = false;
                    return;
                }
                GAddPatient.Visibility = Visibility.Hidden;
                GChoosePatient.Visibility = Visibility.Visible;
                UpdatePatient(ownerList[CBChooseOwner.SelectedIndex].OwnerID);
            }
            else
            {
                GAddPatient.Visibility = Visibility.Visible;
                GChoosePatient.Visibility = Visibility.Hidden;
            }
        }
    }
}
