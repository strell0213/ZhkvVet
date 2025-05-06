using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VetZhukova.DB;

namespace VetZhukova.ServiceFolder
{
    public class AuthService
    {
        public bool Autorize(string login, string pass)
        {
            string hashPass = GetStringHash(pass);

            var owner = App.AC.Owners.Where(c => c.login == login).FirstOrDefault();
            if (owner != null)
            {
                if (owner.password == hashPass) { GlobalSettings.isEmployee = false; GlobalSettings.IDUser = owner.OwnerID; return true; }
                else
                {
                    MessageBox.Show("Не верный логин или пароль!", GlobalSettings.NameApplication, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                var emp = App.AC.Employees.Where(c => c.login == login).FirstOrDefault();
                if (emp != null)
                {
                    if (emp.password == hashPass) { GlobalSettings.isEmployee = true; GlobalSettings.IDUser = emp.EmployeeID; return true; }
                    else
                    {
                        MessageBox.Show("Не верный логин или пароль!", GlobalSettings.NameApplication, MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Пользователь не найден!", GlobalSettings.NameApplication, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }

        public bool Registration(string fio, string email, string phone, string login, string pass)
        {
            if (fio.Length <= 0 || email.Length <= 0 || phone.Length <= 0 || login.Length <= 0 || pass.Length <= 0)
            {
                MessageBox.Show("Есть пустые поля! Пожалуйста заполните их.", GlobalSettings.NameApplication, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            string hashPass = GetStringHash(pass);

            Owner owner = new Owner()
            {
                fio = fio,
                email = email,
                phone = phone,
                login = login,
                password = hashPass,
            };

            try
            {
                App.AC.Owners.Add(owner);
                App.AC.SaveChanges();
            }
            catch { MessageBox.Show("Неизвестная ошибка", GlobalSettings.NameApplication, MessageBoxButton.OK, MessageBoxImage.Error); return false; }

            return true;
        }

        public string GetStringHash(string pass)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(pass);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
