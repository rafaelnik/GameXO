using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameXO;


namespace GameXOTests
{
    [TestClass]
    public class AILogicTests
    {

        /// <summary>
        /// Проверка модели компьютера на первый ход "ноликом"
        /// </summary>
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


        /// <summary>
        /// Проверка модели компьютера на первый ход "крестиком"
        /// </summary>
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


        /// <summary>
        /// Проверка модели компьютера на первый ход "крестиком" и проверка на единственный ход (проверка всего поля)
        /// </summary>
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


        /// <summary>
        /// Проверка модели компьютера на ход для выигрыша следующим ходом "ноликом" по диагонали
        /// </summary>
        [TestMethod]
        public void AIMove_WinNextMove_FigureOForWinInDiagonal()
        {
            // Arrange
            var logic = new AILogic();
            var figure = "O";
            string[,] fieldActual = { { "O", "", "X" }, { "", "O", "" }, { "X", "", "" } };
            string[,] fieldExpected = { { "O", "", "X" }, { "", "O", "" }, { "X", "", "O" } };
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


        /// <summary>
        /// Проверка модели компьютера на ход для выигрыша следующим ходом "крестиком" в строку
        /// </summary>
        [TestMethod]
        public void AIMove_WinNextMove_FigureXForWinInRow()
        {
            // Arrange
            var logic = new AILogic();
            var figure = "O";
            string[,] fieldActual = { { "O", "O", "" }, { "", "X", "" }, { "", "X", "" } };
            string[,] fieldExpected = { { "O", "O", "O" }, { "", "X", "" }, { "", "X", "" } };
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

        /// <summary>
        /// Проверка модели компьютера на ход для выигрыша следующим ходом "крестиком" в столбец
        /// </summary>
        [TestMethod]
        public void AIMove_WinNextMove_FigureXForWinInRCol()
        {
            // Arrange
            var logic = new AILogic();
            var figure = "O";
            string[,] fieldActual = { { "O", "", "" }, { "O", "X", "" }, { "", "X", "" } };
            string[,] fieldExpected = { { "O", "", "" }, { "O", "X", "" }, { "O", "X", "" } };
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

        /// <summary>
        /// Проверка модели компьютера на предотвращение проигрыша следующим ходом крестиком по диагонали
        /// </summary>
        [TestMethod]
        public void AIMove_LoseNextMove_FigureXForNotLoseInDiagonal()
        {
            // Arrange
            var logic = new AILogic();
            var figure = "X";
            string[,] fieldActual = { { "O", "", "" }, { "O", "X", "" }, { "", "", "X" } };
            string[,] fieldExpected = { { "O", "", "" }, { "O", "X", "" }, { "X", "", "X" } };
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

        /// <summary>
        /// Проверка модели компьютера на предотвращение проигрыша следующим ходом крестиком в строку
        /// </summary>
        [TestMethod]
        public void AIMove_LoseNextMove_FigureXForNotLoseInRow()
        {
            // Arrange
            var logic = new AILogic();
            var figure = "X";
            string[,] fieldActual = { { "X", "", "" }, { "O", "O", "" }, { "", "", "X" } };
            string[,] fieldExpected = { { "X", "", "" }, { "O", "O", "X" }, { "", "", "X" } };
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

        /// <summary>
        /// Проверка модели компьютера на предотвращение проигрыша следующим ходом ноликом в столбец
        /// </summary>
        [TestMethod]
        public void AIMove_LoseNextMove_FigureOForNotLoseInCol()
        {
            // Arrange
            var logic = new AILogic();
            var figure = "O";
            string[,] fieldActual = { { "X", "O", "" }, { "X", "", "" }, { "", "", "O" } };
            string[,] fieldExpected = { { "X", "O", "" }, { "X", "", "" }, { "O", "", "O" } };
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

        /// <summary>
        /// Свободный ход "ноликом" в первую свободную ячейку 
        /// </summary>
        [TestMethod]
        public void AIMove_FreeMove_FigureOInFirstEmptyCell()
        {
            // Arrange
            var logic = new AILogic();
            var figure = "O";
            string[,] fieldActual = { { "X", "O", "" }, { "", "", "" }, { "", "", "" } };
            string[,] fieldExpected = { { "X", "O", "O" }, { "", "", "" }, { "", "", "" } };
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


        /// <summary>
        /// Свободный ход "крестиком" в первую свободную ячейку 
        /// </summary>
        [TestMethod]
        public void AIMove_FreeMove_FigureXInFirstEmptyCell()
        {
            // Arrange
            var logic = new AILogic();
            var figure = "O";
            string[,] fieldActual = { { "", "", "" }, { "", "X", "" }, { "", "", "" } };
            string[,] fieldExpected = { { "O", "", "" }, { "", "X", "" }, { "", "", "" } };
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
