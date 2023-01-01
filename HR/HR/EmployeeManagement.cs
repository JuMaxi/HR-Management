using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HR
{
    internal class EmployeeManagement
    {
        List<Employee> NewHiredEmployee = new List<Employee>();

        public void AddEmployee(Employee NewEmployee)
        {
            bool NoAdd = false;

            for (int Position = 0; Position < NewHiredEmployee.Count; Position++)
            {
                if (NewEmployee.Registry == NewHiredEmployee[Position].Registry)
                {
                    NoAdd = true;

                    Console.WriteLine(" ");
                    Console.WriteLine("This Employee, Registry Number " + NewEmployee.Registry + " have already been hired. For this reason, your Add was not completed.");
                    Console.WriteLine(" ");
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
            DateTime Now = DateTime.Now;
            DateTime Now15Days = Now.AddDays(15);

            for (int Position = 0; Position < NewHiredEmployee.Count; Position++)
            {
                Employee Birthday = NewHiredEmployee[Position];
                DateTime DateBegin = Birthday.DateStart;
                int YearsAdd = Now15Days.Year - DateBegin.Year;

                if (YearsAdd > 0)
                {
                    DateBegin = DateBegin.AddYears(YearsAdd);

                    if (DateBegin >= Now && DateBegin <= Now15Days)
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine("Congratulations!!! " + NewHiredEmployee[Position].Name + " Now you are having 1 year with us! We hope to have a lot more years together!");
                        Console.WriteLine(" ");
                    }
                }
            }
        }

        public void FindOldestEmployee()
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
            int DaysMonth = System.DateTime.DaysInMonth(Competencia.Year, Competencia.Month);

            foreach (Employee Line in NewHiredEmployee)
            {
                //Para verificar se a data de contratacao esta dentro do periodo de pagamento, nao no futuro.
                if (Line.DateStart.Year <= Competencia.Year)
                {
                    double Salary = 0;
                    int DaysWorked = 0;

                    //Para verificar se o funcionario foi contratado no mesmo mes e ano da competencia de pagamento e em caso positivo, calcular o salario proporcional
                    if (Line.DateStart.Month == Competencia.Month && Line.DateStart.Year == Competencia.Year)
                    {
                        DaysWorked = ((DaysMonth - Line.DateStart.Day) + 1);
                        Salary = (Line.MonthlySalary / DaysMonth) * DaysWorked;
                    }
                    else
                    {
                        DaysWorked = DaysMonth;
                        Salary = Line.MonthlySalary;
                    }

                    double INSS = Salary * 0.07;
                    double IRRF = (Salary - INSS) * 0.15;

                    double LiquidSalary = Salary - INSS - IRRF;

                    Console.WriteLine(" ");
                    Console.WriteLine("Hello, " + Line.Name + ", Number Registry " + Line.Registry + " follow below your salary details: ");
                    Console.WriteLine("Monthly Salary: " + (Line.MonthlySalary).ToString("C2"));
                    Console.WriteLine("Worked days in " + Competencia.Month + "/" + Competencia.Year + ": " + DaysWorked);
                    Console.WriteLine("Monthly Salary Proportional Worked Days: " + Salary.ToString("C2"));
                    Console.WriteLine("INSS: " + (-INSS).ToString("C2"));
                    Console.WriteLine("IRRF: " + (-IRRF).ToString("C2"));
                    Console.WriteLine("Liquid Salary: " + LiquidSalary.ToString("C2"));
                    Console.WriteLine(" ");
                }
            }
        }
        public void DismissEmployee(string Registry)
        {
            for (int Position = 0; Position < NewHiredEmployee.Count; Position++)
            {
                if (NewHiredEmployee[Position].Registry == Registry)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("The Employee " + NewHiredEmployee[Position].Name + " Registry Number: " + NewHiredEmployee[Position].Registry + " was Dismiss.");
                    Console.WriteLine(" ");

                    NewHiredEmployee.Remove(NewHiredEmployee[Position]);
                }
            }
        }
    }
}
