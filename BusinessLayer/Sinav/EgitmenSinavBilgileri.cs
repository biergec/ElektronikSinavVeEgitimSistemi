using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.UnitOfWork;
using EntityLayer;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Sinav
{
    public class EgitmenSinavBilgileri : IEgitmenSinavBilgileri
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EgitmenSinavBilgileri> _logger;

        public EgitmenSinavBilgileri(IUnitOfWork unitOfWork, ILogger<EgitmenSinavBilgileri> logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
        }



        public Result OlusturdugumSinavlar(Guid sinavSahibiGuidId)
        {
            try
            {
                var sinavBilgileri = _unitOfWork.SinavRepository.GetAll().OrderBy(x => x.SinavEklenmeTarihi);

                return new Result { isSuccess = true, Data = sinavBilgileri };
            }
            catch (Exception e)
            {
                _logger.LogError("Oluşturulan sınavlar getitirilemedi. Detay -> " + e + " | İşlem sahibi -> " + sinavSahibiGuidId);
                return new Result { isSuccess = false };
            }
        }



        public Result OlusturulanSinaviSil(Guid sinavId, Guid sinavSahibiGuidId)
        {
            try
            {
                _unitOfWork.SinavRepository.Remove(new EntityLayer.Sinav.Sinav { SinavId = sinavId });
                _unitOfWork.SaveChanges();

                return new Result { isSuccess = true, Message = "Sınav başarılı bir şekilde silindi" };
            }
            catch (Exception e)
            {
                _logger.LogError("Sinav silme işlemi başarısız. Detaylar -> " + e + " | İşlem Sahibi -> " + sinavSahibiGuidId);
                return new Result { isSuccess = false, Message = "İşlem gerçekleştirilemedi. Lütfen daha sonra tekrar deneyiniz."};
            }
        }


    }
}
