using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EntityLayer.Login
{
    public class AppUser : IdentityUser
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string EgitimGorduguKurumAdi { get; set; }
        public string EgitimGorduguKurumBolum { get; set; }
        public string KurumOgrenciNumarasi { get; set; }
    }
}
