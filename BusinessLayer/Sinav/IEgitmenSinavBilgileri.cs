using EntityLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Sinav
{
    public interface IEgitmenSinavBilgileri
    {
        Result OlusturdugumSinavlar(Guid sinavSahibiGuidId);
        Result OlusturulanSinaviSil(Guid sinavId, Guid sinavSahibiGuidId);
        Result SinavSoruBilgileri(Guid sinavId);
        Result SinavAktiflikDurumuDegistir(Guid sinavId);
    }
}
