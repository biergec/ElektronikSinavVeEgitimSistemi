using System;
using System.Collections.Generic;
using System.Text;
using EntityLayer;
using EntityLayer.Sinav;

namespace BusinessLayer.Sinav
{
    public interface ISinavOlustur
    {
        Result KlasikSinavOlustur(KlasikSinavSorulari klasikSinavSorulari, Guid sinavSahibiIdBilgisi);
        Result TestSinavOlustur(TestSinavSorulari testSinavSorulari, Guid sinavSahibiIdBilgisi);
        Result SinavSuresiDegistir(Guid sinavId, int sinavSuresiDakika);
    }
}
