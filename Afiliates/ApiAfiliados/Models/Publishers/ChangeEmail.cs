using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAfiliados.Models.Publishers
{
    public class ChangeEmail
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
