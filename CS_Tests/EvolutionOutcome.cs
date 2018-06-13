using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Tests
{
    public class EvolutionOutcome
    {
        public Cost Cost = new Cost();
        public Units OutputUnits = new Units();
        public Units CreatedUnits = new Units();
        public List<string> Actions = new List<string>();
        public Units Input;
        public double Value;
        public string CreateMethod;
    }
}
