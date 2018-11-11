using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityLayer;
using EntityLayer.Sinav;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Controllers
{
    [Authorize]
    public class SinavController : Controller
    {

        public IActionResult Index()
        {

            return View();
        }



        [HttpPost, ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin,Egitmen")]
        public IActionResult Index(SinavOlusturmaSecenekleri sinavOlusturmaSecenekleri)
        {
            if (ModelState.IsValid)
            {
                switch (sinavOlusturmaSecenekleri.SinavTuru)
                {
                    case SinavTuru.Klasik:
                        return RedirectToAction("KlasikSinavOlustur", "Sinav", sinavOlusturmaSecenekleri);
                    case SinavTuru.Test:
                        return RedirectToAction("TestSinavOlustur", "Sinav", sinavOlusturmaSecenekleri);
                    default:
                        break;
                }

                return View();
            }
            else
                return View();
        }



        // [Authorize(Roles = "Admin,Egitmen")]
        public IActionResult TestSinavOlustur(SinavOlusturmaSecenekleri sinavOlusturmaSecenekleri)
        {

            return View(sinavOlusturmaSecenekleri);
        }



        [HttpPost, ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin,Egitmen")]
        public async Task<JsonResult> TestSinavOlustur(TestSinavSorulari testSinavSorulari)
        {

            return new JsonResult(new Result { });
        }



        public IActionResult KlasikSinavOlustur(SinavOlusturmaSecenekleri sinavOlusturmaSecenekleri)
        {

            return View(sinavOlusturmaSecenekleri);
        }


        
        [HttpPost, ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin,Egitmen")]
        public async Task<JsonResult> KlasikSinavOlustur(KlasikSinavSorulari klasikSinavSorulari)
        {

            return new JsonResult(new Result { });
        }



    }
}