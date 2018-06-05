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
        const int Max1Star = 5000;
        const int Max2Star = 1000;
        const int Max3Star = 200;
        const int Max4Star = 34;
        const int Max5Star = 5;
        static void Main(string[] args)
        {
            var x = GetBestCost(SumAll, GetCostOfMaking2Star());
            Console.Read();
        }

        static void Go()
        {

        }

        private delegate uint CostAnalysis(Tuple<Node, Cost, List<Node>> cost);

        private static uint SumAll(Tuple<Node, Cost, List<Node>> cost)
        {
            var c = cost.Item2;
            return (uint) (c.EvolutionStones + c.Experience + c.Gold + c.MovementStones);
        }

        private static Cost GetBestCost(CostAnalysis analysis, IEnumerable<Tuple<Node, Cost, List<Node>>> costs)
        {
            var min = uint.MaxValue;
            Cost minCost = null;
            foreach (var cost in costs)
            {
                var price = analysis(cost);
                if (price < min)
                {
                    min = price;
                    minCost = cost.Item2;
                }
            }

            return minCost;
        }

        private static IEnumerable<Tuple<Node,Cost,List<Node>>> GetCostOfMaking2Star()
        {
            var possibleInputs = GetPossiblePowerUpCombos(1);
            foreach(var input in possibleInputs)
            {
                var inputNodes = new Node[input.Item1];
                for (int i = 0; i < input.Item1; i++)
                    inputNodes[i]=new Node(1);
                var from = new Node(1);
                var cost = new Cost();
                from.MakeMaxLevel(cost);
                from.ApplyPowerUp(cost, inputNodes);
                var output = from.Evolve(cost);
                yield return Tuple.Create(from,cost,new List<Node> { output });
                
            }
            
        }

        

        private static IEnumerable<Tuple<Cost, List<Node>>> GetCostOfMaking3Star()
        {
            var best2StarCost = GetBestCost(SumAll, GetCostOfMaking2Star());
            var possibleInputs = GetPossiblePowerUpCombos(2);
            foreach (var input in possibleInputs)
            {
                var cost = new Cost();
                var inputNodes = new Node[input.Item1+input.Item2+input.Item3+input.Item4+input.Item5];
                var i = 0;
                for (;i< input.Item1; i++)
                    inputNodes[i] = new Node(1);
                for(; i < input.Item2; i++)
                {
                    var x = GetBestCost( SumAll, GetCostOfMaking2Star().Select(t => t.Item2));
                }

            }

        }

        static IEnumerable<Tuple<int,int,int,int,int>> GetPossiblePowerUpCombos(int from)
        {
            var coefs = new List<long>();
            for(int i = 1; i<= from; i++)
            {
                coefs.Add(PowerUp.PowerUpPercent[i-from]);
            }
            var min1StarFound = Max1Star;
            var min2StarFound = Max2Star;
            var min3StarFound = Max3Star;
            var min4StarFound = Max4Star;
            var min5StarFound = Max5Star;

            for (int i = 1; i < min1StarFound && coefs.Count >= 1; i++)
            {
                for (int j = 1; j < min2StarFound && coefs.Count >= 2; j++)
                {
                    for (int k = 1; k < min3StarFound && coefs.Count >= 3; k++)
                    {
                        for (int l = 1; l < min4StarFound && coefs.Count >= 4; l++)
                        {
                            for (int m = 1; m < min5StarFound && coefs.Count >= 5; m++)
                            {
                                if (i * coefs[0] + j * coefs[1] + k * coefs[2] + l * coefs[3] + m * coefs[4] >= 5000)
                                {
                                    yield return Tuple.Create(i, j, k, l, m);
                                    if (m < min5StarFound)
                                        min5StarFound = m;
                                    break;
                                }
                            }
                            if (i * coefs[0] + j * coefs[1] + k * coefs[2] + l * coefs[3]  >= 5000)
                            {
                                yield return Tuple.Create(i, j, k, l, 0);
                                if (l < min4StarFound)
                                    min4StarFound = l;
                                break;
                            }
                        }
                        if (i * coefs[0] + j * coefs[1] + k * coefs[2]  >= 5000)
                        {
                            yield return Tuple.Create(i, j, k, 0, 0);
                            if (k < min3StarFound)
                                min3StarFound = k;
                            break;
                        }
                    }
                    if (i * coefs[0] + j * coefs[1]  >= 5000)
                    {
                        yield return Tuple.Create(i, j, 0, 0, 0);
                        if (j < min2StarFound)
                            min2StarFound = j;
                        break;
                    }
                }
                if (i * coefs[0]  >= 5000)
                {
                    yield return Tuple.Create(i, 0, 0, 0, 0);
                    if (i < min1StarFound)
                        min1StarFound = i;
                    break;
                }
            }
        }
    }
}
