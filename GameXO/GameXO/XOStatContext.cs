using System.Data.Entity;

namespace GameXO
{
    public class XOStatContext : DbContext
    {
        public XOStatContext() : base("DBStatistics")
        { }

        public DbSet<XOStat> Statistics { get; set; }
    }
}
