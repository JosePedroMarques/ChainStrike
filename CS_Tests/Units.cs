using System.Collections.Generic;

namespace CS_Tests
{
    public class Units
    {
        public Units()
        {
            
        }

        public Units(int level1, int level2, int level3, int level4, int level5)
        {
            Level1 = level1;
            Level2 = level2;
            Level3 = level3;
            Level4 = level4;
            Level5 = level5;
        }

        public int Level1
        {
            get => Bag[1];
            set => Bag[1] = value;
        }
        public int Level2
        {
            get => Bag[2];
            set => Bag[2] = value;
        }
        public int Level3
        {
            get => Bag[3];
            set => Bag[3] = value;
        }
        public int Level4
        {
            get => Bag[4];
            set => Bag[4] = value;
        }
        public int Level5
        {
            get => Bag[5];
            set => Bag[5] = value;
        }

        public readonly Dictionary<int, int> Bag = new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 } };

        public void Increment(int stars)
        {
            Bag[stars]++;
        }

        public void Add(Units units)
        {
            foreach (var unit in units.Bag)
            {
                Bag[unit.Key] += unit.Value;
            }
        }

        public void Decrement(int stars)
        {
            Bag[stars]--;
        }
    }
}