using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.Sinav
{
    public class SinavSorulariGoruntuleme
    {
        public List<TestSinavSorular> TestSinavSorular { get; set; }
        public List<KlasikSinav> KlasikSinav { get; set; }
        public string DersAdi { get; set; }
    }
}
