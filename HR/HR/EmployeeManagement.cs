using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace HR
{
    public class EmployeeManagement
    {
        List<Employee> NewHiredEmployee = new List<Employee>();
        List<Employee> RemovedEmployee = new List<Employee>();
        public Calculate Calculate = new Calculate();

        public void AddEmployee(Employee NewEmployee)
        {
            bool NoAdd = false;

            NewEmployee.Validate();

            for (int Position = 0; Position < NewHiredEmployee.Count; Position++)
            {
                if (NewEmployee.Registry == NewHiredEmployee[Position].Registry)
                {
                    NoAdd = true;

                    Console.WriteLine("This Employee, Registry Number " + NewEmployee.Registry + " have already been hired. For this reason, your Add was not completed.");
                }
            }
            if (NoAdd == false)
            {
                NewHiredEmployee.Add(NewEmployee);
            }
        }

        public void ListEmployee()
        {
            foreach (Employee Line in NewHiredEmployee)
            {
                Console.WriteLine("Employee Name: " + Line.Name + "\t CPF: " + Line.NumberEmployee.Number + "\t Registry: " + Line.Registry + "\t Date Start: " + Line.DateStart.ToString("dd/MM/yyyy") + "\t Salary: " + Line.MonthlySalary.ToString("C2"));
            }
        }

        public void ShowBirthdayCompany()
        {
            for (int Position = 0; Position < NewHiredEmployee.Count; Position++)
            {
                TimeSpan Difference = DateTime.Now - NewHiredEmployee[Position].DateStart;

                if (Difference.Days >= 365)
                {
                    if (NewHiredEmployee[Position].DateStart.Month == DateTime.Now.Month
                        && NewHiredEmployee[Position].DateStart.Day <= DateTime.Now.AddDays(15).Day)
                    {
                        Console.WriteLine("Congratulations!!! " + NewHiredEmployee[Position].Name + " Now you are having 1 year with us! We hope to have a lot more years together!");

                    }
                }
            }
        }

        internal void FindOldestEmployee()
        {
            DateTime Oldest = DateTime.Now;

            for (int Position = 0; Position < NewHiredEmployee.Count; Position++)
            {
                if (NewHiredEmployee[Position].DateStart < Oldest)
                {
                    Oldest = NewHiredEmployee[Position].DateStart;
                }
            }

            foreach (Employee Line in NewHiredEmployee)
            {
                if (Oldest == Line.DateStart)
                {
                    Console.WriteLine(Line.Name + " you are the oldest employee in this company. You have been with us since " + Line.DateStart.ToString("dd/MM/yyyy") + " for this reason your total time working here is " + (DateTime.Now.Year - Line.DateStart.Year) + " year(s) " + (DateTime.Now.Month - Line.DateStart.Month) + " month(s) and " + (DateTime.Now.Day - Line.DateStart.Day) + " day(s). We Hope you continue with us for a long time!");
                }
            }
        }

       
        public void PromoteEmployee(string Registry, double Percentage)
        {
            Employee Employee = CheckRegistry(Registry);
            double Promote = Employee.MonthlySalary * Percentage;
            Employee.MonthlySalary = Employee.MonthlySalary + Promote;
        }

        public void UnionAgreement(double Percentage, DateTime DateAgreement)
        {
            for (int Position = 0; Position < NewHiredEmployee.Count; Position++)
            {
                Employee Proportional = NewHiredEmployee[Position];
                TimeSpan DaysTotal = DateAgreement - Proportional.DateStart;

                if(DaysTotal.Days > 0)
                {
                    if (DaysTotal.Days < 365)
                    {
                        int DaysActualYear = (DaysTotal.Days % 365) + 1;

                        double PercentageProportional = (Percentage / 365) * DaysActualYear;

                        PromoteEmployee(NewHiredEmployee[Position].Registry, PercentageProportional);
                    }
                    else
                    {
                        PromoteEmployee(NewHiredEmployee[Position].Registry, Percentage);
                    }
                }
            }
        }
       
        public void DismissEmployee(string Registry)
        {
            Employee EmployeeRescisao = CheckRegistry(Registry);

            RemovedEmployee.Add(EmployeeRescisao);

            NewHiredEmployee.Remove(EmployeeRescisao);

            Console.WriteLine("The Employee " + EmployeeRescisao.Name + " Registry Number: " + EmployeeRescisao.Registry + " was Dismiss.");

        }

        public void CalculateSalary(DateTime Competencia)
        {
            Calculate.CalculateSalary(Competencia, NewHiredEmployee);
        }

        public Employee CheckRegistry(string Registry)
        {
            for (int Position = 0; Position < NewHiredEmployee.Count; Position++)
            {
                if (NewHiredEmployee[Position].Registry == Registry)
                {
                    return NewHiredEmployee[Position];
                }
            }
            return null;
        }

        public void Calculate13Salary(DateTime Year13)
        {
            Calculate.Calculate13Salary(Year13, NewHiredEmployee);
        }

        public void Rescisao(string NumberRegistry, DateTime DateExit)
        {
            Calculate.Rescisao(NumberRegistry, DateExit, RemovedEmployee);
        }
    }
}
