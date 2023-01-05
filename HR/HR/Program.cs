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
            EmployeeManagement SecondClass = new EmployeeManagement();

            Menu StartMenu = new Menu();

            StartMenu.Options(SecondClass);
        }
    }
}
