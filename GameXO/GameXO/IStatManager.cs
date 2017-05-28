using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameXO
{
    public interface IStatManager
    {
        void SetStat(string path, List<XOStat> stat);
        List<XOStat> GetStat(string path);
        void ClearStat(string path);
    }
}

