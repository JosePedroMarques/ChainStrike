using CS_Tests.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Tests.Transitions
{
    public static class PowerUp
    {
        public static readonly Dictionary<int, int> PowerUpPercent = new Dictionary<int, int> {
            { 5,5000 }, { 4, 5000 }, { 3, 4000 }, { 2, 3000 }, { 1, 2000 }, { 0, 1000 }, { -1, 400 }, { -2, 150 }, { -3, 25 }, {-4,5 }, {-5,1 } };
        private static readonly Dictionary<int, int> _cost = new Dictionary<int, int> { { 6, 500 }, { 5, 400 }, { 4, 300 }, { 3, 200 }, { 2, 100 }, { 1, 50 } };
        public static void ApplyPowerUp(this Node from, Cost cost, Node[] input)
        {
            foreach (var node in input)
            {
                cost.UsedNodes.Add(node);
                cost.Gold += _cost[from.Stars];
                from.Level += PowerUpPercent[from.Stars - node.Stars];
                if (from.Level >= 5000) break;
            }
           
        }

        public static bool CanPowerUp(Node from)
        {
            return from.Level < 5000;
        }
    }
}
