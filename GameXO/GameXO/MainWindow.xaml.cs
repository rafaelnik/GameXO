using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;


namespace GameXO
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class GameWindow : Window, IView
    {
        private Button[,] gameFieldButtons;
        private int rowClicked, columnClicked;

        public GameWindow()
        {
            InitializeComponent();
            gameFieldButtons = new Button[,] { { but00, but01, but02 }, { but10, but11, but12 }, { but20, but21, but22 } };
        }

        public event EventHandler StartNewGame;
        public event EventHandler GameFieldClick;
        public event EventHandler ShowStat;

        /// <summary>
        /// Состояние опции "игры с компьютером"
        /// </summary>
        public bool AIPlayerOn
        {
            get
            {
                if (chBoxAIPlayerON.IsChecked == true) return true;
                return false;
            }
        }

        /// <summary>
        /// Состояние опции "фигура игрока - крестик"
        /// </summary>
        public bool PlayerFigureX
        {
            get
            {
                if (chBoxPlayerFigureX.IsChecked == true) return true;
                return false;
            }
        }

        /// <summary>
        /// Состояние опции "Игрок ходит первый"
        /// </summary>
        public bool PlayerMoveFirst
        {
            get
            {
                if (chBoxPlayerMoveFirst.IsChecked == true) return true;
                return false;
            }
        }

        public List<XOStat> GameStatistic
        {
            set
            {
                var statWindow = new StatWindow();
                statWindow.dataGridStat.ItemsSource = value;
                int wins = value.Where(p => p.GameResult == GameResult.PlayerWin).Count();
                int loses = value.Where(p => p.GameResult == GameResult.PlayerLose).Count();

                if ((wins + loses) > 0) statWindow.textBoxWinRate.Text = "У игрока " + (wins * 100) / (wins + loses) + "% побед!";
                else statWindow.textBoxWinRate.Text = "Недостаточно игр с компьютером для подсчета статистики!";
                statWindow.Show();
            }
        }

        /// <summary>
        /// Массив состояния игрового поля
        /// </summary>
        public string[,] GameFieldMatrix
        {
            get
            {
                string[,] fieldMatrix = { { "", "", "" }, { "", "", "" }, { "", "", "" } };
                for (int row = 0; row <= 2; row++)
                {
                    for (int col = 0; col <= 2; col++)
                    {
                        fieldMatrix[row, col] = gameFieldButtons[row, col].Content.ToString();
                    }
                }
                return fieldMatrix;
            }

            set
            {
                for (int row = 0; row <= 2; row++)
                {
                    for (int col = 0; col <= 2; col++)
                    {
                        gameFieldButtons[row, col].Content = value[row, col];
                    }
                }
            }
        }

        /// <summary>
        /// Ряд нажатой пользователем ячейки
        /// </summary>
        public int RowClicked
        {
            get { return rowClicked; }
        }

        /// <summary>
        /// Строка нажатой пользователем ячейки
        /// </summary>
        public int ColumnClicked
        {
            get { return columnClicked; }
        }

        /// <summary>
        /// Метод устанавливает состояние игры
        /// </summary>
        /// <param name="gameStatus">Строка состояния игры</param>
        public void SetGameStatus(string gameStatus)
        {
            textBoxGameStatus.Text = gameStatus;
        }

        /// <summary>
        /// Метод устанавливает доступность игрового поля
        /// </summary>
        public void SetFieldEnable(bool fieldEnable)
        {
            foreach (Button btn in gameFieldButtons)
                btn.IsEnabled = fieldEnable;
        }

        /// <summary>
        /// Событие по выбору игры с компьютером
        /// </summary>
        private void chBoxAIPlayerON_Checked(object sender, RoutedEventArgs e)
        {
            chBoxPlayerMoveFirst.IsEnabled = true;
            chBoxPlayerFigureX.IsEnabled = true;
        }

        /// <summary>
        /// Событие по отмене выбора игры с компьютером
        /// </summary>
        private void chBoxAIPlayerON_Unchecked(object sender, RoutedEventArgs e)
        {
            chBoxPlayerMoveFirst.IsEnabled = false;
            chBoxPlayerFigureX.IsEnabled = false;
        }

        /// <summary>
        /// Событие по нажатию кнопки "НОВАЯ ИГРА"
        /// </summary>
        private void buttonNewGame_Click(object sender, RoutedEventArgs e)
        {
            if (StartNewGame != null) StartNewGame(this, EventArgs.Empty);
        }

        /// <summary>
        /// Событие по запросу статистики пользователем
        /// </summary>
        private void buttonStatistic_Click(object sender, RoutedEventArgs e)
        {
            if (ShowStat != null) ShowStat(this, EventArgs.Empty);
        }

        /// <summary>
        /// Событие по нажатию игроком кнопки на игровом поле
        /// </summary>
        private void gameFieldButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            for (int row = 0; row <= 2; row++)
            {
                for (int col = 0; col <= 2; col++)
                {
                    if (gameFieldButtons[row, col] == btn)
                    {
                        rowClicked = row;
                        columnClicked = col;
                        break;
                    }
                }
            }
            if (GameFieldClick != null) GameFieldClick(this, EventArgs.Empty);
        }

    }
}