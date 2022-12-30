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

        public double CalculateSalary()
        {
            double INSS = MonthlySalary * 0.07;
            double IR = (MonthlySalary - INSS) * 0.15;

            double LiquidSalary = MonthlySalary - INSS - IR;

            Console.WriteLine("Hello, " + Name + ", Number Registry " + Registry + " follow below your salary details: ");
            Console.WriteLine("Monthly Salary: £ " + MonthlySalary);
            Console.WriteLine("INSS: £ (" + INSS + ")");
            Console.WriteLine("IRRF: £ (" + IR + ")");
            Console.WriteLine("Liquid Salary: £ " + LiquidSalary);
            Console.WriteLine(" ");

            return LiquidSalary;
        }
        
    }
}
