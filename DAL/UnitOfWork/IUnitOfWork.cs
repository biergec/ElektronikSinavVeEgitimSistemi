using System;
using System.Collections.Generic;
using System.Text;
using DAL.Repository;
using EntityLayer.BaslayanSinavlar;
using EntityLayer.CanliYayin;
using EntityLayer.Ders;
using EntityLayer.Sinav;
using EntityLayer.Sinav;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Sinav> SinavRepository { get; }
        
        IRepository<TestSinav> TestSinavRepository { get; }
        IRepository<TestSinavSorular> TestSinavSorularRepository { get; }

        IRepository<KlasikSinav> KlasikSinavRepository { get; }
        IRepository<KlasikSinavSorular> KlasikSinavSorularRepository { get; }

        IRepository<Dersler> DerslerRepository { get; }

        IRepository<GirilenTestSinavSonuclari> GirilenTestSinavSonuclariRepository { get; }

        IRepository<SuresiBaslamisSinavlar> SuresiBaslamisSinavlarRepository { get; }

        IRepository<KayitliDerslerim> KayitliDerslerimRepository { get; }

        IRepository<GirilenKlasikSinavKayit> GirilenKlasikSinavKayitRepository { get; }
        IRepository<KlasikSinavSinavSoruCevap> KlasikSinavSinavSoruCevapRepository { get; }

        IRepository<CanliYayin> CanliYayinRepository { get; }
        IRepository<CanliYayinDokumanlari> CanliYayinDokumanlariRepository { get; }
        IRepository<CanliYayinaKatilanlar> CanliYayinaKatilanlarRepository { get; }

        void SaveChanges();
    }
}
