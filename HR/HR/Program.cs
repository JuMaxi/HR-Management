using System;

namespace HR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee Employee1 = new Employee();
            Employee1.Initialize("Joao da Silva", "123.456.789-10", "45.125", "01/12/2022", 3000);
            
            Employee Employee2 = new Employee();
            Employee2.Initialize("Joao Santos", "234.569.458-23", "45.121", "10/12/2022", 1800);

            Employee Employee3 = new Employee();
            Employee3.Initialize("Maria Lima", "564.425.569-40", "45.123", "12/12/2022", 2500);

            Employee1.CalculateSalary();
            Employee2.CalculateSalary();
            Employee3.CalculateSalary();

            EmployeeManagement Management = new EmployeeManagement();
            Management.AddEmployee(Employee1);
            Management.AddEmployee(Employee2);
            Management.AddEmployee(Employee3);

            Management.ListEmployee();
           
        }
    }
}
