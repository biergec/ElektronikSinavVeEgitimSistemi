using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.UnitOfWork;
using EntityLayer.BaslayanSinavlar;
using EntityLayer.Ders;

namespace BusinessLayer.SinavGiris
{
    public class OgrenciGirebilecegiSinavlar : IOgrenciGirebilecegiSinavlar
    {
        private readonly IUnitOfWork _unitOfWork;

        public OgrenciGirebilecegiSinavlar(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        IEnumerable<AnaSayfaSinavlarimList> IOgrenciGirebilecegiSinavlar.OgrenciGirebilecegiSinavlar(Guid ogrenciId)
        {
            var kayitliDerslerimGuidIdleri = _unitOfWork.KayitliDerslerimRepository.Get(x => x.OgrenciId == ogrenciId);


            // Her öğrencinin belirli dersleri olur. Ders listesindeki derse ait sınav varsa sınavlarına ekle
            var genelSinavListesi = _unitOfWork.SinavRepository.GetAll();


            var girebilecegimSinavlar = new List<AnaSayfaSinavlarimList>();
            foreach (var item in genelSinavListesi)
            {
                // pasif sinavları hariç tutuyoruz
                if (kayitliDerslerimGuidIdleri.Any(x => x.DerslerId == item.DerslerId) && item.SinavAktiflikDurumu == true)
                {
                    // sinav bitiş zamani geçmiş ise ekleme!
                    bool sinavSuresiDolmusMu = SinavSuresiKontrolSuresiDolmusMu(ogrenciId, item);

                    if (!sinavSuresiDolmusMu)
                    {
                        // bitiş saati başlangıç saatinden büyükse sınava katılmıştır
                        var baslayanSinavlar =
                            _unitOfWork.SuresiBaslamisSinavlarRepository.SingleOrDefault(x => x.OgrenciId == ogrenciId && x.SinavId == item.SinavId);

                        if (baslayanSinavlar != null && baslayanSinavlar.OgrenciSinaviBitirmeZamani < baslayanSinavlar.OgrenciSinavaBaslamaZamani && baslayanSinavlar.OgrenciSinavaBaslamaZamani < DateTime.Now)
                        {
                            girebilecegimSinavlar.Add(new AnaSayfaSinavlarimList { DersAdi = item.Dersler.DersAdi, SinavTuru = item.SinavTuru, SinavId = item.SinavId, SinavSuresiDakika = item.SinavSuresiDakika });
                        }
                        else
                        {
                            girebilecegimSinavlar.Add(new AnaSayfaSinavlarimList { DersAdi = item.Dersler.DersAdi, SinavTuru = item.SinavTuru, SinavId = item.SinavId, SinavSuresiDakika = item.SinavSuresiDakika });
                        }
                    }
                }
            }

            return girebilecegimSinavlar;
        }



        // Girilen sınavları göstermemeliyiz. Girip girmediğini kontrol et !
        private bool SinavSuresiKontrolSuresiDolmusMu(Guid ogrenciId, EntityLayer.Sinav.Sinav item)
        {
            var girilenSinavlar = _unitOfWork.SuresiBaslamisSinavlarRepository.Get(x => x.OgrenciId == ogrenciId && x.SinavId == item.SinavId);
            // sinav başlamamış ise süresi bitmemiştir!
            if (girilenSinavlar.Count() == 0)
                return false;

            // sinav başladıysa 
            var sinavSuresi = item.SinavSuresiDakika;

            var girilenSinav = girilenSinavlar.SingleOrDefault(x => x.SinavId == item.SinavId);
            var sinavBitisZamani = girilenSinav.OgrenciSinavaBaslamaZamani.AddMinutes(sinavSuresi);
            if (DateTime.Now > sinavBitisZamani)
                return true;

            return false;
        }



    }
}
