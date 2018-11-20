using System;
using System.Collections.Generic;
using System.Text;
using EntityLayer.Sinav;

namespace EntityLayer.BaslayanSinavlar
{
    public class AnaSayfaSinavlarimList
    {
        public Guid SinavId { get; set; }

        public int SinavSuresiDakika { get; set; }

        public SinavTuru SinavTuru { get; set; }
        public string DersAdi { get; set; }
    }
}
