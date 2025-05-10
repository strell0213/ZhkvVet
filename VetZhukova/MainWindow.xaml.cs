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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VetZhukova.DB;
using VetZhukova.ServiceFolder;

namespace VetZhukova
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly VisitService _visitService;
        private readonly ServiceService _serviceService;
        private readonly ConsumableService _consumableService;
        private readonly PatientService _patientService;
        int chooseTab=1;       //индекс выбранной вкладки с основной панели

        Storyboard storyboardTab1 = new Storyboard();
        Storyboard storyboardTab2 = new Storyboard();
        Storyboard storyboardTab3 = new Storyboard();
        Storyboard storyboardTab4 = new Storyboard();
        public MainWindow()
        {
            InitializeComponent();

            this.Title = GlobalSettings.NameApplication;

            _visitService = new VisitService();
            _serviceService = new ServiceService();
            _consumableService = new ConsumableService();
            _patientService = new PatientService();

            if (!GlobalSettings.isAuth)
            {
                this.Hide();
                LoginWindow login = new LoginWindow();
                if (login.ShowDialog() == true)
                {
                    GlobalSettings.isAuth = true;
                    this.Show();
                }
            }

            TCMain.SelectedIndex = 0;
            LineGHeaderBlock1.X1 = GHeaderBlock1.ActualWidth;
            UpdateLastVisit();
        }

        public void UpdateLastVisit()
        {
            LBLastVisit.ItemsSource = _visitService.GetLastVisit();
        }

        public void UpdateVisits()
        {
            LVVisit.ItemsSource = _visitService.GetAllVisit();
        }

        public void UpdateService()
        {
            LVService.ItemsSource = _serviceService.GetServices();
        }

        public void UpdateConsumable()
        {
            LVConsumable.ItemsSource = _consumableService.GetConsumables();
        }

        public void UpdatePatients()
        {
            LVPatients.ItemsSource = _patientService.GetAllPatients();
        }

        private void GHeaderBlock1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (chooseTab == 1) return;
            DoubleAnimation da = new DoubleAnimation
            {
                From = 0,
                To = GHeaderBlock1.ActualWidth,
                Duration = TimeSpan.FromMilliseconds(100)
            };

            Storyboard.SetTarget(da, LineGHeaderBlock1);
            Storyboard.SetTargetProperty(da, new PropertyPath(Line.X1Property));

            storyboardTab1.Children.Add(da);
            storyboardTab1.Begin();
        }

        private void GHeaderBlock1_MouseLeave(object sender, MouseEventArgs e)
        {
            if (chooseTab == 1) return;
            DoubleAnimation da = new DoubleAnimation
            {
                From = GHeaderBlock1.ActualWidth,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(100)
            };

            Storyboard.SetTarget(da, LineGHeaderBlock1);
            Storyboard.SetTargetProperty(da, new PropertyPath(Line.X1Property));

            storyboardTab1.Children.Add(da);
            storyboardTab1.Begin();
        }

        private void GHeaderBlock2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (chooseTab == 2) return;
            DoubleAnimation da = new DoubleAnimation
            {
                From = 0,
                To = GHeaderBlock2.ActualWidth,
                Duration = TimeSpan.FromMilliseconds(100)
            };

            Storyboard.SetTarget(da, LineGHeaderBlock2);
            Storyboard.SetTargetProperty(da, new PropertyPath(Line.X1Property));

            storyboardTab2.Children.Add(da);
            storyboardTab2.Begin();
        }

        private void GHeaderBlock2_MouseLeave(object sender, MouseEventArgs e)
        {
            if (chooseTab == 2) return;
            DoubleAnimation da = new DoubleAnimation
            {
                From = GHeaderBlock2.ActualWidth,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(100)
            };

            Storyboard.SetTarget(da, LineGHeaderBlock2);
            Storyboard.SetTargetProperty(da, new PropertyPath(Line.X1Property));

            storyboardTab2.Children.Add(da);
            storyboardTab2.Begin();
        }

        private void GHeaderBlock3_MouseEnter(object sender, MouseEventArgs e)
        {
            if (chooseTab == 3) return;
            DoubleAnimation da = new DoubleAnimation
            {
                From = 0,
                To = GHeaderBlock3.ActualWidth,
                Duration = TimeSpan.FromMilliseconds(100)
            };

            Storyboard.SetTarget(da, LineGHeaderBlock3);
            Storyboard.SetTargetProperty(da, new PropertyPath(Line.X1Property));

            storyboardTab3.Children.Add(da);
            storyboardTab3.Begin();
        }

        private void GHeaderBlock3_MouseLeave(object sender, MouseEventArgs e)
        {
            if (chooseTab == 3) return;
            DoubleAnimation da = new DoubleAnimation
            {
                From = GHeaderBlock3.ActualWidth,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(100)
            };

            Storyboard.SetTarget(da, LineGHeaderBlock3);
            Storyboard.SetTargetProperty(da, new PropertyPath(Line.X1Property));

            storyboardTab3.Children.Add(da);
            storyboardTab3.Begin();
        }
        private void GHeaderBlock4_MouseEnter(object sender, MouseEventArgs e)
        {
            if (chooseTab == 4) return;
            DoubleAnimation da = new DoubleAnimation
            {
                From = 0,
                To = GHeaderBlock4.ActualWidth,
                Duration = TimeSpan.FromMilliseconds(100)
            };

            Storyboard.SetTarget(da, LineGHeaderBlock4);
            Storyboard.SetTargetProperty(da, new PropertyPath(Line.X1Property));

            storyboardTab4.Children.Add(da);
            storyboardTab4.Begin();
        }

        private void GHeaderBlock4_MouseLeave(object sender, MouseEventArgs e)
        {
            if (chooseTab == 4) return;
            DoubleAnimation da = new DoubleAnimation
            {
                From = GHeaderBlock4.ActualWidth,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(100)
            };

            Storyboard.SetTarget(da, LineGHeaderBlock4);
            Storyboard.SetTargetProperty(da, new PropertyPath(Line.X1Property));

            storyboardTab4.Children.Add(da);
            storyboardTab4.Begin();
        }


        public void ClearAllLine()
        {
            storyboardTab1.Remove();
            storyboardTab2.Remove();
            storyboardTab3.Remove();
            storyboardTab4.Remove();
            LineGHeaderBlock1.X1 = 0;
            LineGHeaderBlock2.X1 = 0;
            LineGHeaderBlock3.X1 = 0;
            LineGHeaderBlock4.X1 = 0;
        }

        private void GHeaderBlock1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClearAllLine();
            TCMain.SelectedIndex = 0;
            LineGHeaderBlock1.X1 = GHeaderBlock1.ActualWidth;
            chooseTab = 1;

            UpdateLastVisit();
        }

        private void GHeaderBlock2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClearAllLine();
            TCMain.SelectedIndex = 1;
            LineGHeaderBlock2.X1 = GHeaderBlock2.ActualWidth;
            chooseTab = 2;

            UpdateVisits();
        }

        private void GHeaderBlock3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClearAllLine();
            TCMain.SelectedIndex = 2;
            LineGHeaderBlock3.X1 = GHeaderBlock3.ActualWidth;
            chooseTab = 3;

            if (TCBooks.SelectedIndex == 0) UpdateService();
            else if (TCBooks.SelectedIndex == 1) UpdateConsumable();
            else UpdateService();
        }
        private void GHeaderBlock4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClearAllLine();
            TCMain.SelectedIndex = 3;
            LineGHeaderBlock4.X1 = GHeaderBlock4.ActualWidth;
            chooseTab = 4;
        }

        private void BAddSerAndCons_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (TCBooks.SelectedIndex==0)
            {
                AddServiceWindow add = new AddServiceWindow(null);
                add.Title = "Добавление услуги";
                if(add.ShowDialog()==true)
                {
                    UpdateService();
                }
            }
            else if(TCBooks.SelectedIndex==1)
            {
                AddlConsumableWindow add = new AddlConsumableWindow(null);
                add.Title = "Добавление расходного материала";
                if (add.ShowDialog()==true)
                {
                    UpdateConsumable();
                }
            }
        }

        private void BEditService_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = (Border)sender;
            var service = (Service)item.DataContext;

            AddServiceWindow add = new AddServiceWindow(service);
            add.TBName.Text = service.serviceName;
            add.TBDiscription.Text = service.description;
            add.TBPrice.Text = service.Price.ToString();

            add.Title = "Редактировать услугу";
            if (add.ShowDialog() == true)
            {
                UpdateService();
            }
        }

        private void BDelService_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить услугу?", GlobalSettings.NameApplication, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            var item = (Border)sender;
            var service = (Service)item.DataContext;

            App.AC.Services.Remove(service);
            App.AC.SaveChanges();

            UpdateService();
        }

        private void BEditConsumable_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = (Border)sender;
            var consumable = (Consumable)item.DataContext;

            AddlConsumableWindow add = new AddlConsumableWindow(consumable);
            add.TBName.Text = consumable.name;
            add.TBUnit.Text = consumable.unit;
            add.TBQuantity.Text = consumable.Quantity.ToString();

            add.Title = "Редактировать расходный материал";
            if (add.ShowDialog() == true)
            {
                UpdateConsumable();
            }
        }

        private void BDelConsumable_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить расходный материал?", GlobalSettings.NameApplication, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            var item = (Border)sender;
            var сonsumable = (Consumable)item.DataContext;

            App.AC.Consumables.Remove(сonsumable);
            App.AC.SaveChanges();

            UpdateConsumable();
        }

        private void TCBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TCBooks.SelectedIndex == 0) UpdateService();
            else if (TCBooks.SelectedIndex == 1) UpdateConsumable();
            else if (TCBooks.SelectedIndex == 2) UpdatePatients();
        }

        private void BWritePatientMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddVisit addVisit = new AddVisit();
            if(addVisit.ShowDialog() == true)
            {
                UpdateLastVisit();
            }
        }

        private void BInfoLateVizit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = (Border)sender;
            var visit = (VisitDTO)item.DataContext;

            InfoVisitWindow infoVisitWindow = new InfoVisitWindow();
            infoVisitWindow.idVisit = visit.VisitID;
            infoVisitWindow.GetInfo();
            if(infoVisitWindow.ShowDialog()==true)
            {
                UpdateLastVisit();
            }
        }

        private void BInfoPatient_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = (Border)sender;
            var patient = (Patient)item.DataContext;

            PetWindow petWindow = new PetWindow(patient);
            petWindow.ShowDialog();
        }
    }
}
