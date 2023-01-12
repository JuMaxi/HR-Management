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

        public void AddEmployee(Employee NewEmployee)
        {
            bool NoAdd = false;

            NewEmployee.Validate();
            NewEmployee.CPFValidate();

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
                Console.WriteLine("Employee Name: " + Line.Name + "\t CPF: " + Line.CPF + "\t Registry: " + Line.Registry + "\t Date Start: " + Line.DateStart.ToString("dd/MM/yyyy") + "\t Salary: " + Line.MonthlySalary.ToString("C2"));
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

        public void CalculateTax(double Salary, int Position)
        {
            Employee Line = NewHiredEmployee[Position];

            double INSS = Salary * 0.07;
            double IRRF = (Salary - INSS) * 0.15;

            Console.WriteLine(" ");
            Console.WriteLine("Hello, " + Line.Name + ", Number Registry " + Line.Registry + " follow below your salary details: ");
            Console.WriteLine("Monthly Salary: " + (Salary).ToString("C2"));
            Console.WriteLine("Date Start in this Company: " + Line.DateStart);
            Console.WriteLine("INSS: " + (-INSS).ToString("C2"));
            Console.WriteLine("IRRF: " + (-IRRF).ToString("C2"));
            Console.WriteLine("Liquid Salary: " + (Salary - INSS - IRRF).ToString("C2"));
            Console.WriteLine(" ");
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
                CalculateTax(Salary, Position);
            }
        }

        public int CheckRegistry(string Registry)
        {
            int Position;

            for (Position = 0; Position < NewHiredEmployee.Count; Position++)
            {
                if (NewHiredEmployee[Position].Registry == Registry)
                {
                    return Position;
                }
            }
            return Position;
        }
        public void DismissEmployee(string Registry)
        {
            Console.WriteLine("The Employee " + NewHiredEmployee[CheckRegistry(Registry)].Name + " Registry Number: " + NewHiredEmployee[CheckRegistry(Registry)].Registry + " was Dismiss.");

            NewHiredEmployee.Remove(NewHiredEmployee[CheckRegistry(Registry)]);
        }

        public double CalculatePercentage(double Percentage, int Position)
        {
            DateTime Year = new DateTime(2022, 12, 31);
            TimeSpan Difference = Year - NewHiredEmployee[Position].DateStart;
            double ProportionalPercentage = 0;

            if (Difference.Days < 365)
            {
                if (Difference.Days < 30)
                {
                    ProportionalPercentage = Percentage / 12;
                }
                else
                {
                    if (Difference.Days % 30 != 0)
                    {
                        ProportionalPercentage = (Percentage / 12) * ((Difference.Days / 30) + 1);
                    }
                    else
                    {
                        ProportionalPercentage = (Percentage / 12) * (Difference.Days / 30);
                    }
                }
                return ProportionalPercentage;
            }
            else
            {
                return Percentage;
            }
        }
        public void PromoteEmployee(string Registry, double Percentage)
        {
            double Promote = ((NewHiredEmployee[CheckRegistry(Registry)]).MonthlySalary) * Percentage;
            NewHiredEmployee[CheckRegistry(Registry)].MonthlySalary = (NewHiredEmployee[CheckRegistry(Registry)].MonthlySalary) + Promote;

        }

        public void UnionAgreement(double Percentage)
        {
            for (int Position = 0; Position < NewHiredEmployee.Count; Position++)
            {
                double Percentage2 = CalculatePercentage(Percentage, Position);
                PromoteEmployee(NewHiredEmployee[Position].Registry, Percentage2);
            }
        }
        public void Calculate13Salary(DateTime Year13)
        {
            for (int Position = 0; Position <= NewHiredEmployee.Count; Position++)
            {
                double Salary = NewHiredEmployee[Position].MonthlySalary;

                if (Year13 > NewHiredEmployee[Position].DateStart)
                {
                    if (Year13.Year == NewHiredEmployee[Position].DateStart.Year)
                    {
                        Salary = CalculateSalaryProportional(Position, Year13);
                    }

                }
                CalculateTax(Salary, Position);
            }
        }

        public double CalculateSalaryProportional(int Position, DateTime Date)
        {
            Employee Line = NewHiredEmployee[Position];

            TimeSpan DaysWorked = Date - Line.DateStart;
            int DaysMonthStart = DateTime.DaysInMonth(Line.DateStart.Year, Line.DateStart.Month);
            double Salary = 0;

            if (Line.DateStart.Day > 15)
            {
                Salary = ((Line.MonthlySalary / 365) * (DaysWorked.Days - DaysMonthStart));
            }
            else
            {
                Salary = (Line.MonthlySalary / 365) * DaysWorked.Days;
            }
            return Salary;
        }
        public void Rescisao(string NumberRegistry, DateTime DateExit)
        {
            //PROPORCIONAL CALCULO FERIAS;
            double HolidaysRescisao = CalculateSalaryProportional(CheckRegistry(NumberRegistry), DateExit);
            double SalaryRescisao = 0;

            Employee EmployeeRescisao = NewHiredEmployee[CheckRegistry(NumberRegistry)];
            if (DateExit.Day > 15)
            {
                SalaryRescisao = EmployeeRescisao.MonthlySalary;
            }

            Console.WriteLine("Rescisao Calculation");
            CalculateTax((HolidaysRescisao + SalaryRescisao + (HolidaysRescisao * 0.33)), CheckRegistry(NumberRegistry));

        }

    }
}
