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
using EntityLayer.BaslayanSinavlar;
using EntityLayer.Sinav;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Controllers
{
    public class SinavNotlandirmaController : Controller
    {
        private readonly IDersIslemleri _dersIslemleri;
        private readonly ISinavBilgileri _sinavBilgileri;
        private readonly ISinavKayit _sinavKayit;
        private readonly ISinavNotlandir _sinavNotlandir;

        public SinavNotlandirmaController(IDersIslemleri dersIslemleri, ISinavBilgileri sinavBilgileri, ISinavKayit sinavKayit, ISinavNotlandir sinavNotlandir)
        {
            _dersIslemleri = dersIslemleri;
            this._sinavBilgileri = sinavBilgileri;
            this._sinavKayit = sinavKayit;
            this._sinavNotlandir = sinavNotlandir;
        }


        // Egitmen sınavlarının listesini goruntulesin
        public IActionResult Index()
        {
            // DersViewModel tipinde
            ViewBag.SinavListesi = _sinavBilgileri.EgitmenOlusturduguSinavlar(Guid.Parse(User.Identity.GetUserId()));

            return View();
        }




        public IActionResult SinavaGirenKisiler(string sinavId)
        {
            // List<SinavaGirenKisiler>
            return View(_sinavBilgileri.SinavaGirenKisiBilgileri(Guid.Parse(sinavId)));
        }




        public IActionResult KlasikSinavNotlandir(string sinavId)
        {
            // List<SinavaGirenKisiler>
            ViewBag.SinavSorulari = _sinavBilgileri.OgrenciKlasikSinavSorulari(Guid.Parse(sinavId));
            return View();
        }




        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult KlasikSinavNotlandir(string ogrenciId, string sinavId, double sinavNotu)
        {
            var result = _sinavNotlandir.KlasikSinavNotlandir(Guid.Parse(sinavId), Guid.Parse(ogrenciId), sinavNotu);

            if (result.isSuccess)
            {
                return RedirectToAction("SinavaGirenKisiler", "SinavNotlandirma", new { sinavId });
            }
            else
            {
                ModelState.AddModelError("Hata","Girdiğiniz not hatalıdır.");
                return View();
            }
        }



    }
}