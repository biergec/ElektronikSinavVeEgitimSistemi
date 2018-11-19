using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DersIslemleri
{
    public interface IDersListesiSelectListItem
    {
        List<SelectListItem> KayitliOlmadigimDersler(string kullaniciId);
        List<SelectListItem> DersListesi();
    }
}
