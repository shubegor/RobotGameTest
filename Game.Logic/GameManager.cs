using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Logic
{
    public class GameManager
    {
        public GameManager(int MapSize, int RobotCount, int GoldCount, int HoleCount)
        {
            Map = new Map(MapSize, RobotCount, GoldCount, HoleCount);
            GameState = GameState.Active;
        }
        public Map Map { get; private set; }

        public GameState GameState { get; private set; }
    
        public void SetGamerMove(Direction dir)
        {
            if (GameState == GameState.Active)
            {
                switch (dir)
                {
                    case Direction.Right:
                        Map.Gamer.MoveRight();
                        break;
                    case Direction.Left:
                        Map.Gamer.MoveLeft();
                        break;
                    case Direction.Down:
                        Map.Gamer.MoveDown();
                        break;
                    case Direction.Up:
                        Map.Gamer.MoveUp();
                        break;
                    case Direction.Fire:
                        Map.Gamer.UseBomb();
                        break;
                }
                Tick();
            }
        }
        public void Tick()
        {
            if (GameState == GameState.Active)
            {
                Map.EatGold();
                if (Map.AllAreEaten())
                {     
                    GameState = GameState.Win;                 
                }
                else
                {
                    if (!Map.IsGamerCollided())
                    {
                        Map.MoveRobots();
                        if (Map.IsGamerCollided())
                            GameState = GameState.GameOver; //вторая проверка не наехал ли робот на игрока
                    }
                    else
                    {
                        GameState = GameState.GameOver;
                    }
                }
            }
        }
    }

    public enum GameState
    {
        Active,
        GameOver,
        Win
    }

}
