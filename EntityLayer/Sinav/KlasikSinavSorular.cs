using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.Sinav
{
    public class KlasikSinavSorular
    {
        [Key]
        public Guid KlasikSinavSorularId { get; set; }
        public Guid KlasikSinavId { get; set; }

        public string SoruMetni { get; set; }

        public KlasikSinav KlasikSinav { get; set; }
    }
}
