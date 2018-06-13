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
        /* Evolution  stones
         *     1    2    3   4   5  6
         * 1   -    5   
         * 2   -    -   15
         * 3   -    -   -   30  40
         * 4   -    -   -   -   65
         * 5   -    -   -   -   -   100
         *
         */

        /* Move  stones
       *     1    2    3   4   5  6
       * 1   -    2
       * 2   -    -   10
       * 3   -    -   -   20   30
       * 4   -    -   -   -    45
       * 5   -    -   -   -    -  70
       *
       */


        /* Gold
       *     1    2    3   4   5  6
       * 1   -    5
       * 2   -    -   10
       * 3   -    -   -   20  30
       * 4   -    -   -   -   30
       * 5   -    -   -   -   -  40
       *
       */


        private static readonly Dictionary<int, int> _evoStonesPerStar = new Dictionary<int, int> { { 1, 5 }, { 2, 15}, { 3, 30 }, { 4, 65 }, { 5, 100 } };
        private static readonly Dictionary<int, int> _moveStonesPerStar = new Dictionary<int, int> { { 1, 2 }, { 2, 10 }, { 3, 20 }, { 4, 45 }, { 5, 70 } };
        private static readonly Dictionary<int, int> _costPerStar = new Dictionary<int, int> { { 1, 5000 }, { 2, 10000 }, { 3, 20000 }, { 4, 30000 }, { 5, 40000 } };
        public static void Evolve(this Node from,EvolutionOutcome evolutionOutcome)
        {
            evolutionOutcome.Cost.UsedUnits.Increment(from.Stars);
            from.Stars++;
            evolutionOutcome.Cost.EvolutionStones += _evoStonesPerStar[from.Stars];
            evolutionOutcome.Cost.MovementStones += _moveStonesPerStar[from.Stars];
            evolutionOutcome.Cost.Gold += _costPerStar[from.Stars];
            evolutionOutcome.Units.Increment(from.Stars);
        }

        public static bool CanEvolve(Node from)
        {
            return from.Stars == 1 && from.Level >= 5000 && from.XP == 1 ;
        }
    }
}
