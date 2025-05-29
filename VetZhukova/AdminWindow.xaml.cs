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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private readonly EmployeeService employeeService;
        public AdminWindow()
        {
            InitializeComponent();
            employeeService = new EmployeeService();
            UpdateList();
        }

        public void UpdateList()
        {
            var list = employeeService.GetEmployees();
            LBEmpList.ItemsSource = list;
        }

        private void BAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            if(addEmployeeWindow.ShowDialog() == true)
            {
                UpdateList();
            }
        }
    }
}
