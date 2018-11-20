using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.UnitOfWork;
using EntityLayer;
using EntityLayer.Ders;
using EntityLayer.Sinav;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.DersIslemleri
{
    public class DersIslemleri : IDersIslemleri
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DersIslemleri> _logger;

        public DersIslemleri(IUnitOfWork unitOfWork, ILogger<DersIslemleri> logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
        }

        public Result DersEkle(DersEkleViewModel dersEkleViewModel)
        {
            try
            {
                var dersEklenmeKontrolDersAdi = _unitOfWork.DerslerRepository.Get(x =>
                    x.DersAdi.ToLower().Trim() == dersEkleViewModel.DersAdi.ToLower().Trim());
                var dersEklemeKontrolDersKodu = _unitOfWork.DerslerRepository.Get(x=>x.DersKodu == dersEkleViewModel.DersKodu);
                if (dersEklenmeKontrolDersAdi.Count() != 0 || dersEklemeKontrolDersKodu.Count() != 0)
                {
                    return new Result { isSuccess = false, Message = "Eklemek istediğiniz ders daha önce eklenmiştir!" };
                }


                _unitOfWork.DerslerRepository.Add(new Dersler { DersAdi = dersEkleViewModel.DersAdi, DersEklenmeTarihi = DateTime.Now, DersKodu = dersEkleViewModel.DersKodu, DerslerId = Guid.NewGuid(), DersKayitAnahtari = dersEkleViewModel.DersKayitAnahtari });
                _unitOfWork.SaveChanges();

                return new Result { isSuccess = true, Message = "Ders ekleme işlemi başarılı." };
            }
            catch (Exception e)
            {
                _logger.LogError("Ders ekleme hatası - > " + e);
                return new Result { isSuccess = false, Message = "Ders ekleme hatası. Lütfen daha sonra tekrar deneyiniz." };
            }
        }

        public Result DersSil(Guid dersId)
        {
            try
            {
                _unitOfWork.DerslerRepository.Remove(new Dersler { DerslerId = dersId });
                _unitOfWork.SaveChanges();

                return new Result { isSuccess = true, Message = "Ders silme işlemi başarılı." };
            }
            catch (Exception e)
            {
                _logger.LogError("Ders silme işlemi gerçekleştirilemedi - > " + e);
                return new Result { isSuccess = false, Message = "Ders silme işlemi gerçekleştirilemedi. Lütfen daha sonra tekrar deneyiniz." };
            }
        }

        public Result GetAllDersler()
        {
            try
            {
                var dersList = _unitOfWork.DerslerRepository.GetAll().Select(x => new DersViewModel { DersAdi = x.DersAdi, DersEklenmeTarihi = x.DersEklenmeTarihi, DersKodu = x.DersKodu, DerslerId = x.DerslerId, DersKayitAnahtari = x.DersKayitAnahtari }).ToList();

                return new Result { isSuccess = true, Data = dersList };
            }
            catch (Exception e)
            {
                _logger.LogError("Ders listesi güncelleme hatası - > " + e);
                return new Result { isSuccess = false, Message = "Ders listesi güncellemedi!" };
            }
        }



        public string GetDersAdi(Guid dersId)
        {
            return _unitOfWork.DerslerRepository.SingleOrDefault(x=>x.DerslerId == dersId).DersAdi;
        }


        public List<DersViewModel> GetKayitOlmadigimDersler(Guid ogrenciId)
        {
            var dersList = _unitOfWork.KayitliDerslerimRepository.IncludeMany(x => x.Dersler)
                .Where(x => x.OgrenciId != ogrenciId).Select(x => new DersViewModel
                {
                    DersAdi = x.Dersler.DersAdi, DersEklenmeTarihi = x.Dersler.DersEklenmeTarihi,
                    DersKodu = x.Dersler.DersKodu, DerslerId = x.DerslerId,
                    DersKayitAnahtari = x.Dersler.DersKayitAnahtari
                }).ToList();

            return dersList;
        }


    }
}
