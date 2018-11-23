using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.BaslayanSinavlar
{
    public class KlasikSinavSinavKayitViewModel
    {
        public List<SinavSoruCevap> SinavSoruCevaplari { get; set; }
        public Guid OgrenciId { get; set; }
        public Guid SinavId { get; set; }

        public class SinavSoruCevap
        {
            public string SoruText { get; set; }
            public string SoruCevapText { get; set; }
        }
    }
}
