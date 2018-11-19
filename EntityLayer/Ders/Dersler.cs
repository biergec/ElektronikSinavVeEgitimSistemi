using EntityLayer.Ders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.Sinav
{
    public class Dersler
    {
        [Key]
        public Guid DerslerId { get; set; }

        public double DersKodu { get; set; }
        public string DersAdi { get; set; }

        public string DersKayitAnahtari { get; set; }

        public DateTime DersEklenmeTarihi { get; set; }

        public ICollection<Sinav> Sinav { get; set; }
        public ICollection<KayitliDerslerim> KayitliDerslerim { get; set; }
    }
}