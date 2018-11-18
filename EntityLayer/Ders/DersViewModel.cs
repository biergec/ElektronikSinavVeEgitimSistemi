using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.Sinav
{
    public class DersViewModel
    {
        public Guid DerslerId { get; set; }

        public double DersKodu { get; set; }
        public string DersAdi { get; set; }

        public DateTime DersEklenmeTarihi { get; set; }
    }
}
