using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DersIslemleri;
using BusinessLayer.Sinav;
using DAL.UnitOfWork;
using EntityLayer;
using EntityLayer.Sinav;
using EntityLayer.Sinav;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Controllers
{
    // [Authorize(Roles = "Admin,Egitmen")]
    [Authorize]
    public class SinavController : Controller
    {
        private readonly ISinavOlustur _sinavOlustur;
        private readonly IEgitmenSinavBilgileri _egitmenSinavBilgileri;
        private readonly IDersIslemleri _dersIslemleri;

        public SinavController(ISinavOlustur sinavOlustur, IEgitmenSinavBilgileri egitmenSinavBilgileri, IDersIslemleri dersIslemleri)
        {
            this._sinavOlustur = sinavOlustur;
            this._egitmenSinavBilgileri = egitmenSinavBilgileri;
            this._dersIslemleri = dersIslemleri;
        }


        public IActionResult Index()
        {
            var dersListesi = _dersIslemleri.GetAllDersler();
            ViewBag.DersListesi = DersListesiTurDonusumu((IEnumerable<DersViewModel>)dersListesi.Data);

            return View();
        }


        // ders listesini List<SelectListItem> tipine çeviriyouz
        private List<SelectListItem> DersListesiTurDonusumu(IEnumerable<DersViewModel> dersList)
        {
            List<SelectListItem> dersListTurDonusumu = new List<SelectListItem>();

            foreach (var itemDersler in dersList)
            {
                dersListTurDonusumu.Add(new SelectListItem { Value = itemDersler.DerslerId.ToString(), Text = itemDersler.DersAdi });
            }

            return dersListTurDonusumu;
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

                var dersListesi = _dersIslemleri.GetAllDersler();
                ViewBag.DersListesi = DersListesiTurDonusumu((IEnumerable<DersViewModel>)dersListesi.Data);

                return View();
            }
            else
            {
                var dersListesi = _dersIslemleri.GetAllDersler();
                ViewBag.DersListesi = DersListesiTurDonusumu((IEnumerable<DersViewModel>)dersListesi.Data);

                return View();
            }
        }



        public IActionResult Dersler()
        {
            return View();
        }



        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> DersEkle(DersEkleViewModel dersEkleViewModel)
        {
            if (ModelState.IsValid)
            {
                var sonuc = await Task.FromResult(_dersIslemleri.DersEkle(dersEkleViewModel));

                return new JsonResult(new Result { isSuccess = sonuc.isSuccess, Message = sonuc.Message });
            }
            else
            {
                return new JsonResult(new Result { isSuccess = false, Message = "Girdiğiniz veriler hatalıdır!" });
            }
        }



        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> DersSil(string dersId)
        {
            if (ModelState.IsValid)
            {
                var sonuc = await Task.FromResult(_dersIslemleri.DersSil(Guid.Parse(dersId)));

                return new JsonResult(new Result { isSuccess = sonuc.isSuccess, Message = sonuc.Message });
            }
            else
            {
                return new JsonResult(new Result { isSuccess = false, Message = "İşlem yerine getirilemedi" });
            }
        }



        public async Task<JsonResult> DersListesiGuncelle()
        {
            try
            {
                var sonuc = await Task.FromResult(_dersIslemleri.GetAllDersler());

                var jsonConvertData = JsonConvert.SerializeObject(sonuc.Data);

                return new JsonResult(new Result { isSuccess = sonuc.isSuccess, Message = "Ders Listesi Güncellendi.", Data = jsonConvertData });
            }
            catch (Exception)
            {
                return new JsonResult(new Result { isSuccess = false, Message = "Ders Listesi Güncelleme Hatası." });
            }
        }




        public IActionResult TestSinavOlustur(SinavOlusturmaSecenekleri sinavOlusturmaSecenekleri)
        {

            ViewBag.DersAdi = _dersIslemleri.GetDersAdi(Guid.Parse(sinavOlusturmaSecenekleri.DersGuidId));

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
            ViewBag.DersAdi = _dersIslemleri.GetDersAdi(Guid.Parse(sinavOlusturmaSecenekleri.DersGuidId));

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