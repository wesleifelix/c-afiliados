using BusinessDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrdersDomain
{
    public class Comissions
    {
        [Key]
        public Guid Id_comission { get; set; }

        public virtual Orders GetOrders { get; set; }
        [JsonIgnore]
        public virtual Products GetProducts { get; set; }

        public decimal Values { get; set; }

        [DefaultValue(false)]
        public bool Payed { get; set; }

        public DateTime Date_create { get; set; }
        public DateTime Date_pay { get; set; }

        [DefaultValue(false)]
        public bool Blocked { get; set; }
        public string Blocked_decription { get; set; }

    }
}
