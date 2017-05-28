namespace GameXO
{
    /// <summary>
    /// Перечислинение рузультата игры
    /// </summary>
    public enum GameResult
    {
        /// <summary>
        /// Игрок выиграл
        /// </summary>
        PlayerWin,
        
        /// <summary>
        /// Игрок проиграл
        /// </summary>
        PlayerLose,

        /// <summary>
        /// Игрок X выиграл
        /// </summary>
        PlayerXWin,

        /// <summary>
        /// Игрок O выиграл
        /// </summary>
        PlayerOWin,

        /// <summary>
        /// Ничья
        /// </summary>
        Draw
    }
}
