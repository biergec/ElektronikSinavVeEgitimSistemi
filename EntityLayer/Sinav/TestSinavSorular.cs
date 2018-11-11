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
        public int SoruSikki { get; set; }
        public string SoruSikMetni { get; set; }
        public TestSinav TestSinav { get; set; }
    }
}
