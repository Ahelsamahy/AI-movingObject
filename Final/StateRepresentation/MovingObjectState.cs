using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOT.StateRepresentation
{

    public enum Direction
    {
        up,
        right,
        down,
        left
    }
    public class MovingObjectState : AbstractClass
    {

        public int x { get; private set; }
        public int y { get; private set; }

        private char[,] table = new char[3, 5];
        public char[,] Table
        {
            get
            {
                char[,] temp = new char[5, 3];
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 3; j++)
                        temp[i, j] = table[i, j];
                return temp;
            }
        }

        private static char[,] goalTable = new char[,]{
                {'R', 'R', 'R'},
                {'v', 'f', 'v'},
                {'f', 'f', 'v'},
                {'v', 'f', 'v'},
                {'G', 'G', 'G'}
      };

        public MovingObjectState()
        {
            table = new char[,]{
                {'G', 'G', 'G'},
                {'v', 'f', 'v'},
                {'f', 'f', 'v'},
                {'v', 'f', 'v'},
                {'R', 'R', 'R'}
         };
            
        }

        public override bool IsState()
        {
            if (table[1, 0] != 'v') return false;
            if (table[3, 0] != 'v') return false;
            if (table[1, 2] != 'v') return false;
            if (table[2, 2] != 'v') return false;
            if (table[3, 2] != 'v') return false;

            int countG = 0;
            int countR = 0;
            int countV = 0;
            int countF = 0;
            foreach (char cell in table)
            {
                switch (cell)
                {
                    case 'G': countG++; break;
                    case 'R': countR++; break;
                    case 'v': countV++; break;
                    case 'f': countF++; break;
                    default: return false;
                }
            }
            if (countV != 5) return false;
            if (countF != 4) return false;
            if (countG != countR) return false;
            return true;
        }
        public override bool IsGoalState()
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 3; j++)
                    if (table[i, j] != goalTable[i, j])
                        return false;
            return true;
        }


        private void Swap(int row1, int col1, int row2, int col2)
        {
            char temp = table[row1, col1];
            table[row1, col1] = table[row2, col2];
            table[row2, col2] = temp;
        }



        // restriction in the rules of game
        private bool IsOperator(int row, int col, Direction dir)
        {
            if (table[row, col] != 'R' && table[row, col] != 'G')
                return false;

            if (!(0 <= row && row < 5 &&
                  0 <= col && col < 3))
                return false;

            switch (dir)
            {
                case Direction.up:
                    if (row - 1 < 0) return false;
                    return table[row -1, col] == 'f';
                case Direction.right:
                    if (col + 1 > 2) return false;
                    return table[row, col+1] == 'f';
                case Direction.down:
                    if (row + 1 > 4) return false;
                    return table[row+1, col] == 'f';
                case Direction.left:
                    if (col - 1 < 0) return false;
                    return table[row, col-1] == 'f';

                default:
                    break;
            }

            return true;
        }


        //IsOperator is for the limitation and suproperator is for the moves that can be done

        public bool ColorStep(int row, int col, Direction dir)
        {
            //Check if it is a valid operation
            if (!IsOperator(row, col, dir))
                return false;

            MovingObjectState actualState = (MovingObjectState)this.Clone();

            //Apply the oporation
            switch (dir)
            {
                case Direction.up:
                    Swap(row, col, row - 1, col);
                    break;
                case Direction.right:
                    Swap(row, col, row, col+1);
                    break;
                case Direction.down:
                    Swap(row, col, row + 1, col);
                    break;
                case Direction.left:
                    Swap(row, col, row, col-1);
                    break;
                default:
                    break;
            }

            ////Check if the result after the operation is a valid state
            if (IsState())
                return true;

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 3; j++)
                    this.table[i, j] = actualState.table[i, j];

            return false;



            ////Check if the result after the operation is a valid state
            //if (IsState())
            //    return true;

            ////If not, than GOTO previos state
            //this.x -= row;
            //this.y -= col;
            ////make the clone to return to the previous state before the movement 

            //return false;
        }

        //defining the movment itself
        public override bool SuperOerator(int i)
        {
            switch (i)
            {
                case 0: return ColorStep(0, 0, Direction.up); //First Green
                case 1: return ColorStep(0, 1, Direction.up); //Second Green
                case 2: return ColorStep(0, 2, Direction.up); //Third Green
                case 3: return ColorStep(1, 1, Direction.up); //First Free
                case 4: return ColorStep(2, 1, Direction.up); //Second free
                case 5: return ColorStep(2, 0, Direction.up); //third  Free (to the back)
                case 6: return ColorStep(3, 1, Direction.up); //Forth Free
                case 7: return ColorStep(4, 0, Direction.up); //First Red
                case 8: return ColorStep(4, 1, Direction.up); //Second Red
                case 9: return ColorStep(4, 2, Direction.up); //Third Red

                case 10: return ColorStep(0, 0, Direction.right); //First Green
                case 11: return ColorStep(0, 1, Direction.right); //Second Green
                case 12: return ColorStep(0, 2, Direction.right); //Third Green
                case 13: return ColorStep(1, 1, Direction.right); //First Free
                case 14: return ColorStep(2, 1, Direction.right); //Second free
                case 15: return ColorStep(2, 0, Direction.right); //third  Free (to the back)
                case 16: return ColorStep(3, 1, Direction.right); //Forth Free
                case 17: return ColorStep(4, 0, Direction.right); //First Red
                case 18: return ColorStep(4, 1, Direction.right); //Second Red
                case 19: return ColorStep(4, 2, Direction.right); //Third Red

                case 20: return ColorStep(0, 0, Direction.down); //First Green
                case 21: return ColorStep(0, 1, Direction.down); //Second Green
                case 22: return ColorStep(0, 2, Direction.down); //Third Green
                case 23: return ColorStep(1, 1, Direction.down); //First Free
                case 24: return ColorStep(2, 1, Direction.down); //Second free
                case 25: return ColorStep(2, 0, Direction.down); //third  Free (to the back)
                case 26: return ColorStep(3, 1, Direction.down); //Forth Free
                case 27: return ColorStep(4, 0, Direction.down); //First Red
                case 28: return ColorStep(4, 1, Direction.down); //Second Red
                case 29: return ColorStep(4, 2, Direction.down); //Third Red

                case 30: return ColorStep(0, 0, Direction.left); //First Green
                case 31: return ColorStep(0, 1, Direction.left); //Second Green
                case 32: return ColorStep(0, 2, Direction.left); //Third Green
                case 33: return ColorStep(1, 1, Direction.left); //First Free
                case 34: return ColorStep(2, 1, Direction.left); //Second free
                case 35: return ColorStep(2, 0, Direction.left); //third  Free (to the back)
                case 36: return ColorStep(3, 1, Direction.left); //Forth Free
                case 37: return ColorStep(4, 0, Direction.left); //First Red
                case 38: return ColorStep(4, 1, Direction.left); //Second Red
                case 39: return ColorStep(4, 2, Direction.left); //Third Red

                default: return false;
            }
        }
        public override int NrOfOperators
        {
            get { return 40; }
        }



        public object Clone()
        {
            MovingObjectState clone = new MovingObjectState();
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 3; j++)
                    clone.table[i, j] = table[i, j];
            return clone;
        }

        public void Print2DArray()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    Console.Write(table[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }



    }
}
