using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.Sinav
{
    public class TestSinavSorulari
    {
        public string DersGuidId { get; set; }

        public List<SoruTemplate> SoruTemplate { get; set; }
    }

    public class SoruTemplate
    {
        public string SoruDogruSik { get; set; }
        public string SoruText { get; set; }
        public List<string> SoruSiklari { get; set; }
    }
}
