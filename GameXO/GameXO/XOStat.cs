using System;

namespace GameXO
{
    [Serializable]
    public class XOStat
    {
        public DateTime GameData { get; set; }
        public String GameResult { get; set; }
        public bool PlayerFigure { get; set; }
        public int CountMoves { get; set; }

        public XOStat()
        { }

        public XOStat(DateTime data, string result, bool playerFigure, int countMoves)
        {
            GameData = data;
            GameResult = result;
            PlayerFigure = playerFigure;
            CountMoves = countMoves;
        }
    }
}
