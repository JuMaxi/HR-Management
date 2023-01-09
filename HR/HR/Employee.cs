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
        public string ToReplace(string CPFReplace)
        {
            CPFReplace = CPFReplace.Replace(".", "");
            CPFReplace = CPFReplace.Replace("-", "");

            return CPFReplace;
        }

        public int SumDig(int Modify)
        {
            int Number = 0;

            for (int Position = 0; Position < ToReplace(CPF).Length - (Modify +1); Position++)
            {
                int NumberTemp = Convert.ToInt32(Convert.ToString(ToReplace(CPF)[Position]));

                int Temp = NumberTemp * ((ToReplace(CPF).Length - Modify) - Position);

                Number = Number + Temp;
            }

            return Number;
        }

        public int FindDigit(int Digit)
        {
            Digit = Digit % ToReplace(CPF).Length;

            if (Digit < 2)
            {
                Digit = 0;
            }
            else
            {
                Digit = ToReplace(CPF).Length - Digit;
            }

            return Digit;
        }

        public void ToCompare(int DigitCompare, int Position)
        {
            int Compare = Convert.ToInt32(Convert.ToString(ToReplace(CPF)[Position]));

            if (Compare != DigitCompare)
            {
                throw new Exception("This Number CPF is invalid. Employee: " + Name + ".");
            }
        }

        public void CPFValidate()
        {
            int FirstDig = SumDig(1);
            int SecondDig = SumDig(0);

            FirstDig = FindDigit(FirstDig);
            SecondDig = FindDigit(SecondDig);

            ToCompare(FirstDig, ToReplace(CPF).Length - 2);
            ToCompare(SecondDig, ToReplace(CPF).Length - 1);
        }







    }
}

