using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameXO
{
    class GameLogic
    {
        private string gameStatus;

        /// <summary>
        /// Возвращает строку статуса игры
        /// </summary>
        public string GameStatus
        {
            get { return gameStatus; }
            set { gameStatus = value; }
        }

        /// <summary>
        /// Проверка результата игры на победу проигрыш
        /// </summary>
        /// <returns>Возвращает true если игра окончена</returns>
        public bool CheckResult(Button[,] gameFieldButtons)
        {
            string resHor = "";
            string resVer = "";
            string resDiagUD = "";
            string resDiagDU = "";
            int resField = 0;

            for (int row = 0; row <= 2; row++)
            {
                resDiagUD += gameFieldButtons[row, row].Content.ToString();
                for (int col = 0; col <= 2; col++)
                {
                    resField += gameFieldButtons[row, col].Content.ToString().Length;
                    resHor += gameFieldButtons[row, col].Content.ToString();
                    resVer += gameFieldButtons[col, row].Content.ToString();
                    if ((row + col) == 2) resDiagDU += gameFieldButtons[row, col].Content.ToString();
                }
                if (CheckResultString(resHor) || CheckResultString(resVer) || CheckResultString(resDiagUD) || CheckResultString(resDiagDU))
                {
                    foreach (Button btn in gameFieldButtons) btn.IsEnabled = false;
                    return true;
                }

                resVer = "";
                resHor = "";
            }
            if (resField == 9)
            {
                gameStatus = "НИЧЬЯ!";
                foreach (Button btn in gameFieldButtons) btn.IsEnabled = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Проверка строки на состояние выигрыша одного из игроков
        /// </summary>
        /// <param name="resString">Строка для проверки</param>
        /// <returns>Возвращает true если есть состояние выигрыша одного из игроков</returns>
        private bool CheckResultString(string resString)
        {
            switch (resString)
            {
                case "XXX":
                    gameStatus = "ИГРОК Х ВЫИГРАЛ!";
                    return true;
                case "OOO":
                    gameStatus = "ИГРОК O ВЫИГРАЛ!";
                    return true;
            }
            return false;
        }
    }
}
