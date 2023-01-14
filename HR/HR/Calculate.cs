using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR
{
    public class Calculate
    {
        public void CalculateTax(double Salary, Employee Line)
        {
            if (Salary > 0)
            {
                double INSS = Salary * 0.07;
                double IRRF = (Salary - INSS) * 0.15;

                Console.WriteLine(" ");
                Console.WriteLine("Hello, " + Line.Name + ", Number Registry " + Line.Registry + " here are your salary details: ");
                Console.WriteLine("Monthly Salary: " + (Salary).ToString("C2"));
                Console.WriteLine("Date Start in this Company: " + Line.DateStart.ToString("yyyy/MM/dd"));
                Console.WriteLine("INSS: " + (-INSS).ToString("C2"));
                Console.WriteLine("IRRF: " + (-IRRF).ToString("C2"));
                Console.WriteLine("Liquid Salary: " + (Salary - INSS - IRRF).ToString("C2"));
                Console.WriteLine(" ");
            }
        }
    }
}
