using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.CanliYayin;
using BusinessLayer.DersIslemleri;
using BusinessLayer.Sinav;
using BusinessLayer.SinavGiris;
using Microsoft.AspNetCore.Mvc;
using ElektronikSinavVeEgitimSistemiKullaniciPaneli.Models;
using Microsoft.AspNetCore.Authorization;
using EntityLayer;
using EntityLayer.BaslayanSinavlar;
using EntityLayer.Sinav;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Controllers
{
    [Authorize(Roles = "Egitmen, Admin, Ogrenci")]
    public class HomeController : Controller
    {
        private readonly IKayitliDerslerim _kayitliDerslerim;
        private readonly IDersIslemleri _dersIslemleri;
        private readonly IDersListesiSelectListItem _dersListesiSelectListItem;
        private readonly IOgrenciGirebilecegiSinavlar _ogrenciGirebilecegiSinavlar;
        private readonly ISinavBilgileri _sinavBilgileri;
        private readonly IEgitmenSinavBilgileri _egitmenSinavBilgileri;
        private readonly ISinavKayit _sinavKayit;
        private readonly ICanliYayinIslemleri _canliYayinIslemleri;

        public HomeController(IKayitliDerslerim kayitliDerslerim, IDersIslemleri dersIslemleri, IDersListesiSelectListItem dersListesiSelectListItem, IOgrenciGirebilecegiSinavlar ogrenciGirebilecegiSinavlar, ISinavBilgileri sinavBilgileri, IEgitmenSinavBilgileri egitmenSinavBilgileri, ISinavKayit sinavKayit, ICanliYayinIslemleri canliYayinIslemleri)
        {
            this._kayitliDerslerim = kayitliDerslerim;
            this._dersIslemleri = dersIslemleri;
            this._dersListesiSelectListItem = dersListesiSelectListItem;
            this._ogrenciGirebilecegiSinavlar = ogrenciGirebilecegiSinavlar;
            this._sinavBilgileri = sinavBilgileri;
            this._egitmenSinavBilgileri = egitmenSinavBilgileri;
            this._sinavKayit = sinavKayit;
            _canliYayinIslemleri = canliYayinIslemleri;
        }


        public IActionResult Index()
        {
            ViewBag.DersListesi = _dersListesiSelectListItem.DersListesi();
            var ogrenciGirebilecegiSinavListesi =
                _ogrenciGirebilecegiSinavlar.OgrenciGirebilecegiSinavlar(Guid.Parse(User.Identity.GetUserId()));

            ViewBag.OgrenciKatilabilecegiCanliYayinlar = _canliYayinIslemleri.OgrenciKatilabilecegiCanliYayinlar(Guid.Parse(User.Identity.GetUserId()));

            return View(ogrenciGirebilecegiSinavListesi);
        }


        [Authorize(Roles = "Admin, Ogrenci")]
        public IActionResult OgrenciCanliYayinEkrani(string canliYayinId)
        {
            var canliYayinDetaylari = _canliYayinIslemleri.CanliYayinDetaylari(Guid.Parse(canliYayinId));

            // Ogreci derse katılımını gerçekleştir.
            bool ogrenciDersKatilimKayit =
                _canliYayinIslemleri.OgrenciCanliYayinKatilimGerceklestir(Guid.Parse(User.Identity.GetUserId()),
                    Guid.Parse(canliYayinId));

            ViewBag.CanliYayinDokumanlari =
                _canliYayinIslemleri.CanliYayinDokumanlariListele(Guid.Parse(canliYayinId));

            if (ogrenciDersKatilimKayit)
                return View(canliYayinDetaylari);
            else
                return RedirectToAction("Index");
        }

       
        [Authorize(Roles = "Admin, Ogrenci")]
        [HttpGet]
        public async Task<IActionResult> FileDownload(string filename)
        {
            if (filename == null)
                return Content("Dosya bulunamadı");

            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot\\images\\upload", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                    {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }


        
        [Authorize(Roles = "Admin, Ogrenci")]
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


        
        [Authorize(Roles = "Admin, Ogrenci")]
        public IActionResult KlasikSinavSinavaBasla(string sinavId)
        {
            // Sayfaya yönlendirmeden önce sınav başlangıcını kaydet 
            var sinavBaslamaKayit =
                _sinavKayit.SinavBaslangicBilgisiKayit(Guid.Parse(sinavId), Guid.Parse(User.Identity.GetUserId()));
            if (!sinavBaslamaKayit.isSuccess)
                return Index();

            var result = _egitmenSinavBilgileri.SinavSoruBilgileri(Guid.Parse(sinavId));

            return View((List<SinavSorulariGoruntuleme>)result.Data);
        }



        
        [Authorize(Roles = "Admin, Ogrenci")]
        public IActionResult TestSinavSinavaBasla(string sinavId)
        {
            // Sayfaya yönlendirmeden önce sınav başlangıcını kaydet 
            var sinavBaslamaKayit =
                _sinavKayit.SinavBaslangicBilgisiKayit(Guid.Parse(sinavId), Guid.Parse(User.Identity.GetUserId()));
            if (!sinavBaslamaKayit.isSuccess)
                return Index();

            var result = _egitmenSinavBilgileri.SinavSoruBilgileri(Guid.Parse(sinavId));

            return View((List<SinavSorulariGoruntuleme>)result.Data);
        }



        [Authorize(Roles = "Admin, Ogrenci")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> GirilenTestSinaviniKayitEt(TestSinavSinaviKayitEtViewModel testSinavSinaviKayitEtViewModel)
        {
            testSinavSinaviKayitEtViewModel.OgrenciId = Guid.Parse(User.Identity.GetUserId());

            var result = await Task.FromResult(_sinavKayit.TestSinavOgrenciSinaviKayit(testSinavSinaviKayitEtViewModel));

            return new JsonResult(new Result { isSuccess = result.isSuccess, Message = result.Message });
        }



        [Authorize(Roles = "Admin, Egitmen")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> KayitliDersSil(string dersId)
        {
            var result = await
                Task.FromResult(_kayitliDerslerim.KayitliDersSil(Guid.Parse(dersId),
                    Guid.Parse(User.Identity.GetUserId())));

            return new JsonResult(new Result { Message = result.Message, isSuccess = result.isSuccess });
        }


        
        [Authorize(Roles = "Admin, Ogrenci")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> OgrenciYeniDersKaydi(string dersId, string dersKayitAnahtari)
        {
            var result = await
                Task.FromResult(_kayitliDerslerim.DersKayit(Guid.Parse(dersId), dersKayitAnahtari,
                    Guid.Parse(User.Identity.GetUserId())));

            return new JsonResult(new Result { Message = result.Message, isSuccess = result.isSuccess });
        }


        
        [Authorize(Roles = "Admin, Ogrenci")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> KlasikSinavSinavKayit(KlasikSinavSinavKayitViewModel klasikSinavSinavKayit)
        {
            klasikSinavSinavKayit.OgrenciId = Guid.Parse(User.Identity.GetUserId());
            var result = await
                Task.FromResult(_sinavKayit.KlasikSinavOgrenciSinaviKayit(klasikSinavSinavKayit));

            return new JsonResult(new Result { Message = result.Message, isSuccess = result.isSuccess });
        }



    }
}
