using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HR
{
    internal class EmployeeManagement
    {
        List<Employee> Admission = new List<Employee>();

        public void AddEmployee(Employee NewEmployeeName)
        {
            Admission.Add(NewEmployeeName);
        }
        public void ListEmployee()
        {
            foreach(Employee line in Admission)
            {
                Console.WriteLine("Employee Name: " + line.Name + "\t CPF: " + line.CPF + " \t Registry: " + line.Registry + " \t Date Start: " + line.DateStart);
            }
        }
    }
}
