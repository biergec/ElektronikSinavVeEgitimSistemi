using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EntityLayer.Sinav
{
    public class Sinav
    {
        [Key]
        public Guid SinavId { get; set; }
        public Guid SinavSahibi { get; set; }
        public SinavTuru SinavTuru { get; set; }

        public string DersAdi { get; set; }
        public double DersKodu { get; set; }

        public DateTime SinavEklenmeTarihi { get; set; }

        public TestSinav TestSinav { get; set; }
        public KlasikSinav KlasikSinav { get; set; }
    }
}
