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
                    CanliYayinAktifMi = seciliCanliYayin.CanliYayinAktifMi,
                    CanliYayinId = seciliCanliYayin.CanliYayinId,
                    CanliYayinDersId = seciliCanliYayin.CanliYayinDersId,
                    CanliYayinYayinId = seciliCanliYayin.CanliYayinYayinId,
                    CanliYayinBaslamaZamani = seciliCanliYayin.CanliYayinBaslamaZamani,
                    CanliYayinBitisZamani = seciliCanliYayin.CanliYayinBitisZamani,
                    CanliYayinDersAdi = dersAdi,
                };
            }
            catch (Exception e)
            {
                _logger.LogError("Canlı yayın oluşturma hatası - > " + e);
                return null;
            }
        }


        public List<CanliYayinDokumanlari> CanliYayinDokumanlariListele(Guid canliYayinId)
        {
            try
            {
                return _unitOfWork.CanliYayinDokumanlariRepository.Get(x => x.CanliYayinId == canliYayinId).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError("Canlı yayın oluşturma hatası - > " + e);
                return null;
            }
        }


        public bool CanliYayinDokumanSil(Guid dokumanlarIdGuid)
        {
            try
            {
                var seciliDokuman =
                    _unitOfWork.CanliYayinDokumanlariRepository.SingleOrDefault(x =>
                        x.CanliYayinDokumanlariId == dokumanlarIdGuid);

                _unitOfWork.CanliYayinDokumanlariRepository.Remove(seciliDokuman);
                _unitOfWork.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Canlı yayın dokuman silme hatası - > " + e);
                return false;
            }
        }


        public bool CanliYayiniSil(Guid yayinId)
        {
            try
            {
                var seciliCanliYayin = _unitOfWork.CanliYayinRepository.SingleOrDefault(x => x.CanliYayinId == yayinId);

                _unitOfWork.CanliYayinRepository.Remove(seciliCanliYayin);
                _unitOfWork.SaveChanges();

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


        public bool CanliYayinUploadDosya(List<UploadFileInfo> dosyaFileInfos, Guid canliYayinId)
        {
            try
            {
                var canliYayinDokumanlari = new List<CanliYayinDokumanlari>();

                foreach (var uploadFileInfo in dosyaFileInfos)
                {
                    canliYayinDokumanlari.Add(new CanliYayinDokumanlari { CanliYayinDokumanlariId = Guid.NewGuid(), CanliYayinId = canliYayinId, DokumanAdi = uploadFileInfo.DosyaAdi, DokumanEklenmeTarihi = DateTime.Now, DokumanKayitPath = uploadFileInfo.DosyaYolu });
                }

                _unitOfWork.CanliYayinDokumanlariRepository.AddRange(canliYayinDokumanlari);
                _unitOfWork.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Canlı yayın dosya yükleme hatası - > " + e);
                return false;
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


        public bool OgrenciCanliYayinKatilimGerceklestir(Guid ogrenciId, Guid canliYayinId)
        {
            try
            {
                // daha önce katıldıysa tekrar kayıt etme
                var dahaOnceKatilimSorgula = _unitOfWork.CanliYayinaKatilanlarRepository.SingleOrDefault(x =>
                    x.CanliYayinaKatilanKisiId == ogrenciId && x.CanliYayinId == canliYayinId);
                if (dahaOnceKatilimSorgula == null)
                {
                    _unitOfWork.CanliYayinaKatilanlarRepository.Add(new CanliYayinaKatilanlar { CanliYayinaKatilanKisiId = ogrenciId, CanliYayinId = canliYayinId, CanliYayinKatilmaZamani = DateTime.Now });

                    _unitOfWork.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Canlı yayın öğrenci kayıt etme hatası - > " + e);
                return false;
            }
        }

        public List<CanliYayinDetaylari> OgrenciKatilabilecegiCanliYayinlar(Guid ogrencGuid)
        {
            try
            {
                var katilabilecegiCanliYayinlar = new List<CanliYayinDetaylari>();

                // Öğrencinin derslerini bul, dersi ile ilgili canlı yayın var mı bak varsa ekle
                var ogreciDersleri = _unitOfWork.KayitliDerslerimRepository.Get(x => x.OgrenciId == ogrencGuid);

                // Kayıtlı dersin canlı yayını varmı bul!
                var canliYayinlar = _unitOfWork.CanliYayinRepository.GetAll();

                // Katılabileceği bir şey olmayabilir
                if (canliYayinlar == null || ogreciDersleri == null)
                    return null;

                var dersler =
                    _unitOfWork.DerslerRepository.GetAll();

                foreach (var item in ogreciDersleri)
                {
                    var canliYayin = canliYayinlar.First(x => x.CanliYayinDersId == item.DerslerId && x.CanliYayinAktifMi == true);
                    if (canliYayin != null)
                    {
                        var dersAdi = dersler.FirstOrDefault(x => x.DerslerId == item.DerslerId).DersAdi;
                        katilabilecegiCanliYayinlar.Add(new CanliYayinDetaylari
                        {
                            CanliYayinAktifMi = canliYayin.CanliYayinAktifMi,
                            CanliYayinBaslamaZamani = canliYayin.CanliYayinBaslamaZamani,
                            CanliYayinDersId = canliYayin.CanliYayinDersId,
                            CanliYayinId = canliYayin.CanliYayinId,
                            CanliYayinYayinId = canliYayin.CanliYayinYayinId,
                            CanliYayinBitisZamani = canliYayin.CanliYayinBitisZamani,
                            CanliYayinDersAdi = dersAdi
                        });
                    }
                }

                return katilabilecegiCanliYayinlar;
            }
            catch (Exception e)
            {
                _logger.LogError("Öğrenci katıldığı dersler canlı listesi getirme hatası - > " + e);
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
