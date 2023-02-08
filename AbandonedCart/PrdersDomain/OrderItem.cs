using BusinessDomain;
using PublisherDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrdersDomain
{
    public class OrderItem
    {
        [Key]
        public Guid Id_item { get; set; }
        public virtual Products GetProduct { get; set; }

        [JsonIgnore]
        public virtual Orders GetOrders { get; set; }

        [JsonIgnore]
        public virtual Publisher GetPublisher { get; set; }

        [JsonIgnore]
        public virtual Ofers GetOfers { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        
    }
}
