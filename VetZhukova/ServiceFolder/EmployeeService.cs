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
    }
}
