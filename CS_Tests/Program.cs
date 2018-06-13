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
            var inv = new Inventory(SumAll);
            inv.Go();
            Console.Read();
        }

        static void Go()
        {

        }


        private static double SumAll(EvolutionOutcome o)
        {
            var c = o.Cost;
            return (double) (c.EvolutionStones + c.Experience + c.Gold + c.MovementStones);
        }

     

        

        
    }
}
