using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameXO;


namespace GameXOTests
{
    [TestClass]
    public class AILogicTests
    {

        [TestMethod]
        public void AIMove_EmptyField_FigureOInCenter()
        {
            // Arrange
            var logic = new AILogic();
            var figure = "O";
            string[,] fieldActual = { { "", "", "" }, { "", "", "" }, { "", "", "" } };
            string[,] fieldExpected = { { "", "", "" }, { "", "O", "" }, { "", "", "" } };

            // Act
            logic.AIMove(ref fieldActual, figure);

            //Assert 
            Assert.AreEqual(fieldExpected[2,2], fieldActual[2, 2]);
        }

        [TestMethod]
        public void AIMove_EmptyField_FigureXInCenter()
        {
            // Arrange
            var logic = new AILogic();
            var figure = "X";
            string[,] fieldActual = { { "", "", "" }, { "", "", "" }, { "", "", "" } };
            string[,] fieldExpected = { { "", "", "" }, { "", "X", "" }, { "", "", "" } };

            // Act
            logic.AIMove(ref fieldActual, figure);

            //Assert 
            Assert.AreEqual(fieldExpected[2, 2], fieldActual[2, 2]);
        }

        [TestMethod]
        public void AIMove_EmptyField_FigureXInCenterAndAllFieldOK()
        {
            // Arrange
            var logic = new AILogic();
            var figure = "X";
            string[,] fieldActual = { { "", "", "" }, { "", "", "" }, { "", "", "" } };
            string[,] fieldExpected = { { "", "", "" }, { "", "X", "" }, { "", "", "" } };
            bool result = true;

            // Act
            logic.AIMove(ref fieldActual, figure);

            //Assert 
            for (int row = 0; row <= 2; row++)
            {
                for (int col = 0; col <= 2; col++)
                {
                    if (fieldExpected[row, col] != fieldActual[row, col]) result = false;
                }
            }
            Assert.IsTrue(result);
        }
    }
}
