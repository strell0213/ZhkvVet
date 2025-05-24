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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly AuthService _auth;
        int loginWidth = 920;
        int regWidth = 500;

        public LoginWindow()
        {
            InitializeComponent();
            _auth = new AuthService();
        }

        private void BEnter_Click(object sender, RoutedEventArgs e)
        {
            if (_auth.Autorize(TBLogin.Text, TBPass.Password) == true)
            {
                this.DialogResult = true;
            }
        }

        private void LReg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            turnPage(true);
        }

        private void BReg_Click(object sender, RoutedEventArgs e)
        {
            if(_auth.Registration(TBFIO.Text, TBEmail.Text, TBPhone.Text, TBLoginReg.Text, TBPassReg.Password) == true)
            {
                LStatus.Content = "Успешная регистрация!";
                LStatus.Visibility = Visibility.Visible;
                turnPage(false);
            }
        }

        public void turnPage(bool toReg)
        {
            if (toReg)
            {
                GReg.Visibility = Visibility.Visible;
                GAuth.Visibility = Visibility.Hidden;

                this.Width = regWidth;
            }
            else
            {
                GAuth.Visibility = Visibility.Visible;
                GReg.Visibility = Visibility.Hidden;

                this.Width = loginWidth;
            }
        }

        private void UpdateCueBannerVisibilityTextBox(TextBox textBox)
        {
            // Находим подсказку по имени внутри шаблона TextBox
            TextBlock cueBanner = (TextBlock)textBox.Template.FindName("CueBanner", textBox);

            if (cueBanner != null)
            {
                // Если текст в поле пустой, показываем подсказку, если нет — скрываем
                cueBanner.Visibility = string.IsNullOrEmpty(textBox.Text) && !textBox.IsKeyboardFocused ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void UpdateCueBannerVisibilityPasswordBox(PasswordBox password)
        {
            // Находим подсказку по имени внутри шаблона TextBox
            TextBlock cueBanner = (TextBlock)password.Template.FindName("CueBanner", password);

            if (cueBanner != null)
            {
                // Если текст в поле пустой, показываем подсказку, если нет — скрываем
                cueBanner.Visibility = string.IsNullOrEmpty(password.Password) && !password.IsKeyboardFocused ? Visibility.Visible : Visibility.Collapsed;
            }
        }


        private void TBLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCueBannerVisibilityTextBox(TBLogin);
        }

        private void TBLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdateCueBannerVisibilityTextBox(TBLogin);
        }

        private void TBLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateCueBannerVisibilityTextBox(TBLogin);
        }

        private void TBPass_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateCueBannerVisibilityPasswordBox(TBPass);
        }

        private void TBPass_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdateCueBannerVisibilityPasswordBox(TBPass);
        }

        private void TBPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UpdateCueBannerVisibilityPasswordBox(TBPass);
        }

        private void TBFIO_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCueBannerVisibilityTextBox(TBFIO);
        }

        private void TBFIO_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateCueBannerVisibilityTextBox(TBFIO);
        }

        private void TBFIO_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdateCueBannerVisibilityTextBox(TBFIO);
        }

        private void TBEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdateCueBannerVisibilityTextBox(TBEmail);
        }

        private void TBEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateCueBannerVisibilityTextBox(TBEmail);
        }

        private void TBEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCueBannerVisibilityTextBox(TBEmail);
        }

        private void TBPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCueBannerVisibilityTextBox(TBPhone);
        }

        private void TBPhone_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateCueBannerVisibilityTextBox(TBPhone);
        }

        private void TBPhone_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdateCueBannerVisibilityTextBox(TBPhone);
        }

        private void TBLoginReg_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdateCueBannerVisibilityTextBox(TBLoginReg);
        }

        private void TBLoginReg_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateCueBannerVisibilityTextBox(TBLoginReg);
        }

        private void TBLoginReg_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCueBannerVisibilityTextBox(TBLoginReg);
        }

        private void TBPassReg_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UpdateCueBannerVisibilityPasswordBox(TBPassReg);
        }

        private void TBPassReg_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdateCueBannerVisibilityPasswordBox(TBPassReg);
        }

        private void TBPassReg_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateCueBannerVisibilityPasswordBox(TBPassReg);
        }

        private void LBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            turnPage(false);
        }

        private void TBPass_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                BEnter_Click(null, null);
            }
        }
    }
}
