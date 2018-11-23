using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.BaslayanSinavlar
{
    public class KlasikSinavSinavSoruCevap
    {
        [Key]
        public Guid KlasikSinavSinavSoruCevapId { get; set; }

        // foreign key
        public Guid GirilenKlasikSinavKayitId { get; set; }

        public string SoruText { get; set; }
        public string SoruCevapText { get; set; }

        public GirilenKlasikSinavKayit GirilenKlasikSinavKayit { get; set; }
    }
}
