using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.EgitmenSinavBilgileri
{
    public class SinavaGirenKisiler
    {
        public string UserGuidId { get; set; }
        public string  AdSoyad { get; set; }
        public string OkulNumarasi { get; set; }
        public decimal? AldigiNot { get; set; }
        public Guid SinavGuid { get; set; }
    }
}
