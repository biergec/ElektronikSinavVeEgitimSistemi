﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.UnitOfWork;
using EntityLayer.BaslayanSinavlar;
using EntityLayer.EgitmenSinavBilgileri;
using EntityLayer.Login;
using EntityLayer.Sinav;
using Microsoft.AspNetCore.Identity;

namespace BusinessLayer.Sinav
{
    public class SinavBilgileri : ISinavBilgileri
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public SinavBilgileri(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
        }



        public List<EntityLayer.Sinav.Sinav> EgitmenOlusturduguSinavlar(Guid egitmenId)
        {
            return _unitOfWork.SinavRepository.IncludeMany(x => x.Dersler).Where(x => x.SinavSahibi == egitmenId).ToList();
        }



        public List<GirilenKlasikSinavKayit> OgrenciKlasikSinavSorulari(Guid sinavId)
        {
            var sinavSorulariTablosu =
                _unitOfWork.GirilenKlasikSinavKayitRepository.IncludeMany(x => x.KlasikSinavSinavSoruCevaps,
                    c => c.SuresiBaslamisSinavlar).Where(x => x.SuresiBaslamisSinavlar.SinavId == sinavId).ToList();

            return sinavSorulariTablosu;
        }



        public List<SinavaGirenKisiler> SinavaGirenKisiBilgileri(Guid sinavId)
        {
            var tumKullanicilar = _userManager.Users;
            var sinavaGirenKisilerIdList = _unitOfWork.SuresiBaslamisSinavlarRepository.IncludeMany(x => x.Sinav, x => x.GirilenKlasikSinavKayits).Where(x=>x.SinavId == sinavId);
            List<SinavaGirenKisiler> sinavaGirenKisiler = new List<SinavaGirenKisiler>();

            foreach (var item in sinavaGirenKisilerIdList)
            {
                var sinavSahibiOgrenci = tumKullanicilar.FirstOrDefault(x => x.Id == item.Sinav.SinavSahibi.ToString());

                var ogrenciPuani = _unitOfWork.GirilenKlasikSinavKayitRepository.IncludeMany(x=>x.KlasikSinavSinavSoruCevaps)
                    .FirstOrDefault(x => x.SuresiBaslamisSinavlarId == Guid.Parse(item.SuresiBaslamisSinavlarId.ToString()))
                   ;

                sinavaGirenKisiler.Add(new SinavaGirenKisiler { AdSoyad = sinavSahibiOgrenci.Ad + " " + sinavSahibiOgrenci.Soyad, OkulNumarasi = sinavSahibiOgrenci.KurumOgrenciNumarasi, UserGuidId = item.Sinav.SinavSahibi.ToString(), SinavGuid = item.SinavId, AldigiNot = ogrenciPuani?.OgrenciSinavPuani });
            }

            return sinavaGirenKisiler;
        }



        EntityLayer.Sinav.Sinav ISinavBilgileri.SinavBilgileri(Guid sinavId)
        {
            return _unitOfWork.SinavRepository.SingleOrDefault(x => x.SinavId == sinavId);
        }



    }
}
