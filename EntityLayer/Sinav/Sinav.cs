using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using EntityLayer.BaslayanSinavlar;
using EntityLayer.Ders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EntityLayer.Sinav;

namespace EntityLayer.Sinav
{
    public class Sinav
    {
        [Key]
        public Guid SinavId { get; set; }
        
        public SinavTuru SinavTuru { get; set; }
        public Guid SinavSahibi { get; set; }
        public DateTime SinavEklenmeTarihi { get; set; }
        public bool SinavAktiflikDurumu { get; set; }
        
        [Range(0,1000000000000000000)]
        public int SinavSuresiDakika { get; set; }

        // Dersler ile bağlantı
        public Guid DerslerId { get; set; }

        public Dersler Dersler { get; set; }
        public TestSinav TestSinav { get; set; }
        public KlasikSinav KlasikSinav { get; set; }
        public SuresiBaslamisSinavlar SuresiBaslamisSinavlar { get; set; }
    }
}
