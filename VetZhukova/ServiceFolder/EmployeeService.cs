using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetZhukova.DB;

namespace VetZhukova.ServiceFolder
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployees();
    }
    class EmployeeService : IEmployeeService
    {
        public EmployeeService() { }

        public List<Employee> GetEmployees()
        {
            return App.AC.Employees.ToList();
        }

        public Employee GetEmployeeByID(int id)
        {
            return App.AC.Employees.Where(c => c.EmployeeID == id).FirstOrDefault();
        }

        public string GetStazhWork(Employee curEmp)
        {
            if(curEmp.hireDate == null)
            {
                return $"0 лет, 0 мес.";
            }

            DateTime hireDate = DateTime.Parse(curEmp.hireDate);
            DateTime now = DateTime.Now;

            int years = now.Year - hireDate.Year;
            int months = now.Month - hireDate.Month;

            if (months < 0)
            {
                years--;
                months += 12;
            }

            return $"{years} лет, {months} мес.";
        }

        public bool UpdateImage(Employee curEmp)
        {
            var EmpDB = App.AC.Employees.Where(c => c.EmployeeID == curEmp.EmployeeID).FirstOrDefault();
            if (EmpDB is null) return false;

            EmpDB.imPhoto = EmpDB.imPhoto;
            App.AC.SaveChanges();
            return true;
        }

        public void AddEmployee(string login, string role, string pass)
        {
            Employee employee = new Employee() { 
                login = login,
                position = role,
                password = pass
            };

            App.AC.Employees.Add(employee);
            App.AC.SaveChanges();
        }
    }
}
