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

        [Required(ErrorMessage = "Ders Adi boş olamaz."), MinLength(3, ErrorMessage = "Ders adi en az 3 harfli olmak zorundadır.")]
        public string DersAdi { get; set; }

        public SinavTuru SinavTuru { get; set; }
        
        [Required(ErrorMessage = "Ders kodu zorunludur."), Range(100,1000000000000000000,ErrorMessage = "Ders kodu {1} - {2} aralığında olmalıdır.")]
        public double DersKodu { get; set; }
    }

    public enum SinavTuru
    {
        Test,
        Klasik
    }
}
