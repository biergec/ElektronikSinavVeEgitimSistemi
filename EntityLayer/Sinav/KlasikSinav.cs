using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.Sinav
{
    public class KlasikSinav
    {
        [Key]
        public Guid KlasikSinavId { get; set; }
        public Guid SinavId { get; set; }
        
        public ICollection<KlasikSinavSorular> KlasikSinavSorulars { get; set; }
        public Sinav Sinavs { get; set; }
    }
}
