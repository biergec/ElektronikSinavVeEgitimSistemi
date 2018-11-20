using System;
using System.Collections.Generic;
using System.Text;
using DAL.UnitOfWork;
using EntityLayer;
using EntityLayer.BaslayanSinavlar;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.SinavGiris
{
    public class SinavKayit : ISinavKayit
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SinavKayit> _logger;

        public SinavKayit(IUnitOfWork unitOfWork, ILogger<SinavKayit> logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
        }

        public Result SinavBaslangicKayit(Guid sinavId, Guid ogrenciId)
        {
            try
            {
                // daha önce kayıt edilmiş mi bak ?
                var dahaOnceKayitKontrol =
                    _unitOfWork.SuresiBaslamisSinavlarRepository.SingleOrDefault(x =>
                        x.OgrenciId == ogrenciId && x.SinavId == sinavId);

                if (dahaOnceKayitKontrol != null)
                {
                    // sınav daha önce kayıt edildiyse devam et
                    return new Result { isSuccess = true };
                }

                _unitOfWork.SuresiBaslamisSinavlarRepository.Add(new SuresiBaslamisSinavlar { OgrenciId = ogrenciId, OgrenciSinavaBaslamaZamani = DateTime.Now, SinavId = sinavId, SuresiBaslamisSinavlarId = Guid.NewGuid() });
                _unitOfWork.SaveChanges();

                return new Result { isSuccess = true };
            }
            catch (Exception e)
            {
                _logger.LogError("Sinav başlatılamadı. İstek sahibi -> " + ogrenciId + " | Detay -> " + e);
                return new Result { isSuccess = false };
            }
        }
    }
}
