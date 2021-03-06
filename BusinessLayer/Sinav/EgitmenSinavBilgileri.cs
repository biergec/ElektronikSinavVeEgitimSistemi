﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.UnitOfWork;
using EntityLayer;
using EntityLayer.Sinav;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Sinav
{
    public class EgitmenSinavBilgileri : IEgitmenSinavBilgileri
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EgitmenSinavBilgileri> _logger;

        public EgitmenSinavBilgileri(IUnitOfWork unitOfWork, ILogger<EgitmenSinavBilgileri> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }



        public Result OlusturdugumSinavlar(Guid sinavSahibiGuidId)
        {
            try
            {
                var sinavlar = _unitOfWork.SinavRepository.IncludeMany(x => x.Dersler).Where(x => x.SinavSahibi == sinavSahibiGuidId).OrderBy(x => x.SinavEklenmeTarihi);

                return new Result { isSuccess = true, Data = sinavlar };
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
                return new Result { isSuccess = false, Message = "İşlem gerçekleştirilemedi. Lütfen daha sonra tekrar deneyiniz." };
            }
        }



        // sınav aktiflik durumunu tersine çevirir
        public Result SinavAktiflikDurumuDegistir(Guid sinavId)
        {
            try
            {
                var aktiflikDurumuDegistilecekSinav = _unitOfWork.SinavRepository.SingleOrDefault(x=>x.SinavId == sinavId);
                if (aktiflikDurumuDegistilecekSinav == null)
                    throw new ArgumentNullException("Sinav id ile ilişkili veri bulunamadı.");

                var sinavAktiflikDurumu = aktiflikDurumuDegistilecekSinav.SinavAktiflikDurumu;

                if (sinavAktiflikDurumu)
                {
                    aktiflikDurumuDegistilecekSinav.SinavAktiflikDurumu = false;
                    _unitOfWork.SaveChanges();

                    return new Result { isSuccess = true, Message = "Sınav başarılı bir şekilde pasif hale getirildi." };
                }else
                {
                    aktiflikDurumuDegistilecekSinav.SinavAktiflikDurumu = true;
                    _unitOfWork.SaveChanges();

                    return new Result { isSuccess = true, Message = "Sınav başarılı bir şekilde aktif hale getirildi." };
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Sinav aktiflik durumu değiştirme işlemi başarısız. Detaylar -> " + e + " | Sınav Id -> " + sinavId);
                return new Result { isSuccess = false, Message = "İşlem gerçekleştirilemedi. Lütfen daha sonra tekrar deneyiniz." };
            }
        }



        public Result SinavSoruBilgileri(Guid sinavId)
        {
            try
            {
                // sınavın türünü bul sonra türüne göre 2 tabloyu birleştirip geri döndür
                var sinavTuru = _unitOfWork.SinavRepository.SingleOrDefault(x => x.SinavId == sinavId);
                if (sinavTuru == null)
                    return new Result { isSuccess = false, Message = "Böyle bir sınav bulunamadı!" };

                var sinavSoruBilgileri = new List<SinavSorulariGoruntuleme>();
                sinavSoruBilgileri.Add(new SinavSorulariGoruntuleme());

                // Ders adını ayrı sorgu olarak buluyoruz
                sinavSoruBilgileri[0].DersAdi = _unitOfWork.DerslerRepository.SingleOrDefault(x => x.DerslerId == sinavTuru.DerslerId).DersAdi;

                // Sınav türüne göre soruları çekiyoruz
                switch (sinavTuru.SinavTuru)
                {
                    case SinavTuru.Test:
                        sinavSoruBilgileri[0].TestSinavSorular =
                            _unitOfWork.TestSinavSorularRepository.IncludeMany(x => x.TestSinav,
                                c => c.TestSinavSoruSiklari).ToList();

                        sinavSoruBilgileri[0].TestSinavSorular =
                            sinavSoruBilgileri[0].TestSinavSorular.Where(x => x.TestSinav.SinavId == sinavId).ToList();
                        break;
                    case SinavTuru.Klasik:
                        sinavSoruBilgileri[0].KlasikSinav = _unitOfWork.KlasikSinavRepository.IncludeMany(x => x.Sinavs, sinav => sinav.KlasikSinavSorulars).ToList();

                        sinavSoruBilgileri[0].KlasikSinav =
                            sinavSoruBilgileri[0].KlasikSinav.Where(x => x.SinavId == sinavId).ToList();
                        break;
                    default:
                        throw new ArgumentException("Test türü hatalı");
                }

                return new Result { isSuccess = true, Data = sinavSoruBilgileri };
            }
            catch (Exception e)
            {
                _logger.LogError("Sinav bilgileri çekilemedi. Detaylar -> " + e);
                return new Result { isSuccess = false, Message = "İşlem gerçekleştirilemedi." };
            }
        }


    }
}
