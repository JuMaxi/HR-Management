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

        public void CalculateTax(double Salary, Employee Line)
        {
            if (Salary > 0)
            {
                double INSS = Salary * 0.07;
                double IRRF = (Salary - INSS) * 0.15;

                Console.WriteLine(" ");
                Console.WriteLine("Hello, " + Line.Name + ", Number Registry " + Line.Registry + " here are your salary details: ");
                Console.WriteLine("Monthly Salary: " + (Salary).ToString("C2"));
                Console.WriteLine("Date Start in this Company: " + Line.DateStart.ToString("yyyy/MM/dd"));
                Console.WriteLine("INSS: " + (-INSS).ToString("C2"));
                Console.WriteLine("IRRF: " + (-IRRF).ToString("C2"));
                Console.WriteLine("Liquid Salary: " + (Salary - INSS - IRRF).ToString("C2"));
                Console.WriteLine(" ");
            }
        }

        public void CalculateSalary(DateTime Competencia)
        {
            for (int Position = 0; Position < NewHiredEmployee.Count; Position++)
            {
                Employee Line = NewHiredEmployee[Position];
                double Salary = Line.MonthlySalary;

                if (Competencia > Line.DateStart)
                {
                    if (Competencia.Year == Line.DateStart.Year)
                    {
                        if (Competencia.Month == Line.DateStart.Month)
                        {
                            int DaysMonth = DateTime.DaysInMonth(Competencia.Year, Competencia.Month);
                            Salary = (Line.MonthlySalary / DaysMonth) * ((DaysMonth - Line.DateStart.Day) + 1);
                        }
                    }
                }
                CalculateTax(Salary, Line);
            }
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
        public void Calculate13Salary(DateTime Year13)
        {
            for (int Position = 0; Position < NewHiredEmployee.Count; Position++)
            {
                double Salary = 0;
                if (Year13 > NewHiredEmployee[Position].DateStart)
                {
                    if (Year13.Year == NewHiredEmployee[Position].DateStart.Year)
                    {
                        Salary = CalculateSalaryProportional(NewHiredEmployee[Position], Year13);
                    }
                    else
                    {
                        Salary = NewHiredEmployee[Position].MonthlySalary;
                    }

                }
                CalculateTax(Salary, NewHiredEmployee[Position]);
            }
        }

        public double CalculateSalaryProportional(Employee EmployeeProportional, DateTime Date)
        {
            TimeSpan DaysTotal = Date - EmployeeProportional.DateStart;
            int DaysActualYear = (DaysTotal.Days % 365) + 1;
            double Salary = 0;

            if (DaysTotal.Days < 365 && EmployeeProportional.DateStart.Day > 15)
            {
                int DaysMonthStart = DateTime.DaysInMonth(EmployeeProportional.DateStart.Year, EmployeeProportional.DateStart.Month);
                Salary = ((EmployeeProportional.MonthlySalary / 365) * (DaysActualYear - DaysMonthStart));

                if (Salary < 0)
                {
                    Salary = 0;
                }
            }
            else
            {
                Salary = (EmployeeProportional.MonthlySalary / 365) * DaysActualYear;
            }
            return Salary;
        }
        public void Rescisao(string NumberRegistry, DateTime DateExit)
        {
            //PROPORCIONAL CALCULO FERIAS;

            foreach (Employee EmployeeRescisao in RemovedEmployee)
            {
                if (NumberRegistry == EmployeeRescisao.Registry)
                {
                    double SalaryRescisao = 0;
                    double HolidaysRescisao = CalculateSalaryProportional(EmployeeRescisao, DateExit);

                    if (DateExit.Day > 15)
                    {
                        SalaryRescisao = EmployeeRescisao.MonthlySalary;
                    }
                    Console.WriteLine("");
                    Console.WriteLine("Rescisao Calculation");
                    CalculateTax((HolidaysRescisao + SalaryRescisao + (HolidaysRescisao * 0.33)), EmployeeRescisao);
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

    }
}
