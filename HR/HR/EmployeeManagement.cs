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
                Console.WriteLine("Employee Name: " + Line.Name + "\t CPF: " + Line.CPF + "\t Registry: " + Line.Registry + "\t Date Start: " + Line.DateStart.ToString("dd/MM/yyyy"));
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
                    DateTime Today = DateTime.Now;
                    int Year = Today.Year - Line.DateStart.Year;
                    int Month = Today.Month - Line.DateStart.Month;
                    int Day = Today.Day - Line.DateStart.Day;

                    Console.WriteLine(" ");
                    Console.WriteLine(Line.Name + " you are the oldest employee in this company. You have been with us since " + Line.DateStart.ToString("dd/MM/yyyy") + " for this reason your total time working here is " + Year + " year(s) " + Month + " month(s) and " + Day + " day(s). We Hope you continue with us for a long time!");
                    Console.WriteLine(" ");
                }
            }
        }

        public void CalculateSalary(DateTime Competencia)
        {
            foreach (Employee Line in NewHiredEmployee)
            {
                TimeSpan Compare = Competencia - Line.DateStart;

                //Para verificar se a data de contratacao esta dentro do periodo de pagamento, nao no futuro.
                if (Compare.Days > 0)
                {
                    double Salary = 0;
                    int DaysWorked = 0;

                    //Para verificar se o funcionario foi contratado no mesmo mes e ano da competencia de pagamento e em caso positivo, calcular o salario proporcional
                    if (Line.DateStart.Month == Competencia.Month && Line.DateStart.Year == Competencia.Year)
                    {
                        DaysWorked = ((System.DateTime.DaysInMonth(Competencia.Year, Competencia.Month) - Line.DateStart.Day) + 1);
                        Salary = (Line.MonthlySalary / System.DateTime.DaysInMonth(Competencia.Year, Competencia.Month)) * DaysWorked;
                    }
                    else
                    {
                        DaysWorked = System.DateTime.DaysInMonth(Competencia.Year, Competencia.Month);
                        Salary = Line.MonthlySalary;
                    }

                    double INSS = Salary * 0.07;
                    double IRRF = (Salary - INSS) * 0.15;

                    Console.WriteLine("Hello, " + Line.Name + ", Number Registry " + Line.Registry + " follow below your salary details: ");
                    Console.WriteLine("Monthly Salary: " + (Line.MonthlySalary).ToString("C2"));
                    Console.WriteLine("Date Start in this Company: " + Line.DateStart);
                    Console.WriteLine("Worked days in " + Competencia.Month + "/" + Competencia.Year + ": " + DaysWorked);
                    Console.WriteLine("Monthly Salary Proportional Worked Days: " + Salary.ToString("C2"));
                    Console.WriteLine("INSS: " + (-INSS).ToString("C2"));
                    Console.WriteLine("IRRF: " + (-IRRF).ToString("C2"));
                    Console.WriteLine("Liquid Salary: " + (Salary - INSS - IRRF).ToString("C2"));
                }
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
            foreach (Employee Line in NewHiredEmployee)
            {
                TimeSpan Compare = Year13 - Line.DateStart;

                if (Compare.Days > 0)
                {
                    double Salary13 = 0;

                    if (Compare.Days < 365)
                    {
                        int Months = ((Compare.Days % 365) / 30) + 1;

                        if (Line.DateStart.Day > 15)
                        {
                            Months = Months - 1;
                        }
                        Salary13 = (Line.MonthlySalary / 12) * Months;
                    }
                    else
                    {
                        Salary13 = Line.MonthlySalary;
                    }

                    double INSS = Salary13 * 0.07;
                    double IRRF = (Salary13 - INSS) * 0.15;

                    Console.WriteLine(" ");
                    Console.WriteLine(Line.Name + ", Number Registry: " + Line.Registry + " Your 13 Salary about year " + Year13.Year + " is " + Salary13.ToString("C2") + ", follow below the detailes about the payment: ");
                    Console.WriteLine("Date Start in this Company: " + Line.DateStart);
                    Console.WriteLine("INSS: " + (-INSS).ToString("C2"));
                    Console.WriteLine("IRRF: " + (-IRRF).ToString("C2"));
                    Console.WriteLine("Liquid Salary 13: " + (Salary13 - INSS - IRRF).ToString("C2"));
                    Console.WriteLine(" ");
                }
            }
        }

        public void Rescisao(string NumberRegistry, DateTime DateExit)
        {
            //PROPORCIONAL CALCULO FERIAS;
            TimeSpan Time = DateExit - NewHiredEmployee[CheckRegistry(NumberRegistry)].DateStart;

            int Months = (Time.Days % 365) / 30;

            if (NewHiredEmployee[CheckRegistry(NumberRegistry)].DateStart.Day < 15)
            {
                Months = Months + 1;
            }
            double RescisaoHolidaysMonths = ((NewHiredEmployee[CheckRegistry(NumberRegistry)].MonthlySalary) / 12) * Months;

            //SALARIO (SE DATA DE SAIDA APOS DIA 15);
            double RescisaoSalary = 0;

            if (DateExit.Day > 15)
            {
                RescisaoSalary = NewHiredEmployee[CheckRegistry(NumberRegistry)].MonthlySalary;
            }

            //Impostos s/ LaborHolidays + Proportional Salary;
            double INSS = (RescisaoHolidaysMonths + RescisaoSalary) * 0.07;
            double IRRF = ((RescisaoHolidaysMonths + RescisaoSalary) - INSS) * 0.15;

            Console.WriteLine("Rescisao Calculation");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Proportional Salary: " + (RescisaoSalary).ToString("C"));
            Console.WriteLine("Labor Holidays: " + (RescisaoHolidaysMonths).ToString("C"));
            Console.WriteLine("Add 33% Labor Holidays: " + (RescisaoHolidaysMonths * 0.33).ToString("C2"));
            Console.WriteLine("BC para impostos: " + (RescisaoHolidaysMonths + RescisaoSalary).ToString("C"));
            Console.WriteLine("INSS: " + (-INSS).ToString("C2"));
            Console.WriteLine("IRRF: " + (-IRRF).ToString("C2"));
            Console.WriteLine("Rescisao to receive: " + ((RescisaoHolidaysMonths + RescisaoSalary) - INSS - IRRF).ToString("C2"));

        }

    }
}
