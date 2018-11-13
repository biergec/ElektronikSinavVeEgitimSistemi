using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Sinav;
using DAL.UnitOfWork;
using EntityLayer;
using EntityLayer.Sinav;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Controllers
{
    // [Authorize(Roles = "Admin,Egitmen")]
    [Authorize]
    public class SinavController : Controller
    {
        private readonly ISinavOlustur _sinavOlustur;
        private readonly IEgitmenSinavBilgileri _egitmenSinavBilgileri;

        public SinavController(ISinavOlustur sinavOlustur, IEgitmenSinavBilgileri egitmenSinavBilgileri)
        {
            this._sinavOlustur = sinavOlustur;
            this._egitmenSinavBilgileri = egitmenSinavBilgileri;
        }



        public IActionResult Index()
        {

            return View();
        }



        [HttpPost, ValidateAntiForgeryToken]
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



        public IActionResult TestSinavOlustur(SinavOlusturmaSecenekleri sinavOlusturmaSecenekleri)
        {

            return View(sinavOlusturmaSecenekleri);
        }



        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> TestSinavOlustur(TestSinavSorulari testSinavSorulari)
        {
            var result =
                await Task.FromResult(_sinavOlustur.TestSinavOlustur(testSinavSorulari,
                    Guid.Parse(User.Identity.GetUserId())));

            return new JsonResult(new Result { isSuccess = result.isSuccess, Message = result.Message });
        }



        public IActionResult KlasikSinavOlustur(SinavOlusturmaSecenekleri sinavOlusturmaSecenekleri)
        {

            return View(sinavOlusturmaSecenekleri);
        }



        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> KlasikSinavOlustur(KlasikSinavSorulari klasikSinavSorulari)
        {
            var result = await Task.FromResult(_sinavOlustur.KlasikSinavOlustur(klasikSinavSorulari, Guid.Parse(User.Identity.GetUserId())));

            return new JsonResult(new Result { isSuccess = result.isSuccess, Message = result.Message });
        }



        // Data Sinav tablosu tipinde
        public IActionResult Sinavlarim()
        {
            var sinavlarim = _egitmenSinavBilgileri.OlusturdugumSinavlar(Guid.Parse(User.Identity.GetUserId()));

            return View((IEnumerable<Sinav>)sinavlarim.Data);
        }



        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> SinavSil(string sinavId)
        {
            if (sinavId == null)
                return new JsonResult(new Result { isSuccess = false, Message = "Sınav silme işlemi başarısız!.." });

            var result = await Task.FromResult(_egitmenSinavBilgileri.OlusturulanSinaviSil(Guid.Parse(sinavId), Guid.Parse(User.Identity.GetUserId())));

            return new JsonResult(new Result { isSuccess = result.isSuccess, Message = result.Message });
        }


        // Datayı her zaman object doner on taraft tipini kontrol et parse et
        [HttpGet]
        public IActionResult SinavBilgileriGoster(string sinavId)
        {
            if (sinavId == null)
                return BadRequest();

            var result = _egitmenSinavBilgileri.SinavSoruBilgileri(Guid.Parse(sinavId));

            return View((List<SinavSorulariGoruntuleme>)result.Data);
        }


    }
}