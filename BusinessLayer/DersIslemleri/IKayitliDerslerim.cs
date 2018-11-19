using EntityLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DersIslemleri
{
    public interface IKayitliDerslerim
    {
        Result GetKayitliDerslerim(Guid kullaniciGuid);
        Result KayitliDersSil(Guid dersId, Guid kullaniciIdGuid);
        Result DersKayit(Guid dersId, string dersKayitAnahtari, Guid kullaniciIdGuid);
    }
}
