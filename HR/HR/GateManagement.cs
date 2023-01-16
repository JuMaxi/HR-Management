using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR
{
    public class GateManagement
    {
        List<People> AddPeople = new List<People>();

        public void AddVisitor(People NewPerson)
        {
            AddPeople.Add(NewPerson);
        }
        public void CheckAccess(string CPF)
        {
            for (int Position = 0; Position < AddPeople.Count; Position++)
            {
                CPF CallCPF = new CPF(CPF);

                if (CallCPF.Number == AddPeople[Position].NumberCPF.Number)
                {
                    Console.WriteLine("Access permitted to " + AddPeople[Position].Name);
                    Position = AddPeople.Count;
                }
                else
                {
                    Console.WriteLine("Access denied to CPF number " + CallCPF);
                }
            }
        }
    }
}
