using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.UnitOfWork;
using EntityLayer;
using EntityLayer.CanliYayin;
using EntityLayer.Login;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.CanliYayin
{
    public class CanliYayinIslemleri : ICanliYayinIslemleri
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CanliYayinIslemleri> _logger;
        private readonly Microsoft.AspNetCore.Identity.UserManager<AppUser> _userManager;

        public CanliYayinIslemleri(IUnitOfWork unitOfWork, ILogger<CanliYayinIslemleri> logger, Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userManager = userManager;
        }


        public Result CanliYayinBaslat(Guid yayinId)
        {
            try
            {
                var canliYayin = _unitOfWork.CanliYayinRepository.Get(x => x.CanliYayinId == yayinId).First();

                canliYayin.CanliYayinAktifMi = true;
                canliYayin.CanliYayinBaslamaZamani = DateTime.Now;

                _unitOfWork.SaveChanges();

                return new Result { isSuccess = true, Message = "Canli Yayın Sonlandırma Başarılı" };
            }
            catch (Exception e)
            {
                _logger.LogError("Canlı yayın başlatma hatası - > " + e);
                return new Result { isSuccess = false, Message = "Canli Yayın Başlatma Hatası" };
            }

        }


        public Result CanliYayinBitir(Guid yayinId)
        {
            try
            {
                var canliYayin = _unitOfWork.CanliYayinRepository.Get(x => x.CanliYayinId == yayinId).First();

                canliYayin.CanliYayinAktifMi = false;
                canliYayin.CanliYayinBitisZamani = DateTime.Now;

                _unitOfWork.SaveChanges();

                return new Result { isSuccess = true, Message = "Canli Yayın Sonlandırma Başarılı" };
            }
            catch (Exception e)
            {
                _logger.LogError("Canlı yayın sonlandırma hatası - > " + e);
                return new Result { isSuccess = false, Message = "Canli Yayın Sonlandırma Hatası" };
            }
        }

        public CanliYayinDetaylari CanliYayinDetaylari(Guid yayinId)
        {
            try
            {
                var seciliCanliYayin = _unitOfWork.CanliYayinRepository.SingleOrDefault(x => x.CanliYayinId == yayinId);

                var dersAdi =
                    _unitOfWork.DerslerRepository.SingleOrDefault(x =>
                        x.DerslerId == seciliCanliYayin.CanliYayinDersId).DersAdi;

                return new CanliYayinDetaylari
                {
                    CanliYayinAktifMi = seciliCanliYayin.CanliYayinAktifMi, CanliYayinId = seciliCanliYayin.CanliYayinId, CanliYayinDersId = seciliCanliYayin.CanliYayinDersId, CanliYayinYayinId = seciliCanliYayin.CanliYayinYayinId, CanliYayinBaslamaZamani = seciliCanliYayin.CanliYayinBaslamaZamani, CanliYayinBitisZamani = seciliCanliYayin.CanliYayinBitisZamani, CanliYayinDersAdi = dersAdi, 
                };
            }
            catch (Exception e)
            {
                _logger.LogError("Canlı yayın oluşturma hatası - > " + e);
                return null;
            }
        }

        public Result CanliYayinDosyaEkle()
        {
            throw new NotImplementedException();
        }

        public bool CanliYayiniSil(Guid yayinId)
        {
            try
            {
                var seciliCanliYayin = _unitOfWork.CanliYayinRepository.SingleOrDefault(x => x.CanliYayinId == yayinId);

                _unitOfWork.CanliYayinRepository.Remove(seciliCanliYayin);

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Canlı yayın oluşturma hatası - > " + e);
                return false;
            }
        }

        public Result CanliYayinKayit(CanliYayinOlusturViewModel canliYayinOlusturViewModel, Guid canliYayinKayitEdenKisi)
        {
            try
            {
                var canliYayinId = Guid.NewGuid();
                _unitOfWork.CanliYayinRepository.Add(new EntityLayer.CanliYayin.CanliYayin
                {
                    CanliYayinAktifMi = false,
                    CanliYayinDersId = Guid.Parse(canliYayinOlusturViewModel.DersId),
                    CanliYayinId = canliYayinId,
                    CanliYayinSahibi = canliYayinKayitEdenKisi,
                    CanliYayinYayinId = canliYayinOlusturViewModel.YayinKodu
                });

                _unitOfWork.SaveChanges();

                return new Result { isSuccess = true, Message = "Canli Yayın Oluşturma Başarılı", Data = canliYayinId };
            }
            catch (Exception e)
            {
                _logger.LogError("Canlı yayın oluşturma hatası - > " + e);
                return new Result { isSuccess = false, Message = "Canli Yayın Oluşturma Hatası" };
            }
        }



        public List<CanliYayinYoklama> CanliYayinYoklamaListesi(Guid canliYayinId)
        {
            try
            {
                var canliYayinYoklamaListesi = new List<CanliYayinYoklama>();

                var canliYayinaKatilanlarListesi = _unitOfWork.CanliYayinaKatilanlarRepository.Get(x => x.CanliYayinId == canliYayinId).Distinct();

                var ogrenciListesi = _userManager.Users.ToList();

                foreach (var item in canliYayinaKatilanlarListesi)
                {
                    string ogrenciAdSoyad = ogrenciListesi.FirstOrDefault(x => x.Id == item.CanliYayinaKatilanKisiId.ToString()).Ad + " " + ogrenciListesi.FirstOrDefault(x => x.Id == item.CanliYayinaKatilanKisiId.ToString()).Soyad;

                    string ogrenciNo =
                        ogrenciListesi.FirstOrDefault(x => x.Id == item.CanliYayinaKatilanKisiId.ToString()).KurumOgrenciNumarasi.ToString();

                    canliYayinYoklamaListesi.Add(new CanliYayinYoklama { CanliYayinId = canliYayinId, OgrenciId = item.CanliYayinaKatilanKisiId, OgrenciAdSoyad = ogrenciAdSoyad, OgrenciNumarasi = ogrenciNo });
                }

                return canliYayinYoklamaListesi;
            }
            catch (Exception e)
            {
                _logger.LogError("Canlı yayın oluşturma hatası - > " + e);
                return null;
            }
        }



        public List<CanliYayinDetaylari> OlusturdugumCanliYayinlar(Guid userId)
        {
            try
            {
                var olusturdugumCanliYayinList = new List<CanliYayinDetaylari>();

                var olusturdugumCanliYayinlar = _unitOfWork.CanliYayinRepository.Get(x => x.CanliYayinSahibi == userId);

                var dersler =
                    _unitOfWork.DerslerRepository.GetAll();

                foreach (var item in olusturdugumCanliYayinlar)
                {
                    olusturdugumCanliYayinList.Add(new CanliYayinDetaylari { CanliYayinAktifMi = item.CanliYayinAktifMi, CanliYayinId = item.CanliYayinId, CanliYayinDersId = item.CanliYayinDersId, CanliYayinYayinId = item.CanliYayinYayinId, CanliYayinBaslamaZamani = item.CanliYayinBaslamaZamani, CanliYayinBitisZamani = item.CanliYayinBitisZamani, CanliYayinDersAdi = dersler.FirstOrDefault(x => x.DerslerId == item.CanliYayinDersId).DersAdi });
                }

                return olusturdugumCanliYayinList;
            }
            catch (Exception e)
            {
                _logger.LogError("Canlı yayın oluşturma hatası - > " + e);
                return null;
            }
        }


    }
}
