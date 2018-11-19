using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DersIslemleri;
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

        public HomeController(IKayitliDerslerim kayitliDerslerim, IDersIslemleri dersIslemleri,IDersListesiSelectListItem dersListesiSelectListItem)
        {
            this._kayitliDerslerim = kayitliDerslerim;
            this._dersIslemleri = dersIslemleri;
            this._dersListesiSelectListItem = dersListesiSelectListItem;
        }


        public IActionResult Index()
        {
            ViewBag.DersListesi = _dersListesiSelectListItem.DersListesi();

            return View();
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
