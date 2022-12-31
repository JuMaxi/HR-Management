using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR
{
    internal class Employee
    {
        public string Name;
        public string Registry;
        public string CPF;
        public DateTime DateStart;
        public double MonthlySalary;

        public void Initialize(string NameEmployee, string NumberRegistry, string NumberCPF, DateTime Date, double Salary)
        {
            Name = NameEmployee;
            Registry = NumberRegistry;
            CPF = NumberCPF;
            DateStart = Date;
            MonthlySalary = Salary;
        }

       
        
    }
}
