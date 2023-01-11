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
                    Console.WriteLine(Line.Name + " you are the oldest employee in this company. You have been with us since " + Line.DateStart.ToString("dd/MM/yyyy") + " for this reason your total time working here is " + (DateTime.Now.Year - Line.DateStart.Year) + " year(s) " + (DateTime.Now.Month - Line.DateStart.Month) + " month(s) and " + (DateTime.Now.Day - Line.DateStart.Day) + " day(s). We Hope you continue with us for a long time!");
                }
            }
        }

        public double CalculateSalaryDue(DateTime MonthPayment, int TrueorFalse)
        {
            for (int Position = 0; Position < NewHiredEmployee.Count; Position++)
            {
                TimeSpan Compare = MonthPayment - NewHiredEmployee[Position].DateStart;

                //Para verificar se a data de contratacao esta dentro do periodo de pagamento, nao no futuro.
                if (Compare.Days > 0)
                {
                    //Para verificar se o funcionario esta a menos de um ano na empresa.
                    if (Compare.Days < 365)
                    {
                        if (TrueorFalse == 1) //13 Salary Proportional
                        {
                            if (NewHiredEmployee[Position].DateStart.Day > 15)
                            {
                                NewHiredEmployee[Position].MonthlySalary = (NewHiredEmployee[Position].MonthlySalary / 365) * (Compare.Days - DateTime.DaysInMonth(NewHiredEmployee[Position].DateStart.Year, NewHiredEmployee[Position].DateStart.Month));
                            }
                            else
                            {
                                NewHiredEmployee[Position].MonthlySalary = (NewHiredEmployee[Position].MonthlySalary / 365) * Compare.Days;
                            }
                        }
                        else
                        {//Salary Proportional
                            if (NewHiredEmployee[Position].DateStart.Month != MonthPayment.Month)
                            {
                                NewHiredEmployee[Position].MonthlySalary = NewHiredEmployee[Position].MonthlySalary;
                            }
                            else
                            {
                                NewHiredEmployee[Position].MonthlySalary = (NewHiredEmployee[Position].MonthlySalary / DateTime.DaysInMonth(MonthPayment.Year, MonthPayment.Month)) * (DateTime.DaysInMonth(MonthPayment.Year, MonthPayment.Month) - NewHiredEmployee[Position].DateStart.Day + 1);//Monthy Salary Proportional
                            }
                        }
                    }
                    else//Salary and 13 Salary Integrals
                    {
                        NewHiredEmployee[Position].MonthlySalary = NewHiredEmployee[Position].MonthlySalary;
                    }
                }
                return NewHiredEmployee[Position].MonthlySalary;
            }
            return 0;
        }
        public double CalculateSalary(DateTime Competencia)
        {
            double Salary = CalculateSalaryDue(Competencia, 0);

            double INSS = Salary * 0.07;
            double IRRF = (Salary - INSS) * 0.15;
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
