using System;
using System.Collections.Generic;
using System.Text;
using EntityLayer;
using EntityLayer.Sinav;

namespace BusinessLayer.DersIslemleri
{
    public interface IDersIslemleri
    {
        Result DersEkle(DersEkleViewModel dersEkleViewModel);
        Result DersSil(Guid dersId);
        Result GetAllDersler();
        string GetDersAdi(Guid dersId);
    }
}
