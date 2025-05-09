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
    /// Логика взаимодействия для InfoVisitWindow.xaml
    /// </summary>
    public partial class InfoVisitWindow : Window
    {
        private VisitService _visitService;
        private EmployeeService _employeeService;
        private PatientService _patientService;
        private OwnerService _ownerService;
        private ServiceService _serviceService;

        public int idVisit;
        private Visit _visit;
        private Employee _employee;
        private Owner _owner;
        private Patient _patient;
        private Service _service;
        public InfoVisitWindow()
        {
            InitializeComponent();
            _visitService = new VisitService();
            _employeeService = new EmployeeService();
            _patientService = new PatientService();
            _ownerService = new OwnerService();
            _serviceService = new ServiceService();
        }

        public void GetInfo()
        {
            _visit = _visitService.GetVisitByID(idVisit);
            if (_visit == null) return;

            LTitle.Text = $"Запись №{_visit.VisitID} - {_visit.visitDate}";

            _employee = _employeeService.GetEmployeeByID(_visit.EmployeeID);
            _patient = _patientService.GetPatientByID(_visit.PatientID);
            _owner = _ownerService.GetOwnerByID(_patient.OwnerID);
            _service = _serviceService.GetServiceByID(_visit.ServiceID);

            LPatient.Text = _patient.name;
            LOwner.Text = _owner.fio;
            LService.Text = _service.serviceName;
            LEmployee.Text = _employee.fullName;

            TBNotes.Text = _visit.notes;
        }

        private void BBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = true;
        }

        private void BDone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Done();
        }

        public void Done()
        {
            if (MessageBox.Show("Вы уверены, что хотите выполнить запись?", GlobalSettings.NameApplication, MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.No) return;
            
            if (MessageBox.Show("Использовались ли расходные материалы?", GlobalSettings.NameApplication, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ConsumableWindow consumableWindow = new ConsumableWindow();
                if (consumableWindow.ShowDialog() == true)
                {
                    _visitService.DoneVisit(idVisit);
                    this.DialogResult = true;
                }
            }
            else
            {
                _visitService.DoneVisit(idVisit);
                this.DialogResult = true;
            }
        }

        private void BVisitAppoiment_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddVisit addVisit = new AddVisit();
            addVisit.SelectOwnerPatientInterface(_patient.PatientID, _owner.OwnerID);
            if(addVisit.ShowDialog() == true)
            {
                if (MessageBox.Show("Завершить запись?", GlobalSettings.NameApplication, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Done();
                }
            }
        }
    }
}
