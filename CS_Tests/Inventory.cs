using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS_Tests.Nodes;
using CS_Tests.Transitions;

namespace CS_Tests
{

    public class Inventory
    {
        private const int MinMakeThreshold = 3;

        private const int Max1Star = 5000;
        private const int Max2Star = 1000;
        private const int Max3Star = 200;
        private const int Max4Star = 34;
        private const int Max5Star = 5;
        private readonly Func<EvolutionOutcome, double> _costAnalysis;
        private readonly Units _units = new Units();

        public Inventory(Func<EvolutionOutcome, double> costAnalysis)
        {
            _costAnalysis = costAnalysis;
        }

        /* private Tuple<Node,Cost, List<Node>,Tuple<int, int, int, int, int>> GetUnit(int stars)
         {
             return stars < MinMakeThreshold ? Tuple.Create(new Node(stars), new Cost(),  new List<Node>(), Tuple.Create(1,0,0,0,0)) : Make(stars);
         }

         private Tuple<Node, Cost, List<Node>> Make(int stars)
         {
             var possibleInputs = GetPossiblePowerUpCombos(stars - 1);
             var from = GetUnit(stars - 1);
             var baseUnit = from.Item1;
             var cost = from.Item2;
             var output = new List<Node>();
             var minValue = double.MaxValue;
             var bestOutput = (List<Node>)null;
             var bestCost = (Cost)null;
             var bestInput = default(Tuple<int, int, int, int, int>);
             foreach (var possibleInput in possibleInputs)
             {
                 var input = new Node[possibleInput.Item1 + possibleInput.Item2 + possibleInput.Item3 +
                                      possibleInput.Item4 + possibleInput.Item5];
                 var i = 0;
                 for (int j = 0; j < possibleInput.Item1; j++,i++)
                 {
                     var unit = GetUnit(1);
                     input[i] = unit.Item1;
                     cost.Add(unit.Item2);
                     output.AddRange(unit.Item3);
                 }

                 for (int j = 0; j < possibleInput.Item2; j++, i++)
                 {
                     var unit = GetUnit(2);
                     input[i] = unit.Item1;
                     cost.Add(unit.Item2);
                     output.AddRange(unit.Item3);
                 }
                 for (int j = 0; j < possibleInput.Item3; j++, i++)
                 {
                     var unit = GetUnit(3);
                     input[i] = unit.Item1;
                     cost.Add(unit.Item2);
                     output.AddRange(unit.Item3);
                 }
                 for (int j = 0; j < possibleInput.Item4; j++, i++)
                 {
                     var unit = GetUnit(4);
                     input[i] = unit.Item1;
                     cost.Add(unit.Item2);
                     output.AddRange(unit.Item3);
                 }
                 for (int j = 0; j < possibleInput.Item5; j++, i++)
                 {
                     var unit = GetUnit(5);
                     input[i] = unit.Item1;
                     cost.Add(unit.Item2);
                     output.AddRange(unit.Item3);
                 }
                 baseUnit.MakeMaxLevel(cost);
                 baseUnit.ApplyPowerUp(cost,input);
                 output.Add( baseUnit.Evolve(cost));
                 var value = _costAnalysis(cost, output);
                 if (value < minValue)
                 {
                     minValue = value;
                     bestCost = cost;
                     bestOutput = output;
                     bestInput = possibleInput;
                 }
             }
             return Tuple.Create(new Node(stars), bestCost, bestOutput, bestInput);
         }

         public Tuple<Node, Cost> CreateUnit(int stars)
         {
             var x = GetUnit(stars);
             Bag[1] -= x.Item4.Item1;
             Bag[2] -= x.Item4.Item2;
             Bag[3] -= x.Item4.Item3;
             Bag[4] -= x.Item4.Item4;
             Bag[5] -= x.Item4.Item5;
             foreach (var node in x.Item3)
             {
                 Bag[node.Stars]++;
             }

             return Tuple.Create(x.Item1, x.Item2);
         }*/

        public void Go()
        {
            var bestCostPerStar = new EvolutionOutcome[6];
            bestCostPerStar[0] = new EvolutionOutcome();
            for (var i = 0; i < bestCostPerStar.Length; i++)
            {
                bestCostPerStar[i] = GetBestCostPerStar(i + 1, bestCostPerStar);
                Explain(bestCostPerStar[i], i+1);
            }

            Console.Read();
        }

        private void Explain(EvolutionOutcome evolutionOutcome, int stars)
        {
            Console.WriteLine($"We want to make a {stars}, best input was {evolutionOutcome.Input}, computed value was {evolutionOutcome.Value}");
            Console.WriteLine("Total Cost:");
            Console.WriteLine(evolutionOutcome.Cost);
            Console.WriteLine("Here's the steps");
            foreach (var action in evolutionOutcome.Actions)
            {
                Console.WriteLine(action);
            }
        }

