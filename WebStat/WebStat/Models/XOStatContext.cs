using System.Data.Entity;
using GameXO;

namespace WebStat.Models
{
    public class XOStatContext : DbContext
    {

        public XOStatContext() : base("DBStatistics")
            { }

        public DbSet<XOStat> Statistics { get; set; }
    }
}