using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using EntityLayer.Login;
using Microsoft.AspNetCore.Identity;

namespace BusinessLayer.Login
{
    public class KayitOlManager : IKayitOl
    {
        private readonly UserManager<AppUser> _userManager;
        public KayitOlManager(UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
        }


        public async Task<Result> KayitOl(KayitOlViewModel kayitOlViewModel)
        {
            var user = await _userManager.FindByEmailAsync(kayitOlViewModel.Email);
            if (user != null)
            {
                return new Result { isSuccess = false, Message = "Kayıt olmak istediğiniz E-Mail adresi daha önce kullanulmıştır." };
            }

            user = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = kayitOlViewModel.Email.ToLower(),
                Ad = kayitOlViewModel.Ad,
                Soyad = kayitOlViewModel.Soyad,
                EgitimGorduguKurumAdi = kayitOlViewModel.EgitimGorduguKurumAdi,
                EgitimGorduguKurumBolum = kayitOlViewModel.EgitimGorduguKurumBolum,
                KurumOgrenciNumarasi = kayitOlViewModel.KurumOgrenciNumarasi,
                UserName = kayitOlViewModel.Email.ToLower()
            };
            var identityResultKayit = await _userManager.CreateAsync(user, kayitOlViewModel.Password);
            if (identityResultKayit.Errors != null)
            {
                return new Result { isSuccess = false, Message = identityResultKayit.Errors.First().Description };
            }

            var identityResultAddRole = await _userManager.AddToRoleAsync(user, kayitOlViewModel.UyelikTuru.ToString());

            if (identityResultAddRole.Succeeded && identityResultKayit.Succeeded)
            {
                return new Result { isSuccess = true, Message = "Kullanıcı kaydı başarılı" };
            }

            return new Result { isSuccess = false, Message = "Kullanıcı kaydı başarısız oldu. Lütfen daha sonra tekrar deneyiniz." };
        }

    }
}
