using EntityLayer.Sinav;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.Ders
{
    public class KayitliDerslerim
    {
        [Key]
        public Guid KayitliDerslerimId { get; set; }

        public Guid DerslerId { get; set; }

        public Guid OgrenciId { get; set; }
        public DateTime DersKayitTarihi { get; set; }

        public Dersler Dersler { get; set; }
    }
}
