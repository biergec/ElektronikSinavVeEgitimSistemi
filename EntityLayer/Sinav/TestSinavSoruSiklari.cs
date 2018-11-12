using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.Sinav
{
    public class TestSinavSoruSiklari
    {
        [Key]
        public Guid TestSinavSoruSiklariId { get; set; }

        public Guid TestSinavSorularId { get; set; }

        public int SoruSikki { get; set; }
        public string SoruSikMetni { get; set; }

        public TestSinavSorular TestSinavSorular{ get; set; }
    }
}
