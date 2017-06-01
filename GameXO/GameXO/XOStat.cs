using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GameXO
{
    [Serializable]
    [Table("GameStatistics")]
    public class XOStat
    {
        [Key]
        public DateTime GameData { get; set; }
        public GameResult GameResult { get; set; }
        public bool PlayerFigureX { get; set; }
        public int CountMoves { get; set; }

        public XOStat()
        { }

        public XOStat(DateTime data, GameResult result, bool playerFigure, int countMoves)
        {
            GameData = data;
            GameResult = result;
            PlayerFigureX = playerFigure;
            CountMoves = countMoves;
        }
    }
}
