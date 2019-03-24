using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Logic;
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.Write("Введите размер карты N: ");
            //int MapSize = int.Parse(Console.ReadLine());
            //Console.Write("Введите количество роботов R: ");
            //int RobotCount = int.Parse(Console.ReadLine());
            //Console.Write("Введите количество золота G: ");
            //int GoldCount = int.Parse(Console.ReadLine());
            //Console.Write("Введите количество дырок H: ");
            //int HoleCount = int.Parse(Console.ReadLine());

            int MapSize = 20;
            int RobotCount = 3;
            int GoldCount = 5;
            int HoleCount = 10;

            bool EndGame = false;
            while(!EndGame)
            {
                GameManager MainGame = new GameManager(MapSize, RobotCount, GoldCount, HoleCount);

                RenderMap(MainGame.Map);

                while (MainGame.GameState == GameState.Active)
                {
                    var ch = Console.ReadKey(false).Key;
                    switch (ch)
                    {
                        case ConsoleKey.RightArrow:
                            MainGame.SetGamerMove(Direction.Right);
                            break;
                        case ConsoleKey.LeftArrow:
                            MainGame.SetGamerMove(Direction.Left);
                            break;
                        case ConsoleKey.DownArrow:
                            MainGame.SetGamerMove(Direction.Down);              
                            break;
                        case ConsoleKey.UpArrow:
                            MainGame.SetGamerMove(Direction.Up);
                            break;
                        case ConsoleKey.Spacebar:
                            MainGame.SetGamerMove(Direction.Fire);
                            break;
                    }
                    RenderMap(MainGame.Map);
                }
                switch (MainGame.GameState)
                {
                    case GameState.GameOver:
                        Console.WriteLine("ВЫ ПРОИГРАЛИ");
                        break;
                    case GameState.Win:
                        Console.WriteLine("ВЫ ВЫИГРАЛИ!");
                        break;
                }
                Console.WriteLine("Сыграем еще? [Y]");
                var chd = Console.ReadKey(false).Key;
                if (!(chd == ConsoleKey.Y))
                    EndGame = true;
                

            }

            
        }

        public static void RenderMap(Map map)
        {
            Console.Clear();
            Console.WriteLine("Осталось [" + map.Gamer.Bomb + "] заряда электроизлучателя");
            Console.WriteLine("Золота осталось [" + map.Golds.Count() + "]");
            for (int i = 0; i < map.MapSize; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < map.MapSize; j++)
                {
                    if (map.GameMap[i, j] == null)
                        Console.Write('*');
                    else
                        if (map.GameMap[i, j].CharacterInCell is Robot)
                        Console.Write('R');
                    else
                        if (map.GameMap[i, j].CharacterInCell is Hole)
                        Console.Write('O');
                    else
                        if (map.GameMap[i, j].CharacterInCell is Gold)
                        Console.Write('$');
                    else
                        if (map.GameMap[i, j].CharacterInCell is Gamer)
                        Console.Write('G');
                }
            }
            Console.WriteLine();
        }
    }
}
