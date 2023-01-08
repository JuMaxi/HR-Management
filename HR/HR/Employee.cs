using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR
{
    public class Employee
    {
        public string Name;
        public string Registry;
        public string CPF;
        public DateTime DateStart;
        public double MonthlySalary;

        public void Initialize(string NameEmployee, string NumberRegistry, string NumberCPF, DateTime Date, double Salary)
        {
            Name = NameEmployee;
            Registry = NumberRegistry;
            CPF = NumberCPF;
            DateStart = Date;
            MonthlySalary = Salary;
        }

        public void Validate()
        {
            string NameTrim = Name.Trim();
            Name = NameTrim;

            if (Name.IndexOf(" ") < 0)
            {
                throw new Exception("The name is not completed. You must fill this field with the Complet Name.");
            }

            if (Name == " ")
            {
                throw new Exception("The Name is null. You must fill this field.");
            }
            if (Registry == " ")
            {
                throw new Exception("The Registry is null. For continue, you must fill this field.");
            }
            if (CPF == " ")
            {
                throw new Exception("The CPF is null. For continue, you must fill this field.");
            }
            if (MonthlySalary < 0)
            {
                throw new Exception("The Monthly Salary is null. For continue, you must fill this field with a value bigger than zero.");
            }
        }

        public void CPFValidate(string NumberCPF)
        {
            NumberCPF = NumberCPF.Replace(".", "");
            NumberCPF = NumberCPF.Replace("-", "");
            int FirstDig = 0;
            int SecondDig = 0;
            

            for (int Position = 0; Position < (NumberCPF.Length - 1); Position++)
            {
                char Number = NumberCPF[Position];
                string teste = Convert.ToString(Number);
                int teste2 = Convert.ToInt32(teste);

                if(Position < 9)
                {
                    int Temp = teste2 * ((NumberCPF.Length-1) - Position);
                    FirstDig = FirstDig + Temp;
                }
                if(Position < 10)
                {
                    int Temp = teste2 * (NumberCPF.Length - Position);
                    SecondDig = SecondDig + Temp;
                }
            }

            FirstDig = FirstDig % NumberCPF.Length;
            if(FirstDig < 2)
            {
                FirstDig= 0;
            }
            else
            {
                FirstDig = NumberCPF.Length - FirstDig;
            }

            SecondDig = SecondDig % NumberCPF.Length;
            if(SecondDig < 2)
            {
                SecondDig= 0;
            }
            else
            {
                SecondDig= NumberCPF.Length - SecondDig;
            }

            int teste3 = 0;
            for(int Position = 9; Position < NumberCPF.Length-1; Position++)
            {
                if(FirstDig == NumberCPF[Position] && SecondDig == NumberCPF[Position+1])
                {
                    Console.WriteLine("esta tudo certo.");
                }
            }
        }







    }
}
