using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.Login
{
    public class KayitOlViewModel
    {
        [Required(ErrorMessage = "E-Mail adresi zorunludur"), DataType(DataType.EmailAddress, ErrorMessage = "E-Mail adresinizi doğru olarak girmelisiniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Sifre alanı zorunludur."), DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Şifreleriniz Uyuşmamaktadır."), Required(ErrorMessage = "Sifre alanı zorunludur."), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Üyelik türü zorunludur")]
        public UyelikTuru UyelikTuru { get; set; }

        [Required(ErrorMessage = "{0} Alanı Zorunludur")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "{0} Alanı Zorunludur")]
        public string Soyad { get; set; }

        [Display(Name = "Üniversite"), Required(ErrorMessage = "{0} İsim Alanı Zorunludur")]
        public string EgitimGorduguKurumAdi { get; set; }

        [Display(Name = "Üniversite Bölüm İsmi"), Required(ErrorMessage = "{0} Bölüm İsmi Alanı Zorunludur")]
        public string EgitimGorduguKurumBolum { get; set; }

        [Display(Name = "Öğrenci Numaranız"), Required(ErrorMessage = "{0} Alanı Zorunludur")]
        public string KurumOgrenciNumarasi { get; set; }
    }
}
