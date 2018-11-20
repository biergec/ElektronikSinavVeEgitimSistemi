using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DersIslemleri;
using BusinessLayer.Sinav;
using BusinessLayer.SinavGiris;
using Microsoft.AspNetCore.Mvc;
using ElektronikSinavVeEgitimSistemiKullaniciPaneli.Models;
using Microsoft.AspNetCore.Authorization;
using EntityLayer;
using EntityLayer.Sinav;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IKayitliDerslerim _kayitliDerslerim;
        private readonly IDersIslemleri _dersIslemleri;
        private readonly IDersListesiSelectListItem _dersListesiSelectListItem;
        private readonly IOgrenciGirebilecegiSinavlar _ogrenciGirebilecegiSinavlar;
        private readonly ISinavBilgileri _sinavBilgileri;
        private readonly IEgitmenSinavBilgileri _egitmenSinavBilgileri;
        private readonly ISinavKayit _sinavKayit;

        public HomeController(IKayitliDerslerim kayitliDerslerim, IDersIslemleri dersIslemleri, IDersListesiSelectListItem dersListesiSelectListItem, IOgrenciGirebilecegiSinavlar ogrenciGirebilecegiSinavlar, ISinavBilgileri sinavBilgileri, IEgitmenSinavBilgileri egitmenSinavBilgileri, ISinavKayit sinavKayit)
        {
            this._kayitliDerslerim = kayitliDerslerim;
            this._dersIslemleri = dersIslemleri;
            this._dersListesiSelectListItem = dersListesiSelectListItem;
            this._ogrenciGirebilecegiSinavlar = ogrenciGirebilecegiSinavlar;
            this._sinavBilgileri = sinavBilgileri;
            this._egitmenSinavBilgileri = egitmenSinavBilgileri;
            this._sinavKayit = sinavKayit;
        }


        public IActionResult Index()
        {
            ViewBag.DersListesi = _dersListesiSelectListItem.DersListesi();
            var ogrenciGirebilecegiSinavListesi =
                _ogrenciGirebilecegiSinavlar.OgrenciGirebilecegiSinavlar(Guid.Parse(User.Identity.GetUserId()));

            return View(ogrenciGirebilecegiSinavListesi);
        }


        public IActionResult SinavaBasla(string sinavId)
        {
            var sinavTuru = _sinavBilgileri.SinavBilgileri(Guid.Parse(sinavId));

            switch (sinavTuru.SinavTuru)
            {
                case SinavTuru.Test:
                    return RedirectToAction("TestSinavSinavaBasla", new { sinavId });
                case SinavTuru.Klasik:
                    return RedirectToAction("KlasikSinavSinavaBasla", new { sinavId });
                default:
                    return BadRequest();
            }
        }


        public IActionResult KlasikSinavSinavaBasla(string sinavId)
        {
            // Sayfaya yönlendirmeden önce sınav başlangıcını kaydet 
            var sinavBaslamaKayit =
                _sinavKayit.SinavBaslangicKayit(Guid.Parse(sinavId), Guid.Parse(User.Identity.GetUserId()));
            if (!sinavBaslamaKayit.isSuccess)
                return Index();

            var result = _egitmenSinavBilgileri.SinavSoruBilgileri(Guid.Parse(sinavId));

            return View((List<SinavSorulariGoruntuleme>)result.Data);
        }



        public IActionResult TestSinavSinavaBasla(string sinavId)
        {
            // Sayfaya yönlendirmeden önce sınav başlangıcını kaydet 
            var sinavBaslamaKayit =
                _sinavKayit.SinavBaslangicKayit(Guid.Parse(sinavId), Guid.Parse(User.Identity.GetUserId()));
            if (!sinavBaslamaKayit.isSuccess)
                return Index();

            var result = _egitmenSinavBilgileri.SinavSoruBilgileri(Guid.Parse(sinavId));

            return View((List<SinavSorulariGoruntuleme>)result.Data);
        }



        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> KayitliDersSil(string dersId)
        {
            var result = await
                Task.FromResult(_kayitliDerslerim.KayitliDersSil(Guid.Parse(dersId),
                    Guid.Parse(User.Identity.GetUserId())));

            return new JsonResult(new Result { Message = result.Message, isSuccess = result.isSuccess });
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> OgrenciYeniDersKaydi(string dersId, string dersKayitAnahtari)
        {
            var result = await
                Task.FromResult(_kayitliDerslerim.DersKayit(Guid.Parse(dersId), dersKayitAnahtari,
                    Guid.Parse(User.Identity.GetUserId())));

            return new JsonResult(new Result { Message = result.Message, isSuccess = result.isSuccess });
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
