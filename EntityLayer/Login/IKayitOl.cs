using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Login
{
    public interface IKayitOl
    {
        Task<Result> KayitOl(KayitOlViewModel kayitOlViewModel);
        //Result SifremiUnuttum();
    }
}
