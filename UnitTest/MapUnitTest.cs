using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Logic;

namespace UnitTest
{
    [TestClass]
    public class MapUnitTest
    {
        [TestMethod]
        public void CanGoToTest()
        {
            GameManager MainGame = new GameManager(10, 0, 0, 0);
            Robot TestRobot = new Robot(MainGame.Map, 0, 0);
            Gamer TestGamer = new Gamer(MainGame.Map, 1, 1);

            Assert.AreEqual(MainGame.Map.CanGoTo(TestRobot, -1, 0), false);

            MainGame.Map.AddGold(new Gold(MainGame.Map, 0, 1));
            Assert.AreEqual(MainGame.Map.CanGoTo(TestRobot, 0, 1), false);

           
            Assert.AreEqual(MainGame.Map.CanGoTo(TestGamer, 0, 1), true);

        }
    }
}
