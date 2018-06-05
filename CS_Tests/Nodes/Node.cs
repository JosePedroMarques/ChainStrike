using CS_Tests.Transitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Tests.Nodes
{
    public class Node
    {
        public int Stars;
        public int Level = 0;
        public int XP = 0;
      
        public Node(int stars)
        {
            Stars = stars;
        }
    }
}
