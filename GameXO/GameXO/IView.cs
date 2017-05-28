using System;
using System.Collections.Generic;

namespace GameXO
{
    /// <summary>
    /// Интерфейс для отделения пользовательской части игры от модели самой игры (логики игры)
    /// </summary>
    public interface IView
    {
        event EventHandler StartNewGame;
        event EventHandler GameFieldClick;
        event EventHandler ShowStat;
        string[,] GameFieldMatrix { get; set; }
        int RowClicked { get; }
        int ColumnClicked { get; }
        bool AIPlayerOn { get; }
        bool PlayerMoveFirst { get; }
        bool PlayerFigureX { get; }
        List<XOStat> GameStatistic { set; }
        void SetGameStatus(string gameStatus);
        void SetFieldEnable(bool fieldEnable);
    }
}
