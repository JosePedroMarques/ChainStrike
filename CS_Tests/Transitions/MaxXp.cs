using CS_Tests.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Tests.Transitions
{
    public static class MaxXp 
    {
        private static readonly Dictionary<int, int> _xpRequired = new Dictionary<int, int>
        {
            {1,11190 },
            {2,  29808},
            {3, 81000 },
            {4, 228000 },
            {5,707000  },
        };

        public static void MakeMaxLevel(this Node from, Cost cost)
        {
            from.XP = 1;
            cost.Experience += _xpRequired[from.Stars];
            
        }

        public static bool CanMakeMaxLevel(Node from)
        {
            return from.XP == 0;
        }
    }
}
