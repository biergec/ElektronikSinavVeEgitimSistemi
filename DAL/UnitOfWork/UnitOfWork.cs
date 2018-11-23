using System;
using System.Collections.Generic;
using System.Text;
using DAL.Context;
using DAL.Repository;
using EntityLayer.BaslayanSinavlar;
using EntityLayer.Ders;
using EntityLayer.Sinav;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private EfContext Context { get; }
        private IRepository<Sinav> _sinavRepository;
        private IRepository<TestSinav> _testSinavRepository;
        private IRepository<TestSinavSorular> _testSinavSorularRepository;
        private IRepository<KlasikSinav> _klasikSinavRepository;
        private IRepository<KlasikSinavSorular> _klasikSinavSorularRepository;
        private IRepository<Dersler> _derslerRepository;
        private IRepository<SuresiBaslamisSinavlar> _suresiBaslamisSinavlarRepository;
        private IRepository<KayitliDerslerim> _kayitliDerslerimRepository;
        private IRepository<GirilenKlasikSinavKayit> _girilenKlasikSinavKayitRepository;
        private IRepository<KlasikSinavSinavSoruCevap> _klasikSinavSinavSoruCevapRepository;

        public UnitOfWork(EfContext context)
        {
            Context = context;
        }

        public IRepository<Sinav> SinavRepository
        {
            get
            {
                return _sinavRepository = _sinavRepository ?? new Repository<Sinav>(Context);
            }
        }

        public IRepository<KlasikSinavSinavSoruCevap> KlasikSinavSinavSoruCevapRepository
        {
            get
            {
                return _klasikSinavSinavSoruCevapRepository = _klasikSinavSinavSoruCevapRepository ?? new Repository<KlasikSinavSinavSoruCevap>(Context);
            }
        }

        public IRepository<TestSinav> TestSinavRepository
        {
            get
            {
                return _testSinavRepository = _testSinavRepository ?? new Repository<TestSinav>(Context);
            }
        }

        public IRepository<TestSinavSorular> TestSinavSorularRepository
        {
            get
            {
                return _testSinavSorularRepository = _testSinavSorularRepository ?? new Repository<TestSinavSorular>(Context);
            }
        }

        public IRepository<KlasikSinav> KlasikSinavRepository
        {
            get
            {
                return _klasikSinavRepository = _klasikSinavRepository ?? new Repository<KlasikSinav>(Context);
            }
        }

        public IRepository<KlasikSinavSorular> KlasikSinavSorularRepository
        {
            get
            {
                return _klasikSinavSorularRepository = _klasikSinavSorularRepository ?? new Repository<KlasikSinavSorular>(Context);
            }
        }

        public IRepository<Dersler> DerslerRepository
        {
            get
            {
                return _derslerRepository = _derslerRepository ?? new Repository<Dersler>(Context);
            }
        }


        public IRepository<SuresiBaslamisSinavlar> SuresiBaslamisSinavlarRepository
        {
            get
            {
                return _suresiBaslamisSinavlarRepository = _suresiBaslamisSinavlarRepository ?? new Repository<SuresiBaslamisSinavlar>(Context);
            }
        }


        public IRepository<KayitliDerslerim> KayitliDerslerimRepository
        {
            get
            {
                return _kayitliDerslerimRepository = _kayitliDerslerimRepository ?? new Repository<KayitliDerslerim>(Context);
            }
        }

        public IRepository<GirilenKlasikSinavKayit> GirilenKlasikSinavKayitRepository
        {
            get
            {
                return _girilenKlasikSinavKayitRepository = _girilenKlasikSinavKayitRepository ?? new Repository<GirilenKlasikSinavKayit>(Context);
            }
        }


        public void SaveChanges()
        {
            Context.SaveChanges();
        }


    }
}
