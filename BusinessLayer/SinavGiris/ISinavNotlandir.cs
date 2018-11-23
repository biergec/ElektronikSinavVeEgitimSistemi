using System;
using EntityLayer;

namespace BusinessLayer.SinavGiris
{
    public interface ISinavNotlandir
    {
        Result KlasikSinavNotlandir(Guid sinavId, Guid ogrenciId, double sinavPuani);
        Result TestSinavNotlandir(Guid sinavId, Guid ogrenciId, decimal sinavPuani);
    }
}