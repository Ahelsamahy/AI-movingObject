using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOT
{
    public abstract class AbstractNode
    {
        AbstractClass state;
        AbstractNode parent;
        int depth = 0;

        public AbstractNode(AbstractClass startState)
        {
            this.state = startState;
            this.parent = null;
            this.depth = 0;
        }
        public AbstractNode(AbstractNode parent)
        {
            this.state = (AbstractClass)parent.state.Clone();
            this.parent = parent;
            this.depth++;
        }

        public AbstractNode Parent { get { return this.parent; } }
        public AbstractClass State { get { return this.state; } }
        public int Depth { get { return this.depth; } }
        public bool IsTerminal { get { return this.state.IsGoalState(); } }

        public override bool Equals(object obj)
        {
            if (!(obj is AbstractNode)) return false;
            AbstractNode other = (AbstractNode)obj;
            return this.state.Equals(other.state);
        }
        public override string ToString() { return this.state.ToString(); }

        public abstract List<AbstractNode> Expand();
    }
}
