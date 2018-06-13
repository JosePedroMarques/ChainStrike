using CS_Tests.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS_Tests.Transitions;

namespace CS_Tests
{
    public class Cost
    {
        public int Experience = 0;
        public int EvolutionStones = 0;
        public int MovementStones = 0;
        public int Gold = 0;
        public Units UsedUnits = new Units();

        public void Add(Cost cost)
        {
            Experience += cost.Experience;
            EvolutionStones += cost.EvolutionStones;
            MovementStones += cost.MovementStones;
            Gold += cost.Gold;
            UsedUnits.Add(cost.UsedUnits);
        }
    }
}
