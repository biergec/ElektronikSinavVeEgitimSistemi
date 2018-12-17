using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.CanliYayin
{
    public class CanliYayinYoklama
    {
        public Guid CanliYayinId { get; set; }
        public string OgrenciAdSoyad { get; set; }
        public string OgrenciNumarasi { get; set; }
        public Guid OgrenciId { get; set; }
        public string DersAdi { get; set; }
        public Guid DersId { get; set; }
    }
}
