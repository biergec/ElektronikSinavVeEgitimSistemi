using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.CanliYayin
{
    public class CanliYayinDetaylari
    {
        public Guid CanliYayinId { get; set; }
        public bool CanliYayinAktifMi { get; set; }
        public DateTime? CanliYayinBaslamaZamani { get; set; }
        public DateTime? CanliYayinBitisZamani { get; set; }
        public Guid CanliYayinDersId { get; set; }
        public string CanliYayinDersAdi { get; set; }
        public string CanliYayinYayinId { get; set; }
    }
}
