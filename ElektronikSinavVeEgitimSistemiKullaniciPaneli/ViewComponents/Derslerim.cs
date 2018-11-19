using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DersIslemleri;
using EntityLayer.Ders;
using Microsoft.AspNetCore.Mvc;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.ViewComponents
{
    public class Derslerim : ViewComponent
    {
        private readonly IKayitliDerslerim _kayitliDerslerim;

        public Derslerim(IKayitliDerslerim kayitliDerslerim)
        {
            this._kayitliDerslerim = kayitliDerslerim;
        }

        public IViewComponentResult Invoke(string kullaniciGuidId)
        {
            var derslerim = _kayitliDerslerim.GetKayitliDerslerim(Guid.Parse(kullaniciGuidId));

            return View((List<KayitliDerslerim>)derslerim.Data);
        }
    }
}
