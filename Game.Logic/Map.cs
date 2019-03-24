using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Logic
{
    public class Map
    {
        public MapCell [,] GameMap { get; private set; }
        public int MapSize { get; }
        public Gamer Gamer { get; }

        public IEnumerable<Robot> Robots => _robots;
        private readonly List<Robot> _robots = new List<Robot>();

        public IEnumerable<Gold> Golds => _golds;
        private readonly List<Gold> _golds = new List<Gold>();

        public IEnumerable<Hole> Holes => _holes;

        private readonly List<Hole> _holes = new List<Hole>();

        public Map(int MapSize, int RobotCount, int GoldCount, int HoleCount)
        {
            
            this.MapSize = MapSize;
            
            Gamer = new Gamer(this, MapSize / 2, MapSize / 2);
       
            GenerateGolds(GoldCount);
            GenerateRobots(RobotCount);
            GenerateHoles(HoleCount);

            RefreshMap();
        }

        public void RefreshMap()
        {
            GameMap = new MapCell[MapSize, MapSize];
            List<Character> CheckList = new List<Character>();
            CheckList.AddRange(_golds);
            CheckList.AddRange(_robots);
            CheckList.AddRange(_holes);
            CheckList.Add(Gamer);
            foreach (var character in CheckList)
            {
                GameMap[character.Y, character.X] = new MapCell { CharacterInCell = character, IsEmpty = false };
            }
        }
        private bool GenerateGolds(int goldCount)
        {
            Random r = new Random();
            
            while(goldCount > 0)
            {
                int x = r.Next(MapSize);
                int y = r.Next(MapSize);
                if(x!=Gamer.X && y!= Gamer.Y && !_golds.Any(gold => gold.X == x && gold.Y == y))
                {
                    AddGold(new Gold(this, x, y));
                    goldCount--;
                }
            }
            return true;
        }
        private void GenerateRobots(int robotCount)
        {
            Random r = new Random();
            while (robotCount > 0)
            {
                int x = r.Next(MapSize);
                int y = r.Next(MapSize);
                if (       (Math.Abs(x - Gamer.X)>2 || Math.Abs(y - Gamer.Y) > 2 )
                    && !_robots.Any(robot => robot.X == x && robot.Y == y) 
                    && !_golds.Any(gold => gold.X == x && gold.Y == y))
                {
                    AddRobot(new Robot(this, x, y));
                    robotCount--;
                }
            }
        }

        private void GenerateHoles(int holeCount)
        {
            Random r = new Random();
            while (holeCount > 0)
            {
                int x = r.Next(MapSize);
                int y = r.Next(MapSize);

                if (x != Gamer.X 
                    && y != Gamer.Y 
                    && !_robots.Any(robot => robot.X == x && robot.Y == y)
                    && !_golds.Any(gold => gold.X == x && gold.Y == y)  
                    && !SomeWillSorrounded(x,y) )
                {
                    AddHole(new Hole(this, x, y));
                    holeCount--;
                }
            }
        }

        private bool SomeWillSorrounded(int x, int y)
        {
            List<Character> CheckList = new List<Character>();
            CheckList.AddRange(_golds);
            CheckList.AddRange(_robots);
            CheckList.Add(Gamer);

            int HolesCountAround = 0;
            foreach (var c in CheckList)
            {
                for(int i = c.X-1; i<=c.X+1; i++)
                    for(int j = c.Y-1; j <= c.Y+1; j++)
                    {
                        if (_holes.Any(hole => hole.X == i && hole.Y == j) || (i==x && j==y) )
                            HolesCountAround++;
                    }
                if (HolesCountAround == 8) return true;
            }
            
            return false;
        }

        public void AddRobot(Robot robot)
        {
            _robots.Add(robot);
        }
        public void AddGold(Gold gold)
        {
            _golds.Add(gold);
        }
        public void AddHole(Hole hole)
        {
            _holes.Add(hole);
        }

        public bool CanGoTo(Character c, int x, int y)
        {
            if(c is Robot)
            {
                if (IsInsideMap(x, y) && !_robots.Any(r => r.X == x && r.Y == y) && !_golds.Any(r => r.X == x && r.Y == y) && !_holes.Any(r => r.X == x && r.Y == y))
                    return true;
                else
                    return false;
            }
            if (c is Gamer)
            {
                if (IsInsideMap(x, y))
                    return true;
                else
                    return false;
            }
            return false;

        }
        
        private bool IsInsideMap(int x, int y)
        {
            return x >= 0 && x < MapSize && y >= 0 && y < MapSize;
        }

        public void MoveRobots()
        {
            foreach(var robot in _robots)
            {
                int Try = 10;
                while (Try>0)
                {
                    if (robot.MakeMove()) break;
                    Try--;
                }
                    
                
            }
        }

        public bool IsGamerCollided()
        {
            return _robots.Any(rob => rob.X == Gamer.X && rob.Y == Gamer.Y) || _holes.Any(hole => hole.X == Gamer.X && hole.Y == Gamer.Y);
        }

        public void EatGold()
        {
            if (_golds.Exists(gold => gold.X == Gamer.X && gold.Y == Gamer.Y))
            {
                _golds.Remove(_golds.FirstOrDefault(gold => gold.X == Gamer.X && gold.Y == Gamer.Y));
            }
        }

        public bool AllAreEaten()
        {
            if (_golds.Count == 0)
                return true;
            return false;
        }


    }
}
