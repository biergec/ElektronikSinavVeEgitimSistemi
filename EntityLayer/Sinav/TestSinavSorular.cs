using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.Sinav
{
    public class TestSinavSorular
    {
        [Key]
        public Guid TestSinavSorularId { get; set; }
        public Guid TestSinavId { get; set; }

        public int SoruCevabi { get; set; }
        public string TestSinavSorusuMetni { get; set; }

        public ICollection<TestSinavSoruSiklari> TestSinavSoruSiklari { get; set; }
        public TestSinav TestSinav { get; set; }
    }
}
