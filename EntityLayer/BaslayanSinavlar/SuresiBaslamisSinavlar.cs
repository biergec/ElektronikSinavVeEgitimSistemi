using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.BaslayanSinavlar
{
    public class SuresiBaslamisSinavlar
    {
        [Key]
        public Guid SuresiBaslamisSinavlarId { get; set; }

        public Guid SinavId { get; set; }
        public Guid OgrenciId { get; set; }
        public DateTime OgrenciSinavaBaslamaZamani { get; set; }
        public DateTime OgrenciSinaviBitirmeZamani { get; set; }

        public ICollection<GirilenKlasikSinavKayit> GirilenKlasikSinavKayits { get; set; }
        public Sinav.Sinav Sinav { get; set; }
    }
}
