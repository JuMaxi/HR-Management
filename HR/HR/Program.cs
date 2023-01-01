using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;

namespace HR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee Employee1 = new Employee();
            DateTime Date1 = new DateTime(2022, 12, 01);
            Employee1.Initialize("Joao da Silva", "45.125", "123.456.789-10", Date1, 3000);
            
            
            Employee Employee2 = new Employee();
            DateTime Date2 = new DateTime(2022, 12, 10);
            Employee2.Initialize("Joao Santos", "45.121", "234.569.458-23", Date2, 1800);

            Employee Employee3 = new Employee();
            DateTime Date3 = new DateTime(2022, 12, 12);
            Employee3.Initialize("Maria Lima", "45.123", "564.425.569-40", Date3, 2500);


            EmployeeManagement AddNewEmployee = new EmployeeManagement();
            AddNewEmployee.AddEmployee(Employee1);
            AddNewEmployee.AddEmployee(Employee2);
            AddNewEmployee.AddEmployee(Employee3);

            // Part 4
            string Path = @"C:\Dev\RH\HR\HR\Employees2.csv";
            string[] Read = File.ReadAllLines(Path);

            for(int Position = 1; Position < Read.Length; Position++) 
            {
                string[] Break = Read[Position].Split(";");
                Employee Employee4 = new Employee();

                string DateReplace = Break[3];
                DateReplace = DateReplace.Replace('-', '/');
                DateTime Date = Convert.ToDateTime(DateReplace);

                string SalaryReplace = Break[4];
                SalaryReplace = SalaryReplace.Replace(".","");
                SalaryReplace = SalaryReplace.Replace(',','.');
                double Salary = Convert.ToDouble(SalaryReplace);

                Employee4.Initialize(Break[0], Break[1], Break[2], Date, Salary);

                AddNewEmployee.AddEmployee(Employee4);
            }

            AddNewEmployee.ListEmployee();

            // Part 5
            AddNewEmployee.ShowBirthdayCompany();

            // Part 6
            AddNewEmployee.FindOldestEmployee();

            // Part 10
            AddNewEmployee.PromoteEmployee("55525", 0.10);

            // Part 7
            DateTime Competencia = new DateTime(2022, 12, 01);
            AddNewEmployee.CalculateSalary(Competencia);

            //Part 8
            AddNewEmployee.DismissEmployee("25936");
        }
    }
}
