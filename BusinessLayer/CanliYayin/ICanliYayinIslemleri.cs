using System;
using System.Collections.Generic;
using System.Text;
using EntityLayer;
using EntityLayer.CanliYayin;

namespace BusinessLayer.CanliYayin
{
    public interface ICanliYayinIslemleri
    {
        Result CanliYayinKayit(CanliYayinOlusturViewModel canliYayinOlusturViewModel, Guid canliYayinKayitEdenKisi);
        Result CanliYayinBitir(Guid yayinId);
        CanliYayinDetaylari CanliYayinDetaylari(Guid yayinId);
        List<CanliYayinDetaylari> OlusturdugumCanliYayinlar(Guid userId);
        Result CanliYayinBaslat(Guid yayinId);
        Result CanliYayinDosyaEkle();
        List<CanliYayinYoklama> CanliYayinYoklamaListesi(Guid canliYayinId);
        bool CanliYayiniSil(Guid yayinId);
    }
}
