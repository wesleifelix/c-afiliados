using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrdersDomain;

namespace ApiAfiliados.Models.Comissions
{
    public class MVComissions
    {
        public string Reference { get; set; }
        public string Customer { get; set; }
        public virtual OrdersDomain.Comissions comissions { get; set; }
        public string Status { get; set; }
        public DateTime DateCreate { get; set; }
        public bool Payed { get; set; }
    }
}
