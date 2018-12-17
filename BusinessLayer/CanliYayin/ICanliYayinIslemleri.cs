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
        List<CanliYayinYoklama> CanliYayinYoklamaListesi(Guid canliYayinId);
        bool CanliYayiniSil(Guid yayinId);
        bool CanliYayinUploadDosya(List<UploadFileInfo> dosyaFileInfos, Guid sinavId);
        List<CanliYayinDokumanlari> CanliYayinDokumanlariListele(Guid canliYayinId);
        bool CanliYayinDokumanSil(Guid dokumanlarIdGuid);

        List<CanliYayinDetaylari> OgrenciKatilabilecegiCanliYayinlar(Guid ogreciId);
        bool OgrenciCanliYayinKatilimGerceklestir(Guid ogrenciId, Guid canliYayinId);

    }
}
