using System.Windows;

namespace GameXO
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            GameWindow gw = new GameWindow();
            GameLogic gl = new GameLogic(gw);
            gw.Show();
        }

    }
}
