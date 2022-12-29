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
            Employee1.Initialize("Joao da Silva", "45.125", "123.456.789-10", "01/12/2022", 3000);
            
            Employee Employee2 = new Employee();
            Employee2.Initialize("Joao Santos", "45.121", "234.569.458-23", "10/12/2022", 1800);

            Employee Employee3 = new Employee();
            Employee3.Initialize("Maria Lima", "45.123", "564.425.569-40", "12/12/2022", 2500);

            Employee1.CalculateSalary();
            Employee2.CalculateSalary();
            Employee3.CalculateSalary();

            EmployeeManagement AddNewEmployee = new EmployeeManagement();
            AddNewEmployee.AddEmployee(Employee1);
            AddNewEmployee.AddEmployee(Employee2);
            AddNewEmployee.AddEmployee(Employee3);

            //AddNewEmployee.ListEmployee();

            string Path = @"C:\Dev\RH\HR\HR\Employees.csv";
            string[] Read = File.ReadAllLines(Path);

            for(int Position = 1; Position < Read.Length; Position++) 
            {
                string[] Break = Read[Position].Split(";");
                Employee Employee4 = new Employee();

                string teste = Break[4];
                teste = teste.Replace(".","");
                teste = teste.Replace(',','.');

                double Salary = Convert.ToDouble(teste);

                Employee4.Initialize(Break[0], Break[1], Break[2], Break[3], Salary);

                AddNewEmployee.AddEmployee(Employee4);

                Employee4.CalculateSalary();
            }

            AddNewEmployee.ListEmployee();
        }
    }
}
