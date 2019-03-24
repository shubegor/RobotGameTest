using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Logic
{
    public class Robot : Character
    {
        public Robot(Map map, int x, int y) : base(map, x, y)
        {
            CantMove = 0;
        }

        private int CantMove { get; set; }
        public void Paralize()

        {
            CantMove += 5;
        }
        public bool MakeMove()
        {
            if (CantMove > 0)
            {
                CantMove--;
                return true;
            }

            //var dir = _random.Next(1, 5);
            //switch (dir)
            //{
            //    case 1:
            //        return MoveDown();
            //    case 2:
            //        return MoveLeft();
            //    case 3:
            //        return MoveUp();
            //    case 4:
            //        return MoveRight();
            //}


            switch (CalculateDir())
            {
                case Direction.Down:
                    return MoveDown();
                case Direction.Left:
                    return MoveLeft();
                case Direction.Up:
                    return MoveUp();
                case Direction.Right:
                    return MoveRight();
            }

            return false;
        }
        private Direction CalculateDir()
        {
            Direction dir1, dir2;


            if (X > Map.Gamer.X)
                dir1 = Direction.Left;
            else
                dir1 = Direction.Right;
            if (Y > Map.Gamer.Y)
                dir2 = Direction.Up;
            else
                dir2 = Direction.Down;
            if(_random.Next(100) < 50)
            {
                return dir1;
            }
            else return dir2;
        }
        private static readonly Random _random = new Random();

    }
}
