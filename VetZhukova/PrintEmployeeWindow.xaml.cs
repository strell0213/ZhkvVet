using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.Win32;
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
using VetZhukova.Word;

namespace VetZhukova
{
    /// <summary>
    /// Логика взаимодействия для PrintEmployeeWindow.xaml
    /// </summary>
    public partial class PrintEmployeeWindow : Window
    {
        private readonly VisitService _visitService;
        private readonly EmployeeService _employeeService;
        public PrintEmployeeWindow()
        {
            InitializeComponent();

            _visitService = new VisitService();
            _employeeService = new EmployeeService();
        }

        private void BSaveEmployee_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var employee = _employeeService.GetEmployeeByID(GlobalSettings.IDUser);
            var visits = _visitService.GetDoneVisitByEmp(employee.EmployeeID);
            if(visits.Count == 0)
            {
                MessageBox.Show("Нет данных для печати!", GlobalSettings.NameApplication, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var saveFileDialog = new SaveFileDialog
            {
                Title = "Сохранить документ",
                Filter = "Word документ (*.docx)|*.docx",
                FileName = $"VisitDoneEmp_{DateTime.Now:yyyyMMdd_HHmm}.docx",
                DefaultExt = ".docx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                var builder = new WordHelper(saveFileDialog.FileName);
                builder.CreateDoneVisitByEmp(visits);

                MessageBox.Show("Документ успешно сохранён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                System.Diagnostics.Process.Start("explorer", saveFileDialog.FileName);
            }
        }
    }
}
