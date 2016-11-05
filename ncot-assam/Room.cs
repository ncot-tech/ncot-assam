using System;
using Microsoft.Xna.Framework;
namespace RoomGen
{
    public enum Exit { NORTH, EAST, SOUTH, WEST };

    public class Room
    {
        public Point[] Exits { get; private set; }
        public int numExits { get; private set; }
        public Vector2 location { get { return new Vector2(_x, _y); } }
        int _x, _y;
        public Room(int x, int y)
        {
            _x = x;
            _y = y;
            numExits = 0;
            Exits = new Point[4];
            for (int i = 0; i < 4; i++)
            {
                Exits[i].X = -1;
                Exits[i].Y = -1;
            }
        }

        public void AddExit(int x, int y)
        {
            if (numExits == 4)
                return;
            Exits[numExits].Y = y;
            Exits[numExits].X = x;
            numExits++;
        }

        public Point GetExitEntry(int exitIndex)
        {
            return Exits[exitIndex];
        }

        public bool CheckValidExit(Exit direction)
        {
            bool exit = false;

            for (int i = 0; i < numExits; i++)
            {
                int edx = Exits[i].X - _x;
                int edy = Exits[i].Y - _y;
                if (edx == 0 && edy == -1 && direction == Exit.NORTH)            // N
                {
                    exit = true;
                    break;
                }
                else if (edx == 1 && edy == 0 && direction == Exit.EAST)      // E                                                            
                {
                    exit = true;
                    break;
                }
                else if (edx == 0 && edy == 1 && direction == Exit.SOUTH)      // S                                                           
                {
                    exit = true;
                    break;
                }
                else if (edx == -1 && edy == 0 && direction == Exit.WEST)     // W                                                            
                {
                    exit = true;
                    break;
                }
            }

            return exit;
        }
    }
}
