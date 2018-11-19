using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.UnitOfWork;
using EntityLayer;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.DersIslemleri
{
    public class KayitliDerslerimManger : IKayitliDerslerim
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<KayitliDerslerimManger> _logger;

        public KayitliDerslerimManger(IUnitOfWork unitOfWork, ILogger<KayitliDerslerimManger> logger)
        {
            _unitOfWork = unitOfWork;
            this._logger = logger;
        }

        public Result DersKayit(Guid dersId, string dersKayitAnahtari, Guid kullaniciIdGuid)
        {
            try
            {
                var dersSorugulama = _unitOfWork.DerslerRepository.SingleOrDefault(x =>
                    x.DersKayitAnahtari.Trim() == dersKayitAnahtari.Trim() && x.DerslerId == dersId);

                if (dersSorugulama == null)
                    throw new NullReferenceException("Hatalı ders kayit isteği. İşlem sahibi -> " + kullaniciIdGuid);



                var dersKayitSorgulama =
                    _unitOfWork.KayitliDerslerimRepository.SingleOrDefault(x =>
                        x.OgrenciId == kullaniciIdGuid && x.DerslerId == dersId);

                if (dersKayitSorgulama != null)
                    return new Result { isSuccess = false, Message = "Ders kaydınız daha önce yapılmıştır." };


                _unitOfWork.KayitliDerslerimRepository.Add(new EntityLayer.Ders.KayitliDerslerim { DersKayitTarihi = DateTime.Now, DerslerId = dersId, OgrenciId = kullaniciIdGuid, KayitliDerslerimId = Guid.NewGuid() });

                _unitOfWork.SaveChanges();

                return new Result { isSuccess = true, Message = "Ders kaydınız yapılmıştır." };
            }
            catch (Exception e)
            {
                _logger.LogError("Ders kaydı başarısız. -> " + e);
                return new Result { isSuccess = false, Message = "Ders kaydı başarısız. Lütfen ders anahtarını kontrol edin." };
            }
        }

        public Result GetKayitliDerslerim(Guid kullaniciGuid)
        {
            try
            {
                var kayitliDerslerim = _unitOfWork.KayitliDerslerimRepository.IncludeMany(x => x.Dersler)
                    .Where(x => x.OgrenciId == kullaniciGuid).OrderBy(x => x.Dersler.DersAdi).ToList();

                return new Result { Data = kayitliDerslerim, isSuccess = true };
            }
            catch (Exception e)
            {
                _logger.LogError("Kayitli ders listesi güncellenemedi. -> " + e);
                return new Result { isSuccess = false, Message = "Kayitli ders listesi güncellenemedi..." };
            }
        }

        public Result KayitliDersSil(Guid dersId, Guid kullaniciIdGuid)
        {
            try
            {
                var kayitliDersimId =
                    _unitOfWork.KayitliDerslerimRepository.SingleOrDefault(x =>
                        x.DerslerId == dersId && x.OgrenciId == kullaniciIdGuid);
                if (kayitliDersimId == null)
                    throw new NullReferenceException("Aranılan ders, kayıtlı derslerimde bulunamadı. İşlem sahibi -> " + kullaniciIdGuid);

                _unitOfWork.KayitliDerslerimRepository.Remove(kayitliDersimId);
                _unitOfWork.SaveChanges();

                return new Result { isSuccess = true, Message = "Kayitli ders başarılı bir şekilde silindi." };
            }
            catch (Exception e)
            {
                _logger.LogError("Kayitli ders silme hatası. -> " + e);
                return new Result { isSuccess = false, Message = "Kayitli ders silinemedi..." };
            }
        }
    }
}
