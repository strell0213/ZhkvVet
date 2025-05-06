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

namespace VetZhukova
{
    /// <summary>
    /// Логика взаимодействия для AddServiceWindow.xaml
    /// </summary>
    public partial class AddServiceWindow : Window
    {
        Service _service;
        public AddServiceWindow(Service service)
        {
            InitializeComponent();
            this._service = service;
        }

        public void Add()
        {
            Service service = new Service()
            {
                serviceName = TBName.Text,
                description = TBDiscription.Text,
                Price = Convert.ToInt32(TBPrice.Text)
            };
            App.AC.Services.Add(service);
            App.AC.SaveChanges();
        }

        public void Edit()
        {
            _service.serviceName = TBName.Text;
            _service.description = TBDiscription.Text;
            _service.Price = Convert.ToInt32(TBPrice.Text);

            App.AC.SaveChanges();
        }

        private void BOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(TBName.Text.Length == 0 || TBDiscription.Text.Length == 0 || TBPrice.Text.Length == 0)
            {
                MessageBox.Show("Все поля должны быть заполнены!", GlobalSettings.NameApplication, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_service != null) Edit();
            else Add();

            this.DialogResult = true;
        }

        private void TBPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }

        private void BCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
