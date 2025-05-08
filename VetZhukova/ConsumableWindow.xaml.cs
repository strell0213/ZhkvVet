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
    /// Логика взаимодействия для ConsumableWindow.xaml
    /// </summary>
    public partial class ConsumableWindow : Window
    {
        private ConsumableService _consumableService;
        private List<SetConsumable> _setConsumables;
        public ConsumableWindow()
        {
            InitializeComponent();
            _consumableService = new ConsumableService();

            GenerateList();
        }

        public void GenerateList()
        {
            _setConsumables = _consumableService.GetSetConsumables();
            UpdateList();
        }

        public void UpdateList()
        {
            LVConsumbale.ItemsSource = _setConsumables.Where(c=>c.Name.Contains(TBSearch.Text)).ToList();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = (Border)sender;
            var sc = (SetConsumable)item.DataContext;

            foreach (var scA in _setConsumables)
            {
                if (scA.ConsumableID == sc.ConsumableID)
                {
                    scA.count += 1;
                    if (scA.count > scA.quantity)
                    {
                        scA.count = scA.quantity;
                    }
                }
            }
            UpdateList();
        }

        private void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            var item = (Border)sender;
            var sc = (SetConsumable)item.DataContext;

            foreach (var scA in _setConsumables)
            {
                if(scA.ConsumableID == sc.ConsumableID)
                {
                    scA.count -= 1;
                    if(scA.count < 0)
                    {
                        scA.count = 0;
                    }
                }
            }
            UpdateList();
        }

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void BCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        private void BDone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _consumableService.UpdateConsumablesDB(_setConsumables);
            this.DialogResult = true;
        }
    }
}