        /// <summary>
        /// gets the best cost of making a <paramref name="stars"/> unit. Meaning powering up a
        /// unit thats one level lower to +5 and evolving it
        /// </summary>
        /// <param name="stars"></param>
        /// <param name="bestCostPerStar"></param>
        /// <returns></returns>
        private EvolutionOutcome GetBestCostPerStar(int stars, EvolutionOutcome[] bestCostPerStar)
        {
            if (stars == 1)
            {
                var ret = new EvolutionOutcome();
                ret.Actions.Add("Summon a level 1");
                ret.Cost.Gold += 3000;
                ret.Cost.Books++;
                ret.CreatedUnits.Level1++;
                ret.CreateMethod = "Summon";
                return ret;
            }

            if (stars == 2)
            {
                var ret = new EvolutionOutcome();
                ret.Actions.Add("Summon a level 2");
                ret.Cost.Gold += 30000;
                ret.Cost.Books+=10;
                ret.CreatedUnits.Level2++;
                ret.CreateMethod = "Summon";
                return ret;
            }
            var possibleInputs = GetPossiblePowerUpCombos(stars - 1);
            var minValue = double.MaxValue;
            var bestCost = new EvolutionOutcome();
            foreach (var possibleInput in possibleInputs)
            {
                var current = new EvolutionOutcome { Input = possibleInput };
                var baseUnit = GetUnit(stars - 1, current, bestCostPerStar);
                //var baseUnit = new Node(stars-1);
                for (var i = 0; i < possibleInput.Level5; i++)
                {
                    GetUnit(5, current, bestCostPerStar);
                }
                for (var i = 0; i < possibleInput.Level4; i++)
                {
                    GetUnit(4, current, bestCostPerStar);
                }
                for (var i = 0; i < possibleInput.Level3; i++)
                {
                    GetUnit(3, current, bestCostPerStar);
                }
                for (var i = 0; i < possibleInput.Level2; i++)
                {
                    GetUnit(2, current, bestCostPerStar);
                }
                for (var i = 0; i < possibleInput.Level1; i++)
                {
                    GetUnit(1, current, bestCostPerStar);
                }
              
                baseUnit.MakeMaxLevel(current.Cost);
                baseUnit.ApplyPowerUp(current.Cost, current.CreatedUnits);
                baseUnit.Evolve(current);
                var value = _costAnalysis(current);
                current.Value = value;
                if (value < minValue)
                {
                    minValue = value;
                    bestCost = current;
                }
            }
            bestCost.Input.Increment(stars-1);
            bestCost.CreateMethod = $"Evolve with {bestCost.Input}";
            return bestCost;
        }

        private Node GetUnit(int stars, EvolutionOutcome current, EvolutionOutcome[] bestCostPerStar)
        {
            //if we have one, we return that one
            if (current.OutputUnits.Bag[stars] > 0)
            {
                current.Actions.Add($"Needed a {stars}, had one in inventory, using that");
                current.OutputUnits.Decrement(stars);
                current.CreatedUnits.Increment(stars);
                current.Actions.Add($"Current output bag state: {current.OutputUnits}");
                current.Actions.Add($"Current create bag state: {current.CreatedUnits}");
                return new Node(stars);
            }
            //else we need to make a new one
            current.Actions.Add($"Needed a {stars}, creating one by {bestCostPerStar[stars-1].CreateMethod}");
            current.Cost.Add(bestCostPerStar[stars-1].Cost);
            current.OutputUnits.Add(bestCostPerStar[stars-1].OutputUnits);
            current.CreatedUnits.Increment(stars);
            current.Actions.Add($"Current output bag state: {current.OutputUnits}");
            current.Actions.Add($"Current create bag state: {current.CreatedUnits}");
            return new Node(stars);

        }

        /// <summary>
        /// To get a unit of <paramref name="from"/> stars to +5 we need the following units
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        static IEnumerable<Units> GetPossiblePowerUpCombos(int from)
        {
            var coefs = new List<long>();
            for (int i = 1; i <= from; i++)
            {
                coefs.Add(PowerUp.PowerUpPercent[i - from]);
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
                                    yield return new Units(i, j, k, l, m);
                                    if (m < min5StarFound)
                                        min5StarFound = m;
                                    break;
                                }
                            }
                            if (i * coefs[0] + j * coefs[1] + k * coefs[2] + l * coefs[3] >= 5000)
                            {
                                yield return new Units(i, j, k, l, 0);
                                if (l < min4StarFound)
                                    min4StarFound = l;
                                break;
                            }
                        }
                        if (i * coefs[0] + j * coefs[1] + k * coefs[2] >= 5000)
                        {
                            yield return new Units(i, j, k, 0, 0);
                            if (k < min3StarFound)
                                min3StarFound = k;
                            break;
                        }
                    }
                    if (i * coefs[0] + j * coefs[1] >= 5000)
                    {
                        yield return new Units(i, j, 0, 0, 0);
                        if (j < min2StarFound)
                            min2StarFound = j;
                        break;
                    }
                }
                if (i * coefs[0] >= 5000)
                {
                    yield return new Units(i, 0, 0, 0, 0);
                    if (i < min1StarFound)
                        min1StarFound = i;
                    break;
                }
            }
        }

    }
}
