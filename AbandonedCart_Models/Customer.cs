using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AbandonedCart_Models
{
    public class Customer
    {
        [Key]
        public Guid Id_customer { get; set; }

        public string Namer { get; set; }

        public string Site { get; set; }
        public string Url_site { get; set; }
       // public virtual Guid MyProperty { get; set; }
    }
}
