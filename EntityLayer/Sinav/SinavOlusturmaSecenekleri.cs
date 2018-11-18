using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.Sinav
{
    public class SinavOlusturmaSecenekleri
    {
        [Required(ErrorMessage = "Soru şık sayısı en az 3 olabilir."), Range(3,10, ErrorMessage = "Şık sayısı en az 3, en fazla 10 olabilir")]
        public int SoruSikSayisi { get; set; }

        public SinavTuru SinavTuru { get; set; }
        
        [Required(ErrorMessage = "Ders İsmi zorunludur.")]
        public string DersGuidId { get; set; }
    }

    public enum SinavTuru
    {
        Test,
        Klasik
    }
}
