using System;

namespace GameXO
{

    /// <summary>
    /// Класс логики игры
    /// </summary>
    class GameLogic
    {
        private string gameStatus;
        private string playerFigure;
        private string aiFigure;
        private readonly IView _view;
        private AILogic _aiLogic;

        public GameLogic(IView view)
        {
            _view = view;
            _aiLogic = new AILogic();

            // Подписываемся на события GameWindow через интерфейс IView
            view.StartNewGame += View_StartNewGame;
            view.GameFieldClick += View_GameFieldClick;
        }

        /// <summary>
        /// Событие по нажатию ячейки на игровом поле
        /// </summary>
        private void View_GameFieldClick(object sender, EventArgs e)
        {
            NextMove();
        }

        /// <summary>
        /// Событие по нажатию кнопки "Новая игра"
        /// </summary>
        private void View_StartNewGame(object sender, EventArgs e)
        {
            _view.SetFieldEnable(true); // делаем достыпным игровое поле
            SetGameFieldEmpty(); // очищаем игровое поле

            // выставляем фигуры игроку и компьютеру
            if (_view.PlayerFigureX)
            {
                playerFigure = "X";
                aiFigure = "O";
            }
            else
            {
                playerFigure = "O";
                aiFigure = "X";
            }

            // проверяем ходит ли компьютер первый и выбрана ли игра с компьютером
            if (!(_view.PlayerMoveFirst) && (_view.AIPlayerOn))
            {
                string[,] field = _view.GameFieldMatrix;
                _aiLogic.AIMove(ref field, aiFigure);
                _view.GameFieldMatrix = field;
            }
        }


        
        private void NextMove()
        {
            string[,] field = _view.GameFieldMatrix;
            if (field [_view.RowClicked, _view.ColumnClicked] == "")
            {
                field[_view.RowClicked, _view.ColumnClicked] = playerFigure;
                if (CheckResult(field))
                {
                    _view.SetGameStatus(gameStatus);
                    _view.SetFieldEnable(false);
                }
                else if (_view.AIPlayerOn)
                {
                    _aiLogic.AIMove(ref field, aiFigure);
                    if (CheckResult(field))
                    {
                        _view.SetGameStatus(gameStatus);
                        _view.SetFieldEnable(false);
                    }
                }
                else if (playerFigure == "O") playerFigure = "X";
                else playerFigure = "O";
            }
            _view.GameFieldMatrix = field;
        }

        /// <summary>
        /// Очищает ячейки игрового поля
        /// </summary>
        private void SetGameFieldEmpty()
        {
            string [,] field = _view.GameFieldMatrix;
            for (int row = 0; row <= 2; row++)
            {
                for (int col = 0; col <= 2; col++)
                {
                    field[row, col] = "";
                }
            }
            _view.GameFieldMatrix = field;
        }

        /// <summary>
        /// Проверка результата игры на победу проигрыш
        /// </summary>
        /// <returns>Возвращает true если игра окончена</returns>
        public bool CheckResult(string[,] gameField)
        {
            string resHor = "";
            string resVer = "";
            string resDiagUD = "";
            string resDiagDU = "";
            string resField = "";

            for (int row = 0; row <= 2; row++)
            {
                resDiagUD += gameField[row, row];
                for (int col = 0; col <= 2; col++)
                {
                    resField += gameField[row, col];
                    resHor += gameField[row, col];
                    resVer += gameField[col, row];
                    if ((row + col) == 2) resDiagDU += gameField[row, col];
                }
                if (CheckResultString(resHor) || CheckResultString(resVer) || CheckResultString(resDiagUD) || CheckResultString(resDiagDU))
                {
                    return true;
                }
                resVer = "";
                resHor = "";
            }
            if (resField.Length == 9)
            {
                gameStatus = "НИЧЬЯ!";
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
