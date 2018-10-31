using System.Threading.Tasks;
using BusinessLayer.Login;
using EntityLayer;
using EntityLayer.Login;
using Microsoft.AspNetCore.Mvc;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.ViewComponents
{
    public class KayitOlViewComponent : ViewComponent
    {
        private readonly IKayitOl _kayitOl;

        public KayitOlViewComponent(IKayitOl kayitOl)
        {
            this._kayitOl = kayitOl;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
