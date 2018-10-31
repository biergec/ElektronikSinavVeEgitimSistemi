using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Save();
    }
}
