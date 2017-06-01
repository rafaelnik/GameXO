using GameXO;
using System.Collections.Generic;

namespace WebStat.Models
{
    public class WebStatistics
    {
        public List<XOStat> Statistics
        { get; set; }

        public WebStatistics()
        {
            using (var db = new XOStatContext())
            {
                Statistics = new List<XOStat>();

                foreach (XOStat st in db.Statistics) Statistics.Add(st);
            }
        }
    }
}