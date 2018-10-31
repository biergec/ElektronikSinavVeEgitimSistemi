using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using EntityLayer;
using EntityLayer.Login;
using Microsoft.AspNetCore.Mvc;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Controllers
{
    public class LoginController : Controller
    {
        public IKayitOl _kayitOl { get; }


        public LoginController(IKayitOl _kayitOl)
        {
            this._kayitOl = _kayitOl;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> KayitOl([FromBody]KayitOlViewModel kayitOlViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!(kayitOlViewModel.UyelikTuru == UyelikTuru.Egitmen || kayitOlViewModel.UyelikTuru == UyelikTuru.Ogrenci))
                    {
                        // Farklı bir üyelik tipine izin verme!
                        throw new InvalidEnumArgumentException("Yetki tipi hatalı girildi.");
                    }
                    var result = await _kayitOl.KayitOl(kayitOlViewModel);
                    return new JsonResult(new Result { isSuccess = result.isSuccess, Message = result.Message });
                }
                else
                {
                    return new JsonResult(new Result { isSuccess = false, Message = "İşlem yerine getirilemedi. Lütfen bilgilerinizi kontrol ediniz!" });
                }
            }
            catch (Exception)
            {
                // TODO LOG Kayıt işlemi yerine getirilemedi!
                return new JsonResult(new Result { isSuccess = false, Message = "İşlem yerine getirilemedi. Lütfen bilgilerinizi kontrol ediniz!" });
            }
        }



    }
}