using System;
using EntityLayer;

namespace BusinessLayer.SinavGiris
{
    public interface ISinavKayit
    {
        Result SinavBaslangicKayit(Guid sinavId, Guid ogrenciId);
    }
}