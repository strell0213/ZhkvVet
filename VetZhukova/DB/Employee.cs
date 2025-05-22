using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetZhukova.DB
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        private string FullName="", Position="", HireDate = "", Phone = "", Email = "", Login = "", Password = "";

        public string fullName { get { return FullName; } set { FullName = value; } }
        public string position { get { return Position; } set { Position = value; } }
        public string hireDate { get { return HireDate; } set { HireDate = value; } }
        public string phone { get { return Phone; } set { Phone = value; } }
        public string email { get { return Email; } set { Email = value; } }
        public string login { get { return Login; } set { Login = value; } }
        public string password { get { return Password; } set { Password = value; } }

        public Employee() { }
        public Employee(string FullName, string Position, string HireDate, string Phone, string Email, string Login, string Password)
        {
            this.FullName = FullName;
            this.Position = Position;
            this.HireDate = HireDate;
            this.Phone = Phone;
            this.Email = Email;
            this.Login = Login;
            this.Password = Password;
        }
    }
}
