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
    /// Логика взаимодействия для AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        private readonly EmployeeService employeeService;
        public AddEmployeeWindow()
        {
            InitializeComponent();
            employeeService = new EmployeeService();

            CBRole.Items.Add("Ветеринарный врач");
            CBRole.Items.Add("Ветеринар-хирург");
            CBRole.Items.Add("Помощник ветеринара");
            CBRole.Items.Add("Терапевт");
            CBRole.Items.Add("Специалист по УЗИ");
            CBRole.Items.Add("Грумер");
            CBRole.Items.Add("Менеджер");
            CBRole.Items.Add("Администратор");
        }

        private void BAdd_Click(object sender, RoutedEventArgs e)
        {
            employeeService.AddEmployee(TBLogin.Text, TBPass.Text, CBRole.Text);
            this.DialogResult = true;
        }
    }
}
