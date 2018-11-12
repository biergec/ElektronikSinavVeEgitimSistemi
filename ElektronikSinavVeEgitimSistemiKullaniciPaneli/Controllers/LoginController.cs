using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EntityLayer;
using EntityLayer.Login;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Controllers
{
    public class LoginController : Controller
    {
        private readonly IKayitOl _kayitOl;
        private readonly SignInManager<AppUser> _signInManager;
        public Microsoft.AspNetCore.Identity.UserManager<AppUser> _userManager;

        public LoginController(IKayitOl kayitOl, SignInManager<AppUser> signInManager, Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager)
        {
            this._kayitOl = kayitOl;
            this._signInManager = signInManager;
            this._userManager = userManager;
        }


        public IActionResult Index()
        {
            var girisControl = User.Identity.IsAuthenticated;
            if (girisControl)
                return RedirectToAction("Index", "Home");
            
            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> GirisYap(Login loginBilgileri)
        {
            if (!ModelState.IsValid)
                return new JsonResult(new Result { isSuccess = false, Message = "Giriş bilgileri hatalı." });

            var girisResult = await _signInManager.PasswordSignInAsync(loginBilgileri.Email, loginBilgileri.Password, false, false);

            return girisResult.Succeeded ? new JsonResult(new Result { isSuccess = true, Message = "/Home/Index"}) : new JsonResult(new Result { isSuccess = false, Message = "Lütfen giriş bilgilerinizi kontrol ediniz."});
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }



        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> KayitOl(KayitOlViewModel kayitOlViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
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