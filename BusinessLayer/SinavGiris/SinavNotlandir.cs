using System;
using System.Collections.Generic;
using System.Text;
using DAL.UnitOfWork;
using EntityLayer;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.SinavGiris
{
    public class SinavNotlandir : ISinavNotlandir
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SinavNotlandir> _logger;

        public SinavNotlandir(IUnitOfWork unitOfWork, ILogger<SinavNotlandir> logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
        }

        public Result KlasikSinavNotlandir(Guid sinavId, Guid ogrenciId, double sinavPuani)
        {
            try
            {
                var notlandirilacakSinavSuresiBaslamisSinavlar =
                    _unitOfWork.SuresiBaslamisSinavlarRepository.SingleOrDefault(x =>
                        x.OgrenciId == ogrenciId && x.SinavId == sinavId);

                var sinav = _unitOfWork.GirilenKlasikSinavKayitRepository.SingleOrDefault(x=>x.SuresiBaslamisSinavlarId == notlandirilacakSinavSuresiBaslamisSinavlar.SuresiBaslamisSinavlarId);

                sinav.OgrenciSinavPuani = (decimal) sinavPuani;

                _unitOfWork.SaveChanges();

                return new Result { isSuccess = true, Message = "Sınav notlandırma başarılı" };
            }
            catch (Exception e)
            {
                _logger.LogError("Klasik sinav notlandırma başarısız. İstek sahibi -> " + ogrenciId + " | Detay -> " + e);
                return new Result { isSuccess = false };
            }
        }

        public Result TestSinavNotlandir(Guid sinavId, Guid ogrenciId, decimal sinavPuani)
        {
            try
            {
                var notlandirilacakSinav =
                    _unitOfWork.SuresiBaslamisSinavlarRepository.SingleOrDefault(x =>
                        x.OgrenciId == ogrenciId && x.SinavId == sinavId);

               

                return new Result { isSuccess = true, Message = "Sınav notlandırma başarılı" };
            }
            catch (Exception e)
            {
                _logger.LogError("Test sinav notlandırma başarısız. İstek sahibi -> " + ogrenciId + " | Detay -> " + e);
                return new Result { isSuccess = false };
            }
        }

    }
}
