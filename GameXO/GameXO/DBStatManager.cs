using System;
using System.Collections.Generic;

namespace GameXO
{
    public class DBStatManager : IStatManager
    {

        /// <summary>
        /// Метод для записи статистики игры в БД
        /// </summary>
        public void SetStat(List<XOStat> stat)
        {
            SetStat("", stat);
        }

        /// <summary>
        /// Метод для записи статистики игры в БД
        /// </summary>
        /// <param name="path"></param>
        /// <param name="stat">Данные для записи</param>
        public void SetStat(string path, List<XOStat> stat)
        {
            using (var db = new XOStatContext())
            {
                foreach (XOStat st in stat) db.Statistics.Add(st);

                db.SaveChanges();
            }
        }

        /// <summary>
        /// Метод возвращающий статистику из БД
        /// </summary>
        /// <returns></returns>
        public List<XOStat> GetStat()
        {
            return GetStat("");
        }

        /// <summary>
        /// Метод возвращающий статистику из БД
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<XOStat> GetStat(string path)
        {
            var statistics = new List<XOStat>();

            using (var db = new XOStatContext())
            {
                foreach (XOStat st in db.Statistics) statistics.Add(st);
            }

            return statistics;
        }

        public void ClearStat()
        {
            ClearStat("");
        }

        public void ClearStat(string path)
        {
            using (var db = new XOStatContext())
            {
                db.Statistics.RemoveRange(db.Statistics);
                db.SaveChanges();
            }
        }
    }
}
