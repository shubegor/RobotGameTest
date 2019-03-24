using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Logic
{
    public class Gamer : Character
    {
        public int Bomb { get; private set; }
        public Gamer(Map map, int x, int y) : base(map, x, y)
        {
            Bomb = 3;
        }
        
        public void UseBomb()
        {
            if(Bomb>0)
            {
                Bomb--;

                for (int i = X - 1; i <= X + 1; i++)
                    for (int j = Y - 1; j <= Y + 1; j++)
                    {

                        var x = Map.Robots.FirstOrDefault(r => r.X == i && r.Y == j);
                        if (x != null) x.Paralize();
                    }
            }
            

            
        }

    }

}
