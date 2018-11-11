using System;
using System.Collections.Generic;
using System.Text;
using DAL.Context;
using DAL.Repository;
using EntityLayer.Sinav;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DbContext = System.Data.Entity.DbContext;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private EfContext _context { get; }
        private IRepository<Sinav> _sinavRepository;
        private IRepository<TestSinav> _testSinavRepository;
        private IRepository<TestSinavSorular> _testSinavSorularRepository;
        private IRepository<KlasikSinav> _klasikSinavRepository;
        private IRepository<KlasikSinavSorular> _klasikSinavSorularRepository;

        public UnitOfWork(EfContext context)
        {
            this._context = context;
        }

        public IRepository<Sinav> SinavRepository
        {
            get
            {
                return _sinavRepository = _sinavRepository ?? new Repository<Sinav>(_context);
            }
        }

        public IRepository<TestSinav> TestSinavRepository
        {
            get
            {
                return _testSinavRepository = _testSinavRepository ?? new Repository<TestSinav>(_context);
            }
        }

        public IRepository<TestSinavSorular> TestSinavSorularRepository
        {
            get
            {
                return _testSinavSorularRepository = _testSinavSorularRepository ?? new Repository<TestSinavSorular>(_context);
            }
        }

        public IRepository<KlasikSinav> KlasikSinavRepository
        {
            get
            {
                return _klasikSinavRepository = _klasikSinavRepository ?? new Repository<KlasikSinav>(_context);
            }
        }

        public IRepository<KlasikSinavSorular> KlasikSinavSorularRepository
        {
            get
            {
                return _klasikSinavSorularRepository = _klasikSinavSorularRepository ?? new Repository<KlasikSinavSorular>(_context);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
