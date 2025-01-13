using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulOtomasyonu
{
    public class DersProgramiYolu
    {
        public string DosyaAdi { get; set; }
        public string DosyaYolu { get; set; }
        public string YuklemeTarihi { get; set; }
        public override string ToString()
        {
            return $"{DosyaAdi} - {YuklemeTarihi}";
        }
    }
}
