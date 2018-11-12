using System;
using System.Collections.Generic;
using System.Text;
using DAL.UnitOfWork;
using EntityLayer;
using EntityLayer.Sinav;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Sinav
{
    public class SinavOlustur : ISinavOlustur
    {
        private readonly ILogger<SinavOlustur> _logger;
        private IUnitOfWork UnitOfWork { get; }

        public SinavOlustur(IUnitOfWork unitOfWork, ILogger<SinavOlustur> logger)
        {
            this.UnitOfWork = unitOfWork;
            this._logger = logger;
        }



        public Result KlasikSinavOlustur(KlasikSinavSorulari klasikSinavSorulari, Guid sinavSahibiIdBilgisi)
        {
            try
            {
                if (klasikSinavSorulari == null || sinavSahibiIdBilgisi.ToString() == null)
                    throw new ArgumentNullException("Gelen değerlerden bir tanesi null");

                // klasik soruların yer aldığı soru metinleri tablosu soruların eklenmesi
                var klasikSinavSorularList = new List<KlasikSinavSorular>();
                foreach (var item in klasikSinavSorulari.Sorular)
                {
                    klasikSinavSorularList.Add(new KlasikSinavSorular { KlasikSinavSorularId = Guid.NewGuid(), SoruMetni = item });
                }

                // klasik sinav tablosu verilerin eklenmesi
                var klasikSinavSorulariTablosu = new KlasikSinav { KlasikSinavId = Guid.NewGuid(), KlasikSinavSorulars = klasikSinavSorularList };

                // sinav tablosu
                var sinavBilgileri = new EntityLayer.Sinav.Sinav { DersAdi = klasikSinavSorulari.DersAdi, SinavTuru = SinavTuru.Klasik, DersKodu = klasikSinavSorulari.DersKodu, SinavId = Guid.NewGuid(), SinavSahibi = sinavSahibiIdBilgisi, KlasikSinav = klasikSinavSorulariTablosu, SinavEklenmeTarihi = DateTime.Now };


                // Sinav Bilgileri kayıt edildi
                UnitOfWork.SinavRepository.Add(sinavBilgileri);

                UnitOfWork.SaveChanges();

                _logger.LogInformation("Yeni sınav oluşturuldu, İşlem sahibi -> " + sinavSahibiIdBilgisi);
                return new Result { IsSuccessful = true, Message = "Sınav başarılı bir şekilde kayıt edildi." };
            }
            catch (Exception e)
            {
                _logger.LogError("Sinav kayıt başarısız -> " + e.ToString() + " | İşlem sahibi -> " + sinavSahibiIdBilgisi);
                return new Result { IsSuccessful = false, Message = "Sınav kayıt işlemi başarısız." };
            }
        }



        public Result TestSinavOlustur(TestSinavSorulari testSinavSorulari, Guid sinavSahibiIdBilgisi)
        {
            try
            {
                if (testSinavSorulari == null || sinavSahibiIdBilgisi.ToString() == null)
                    throw new ArgumentNullException("Gelen değerlerden bir tanesi null");

                var sinavSorularTablosuTumSorularList = new List<TestSinavSorular>();


                // Kaç soru olduğu testSinavSorulari.SoruTemplate içerisinde.
                // Buradaki sorular kadar dönüp db kayıt edeceğiz.
                for (int m = 0; m < testSinavSorulari.SoruTemplate.Count; m++)
                {
                    // . soru için şıklar
                    // Tüm soru şıklarını soruşıkları tablosuna eklemek için list haline getiriyoruz
                    var testSinavSorulariSiklariList = new List<TestSinavSoruSiklari>();
                    for (int i = 0; i < testSinavSorulari.SoruTemplate[m].SoruSiklari.Count; i++)
                    {
                        testSinavSorulariSiklariList.Add(new TestSinavSoruSiklari { SoruSikki = (i + 1), SoruSikMetni = testSinavSorulari.SoruTemplate[m].SoruSiklari[i], TestSinavSoruSiklariId = Guid.NewGuid() });
                    }

                    var testSinavSorularTablosu = new TestSinavSorular { TestSinavSoruSiklari = testSinavSorulariSiklariList, TestSinavSoruSirasi = 0, TestSinavSorusuMetni = testSinavSorulari.SoruTemplate[m].SoruText, TestSinavSorularId = Guid.NewGuid() };


                    sinavSorularTablosuTumSorularList.Add(testSinavSorularTablosu);
                }

                // Test sınav tablosu verileri
                var testSinavTestSinavTablosu = new TestSinav { TestSinavId = Guid.NewGuid(), TestSinavSorulars = sinavSorularTablosuTumSorularList };

                // Sinav tablosu verileri
                var sinavTablosu = new EntityLayer.Sinav.Sinav
                {
                    DersAdi = testSinavSorulari.DersAdi,
                    DersKodu = testSinavSorulari.DersKodu,
                    SinavId = Guid.NewGuid(),
                    SinavSahibi = sinavSahibiIdBilgisi,
                    SinavTuru = SinavTuru.Test,
                    SinavEklenmeTarihi = DateTime.Now,
                    TestSinav = testSinavTestSinavTablosu
                };

                UnitOfWork.SinavRepository.Add(sinavTablosu);
                UnitOfWork.SaveChanges();

                _logger.LogInformation("Test sınavı başarılı bir şekilde kayıt edildi. İşlem sahibi -> " + sinavSahibiIdBilgisi);
                return new Result { IsSuccessful = true, Message = "Test sınavı başarılı bir şekilde kayıt edildi." };
            }
            catch (Exception e)
            {
                _logger.LogError("Test sınavı kayıt işlemi başarısız. Detaylar -> " + e + " | İşlem sahibi -> " + sinavSahibiIdBilgisi);
                return new Result { IsSuccessful = false, Message = "Sınav kayıt işlemi başarısız." };
            }
        }



    }
}
