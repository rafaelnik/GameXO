using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameXO;
using Ploeh.AutoFixture;
using System.Collections.Generic;
using System.IO;

namespace GameXOTests
{
    [TestClass]
    public class JsonStatManagerTests
    {

        [TestMethod]
        public void ClearStat_EmptyFileExpected()
        {
            // Arrange
            var statManger = new JsonStatManager();
            var path = "stat.JSON";
            var res = "";
            var stats = new List<XOStat>();
            stats.Add(new Fixture().Create<XOStat>());

            // Act
            statManger.SetStat(path, stats);
            statManger.ClearStat(path);
            if (File.Exists(path)) res = File.ReadAllText(path);

            //Assert 
            Assert.AreEqual("" , res);
        }

        [TestMethod]
        public void GetStat_and_SetStat_SerializeAndDeserialize()
        {
            // Arrange
            var statManger = new JsonStatManager();
            var path = "stat.JSON";
            var statsExpected = new List<XOStat>();
            var statsActual = new List<XOStat>();
            statsExpected.Add(new Fixture().Create<XOStat>());

            // Act
            statManger.SetStat(path, statsExpected);
            statsActual = statManger.GetStat(path);

            //Assert 
            Assert.AreEqual(statsExpected[0].CountMoves, statsActual[0].CountMoves, "Не совпадает счетчик ходов (CountMoves)");
            Assert.AreEqual(statsExpected[0].GameData, statsActual[0].GameData, "Не совпадает дата игры (GameData)");
            Assert.AreEqual(statsExpected[0].GameResult, statsActual[0].GameResult, "Не совпадает результат игры (GameResult)");
            Assert.AreEqual(statsExpected[0].PlayerFigureX, statsActual[0].PlayerFigureX, "Не совпадает признак фигуры игрока (PlayerFigureX)");
        }

    }
}
