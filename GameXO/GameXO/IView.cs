using System;

namespace GameXO
{
    /// <summary>
    /// Интерфейс для отделения пользовательской части игры от модели самой игры (логики игры)
    /// </summary>
    public interface IView
    {
        event EventHandler StartNewGame;
        event EventHandler GameFieldClick;

        string[,] GameFieldMatrix { get; set; }
        int RowClicked { get; }
        int ColumnClicked { get; }
        bool AIPlayerOn { get; }
        bool PlayerMoveFirst { get; }
        bool PlayerFigureX { get; }
        void SetGameStatus(string gameStatus);
        void SetFieldEnable(bool fieldEnable);
    }
}
