using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameXO
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        Button[,] gameFieldButtons;
        AILogic aiPlayer;
        GameLogic gameLogic;
        string playerFigure;

        public GameWindow()
        {
            InitializeComponent();
            gameFieldButtons = new Button[,] { { but00, but01, but02 }, { but10, but11, but12 }, { but20, but21, but22 } };
            aiPlayer = new AILogic();
            gameLogic = new GameLogic();
            playerFigure = "O";
        }



        /// <summary>
        /// Событие по выбору игры с компьютером
        /// </summary>
        private void chBoxAIPlayerON_Checked(object sender, RoutedEventArgs e)
        {
            chBoxPlayerMoveFirst.IsEnabled = true;
        }

        /// <summary>
        /// Событие по отмене выбора игры с компьютером
        /// </summary>
        private void chBoxAIPlayerON_Unchecked(object sender, RoutedEventArgs e)
        {
            chBoxPlayerMoveFirst.IsEnabled = false;
        }

        /// <summary>
        /// Событие по нажатию кнопки "НОВАЯ ИГРА"
        /// </summary>
        private void buttonNewGame_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button btn in gameFieldButtons) 
            {
                btn.IsEnabled = true; // даем доступ к кнопкам
                btn.Content = ""; // обнуляем значения
                textBoxGameStatus.Text = "";
            }

            if ((chBoxPlayerMoveFirst.IsChecked == false) && (chBoxAIPlayerON.IsChecked == true)) aiPlayer.AIMove(gameFieldButtons);
        }

        /// <summary>
        /// Событие по нажатию игроком кнопки на игровом поле
        /// </summary>
        private void gameFieldButton_Click(object sender, RoutedEventArgs e)
        {
            Button btnClicked = (Button)sender;
            if (btnClicked.Content.ToString() == "")
            {
                btnClicked.Content = playerFigure;
                if (gameLogic.CheckResult(gameFieldButtons)) textBoxGameStatus.Text = gameLogic.GameStatus;
                else if (chBoxAIPlayerON.IsChecked == true)
                {
                    aiPlayer.AIMove(gameFieldButtons);
                    if (gameLogic.CheckResult(gameFieldButtons)) textBoxGameStatus.Text = gameLogic.GameStatus;
                }
                else if (playerFigure == "O") playerFigure = "X";
                else playerFigure = "O";
            }
        }
        
    }
}
