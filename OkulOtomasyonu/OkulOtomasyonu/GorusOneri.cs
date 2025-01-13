using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace OkulOtomasyonu
{
    public static class JsonHelper
    {
        private static readonly string filePath = "gorusler.json";

        public static List<GorusOneri> Oku()
        {
            if (!File.Exists(filePath))
            {
                return new List<GorusOneri>();
            }

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<GorusOneri>>(json) ?? new List<GorusOneri>();
        }

        public static void Yaz(List<GorusOneri> gorusler)
        {
            var json = JsonConvert.SerializeObject(gorusler, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
    public class GorusOneri
    {
        public string Metin { get; set; }
        public DateTime Tarih { get; set; }
    }

}
