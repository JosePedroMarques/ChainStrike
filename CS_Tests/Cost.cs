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
        public int Books = 0;

        public void Add(Cost cost)
        {
            Experience += cost.Experience;
            EvolutionStones += cost.EvolutionStones;
            MovementStones += cost.MovementStones;
            Gold += cost.Gold;
            UsedUnits.Add(cost.UsedUnits);
            Books += cost.Books;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            if (Experience > 0)
                builder.Append($"Experience : {Experience}\t");
            if (EvolutionStones > 0)
                builder.Append($"EvolutionStones : {EvolutionStones}\t");
            if (MovementStones > 0)
                builder.Append($"MovementStones : {MovementStones}\t");
            if (Gold > 0)
                builder.Append($"Gold : {Gold}\t");
            if (Books > 0)
                builder.Append($"Books : {Books}");

            return builder.ToString();
        }
    }
}
