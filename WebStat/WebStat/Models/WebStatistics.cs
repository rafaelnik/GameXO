using GameXO;
using System;
using System.Collections.Generic;
using System.IO;

namespace WebStat.Models
{
    public class WebStatistics
    {
        public List<XOStat> Statistics
        { get; set; }

        public WebStatistics()
        {
            var statManager = new JsonStatManager();
            if (File.Exists("c://temp//stat.JSON")) Statistics = statManager.GetStat("c://temp//stat.JSON");
        }


    }
}