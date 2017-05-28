using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace GameXO
{
    class JsonStatManager : IStatManager 
    {
        /// <summary>
        /// Метод для записи статистики игры в JSON файл по директории path
        /// </summary>
        /// <param name="path">Путь к файлу для записи статистики</param>
        /// <param name="stat">Данные для записи</param>
        public void SetStat(string path, List<XOStat> stat)
        {
            using (var sw = new StreamWriter(path))
            {
                sw.Write(JsonConvert.SerializeObject(stat));
            }
        }

        /// <summary>
        /// Метод возвращающий статистику из JSON файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<XOStat> GetStat(string path)
        {
            using (var sr = new StreamReader(path))
            {
                return JsonConvert.DeserializeObject<List<XOStat>>(sr.ReadToEnd());
            }
        }

        public void ClearStat(string path)
        {
            var sw = new StreamWriter(path, false);
            sw.Dispose();
        }
    }
}
