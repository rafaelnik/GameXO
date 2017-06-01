﻿using System;
using System.IO;
using System.Collections.Generic;

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
        private bool aiPlayerOn;
        private int countPlayerMoves;
        private readonly IStatManager _statManager;

        public GameLogic(IView view)
        {
            _view = view;
            _aiLogic = new AILogic();
            _statManager = new DBStatManager();

            // Подписываемся на события GameWindow через интерфейс IView
            view.StartNewGame += View_StartNewGame;
            view.GameFieldClick += View_GameFieldClick;
            view.ShowStat += View_ShowStat;
        }

        /// <summary>
        /// Событие по нажатию ячейки на игровом поле
        /// </summary>
        private void View_GameFieldClick(object sender, EventArgs e)
        {
            try
            {
                NextMove();
            }
            catch (Exception ex)
            {
                _view.SetGameStatus(ex.Message);
            }
        }

        /// <summary>
        /// Событие по вызову статистики пользователем
        /// </summary>
        private void View_ShowStat(object sender, EventArgs e)
        {
            _view.GameStatistic = _statManager.GetStat("C:\\temp\\stat.JSON");
        }

        /// <summary>
        /// Событие по нажатию кнопки "Новая игра"
        /// </summary>
        private void View_StartNewGame(object sender, EventArgs e)
        {
            _view.SetFieldEnable(true); // делаем достыпным игровое поле
            SetGameFieldEmpty(); // очищаем игровое поле
            aiPlayerOn = _view.AIPlayerOn; // проверяем выбрана ли игра с компьютером
            countPlayerMoves = 0; // обнуляем счетчик ходов пользователя

            gameStatus = ""; // обнуляем статус игры
            _view.SetGameStatus("");

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
            if (field[_view.RowClicked, _view.ColumnClicked] == "")
            {
                field[_view.RowClicked, _view.ColumnClicked] = playerFigure;
                countPlayerMoves++;

                if (CheckResult(field))
                {
                    _view.SetGameStatus(gameStatus);
                    _view.SetFieldEnable(false);
                }
                else if (aiPlayerOn)
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
            string[,] field = _view.GameFieldMatrix;
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
                    WriteStat("C:\\temp\\stat.JSON");
                    return true;
                }
                resVer = "";
                resHor = "";
            }
            if (resField.Length == 9)
            {
                gameStatus = "НИЧЬЯ!";
                WriteStat("C:\\temp\\stat.JSON");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Проверка на выигрыш пользователем
        /// </summary>
        /// <returns></returns>
        private GameResult PlayerWinCheck()
        {
            if (aiPlayerOn)
            {
                if (((gameStatus == "ИГРОК Х ВЫИГРАЛ!") && _view.PlayerFigureX) || ((gameStatus == "ИГРОК O ВЫИГРАЛ!") && !(_view.PlayerFigureX))) return GameResult.PlayerWin;
                else if (((gameStatus == "ИГРОК O ВЫИГРАЛ!") && _view.PlayerFigureX) || ((gameStatus == "ИГРОК X ВЫИГРАЛ!") && !(_view.PlayerFigureX))) return GameResult.PlayerLose;
                else if (gameStatus == "НИЧЬЯ!") return GameResult.Draw;
            }
            else if (gameStatus == "ИГРОК Х ВЫИГРАЛ!") return GameResult.PlayerXWin;
            else if (gameStatus == "ИГРОК O ВЫИГРАЛ!") return GameResult.PlayerOWin;

            return GameResult.Draw;
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


        /// <summary>
        /// Метод сохраняет результат игры
        /// </summary>
        /// <param name="path"></param>
        private void WriteStat(string path)
        {
            var stat = new List<XOStat>();

            //if (File.Exists(path)) stat = _statManager.GetStat(path);

            //if (stat == null) stat = new List<XOStat>();

            stat.Add(new XOStat()
            {
                GameData = System.DateTime.Now,
                GameResult = PlayerWinCheck(),
                PlayerFigureX = _view.PlayerFigureX,
                CountMoves = countPlayerMoves
            });

            _statManager.SetStat(path, stat);
        }
    }
}
