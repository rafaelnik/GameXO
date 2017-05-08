using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameXO
{
    public class AILogic
    {

        /// <summary>
        /// Ход компьютера
        /// </summary>
        /// <param name="gameFieldButtons">Двумерный массив кнопок игрового поля</param>
        /// <param name="AIfigure">Фигура которой ходит компьютер</param>
        public void AIMove(Button [,] gameFieldButtons, string AIFigure = "X")
        {
            if (CheckFirstMove(gameFieldButtons, AIFigure)) return;
            else if (CheckWinNextMove(gameFieldButtons, AIFigure)) return;
            else if (CheckLoseNextMove(gameFieldButtons, AIFigure)) return;
            else
            {
                // рандомный ход в случае невозможности выиграть/проиграть следующим ходом
                foreach (Button btn in gameFieldButtons)
                {
                    if (btn.Content.ToString() == "")
                    {
                        btn.Content = AIFigure;
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// Проверка возможности сделать первый ход
        /// </summary>
        /// <param name="gameFieldButtons">Двумерный массив кнопок игрового поля</param>
        /// <param name="AIfigure">Фигура которой ходит компьютер</param>
        private bool CheckFirstMove(Button[,] gameFieldButtons, string AIFigure)
        {
            string statusField = "";
            string statusCheckString = AIFigure + AIFigure; // формируем строку для проверки выгрыша AI следующим ходом

            // проверяем поле на возможность первого хода
            foreach (Button btn in gameFieldButtons) statusField += btn.Content.ToString();
            if (statusField == "")
            {
                gameFieldButtons[1, 1].Content = AIFigure; // делаем первый ход
                return true;
            }
            return false;
        }

        /// <summary>
        /// Проверка возможности выиграть следующим ходом
        /// </summary>
        /// <param name="gameFieldButtons">Двумерный массив кнопок игрового поля</param>
        /// <param name="AIfigure">Фигура которой ходит компьютер</param>
        private bool CheckWinNextMove(Button[,] gameFieldButtons, string AIFigure)
        {
            // объявляем переменные для проверки статуса "линий" игрового поля
            string statusHor = "";
            string statusVer = "";
            string statusDiagUD = "";
            string statusDiagDU = "";

            string statusCheckString = AIFigure + AIFigure; // формируем строку для проверки выгрыша AI следующим ходом

            for (int row = 0; row <= 2; row++)
            {
                statusDiagUD += gameFieldButtons[row, row].Content.ToString();
                for (int column = 0; column <= 2; column++)
                {
                    statusHor += gameFieldButtons[row, column].Content.ToString(); // формируем состояние игрового поля по горизонтали
                    statusVer += gameFieldButtons[column, row].Content.ToString(); // формируем состояние игрового поля по вертикали
                    if ((row + column) == 2) statusDiagDU += gameFieldButtons[row, column].Content.ToString();
                }

                if (statusHor == statusCheckString) // проверяем есть ли две фигуры AIPlayer в ряду
                {
                    // ставим третью фигуру в ряд
                    for (int col = 0; col <= 2; col++)
                        if (gameFieldButtons[row, col].Content.ToString() == "")
                        {
                            gameFieldButtons[row, col].Content = AIFigure;
                            return true;
                        }
                }
                else
                    if (statusVer == statusCheckString) // проверяем есть ли две фигуры AIPlayer в столбце
                {
                    // ставим третью фигуру в столбец
                    for (int col = 0; col <= 2; col++)
                        if (gameFieldButtons[col, row].Content.ToString() == "")
                        {
                            gameFieldButtons[col, row].Content = AIFigure;
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
                    if (gameFieldButtons[i, i].Content.ToString() == "")
                    {
                        gameFieldButtons[i, i].Content = AIFigure;
                        return true;
                    }
            }
            else
                if (statusDiagDU == statusCheckString) // проверяем есть ли две фигуры AIPlayer в диагонали DownUp
            {
                // ставим третью фигуру в диагональ DownUp
                if (gameFieldButtons[2, 0].Content.ToString() == "")
                {
                    gameFieldButtons[2, 0].Content = AIFigure;
                    return true;
                }
                if (gameFieldButtons[1, 1].Content.ToString() == "")
                {
                    gameFieldButtons[1, 1].Content = AIFigure;
                    return true;
                }
                if (gameFieldButtons[0, 2].Content.ToString() == "")
                {
                    gameFieldButtons[0, 2].Content = AIFigure;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Проверка возможности проиграть следующим ходом
        /// </summary>
        /// <param name="gameFieldButtons">Двумерный массив кнопок игрового поля</param>
        /// <param name="AIfigure">Фигура которой ходит компьютер</param>
        private bool CheckLoseNextMove(Button[,] gameFieldButtons, string AIFigure)
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
                statusDiagUD += gameFieldButtons[row, row].Content.ToString();
                for (int column = 0; column <= 2; column++)
                {
                    statusHor += gameFieldButtons[row, column].Content.ToString(); // формируем состояние игрового поля по горизонтали
                    statusVer += gameFieldButtons[column, row].Content.ToString(); // формируем состояние игрового поля по вертикали
                    if ((row + column) == 2) statusDiagDU += gameFieldButtons[row, column].Content.ToString();
                }

                if (statusHor == statusCheckString) // проверяем есть ли две фигуры ИГРОКА в ряду
                {
                    // ставим фигуру AI в ряд
                    for (int col = 0; col <= 2; col++)
                        if (gameFieldButtons[row, col].Content.ToString() == "")
                        {
                            gameFieldButtons[row, col].Content = AIFigure;
                            return true;
                        }
                }
                else
                    if (statusVer == statusCheckString) // проверяем есть ли две фигуры ИГРОКА в столбце
                {
                    // ставим фигуру AI в столбец
                    for (int col = 0; col <= 2; col++)
                        if (gameFieldButtons[col, row].Content.ToString() == "")
                        {
                            gameFieldButtons[col, row].Content = AIFigure;
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
                    if (gameFieldButtons[i, i].Content.ToString() == "")
                    {
                        gameFieldButtons[i, i].Content = AIFigure;
                        return true;
                    }
            }
            else
                if (statusDiagDU == statusCheckString) // проверяем есть ли две фигуры ИГРОКА в диагонали DownUp
            {
                // ставим третью фигуру в диагональ DownUp
                if (gameFieldButtons[2, 0].Content.ToString() == "")
                {
                    gameFieldButtons[2, 0].Content = AIFigure;
                    return true;
                }
                if (gameFieldButtons[1, 1].Content.ToString() == "")
                {
                    gameFieldButtons[1, 1].Content = AIFigure;
                    return true;
                }
                if (gameFieldButtons[0, 2].Content.ToString() == "")
                {
                    gameFieldButtons[0, 2].Content = AIFigure;
                    return true;
                }
            }
            return false;
        }
    }
}
