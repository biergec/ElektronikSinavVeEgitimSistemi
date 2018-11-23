using System;
using System.Collections.Generic;
using System.Text;
using EntityLayer.Sinav;

namespace EntityLayer.EgitmenSinavBilgileri
{
    public class SinavaGirenKisiler
    {
        public string UserGuidId { get; set; }
        public string  AdSoyad { get; set; }
        public string OkulNumarasi { get; set; }
        public double AldigiNot { get; set; }
        public Guid SinavGuid { get; set; }
        public SinavTuru SinavTuru { get; set; }
    }
}
