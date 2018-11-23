using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.BaslayanSinavlar
{
    public class GirilenTestSinavSonuclari
    {
        [Key]
        public Guid GirilenTestSinavSonuclariId { get; set; }

        public Guid SuresiBaslamisSinavlarId { get; set; }

        public double SinavPuani { get; set; }

        public SuresiBaslamisSinavlar SuresiBaslamisSinavlar { get; set; }
    }
}
