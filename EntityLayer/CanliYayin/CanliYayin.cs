using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.CanliYayin
{
    public class CanliYayin
    {
        [Key]
        public Guid CanliYayinId { get; set; }

        public bool CanliYayinAktifMi { get; set; }

        public DateTime? CanliYayinBaslamaZamani { get; set; }
        public DateTime? CanliYayinBitisZamani { get; set; }

        public Guid CanliYayinSahibi { get; set; }
        public Guid CanliYayinDersId { get; set; }

        public string CanliYayinYayinId { get; set; }

        public ICollection<CanliYayinaKatilanlar> CanliYayinaKatilanlar { get; set; }
        public ICollection<CanliYayinDokumanlari> CanliYayinDokumanlari { get; set; }
    }
}
