using System;
using System.Collections.Generic;
using EntityLayer.BaslayanSinavlar;
using EntityLayer.EgitmenSinavBilgileri;

namespace BusinessLayer.Sinav
{
    public interface ISinavBilgileri
    {
        EntityLayer.Sinav.Sinav SinavBilgileri(Guid sinavId);
        List<SinavaGirenKisiler> SinavaGirenKisiBilgileri(Guid sinavId);
        List<EntityLayer.Sinav.Sinav> EgitmenOlusturduguSinavlar(Guid egitmenId);
        List<GirilenKlasikSinavKayit> OgrenciKlasikSinavSorulari(Guid sinavId);
    }
}