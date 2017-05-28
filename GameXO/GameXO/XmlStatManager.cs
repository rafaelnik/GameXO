using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace GameXO
{
    public class XmlStatManager : IStatManager
    {
        /// <summary>
        /// Метод для записи статистики игры в файл по директории path
        /// </summary>
        /// <param name="path">Путь к файлу для записи статистики</param>
        /// <param name="stat">Данные для записи</param>
        public void SetStat(string path, List<XOStat> stat)
        {
            var xmlSer = new XmlSerializer(typeof(List<XOStat>));

            using (var sw = new StreamWriter(path))
            {
                xmlSer.Serialize(sw, stat);
            }
        }

        /// <summary>
        /// Метод возвращающий статистику из XML файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<XOStat> GetStat(string path)
        {
            XmlSerializer xmlSer = new XmlSerializer(typeof(List<XOStat>));
            using (var sr = new StreamReader(path))
            {
                return xmlSer.Deserialize(sr) as List<XOStat>;
            }
        }

        public void ClearStat(string path)
        {
            var sw = new StreamWriter(path, false);
            sw.Dispose();
        }
    }
}
