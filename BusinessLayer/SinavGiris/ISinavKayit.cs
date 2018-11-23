using System;
using EntityLayer;
using EntityLayer.BaslayanSinavlar;

namespace BusinessLayer.SinavGiris
{
    public interface ISinavKayit
    {
        Result SinavBaslangicBilgisiKayit(Guid sinavId, Guid ogrenciId);
        Result KlasikSinavOgrenciSinaviKayit(KlasikSinavSinavKayitViewModel klasikSinavOgrenciCevaplari);
    }
}