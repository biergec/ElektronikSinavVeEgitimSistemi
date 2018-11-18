using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EntityLayer.Sinav;

namespace EntityLayer.Sinav
{
    public class Sinav
    {
        [Key]
        public Guid SinavId { get; set; }
        public Guid SinavSahibi { get; set; }
        public Guid DerslerId { get; set; }

        public SinavTuru SinavTuru { get; set; }

        public DateTime SinavEklenmeTarihi { get; set; }

        public Dersler Dersler { get; set; }
        public TestSinav TestSinav { get; set; }
        public KlasikSinav KlasikSinav { get; set; }
    }
}
