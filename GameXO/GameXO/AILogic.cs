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
        public void AIMove(Button [,] gameFieldButtons, string AIfigure = "X")
        {
            //foreach (Button btn in gameFieldButtons)
            //{
            //    if (btn.Content.ToString() == "")
            //    {
            //        btn.Content = AIfigure;
            //        return;
            //    }
            //}
            string statusHor = "";
            string statusVer = "";
            string statusDiagUD = "";
            string statusDiagDU = "";
            string statusField = "";

            string statusCheckString = AIfigure + AIfigure;

            foreach (Button btn in gameFieldButtons) statusField += btn.Content.ToString();

            if (statusField == "") // проверяем поле на возможность первого хода
            {
                gameFieldButtons[1, 1].Content = AIfigure; // делаем первый ход
                return;
            }

            #region Проверка на возможность выиграть следующим ходом
            for (int row = 0; row <= 2; row++)
            {
                statusDiagUD += gameFieldButtons[row, row].Content.ToString();
                for (int column = 0; column <= 2; column++)
                {
                    statusHor += gameFieldButtons[row, column].Content.ToString(); // проверяем состояние игрового поля по горизонтали
                    statusVer += gameFieldButtons[column, row].Content.ToString(); // проверяем состояние игрового поля по вертикали
                    if ((row + column) == 2) statusDiagDU += gameFieldButtons[row, column].Content.ToString();
                }

                if (statusHor == statusCheckString) // проверяем есть ли две фигуры AIPlayer в ряду
                {
                    // если возможно ставим третью фигуру в ряд
                    for (int col = 0; col <= 2; col++)
                        if (gameFieldButtons[row, col].Content.ToString() == "")
                        {
                            gameFieldButtons[row, col].Content = AIfigure;
                            return;
                        }
                }
                else
                    if (statusVer == statusCheckString) // проверяем есть ли две фигуры AIPlayer в столбце
                {
                    // если возможно ставим третью фигуру в столбец
                    for (int col = 0; col <= 2; col++)
                        if (gameFieldButtons[col, row].Content.ToString() == "")
                        {
                            gameFieldButtons[col, row].Content = AIfigure;
                            return;
                        }
                }

                statusVer = "";
                statusHor = "";
            }

            if (statusDiagUD == statusCheckString) // проверяем есть ли две фигуры AIPlayer в диагонали UpDown
            {
                // если возможно ставим третью фигуру в ряд
                for (int i = 0; i <= 2; i++)
                    if (gameFieldButtons[i, i].Content.ToString() == "")
                    {
                        gameFieldButtons[i, i].Content = AIfigure;
                        return;
                    }
            }
            else
                if (statusDiagDU == statusCheckString) // проверяем есть ли две фигуры AIPlayer в диагонали DownUp
            {
                // если возможно ставим третью фигуру в диагональ DownUp
                if (gameFieldButtons[2, 0].Content.ToString() == "")
                {
                    gameFieldButtons[2, 0].Content = AIfigure;
                    return;
                }
                if (gameFieldButtons[1, 1].Content.ToString() == "")
                {
                    gameFieldButtons[1, 1].Content = AIfigure;
                    return;
                }
                if (gameFieldButtons[0, 2].Content.ToString() == "")
                {
                    gameFieldButtons[0, 2].Content = AIfigure;
                    return;
                }
            }
            #endregion

            // рандомный ход в случае невозможности выиграть следующим ходом
            foreach (Button btn in gameFieldButtons)
            {
                if (btn.Content.ToString() == "")
                {
                    btn.Content = AIfigure;
                    return;
                }
            }
        }
    }
}
