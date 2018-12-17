using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.CanliYayin;
using BusinessLayer.DersIslemleri;
using EntityLayer;
using EntityLayer.CanliYayin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Controllers
{
    public class CanliYayinController : Controller
    {
        private readonly ICanliYayinIslemleri _canliYayinIslemleri;
        private readonly IDersListesiSelectListItem _dersListesiSelectListItem;
        public CanliYayinController(ICanliYayinIslemleri canliYayinIslemleri, IDersListesiSelectListItem dersListesiSelectListItem)
        {
            _canliYayinIslemleri = canliYayinIslemleri;
            _dersListesiSelectListItem = dersListesiSelectListItem;
        }

        public IActionResult Index()
        {
            var canliYayinListem =
                _canliYayinIslemleri.OlusturdugumCanliYayinlar(Guid.Parse(User.Identity.GetUserId()));

            return View(canliYayinListem);
        }


        // TODO Sadeceğitmenler yapabilsin
        public IActionResult CanliYayinOlustur()
        {
            ViewBag.DersListesi = _dersListesiSelectListItem.DersListesi();
            return View();
        }


        // TODO Sadeceğitmenler yapabilsin
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


        // TODO Sadeceğitmenler yapabilsin
        public IActionResult CanliYayinDetaylari(string canliYayinGuid)
        {
            var canliYayinDetaylari = _canliYayinIslemleri.CanliYayinDetaylari(Guid.Parse(canliYayinGuid));

            ViewBag.YoklamaListesi = _canliYayinIslemleri.CanliYayinYoklamaListesi(Guid.Parse(canliYayinGuid)).OrderBy(x => x.OgrenciNumarasi);

            return View(canliYayinDetaylari);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult CanliYayinBaslat(string canliYayinGuid)
        {
            _canliYayinIslemleri.CanliYayinBaslat(Guid.Parse(canliYayinGuid));
            return new JsonResult(new Result { isSuccess = true, Data = canliYayinGuid });
        }


        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult CanliYayinSonlandir(string canliYayinGuid)
        {
            _canliYayinIslemleri.CanliYayinBitir(Guid.Parse(canliYayinGuid));
            return new JsonResult(new Result { isSuccess = true, Data = canliYayinGuid });;
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size, filePath});
        }


          [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CanliYayiniSil(string canliYayinGuid)
        {
            var sonuc = _canliYayinIslemleri.CanliYayiniSil(Guid.Parse(canliYayinGuid));
            if (sonuc)
            {
                return new JsonResult(new Result { isSuccess = true });;
            }
            else
            {
                return new JsonResult(new Result { isSuccess = true, Data = canliYayinGuid });;
            }
        }


    }
}