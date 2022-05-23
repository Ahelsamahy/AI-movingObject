using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOT
{
   public abstract class AbstractClass : ICloneable
   {
      public abstract bool IsState();
      public abstract bool IsGoalState();
      public abstract bool SuperOerator(int i);
      public abstract int NrOfOperators { get; }

      public object Clone()
      {
         return this.MemberwiseClone();
      }
   }
}
