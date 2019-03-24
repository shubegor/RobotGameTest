using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Logic
{
    public abstract class Character
    {
        public Character(Map map, int x, int y)
        {
            _map = map;
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }

        protected Map Map => _map;
        private readonly Map _map;



        public bool MoveRight()
        {
            if (_map.CanGoTo(this, X + 1, Y))
            {
                X++;
                _map.RefreshMap();
                return true;
            }
            return false;
        }

        public bool MoveLeft()
        {
            if (_map.CanGoTo(this, X - 1, Y))
            {
                X--;
                _map.RefreshMap();

                return true;
            }

            return false;
        }

        public bool MoveUp()
        {
            if (_map.CanGoTo(this, X, Y - 1))
            {
                Y--;
                _map.RefreshMap();

                return true;
            }
            return false;
        }

        public bool MoveDown()
        {
            if (_map.CanGoTo(this, X, Y + 1))
            {
                Y++;
                _map.RefreshMap();

                return true;
            }
            return false;
        }
    }
}
