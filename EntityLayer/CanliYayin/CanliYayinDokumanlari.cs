using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.CanliYayin
{
    public class CanliYayinDokumanlari
    {
        [Key]
        public Guid CanliYayinDokumanlariId { get; set; }

        // foreign key
        public Guid CanliYayinId { get; set; }

        public DateTime DokumanEklenmeTarihi { get; set; }

        public string DokumanAdi { get; set; }
        public string DokumanKayitPath { get; set; }

        public CanliYayin CanliYayin { get; set; }
    }
}
