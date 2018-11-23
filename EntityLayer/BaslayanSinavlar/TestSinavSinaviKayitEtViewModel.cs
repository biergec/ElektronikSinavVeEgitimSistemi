using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.BaslayanSinavlar
{
    public class TestSinavSinaviKayitEtViewModel
    {
        public Guid OgrenciId { get; set; }
        public string SinavId { get; set; }
        public List<TestSinavCevaplari> TestSinavCevaplariList { get; set; }

        public class TestSinavCevaplari
        {
            public Guid TestSinavSorularId { get; set; }
            public int SoruCevapSikki { get; set; }
        }
    }
}
