using System;
using System.Collections.Generic;
using System.Text;
using DAL.Repository;
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

        void Save();
    }
}
