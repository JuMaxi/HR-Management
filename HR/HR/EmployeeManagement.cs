using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR
{
    internal class EmployeeManagement
    {
        List<Employee> NewHiredEmployee = new List<Employee>();

        public void AddEmployee(Employee NewEmployee)
        {
            NewHiredEmployee.Add(NewEmployee);
        }

        public void ListEmployee()
        {
            foreach(Employee Line in NewHiredEmployee)
            {
                Console.WriteLine("Employee Name: " + Line.Name + "\t CPF: " + Line.CPF + "\t Registry: " + Line.Registry + "\t Date Start: " + Line.DateStart.ToString("dd/MM/yyyy"));
            }
        }

        public void ShowBirthdayCompany()
        {
            DateTime Now = DateTime.Now;
            DateTime Now15Days = Now.AddDays(15);

            for(int Position = 0; Position < NewHiredEmployee.Count; Position++)
            {
                Employee Birthday = NewHiredEmployee[Position];
                DateTime DateBegin = Birthday.DateStart;
                int YearsAdd = Now15Days.Year - DateBegin.Year;

                if(YearsAdd > 0)
                {
                    DateBegin = DateBegin.AddYears(YearsAdd);

                    if (DateBegin >= Now && DateBegin <= Now15Days)
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine("Congratulations!!! " + NewHiredEmployee[Position].Name + " Now you are having 1 year with us! We hope to have a lot more years together!");
                    }
                }
            }
        }

        public void FindOldestEmployee()
        {
            DateTime Oldest = DateTime.Now;
            
            for(int Position = 0; Position <NewHiredEmployee.Count; Position++)
            {
                if (NewHiredEmployee[Position].DateStart < Oldest)
                {
                    Oldest = NewHiredEmployee[Position].DateStart;
                }
            }

            foreach(Employee Line in NewHiredEmployee)
            {
                if(Oldest == Line.DateStart)
                {
                    DateTime Today = DateTime.Now;
                    int Year = Today.Year - Line.DateStart.Year;
                    int Month = Today.Month - Line.DateStart.Month;
                    int Day = Today.Day - Line.DateStart.Day;

                    Console.WriteLine(" ");
                    Console.WriteLine(Line.Name + " you are the oldest employee in this company. You have been with us since " + Line.DateStart.ToString("dd/MM/yyyy") + " for this reason your total time working here is " + Year + " year(s) " + Month + " month(s) and " + Day + " day(s). We Hope you continue with us for a long time!"  );
                }
            }
        }
    }
}
