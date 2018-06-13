using CS_Tests.Nodes;
using CS_Tests.Transitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Tests
{
    class Program
    {
       
        static void Main(string[] args)
        {
            var inv = new Inventory(ExpBooks);
            inv.Go();
            Console.Read();
        }

        static void Go()
        {

        }


        private static double SumAll(EvolutionOutcome o)
        {
            var c = o.Cost;
            return (double) (c.EvolutionStones + c.Experience + c.Gold + c.MovementStones+1000000*c.Books);
        }
        private static double ExpOnly(EvolutionOutcome o)
        {
            var c = o.Cost;
            return c.Experience;
        }

        private static double ExpBooks(EvolutionOutcome o)
        {
            var c = o.Cost;
            return c.Experience/100.0 +c.Books*c.Books-o.OutputUnits.Level1-o.OutputUnits.Level2*10-o.OutputUnits.Level3*100-o.OutputUnits.Level4*1000-o.OutputUnits.Level5*10000;
        }

        private static double UnitCount(EvolutionOutcome o)
        {
            var c = o.Cost;
            return o.Input.Bag.Sum(kvp => kvp.Value);
        }





    }
}
