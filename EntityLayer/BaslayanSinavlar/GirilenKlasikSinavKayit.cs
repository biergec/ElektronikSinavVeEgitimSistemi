using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.BaslayanSinavlar
{
    public class GirilenKlasikSinavKayit
    {
        [Key]
        public Guid GirilenKlasikSinavKayitId { get; set; }

        // Foreign key
        public Guid SuresiBaslamisSinavlarId { get; set; }

        public decimal OgrenciSinavPuani { get; set; }

        public SuresiBaslamisSinavlar SuresiBaslamisSinavlar { get; set; }
        public ICollection<KlasikSinavSinavSoruCevap> KlasikSinavSinavSoruCevaps { get; set; }
    }
}
