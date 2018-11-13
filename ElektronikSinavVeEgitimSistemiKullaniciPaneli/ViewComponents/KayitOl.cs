using System.Threading.Tasks;
using BusinessLayer.Login;
using EntityLayer;
using EntityLayer.Login;
using Microsoft.AspNetCore.Mvc;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.ViewComponents
{
    public class KayitOlViewComponent : ViewComponent
    {
        public KayitOlViewComponent() { }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
