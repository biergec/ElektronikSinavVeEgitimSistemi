using System;
using System.Collections.Generic;
using System.Text;
using DAL.UnitOfWork;
using EntityLayer.Sinav;

namespace BusinessLayer.Sinav
{
    public class SinavBilgileri : ISinavBilgileri
    {
        private readonly IUnitOfWork _unitOfWork;

        public SinavBilgileri(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        EntityLayer.Sinav.Sinav ISinavBilgileri.SinavBilgileri(Guid sinavId)
        {
            return _unitOfWork.SinavRepository.SingleOrDefault(x => x.SinavId == sinavId);
        }
    }
}
