using System;
using System.Collections.Generic;
using System.Linq;
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

        public Result SinavBaslangicBilgisiKayit(Guid sinavId, Guid ogrenciId)
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



        public Result KlasikSinavOgrenciSinaviKayit(KlasikSinavSinavKayitViewModel klasikSinavOgrenciCevaplari)
        {
            try
            {
                // süresi başlamış sınavlardan ulaşacağız.Süresi başlamamış sınav kayıt edilemez
                var suresiBaslamisSinav =
                    _unitOfWork.SuresiBaslamisSinavlarRepository.SingleOrDefault(x =>
                        x.OgrenciId == klasikSinavOgrenciCevaplari.OgrenciId && x.SinavId == klasikSinavOgrenciCevaplari.SinavId);
                if (suresiBaslamisSinav == null)
                    throw new NullReferenceException("Sınav soruları kayıt edilmek istenen sonav bulunamadı!");

                // Soru cevaplarını tabloya ekliyoruz
                List<KlasikSinavSinavSoruCevap> klasikSinavSinavSoruCevapList = new List<KlasikSinavSinavSoruCevap>();
                foreach (var item in klasikSinavOgrenciCevaplari.SinavSoruCevaplari)
                {
                    klasikSinavSinavSoruCevapList.Add(new KlasikSinavSinavSoruCevap { SoruText = item.SoruText, SoruCevapText = item.SoruCevapText, KlasikSinavSinavSoruCevapId = Guid.NewGuid() });
                }

                suresiBaslamisSinav.GirilenKlasikSinavKayits = new List<GirilenKlasikSinavKayit>
                {
                    new GirilenKlasikSinavKayit
                    {
                        GirilenKlasikSinavKayitId = Guid.NewGuid(),
                        OgrenciSinavPuani = 0,
                        KlasikSinavSinavSoruCevaps = klasikSinavSinavSoruCevapList
                    }
                };

                suresiBaslamisSinav.OgrenciSinaviBitirmeZamani = DateTime.Now;

                _unitOfWork.SaveChanges();

                return new Result { isSuccess = true, Message = "Sınav başarılı bir şekilde kayıt edildi." };
            }
            catch (Exception e)
            {
                _logger.LogError("Klasik sinav kaydı başarısız. İstek sahibi -> " + klasikSinavOgrenciCevaplari.OgrenciId + " | Sinav -> " + klasikSinavOgrenciCevaplari.SinavId + " | Detay -> " + e);
                return new Result { isSuccess = false, Message = "Sınav kaydı başarısız!" };
            }
        }



        public Result TestSinavOgrenciSinaviKayit(TestSinavSinaviKayitEtViewModel testSinavSinaviKayitEtViewModel)
        {
            try
            {
                double ogrenciSinavPuani = 0;

                // sınav sonucunu kayıt edebilmek için tabloları birleştirdik
                var suresiBaslamisSinavlars =
                    _unitOfWork.SuresiBaslamisSinavlarRepository.IncludeMany(x => x.GirilenTestSinavSonuclaris).SingleOrDefault(x => x.OgrenciId == testSinavSinaviKayitEtViewModel.OgrenciId && x.SinavId == Guid.Parse(testSinavSinaviKayitEtViewModel.SinavId));

                if (suresiBaslamisSinavlars == null)
                    throw new NullReferenceException("İlgili sinav bulunamadı.");

                var ilgiliTestSinav =
                    _unitOfWork.TestSinavSorularRepository.IncludeMany(x => x.TestSinav, x => x.TestSinavSoruSiklari).Where(x => x.TestSinav.SinavId == Guid.Parse(testSinavSinaviKayitEtViewModel.SinavId)).ToList();

                double ogrenciSoruBasinaAlacagiPuan = 100 / ilgiliTestSinav.Count();
                foreach (var item in testSinavSinaviKayitEtViewModel.TestSinavCevaplariList)
                {
                    int dogruSoruCevabi = ilgiliTestSinav.FirstOrDefault(x => x.TestSinavSorularId == item.TestSinavSorularId).SoruCevabi;

                    // soru doğru cevaplanmıştır puan ver
                    if (dogruSoruCevabi == item.SoruCevapSikki)
                        ogrenciSinavPuani += ogrenciSoruBasinaAlacagiPuan;
                }

                // puanı bulunan öğrenci puanını kayıt et
                suresiBaslamisSinavlars.OgrenciSinaviBitirmeZamani = DateTime.Now;
                suresiBaslamisSinavlars.GirilenTestSinavSonuclaris = new GirilenTestSinavSonuclari { SinavPuani = ogrenciSinavPuani, GirilenTestSinavSonuclariId = Guid.NewGuid() };

                _unitOfWork.SaveChanges();
                return new Result { isSuccess = true, Message = "Sınav başarılı bir şekilde kayıt edildi." };
            }
            catch (Exception e)
            {
                _logger.LogError("Klasik sinav kaydı başarısız. İstek sahibi -> " + testSinavSinaviKayitEtViewModel.OgrenciId + " | Sinav -> " + testSinavSinaviKayitEtViewModel.SinavId + " | Detay -> " + e);
                return new Result { isSuccess = false, Message = "Sınav kaydı başarısız!" };
            }
        }



    }
}
