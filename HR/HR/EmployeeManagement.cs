using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR
{
    internal class EmployeeManagement
    {
        List<Employee> NewHiredEmployee = new List<Employee>();

        public void AddEmployee(Employee NewEmployee)
        {
            NewHiredEmployee.Add(NewEmployee);
        }

        public void ListEmployee()
        {
            foreach(Employee Line in NewHiredEmployee)
            {
                Console.WriteLine("Employee Name: " + Line.Name + "\t CPF: " + Line.CPF + "\t Registry: " + Line.Registry + "\t Date Start: " + Line.DateStart);
            }
        }

        public void ShowBirthdayCompany()
        {

        }
    }
}
