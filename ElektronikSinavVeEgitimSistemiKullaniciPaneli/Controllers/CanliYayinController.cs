﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.CanliYayin;
using BusinessLayer.DersIslemleri;
using EntityLayer;
using EntityLayer.CanliYayin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Controllers
{
    [Authorize(Roles = "Egitmen, Admin, Ogrenci")]
    public class CanliYayinController : Controller
    {
        private readonly ICanliYayinIslemleri _canliYayinIslemleri;
        private readonly IDersListesiSelectListItem _dersListesiSelectListItem;
        public CanliYayinController(ICanliYayinIslemleri canliYayinIslemleri, IDersListesiSelectListItem dersListesiSelectListItem)
        {
            _canliYayinIslemleri = canliYayinIslemleri;
            _dersListesiSelectListItem = dersListesiSelectListItem;
        }

        
        
        [Authorize(Roles = "Egitmen, Admin")]
        public IActionResult Index()
        {
            var canliYayinListem =
                _canliYayinIslemleri.OlusturdugumCanliYayinlar(Guid.Parse(User.Identity.GetUserId()));

            return View(canliYayinListem);
        }


        [Authorize(Roles = "Egitmen, Admin")]
        public IActionResult CanliYayinOlustur()
        {
            ViewBag.DersListesi = _dersListesiSelectListItem.DersListesi();
            return View();
        }


        [Authorize(Roles = "Egitmen, Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CanliYayinOlustur(CanliYayinOlusturViewModel canliYayinOlusturViewModel)
        {
            if (ModelState.IsValid)
            {
                var sonuc = _canliYayinIslemleri.CanliYayinKayit(canliYayinOlusturViewModel, Guid.Parse(User.Identity.GetUserId()));

                if (sonuc.isSuccess)
                {
                    return RedirectToAction("CanliYayinDetaylari", "CanliYayin", new { canliYayinGuid = sonuc.Data.ToString() });
                }
                else
                {
                    ModelState.AddModelError("Hata", "Lütfen tüm alanları doldurun!");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("Hata", "Lütfen tüm alanları doldurun!");
                return View();
            }
        }


        [Authorize(Roles = "Egitmen, Admin")]
        public IActionResult CanliYayinDetaylari(string canliYayinGuid)
        {
            var canliYayinDetaylari = _canliYayinIslemleri.CanliYayinDetaylari(Guid.Parse(canliYayinGuid));

            ViewBag.YoklamaListesi = _canliYayinIslemleri.CanliYayinYoklamaListesi(Guid.Parse(canliYayinGuid)).OrderBy(x => x.OgrenciNumarasi);

            ViewBag.CanliYayinDokumanlari =
                _canliYayinIslemleri.CanliYayinDokumanlariListele(Guid.Parse(canliYayinGuid));

            return View(canliYayinDetaylari);
        }



        [Authorize(Roles = "Egitmen, Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult CanliYayinBaslat(string canliYayinGuid)
        {
            _canliYayinIslemleri.CanliYayinBaslat(Guid.Parse(canliYayinGuid));
            return new JsonResult(new Result { isSuccess = true, Data = canliYayinGuid });
        }



        [Authorize(Roles = "Egitmen, Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult CanliYayinSonlandir(string canliYayinGuid)
        {
            _canliYayinIslemleri.CanliYayinBitir(Guid.Parse(canliYayinGuid));
            return new JsonResult(new Result { isSuccess = true, Data = canliYayinGuid }); ;
        }


        [Authorize(Roles = "Egitmen, Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files, string canliYayinId)
        {
            if (files.Count == 0 || canliYayinId == null)
            {
                return RedirectToAction("CanliYayinDetaylari", "CanliYayin", new { canliYayinGuid = canliYayinId });
            }

            var canliYayinDosyalari = new List<UploadFileInfo>();

            foreach (var formFile in files)
            {
                var fileName = GetUniqueFileName(formFile.FileName.Trim());
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot\\images\\upload",
                    fileName);

                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                        canliYayinDosyalari.Add(new UploadFileInfo { DosyaAdi = fileName, DosyaYolu = filePath });
                    }
                }
            }

            _canliYayinIslemleri.CanliYayinUploadDosya(canliYayinDosyalari, Guid.Parse(canliYayinId));

            return RedirectToAction("CanliYayinDetaylari", "CanliYayin", new { canliYayinGuid = canliYayinId });
        }


        [Authorize(Roles = "Egitmen, Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CanliYayiniYuklenenDosyayiSil(string dosyaDokumanId, string canliYayinGuid)
        {
            var sonuc = _canliYayinIslemleri.CanliYayinDokumanSil(Guid.Parse(dosyaDokumanId));
            return RedirectToAction("CanliYayinDetaylari", "CanliYayin", new { canliYayinGuid = canliYayinGuid });
        }


        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                    + "_"
                    + Guid.NewGuid().ToString().Substring(0, 4)
                    + Path.GetExtension(fileName);
        }


        [Authorize(Roles = "Egitmen, Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CanliYayiniSil(string canliYayinGuid)
        {
            var sonuc = _canliYayinIslemleri.CanliYayiniSil(Guid.Parse(canliYayinGuid));
            if (sonuc)
            {
                return new JsonResult(new Result { isSuccess = true }); ;
            }
            else
            {
                return new JsonResult(new Result { isSuccess = true, Data = canliYayinGuid }); ;
            }
        }

    }
}