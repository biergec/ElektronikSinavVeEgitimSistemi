using System;

namespace BusinessLayer.Sinav
{
    public interface ISinavBilgileri
    {
        EntityLayer.Sinav.Sinav SinavBilgileri(Guid sinavId);
    }
}