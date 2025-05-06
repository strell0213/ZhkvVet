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
    /// Логика взаимодействия для AddlConsumableWindow.xaml
    /// </summary>
    public partial class AddlConsumableWindow : Window
    {
        Consumable _consumable;
        public AddlConsumableWindow(Consumable consumable)
        {
            InitializeComponent();
            this._consumable = consumable;
        }

        public void Add()
        {
            Consumable consumable = new Consumable()
            {
                name = TBName.Text,
                unit = TBUnit.Text,
                Quantity = Convert.ToInt32(TBQuantity.Text),
                lastUpdated = DateTime.Now.ToString(),
            };
            App.AC.Consumables.Add(consumable);
            App.AC.SaveChanges();
        }

        public void Edit()
        {
            _consumable.unit = TBUnit.Text;
            _consumable.name = TBName.Text;
            _consumable.Quantity = Convert.ToInt32(TBQuantity.Text);
            App.AC.SaveChanges();
        }

        private void BOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (TBName.Text.Length == 0 || TBUnit.Text.Length == 0 || TBQuantity.Text.Length == 0)
            {
                MessageBox.Show("Все поля должны быть заполнены!", GlobalSettings.NameApplication, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_consumable != null) Edit();
            else Add();

            this.DialogResult = true;
        }

        private void BCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = false;
        }

        private void TBQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }
    }
}
