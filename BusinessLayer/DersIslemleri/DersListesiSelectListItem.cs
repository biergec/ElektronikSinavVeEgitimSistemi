using System;
using System.Collections.Generic;
using System.Text;
using EntityLayer.Sinav;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BusinessLayer.DersIslemleri
{
    public class DersListesiSelectListItem : IDersListesiSelectListItem
    {
        private readonly IDersIslemleri _dersIslemleri;

        public DersListesiSelectListItem(IDersIslemleri dersIslemleri)
        {
            this._dersIslemleri = dersIslemleri;
        }


        public List<SelectListItem> DersListesi()
        {
            var dersListesi = _dersIslemleri.GetAllDersler();

            return DersListesiTurDonusumu((IEnumerable<DersViewModel>)dersListesi.Data);
        }


        public List<SelectListItem> KayitliOlmadigimDersler(string kullaniciId)
        {
            var kayitOlmadigimDersler = _dersIslemleri.GetKayitOlmadigimDersler(Guid.Parse(kullaniciId));

            return DersListesiTurDonusumu(kayitOlmadigimDersler);
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

    }
}
