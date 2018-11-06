using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.Login
{
    public class Login
    {
        [Required(ErrorMessage = "E-Mail alanı zorunludur."),DataType(DataType.EmailAddress, ErrorMessage = "E Mail adresi hatalı")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur."),DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
