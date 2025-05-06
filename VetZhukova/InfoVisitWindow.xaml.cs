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
            var visit = _visitService.GetVisitByID(idVisit);
            if (visit == null) return;

            LTitle.Text = $"Запись №{visit.VisitID} - {visit.visitDate}";

            var employee = _employeeService.GetEmployeeByID(visit.EmployeeID);
            var patient = _patientService.GetPatientByID(visit.PatientID);
            var owner = _ownerService.GetOwnerByID(patient.OwnerID);
            var service = _serviceService.GetServiceByID(visit.ServiceID);

            LPatient.Text = patient.name;
            LOwner.Text = owner.fio;
            LService.Text = service.serviceName;
            LEmployee.Text = employee.fullName;

            TBNotes.Text = visit.notes;
        }

        private void BBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
