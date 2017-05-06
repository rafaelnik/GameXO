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
        public GameWindow()
        {
            InitializeComponent();
            gameFieldButtons = new Button[,] { { but00, but01, but02 }, { but10, but11, but12 }, { but20, but21, but22 } };
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

            if ((chBoxPlayerMoveFirst.IsChecked == false) && (chBoxAIPlayerON.IsChecked == true)) AIMove();
        }

        /// <summary>
        /// Событие по нажатию игроком кнопки на игровом поле
        /// </summary>
        private void gameFieldButton_Click(object sender, RoutedEventArgs e)
        {
            Button btnClicked = (Button)sender;
            if (btnClicked.Content.ToString() == "")
            {
                btnClicked.Content = "O";
                if (CheckResult() == false) AIMove();
            }
        }

        /// <summary>
        /// Ход компьютера
        /// </summary>
        private void AIMove()
        {
            foreach (Button btn in gameFieldButtons)
            {
                if (btn.Content.ToString() == "")
                {
                    btn.Content = "X";
                    if (CheckResult() == true) textBoxGameStatus.Text = "ВЫ ПРОИГРАЛИ";
                    return;
                }
            }
        }

        /// <summary>
        /// Проверка результата игры на победу проигрыш
        /// </summary>
        /// <returns>Возвращает true если игра окончена</returns>
        private bool CheckResult()
        {
            string resHor = "";
            string resVer = "";
            string resDiagUD = "";
            string resDiagDU = "";

            for (int i = 0; i <= 2; i++)
            {
                resDiagUD += gameFieldButtons[i, i].Content.ToString();
                for (int j = 0; j <= 2; j++)
                {
                    resHor += gameFieldButtons[i, j].Content.ToString();
                    resVer += gameFieldButtons[j, i].Content.ToString();
                    if ((i+j) == 2) resDiagDU += gameFieldButtons[i, j].Content.ToString();
                }
                if (CheckResultString(resHor) || CheckResultString(resVer) || CheckResultString(resDiagUD) || CheckResultString(resDiagDU))
                {
                    foreach (Button btn in gameFieldButtons) btn.IsEnabled = false;
                    return true;
                }

                resVer = "";
                resHor = "";
            }
            return false;
        }

        /// <summary>
        /// Проверка строки на состояние выигрыша/проигрыша
        /// </summary>
        /// <param name="resString">Строка для проверки</param>
        /// <returns>Возвращает true если есть состояние выигрыша/проигрыша</returns>
        private bool CheckResultString(string resString)
        {
            switch (resString)
            {
                case "XXX":
                    textBoxGameStatus.Text = "ВЫ ПРОИГРАЛИ";
                    return true;
                case "OOO":
                    textBoxGameStatus.Text = "ВЫ ВЫИГРАЛИ!";
                    return true;
            }
            return false;
        }

    }
}
