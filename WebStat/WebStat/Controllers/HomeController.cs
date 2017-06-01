using System.Web.Mvc;
using WebStat.Models;
using System.Linq;
using GameXO;

namespace WebStat.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: 
        public ActionResult Index()
        {
            var stat = new WebStatistics();
            ViewBag.Stats = stat.Statistics;
            int wins = stat.Statistics.Where(p => p.GameResult == GameResult.PlayerWin).Count();
            int loses = stat.Statistics.Where(p => p.GameResult == GameResult.PlayerLose).Count();

            if ((wins + loses) > 0) ViewBag.WinRate = "У игрока " + (wins * 100) / (wins + loses) + "% побед!";
            else ViewBag.WinRate = "Недостаточно игр с компьютером для подсчета статистики!";


            return View();
        }
    }
}