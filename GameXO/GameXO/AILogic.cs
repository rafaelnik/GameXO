namespace GameXO
{

    public class AILogic
    {

        /// <summary>
        /// Ход компьютера
        /// </summary>
        /// <param name="gameField">Двумерный массив игрового поля</param>
        /// <param name="AIfigure">Фигура которой ходит компьютер</param>
        public void AIMove(ref string [,] gameField, string AIFigure = "X")
        {
            if (CheckFirstMove(gameField, AIFigure)) return;
            else if (CheckWinNextMove(gameField, AIFigure)) return;
            else if (CheckLoseNextMove(gameField, AIFigure)) return;
            else
            {
                // рандомный ход в случае невозможности выиграть/проиграть следующим ходом
                for (int row = 0; row <= 2; row++)
                {
                    for (int col = 0; col <= 2; col++)
                    {
                        if (gameField[row, col] == "")
                        {
                            gameField[row, col] = AIFigure;
                            return;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Проверка возможности сделать первый ход
        /// </summary>
        /// <param name="gameField">Двумерный массив игрового поля</param>
        /// <param name="AIfigure">Фигура которой ходит компьютер</param>
        private bool CheckFirstMove(string[,] gameField, string AIFigure)
        {
            string statusField = "";
            string statusCheckString = AIFigure + AIFigure; // формируем строку для проверки выгрыша AI следующим ходом

            // проверяем поле на возможность первого хода
            foreach (string cell in gameField) statusField += cell;
            if (statusField == "")
            {
                gameField[1, 1] = AIFigure; // делаем первый ход
                return true;
            }
            return false;
        }

        /// <summary>
        /// Проверка возможности выиграть следующим ходом
        /// </summary>
        /// <param name="gameField">Двумерный массив кнопок игрового поля</param>
        /// <param name="AIfigure">Фигура которой ходит компьютер</param>
        private bool CheckWinNextMove(string[,] gameField, string AIFigure)
        {
            // объявляем переменные для проверки статуса "линий" игрового поля
            string statusHor = "";
            string statusVer = "";
            string statusDiagUD = "";
            string statusDiagDU = "";

            string statusCheckString = AIFigure + AIFigure; // формируем строку для проверки выгрыша AI следующим ходом

            for (int row = 0; row <= 2; row++)
            {
                statusDiagUD += gameField[row, row];
                for (int column = 0; column <= 2; column++)
                {
                    statusHor += gameField[row, column]; // формируем состояние игрового поля по горизонтали
                    statusVer += gameField[column, row]; // формируем состояние игрового поля по вертикали
                    if ((row + column) == 2) statusDiagDU += gameField[row, column];
                }

                if (statusHor == statusCheckString) // проверяем есть ли две фигуры AIPlayer в ряду
                {
                    // ставим третью фигуру в ряд
                    for (int col = 0; col <= 2; col++)
                        if (gameField[row, col] == "")
                        {
                            gameField[row, col] = AIFigure;
                            return true;
                        }
                }
                else
                    if (statusVer == statusCheckString) // проверяем есть ли две фигуры AIPlayer в столбце
                {
                    // ставим третью фигуру в столбец
                    for (int col = 0; col <= 2; col++)
                        if (gameField[col, row] == "")
                        {
                            gameField[col, row] = AIFigure;
                            return true;
                        }
                }

                statusVer = "";
                statusHor = "";
            }

            if (statusDiagUD == statusCheckString) // проверяем есть ли две фигуры AIPlayer в диагонали UpDown
            {
                // ставим третью фигуру в диагональ UpDown
                for (int i = 0; i <= 2; i++)
                    if (gameField[i, i] == "")
                    {
                        gameField[i, i] = AIFigure;
                        return true;
                    }
            }
            else
                if (statusDiagDU == statusCheckString) // проверяем есть ли две фигуры AIPlayer в диагонали DownUp
            {
                // ставим третью фигуру в диагональ DownUp
                if (gameField[2, 0] == "")
                {
                    gameField[2, 0] = AIFigure;
                    return true;
                }
                if (gameField[1, 1] == "")
                {
                    gameField[1, 1] = AIFigure;
                    return true;
                }
                if (gameField[0, 2] == "")
                {
                    gameField[0, 2] = AIFigure;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Проверка возможности проиграть следующим ходом
        /// </summary>
        /// <param name="gameField">Двумерный массив игрового поля</param>
        /// <param name="AIfigure">Фигура которой ходит компьютер</param>
        private bool CheckLoseNextMove(string[,] gameField, string AIFigure)
        {
            // объявляем переменные для проверки статуса "линий" игрового поля
            string statusHor = "";
            string statusVer = "";
            string statusDiagUD = "";
            string statusDiagDU = "";
            string playerFigure = "O";

            // проверяем какими фигурами ходит игрок
            if (AIFigure == "O") playerFigure = "X";
            string statusCheckString = playerFigure + playerFigure; // формируем строку для проверки

            for (int row = 0; row <= 2; row++)
            {
                statusDiagUD += gameField[row, row];
                for (int column = 0; column <= 2; column++)
                {
                    statusHor += gameField[row, column]; // формируем состояние игрового поля по горизонтали
                    statusVer += gameField[column, row]; // формируем состояние игрового поля по вертикали
                    if ((row + column) == 2) statusDiagDU += gameField[row, column];
                }

                if (statusHor == statusCheckString) // проверяем есть ли две фигуры ИГРОКА в ряду
                {
                    // ставим фигуру AI в ряд
                    for (int col = 0; col <= 2; col++)
                        if (gameField[row, col] == "")
                        {
                            gameField[row, col] = AIFigure;
                            return true;
                        }
                }
                else
                    if (statusVer == statusCheckString) // проверяем есть ли две фигуры ИГРОКА в столбце
                {
                    // ставим фигуру AI в столбец
                    for (int col = 0; col <= 2; col++)
                        if (gameField[col, row] == "")
                        {
                            gameField[col, row] = AIFigure;
                            return true;
                        }
                }

                statusVer = "";
                statusHor = "";
            }

            if (statusDiagUD == statusCheckString) // проверяем есть ли две фигуры ИГРОКА в диагонали UpDown
            {
                // ставим третью фигуру в ряд
                for (int i = 0; i <= 2; i++)
                    if (gameField[i, i] == "")
                    {
                        gameField[i, i] = AIFigure;
                        return true;
                    }
            }
            else
                if (statusDiagDU == statusCheckString) // проверяем есть ли две фигуры ИГРОКА в диагонали DownUp
            {
                // ставим третью фигуру в диагональ DownUp
                if (gameField[2, 0] == "")
                {
                    gameField[2, 0] = AIFigure;
                    return true;
                }
                if (gameField[1, 1] == "")
                {
                    gameField[1, 1] = AIFigure;
                    return true;
                }
                if (gameField[0, 2] == "")
                {
                    gameField[0, 2] = AIFigure;
                    return true;
                }
            }
            return false;
        }
    }
}
