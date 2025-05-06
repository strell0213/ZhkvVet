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
    }
}
