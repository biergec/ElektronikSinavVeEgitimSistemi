using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.Sinav
{
    public class DersEkleViewModel
    {
        [Required(ErrorMessage ="Ders kayıt anahtarı gereklidir.")]
        public string DersKayitAnahtari { get; set; }

        [Required(ErrorMessage ="Ders kodu gereklidir.")]
        public double DersKodu { get; set; }
        
        [Required(ErrorMessage ="Ders adı alanı zorunludur gereklidir.")]
        public string DersAdi { get; set; }
    }
}
