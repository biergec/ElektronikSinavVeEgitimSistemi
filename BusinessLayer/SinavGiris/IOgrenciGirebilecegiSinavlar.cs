using System;
using System.Collections.Generic;
using EntityLayer.BaslayanSinavlar;

namespace BusinessLayer.SinavGiris
{
    public interface IOgrenciGirebilecegiSinavlar
    {
        IEnumerable<AnaSayfaSinavlarimList> OgrenciGirebilecegiSinavlar(Guid ogrenciId);
    }
}
