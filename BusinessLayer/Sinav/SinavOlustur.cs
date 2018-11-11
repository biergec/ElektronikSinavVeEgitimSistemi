using System;
using System.Collections.Generic;
using System.Text;
using DAL.UnitOfWork;
using EntityLayer;
using EntityLayer.Sinav;
using Microsoft.AspNet.Identity;

namespace BusinessLayer.Sinav
{
    public class SinavOlustur : ISinavOlustur
    {
        private IUnitOfWork _unitOfWork { get; }


        public SinavOlustur(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        public Result KlasikSinavOlustur(KlasikSinavSorulari klasikSinavSorulari, Guid sinavSahibiIdBilgisi)
        {
            try
            {
                // kalsik sorualrın yer aldığı soru metinleri tablosı
                var klasikSinavSorularList = new List<KlasikSinavSorular>();
                foreach (var item in klasikSinavSorulari.Sorular)
                {
                    klasikSinavSorularList.Add(new KlasikSinavSorular { KlasikSinavSorularId = Guid.NewGuid(), SoruMetni = item });
                }

                // klasik sinav tablosu içeriği
                var klasikSinavSorulariTablosu = new KlasikSinav { KlasikSinavId = Guid.NewGuid(), KlasikSinavSorulars = klasikSinavSorularList };


                // sinav tablosu
                var sinavBilgileri = new EntityLayer.Sinav.Sinav { DersAdi = klasikSinavSorulari.DersAdi, SinavTuru = SinavTuru.Klasik, DersKodu = klasikSinavSorulari.DersKodu, SinavId = Guid.NewGuid(), SinavSahibi = sinavSahibiIdBilgisi, KlasikSinav = klasikSinavSorulariTablosu, SinavEklenmeTarihi = DateTime.Now };


                // Sinav Bilgileri kayıt edildi
                _unitOfWork.SinavRepository.Add(sinavBilgileri);

                _unitOfWork.Save();

                return null;
            }
            catch (Exception e)
            {

            }

            return null;
        }



        public Result TestSinavOlustur(TestSinavSorulari testSinavSorulari)
        {
            throw new NotImplementedException();
        }



    }
}
