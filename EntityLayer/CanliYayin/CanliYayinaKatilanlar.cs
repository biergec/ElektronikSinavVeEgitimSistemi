using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.CanliYayin
{
    public class CanliYayinaKatilanlar
    {
        [Key]
        public Guid CanliYayinaKatilanlarId { get; set; }
        
        // Foreign key
        public Guid CanliYayinId { get; set; }

        public DateTime CanliYayinKatilmaZamani { get; set; }
        public DateTime? CanliYayinAyrilmaZamani { get; set; }

        public Guid CanliYayinaKatilanKisiId { get; set; }

        public CanliYayin CanliYayin { get; set; }
    }
}
