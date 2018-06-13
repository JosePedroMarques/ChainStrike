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
        public Units Units = new Units();
        public List<string> Actions = new List<string>();
        public Units Input;
        public void Add(EvolutionOutcome evolutionOutcome)
        {
           Cost.Add(evolutionOutcome.Cost);
            Units.Add(evolutionOutcome.Units);
        }
    }
}
