using System.Web.Mvc;
using WebStat.Models;

namespace WebStat.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: Statistic
        public ActionResult Index()
        {
            var stat = new WebStatistics();
            ViewBag.Stats = stat.Statistics;
            return View();
        }
    }
}