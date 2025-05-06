using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetZhukova.DB
{
    public class Owner
    {
        public int OwnerID { get; set; }
        private string FIO, Email, Phone, Login, Password;

        public string fio { get { return FIO; } set { FIO = value; } }
        public string email { get { return Email; } set { Email = value; } }
        public string phone { get { return Phone; } set { Phone = value; } }
        public string login { get { return Login; } set { Login = value; } }
        public string password { get { return Password; } set { Password = value; } }

        public Owner() { }
        public Owner(string FIO, string Email, string Phone, string Login, string Password)
        {
            this.FIO = FIO;
            this.Email = Email;
            this.Phone = Phone;
            this.Login = Login;
            this.Password = Password;
        }
    }
}
