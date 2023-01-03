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

            NewEmployee.Validate();

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

        public void PromoteEmployee(string Registry, double Percentage)
        {
            foreach (Employee Line in NewHiredEmployee)
            {
                if (Line.Registry == Registry)
                {
                    double Promote = (Line.MonthlySalary) * Percentage;
                    Line.MonthlySalary = Line.MonthlySalary + Promote;
                }
            }
        }

        public void UnionAgreement(double Percentage)
        {
            foreach (Employee Line in NewHiredEmployee)
            {
                DateTime Now = DateTime.Now;
                int WorkedMonthsYear = 0;

                int Year = Now.Year - Line.DateStart.Year;
                int Months = Now.Month - Line.DateStart.Month;

                if (Year < 2)
                {
                    if (Year == 1)
                    {
                        if (Months != 0)
                        {
                            if (Months < 0)
                            {
                                WorkedMonthsYear = Months + 11;
                            }
                            else
                            {
                                WorkedMonthsYear = Months - 1;
                            }
                        }
                    }
                    else
                    {
                        WorkedMonthsYear = Months - 1;
                    }
                    //Calcula o percentual proporcional mensal;
                    double ProportionalPercentageMonth = (Percentage / 12);

                    //Calcula quantos dias tem o mes de contratacao;
                    int DaysMonth = System.DateTime.DaysInMonth(Line.DateStart.Year, Line.DateStart.Month);

                    //Calcula o percentual proporcional diario;
                    double ProportionalPercentageDay = ProportionalPercentageMonth / DaysMonth;

                    //Calcula os dias trabalhados no mes de contratacao
                    int WorkedDaysMonth = (DaysMonth - Line.DateStart.Day) + 1;

                    //Calcula os totais proporcionais de meses/anocontratacao trabalhados e dias/mes contratacao trabalhados;
                    double TotalProportionalPercentage = (ProportionalPercentageMonth * WorkedMonthsYear) + (ProportionalPercentageDay * WorkedDaysMonth);

                    PromoteEmployee(Line.Registry, TotalProportionalPercentage);
                }
                else
                {
                    PromoteEmployee(Line.Registry, Percentage);
                }
            }
        }

        public void Calculate13Salary(DateTime Year13)
        {
            double LiquidSalary13 = 0;

            foreach (Employee Line in NewHiredEmployee)
            {
                if (Year13.Year >= Line.DateStart.Year)
                {
                    double Salary13 = 0;

                    if (Year13.Year == Line.DateStart.Year)
                    {
                        int ProportionalMonths13 = (Year13.Month - Line.DateStart.Month) + 1;

                        if (Line.DateStart.Day > 15)
                        {
                            ProportionalMonths13 = ProportionalMonths13 - 1;
                        }

                        Salary13 = (Line.MonthlySalary / 12) * (ProportionalMonths13);
                    }
                    else
                    {
                        Salary13 = Line.MonthlySalary;
                    }

                    double INSS = Salary13 * 0.07;
                    double IRRF = (Salary13 - INSS) * 0.15;

                    LiquidSalary13 = Salary13 - INSS - IRRF;

                    if (LiquidSalary13 > 0)
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine(Line.Name + " Number Registry: " + Line.Registry + " Your 13 Salary about year " + Year13.Year + " is " + Salary13.ToString("C2") + ", follow below the detailes about the payment: ");
                        Console.WriteLine("INSS: " + (-INSS).ToString("C2"));
                        Console.WriteLine("IRRF: " + (-IRRF).ToString("C2"));
                        Console.WriteLine("Liquid Salary 13: " + LiquidSalary13.ToString("C2"));
                        Console.WriteLine(" ");
                    }
                }
            }
        }

        public void Rescisao(string NumberRegistry, DateTime DateExit)
        {
            foreach (Employee Line in NewHiredEmployee)
            {
                if (NumberRegistry == Line.Registry)
                {
                    //CALCULO DE FERIAS

                    double ProportionalDaysMonthExit = 0;
                    int Months = (DateExit.Month - Line.DateStart.Month) + 1;

                    //Calculo meses cheios trabalhados ate mes anterior a demissao;
                    if (Months <= 0)
                    {
                        Months = Months + 11;
                    }

                    if (Line.DateStart.Day > 15)
                    {
                        Months = Months - 1;
                    }

                    double ProportionalMonths = (Line.MonthlySalary / 12) * Months;

                    //Calculo dias devidos no mes de demissao;
                    if (DateExit.Day > 15)
                    {
                        //Calcula dias do mes de rescisao
                        int DaysinMonth = System.DateTime.DaysInMonth(DateExit.Year, DateExit.Month);

                        //Calcula valor dias de ferias devidos no mes da rescisao (desde que maior que 15);
                        ProportionalDaysMonthExit = ((Line.MonthlySalary / 12) / DaysinMonth) * DateExit.Day;

                        //Calculo salario do mes proporcional aos dias trabalhados, considerando que a demissao ocorreu apos o dia 15;
                        double ProportionalSalary = (Line.MonthlySalary / DaysinMonth) * DateExit.Day;

                        //Impostos s/ Proportional Salary;
                        double INSSSalary = ProportionalSalary * 0.07;
                        double IRRFSalary = (ProportionalSalary - INSSSalary) * 0.15;

                        //Calculo Salario Proporcional Liquido 
                        double LiquidProportionalSalary = ProportionalSalary - INSSSalary - IRRFSalary;

                        if(LiquidProportionalSalary > 0)
                        {
                            Console.WriteLine("Calculation Proportional Salary Rescisao");
                            Console.WriteLine("---------------------------------------------");
                            Console.WriteLine("Montly Salary: " + (Line.MonthlySalary).ToString("C2"));
                            Console.WriteLine("Worked Days in " + DateExit.Month + ": " + DateExit.Day);
                            Console.WriteLine("Proportional Montly Salary, about Worked Days: " + (ProportionalSalary).ToString("C2"));
                            Console.WriteLine("INSS s/ Proportional Montly Salary: " + (-INSSSalary).ToString("C2"));
                            Console.WriteLine("IRRF s/ Proportional Montly Salary: " + (-IRRFSalary).ToString("C2"));
                            Console.WriteLine("Proportional Montly Salary to receive: " + (LiquidProportionalSalary).ToString("C2"));
                        }
                    }

                    //Calculo total proporcionais mensal e diario + (1/3) s/ ferias;
                    double Add33LaborHolidays = (ProportionalDaysMonthExit + ProportionalMonths) * 0.33;
                    double LaborHolidays = ProportionalDaysMonthExit + ProportionalMonths + Add33LaborHolidays;

                    //Impostos s/ LaborHolidays (um terco sobre ferias nao e devido impostos);
                    double INSSLaborHolidays = (ProportionalDaysMonthExit + ProportionalMonths) * 0.07;
                    double IRRFLaborHolidays = ((ProportionalDaysMonthExit + ProportionalMonths) - INSSLaborHolidays) * 0.15;

                    //Calculo liquido de Ferias
                    double LiquidLaborHolidays = LaborHolidays - INSSLaborHolidays - IRRFLaborHolidays;

                    if(LiquidLaborHolidays > 0)
                    {
                        Console.WriteLine("Calculation Proportional Labor Holidays Rescisao");
                        Console.WriteLine("------------------------------------------------");
                        Console.WriteLine("Months Labor Holidays: " + Months);
                        if(ProportionalDaysMonthExit > 0)
                        {
                            Console.WriteLine("Days Labor Holidays month exit: " + DateExit.Day);
                        }
                        Console.WriteLine("Labor Holidays: " + (ProportionalDaysMonthExit + ProportionalMonths).ToString("C"));
                        Console.WriteLine("Add 33% Labor HOlidays: " + (Add33LaborHolidays).ToString("C2"));
                        Console.WriteLine("BC para impostos: " + (ProportionalDaysMonthExit + ProportionalMonths).ToString("C"));
                        Console.WriteLine("INSS Labor Holidays: " + (INSSLaborHolidays).ToString("C2"));
                        Console.WriteLine("IRRF Labor Holidays: " + (IRRFLaborHolidays).ToString("C2"));
                        Console.WriteLine("Labor Holidays to receive: " + (LiquidLaborHolidays).ToString("C2"));
                    }
                }
            }
        }
    }
}
