using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.Sinav
{
    public class TestSinav
    {
        [Key]
        public Guid TestSinavId { get; set; }
        public Guid SinavId { get; set; }
        public string SoruMetni { get; set; }

        public ICollection<TestSinavSorular> TestSinavSorulars { get; set; }
        public Sinav Sinavs { get; set; }
    }
}
