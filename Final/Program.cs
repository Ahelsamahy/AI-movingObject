using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOT.StateRepresentation;

namespace MOT
{
    class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {

            MovingObjectState MOS = new MovingObjectState();
            int steps = 0;
            Console.Clear();
            do
            {
                if (MOS.SuperOerator(rnd.Next(MOS.NrOfOperators)))
                {
                    steps++;
                    MOS.Print2DArray();
                    //Console.ReadLine();
                }
            } while (!MOS.IsGoalState());

            Console.WriteLine($"Steps : {steps}");
            Console.ReadLine();
        }
    }
}
