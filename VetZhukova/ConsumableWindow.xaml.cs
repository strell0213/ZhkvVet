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
    /// Логика взаимодействия для ConsumableWindow.xaml
    /// </summary>
    public partial class ConsumableWindow : Window
    {
        private ConsumableService _consumableService;
        public ConsumableWindow()
        {
            InitializeComponent();
            _consumableService = new ConsumableService();
            UpdateList();
        }

        public void UpdateList()
        {
            var list = _consumableService.GetSetConsumables(TBSearch.Text);
            LVConsumbale.ItemsSource = list;
        }
    }
}
