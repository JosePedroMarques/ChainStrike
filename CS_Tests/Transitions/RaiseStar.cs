using CS_Tests.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Tests.Transitions
{
    public static class RaiseStar 
    {
        private static readonly Dictionary<int, int> _evoStonesPerStar = new Dictionary<int, int> { { 1, 5 }, { 2, 10 }, { 3, 30 }, { 4, 65 }, { 5, 70 } };
        private static readonly Dictionary<int, int> _moveStonesPerStar = new Dictionary<int, int> { { 1, 2 }, { 2, 10 }, { 3, 20 }, { 4, 45 }, { 5, 40 } };
        private static readonly Dictionary<int, int> _costPerStar = new Dictionary<int, int> { { 1, 5000 }, { 2, 10000 }, { 3, 20000 }, { 4, 30000 }, { 5, 40000 } };
        public static Node Evolve(this Node from,Cost cost)
        {
            cost.UsedNodes.Add(new Node(from.Stars));
            from.Stars++;
            cost.EvolutionStones += _evoStonesPerStar[from.Level];
            cost.MovementStones += _moveStonesPerStar[from.Level];
            cost.Gold += _costPerStar[from.Level];
            return new Node(from.Stars) { XP = 1 };
        }

        public static bool CanEvolve(Node from)
        {
            return from.Stars == 1 && from.Level >= 5000 && from.XP == 1 ;
        }
    }
}
