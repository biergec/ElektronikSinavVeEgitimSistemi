using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.Sinav
{
    public class KlasikSinavSorulari
    {
        public string DersAdi { get; set; }
        public double DersKodu { get; set; }

        public List<string> Sorular { get; set; }
    }
}
