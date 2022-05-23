using MOT.StateRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOT
{
    public class MovingObjectNode : AbstractNode
    {

        public MovingObjectNode(AbstractClass startState) : base(startState) { }
        public MovingObjectNode(AbstractNode node) : base(node) { }

        public override List<AbstractNode> Expand()
        {
            List<AbstractNode> newNodes = new List<AbstractNode>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    foreach (int dir in Enum.GetValues(typeof(Direction)))
                    {
                        MovingObjectNode newNode = new MovingObjectNode(this);
                        if ((newNode.State as MovingObjectState).ColorStep(i, j, (Direction)dir) != null)
                        {
                            if (!newNodes.Contains(newNode))
                                newNodes.Add(newNode);
                        }
                    }

                }
            }
            return newNodes;
        }
    }
}
