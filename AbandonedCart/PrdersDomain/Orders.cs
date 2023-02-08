using BusinessDomain;
using PublisherDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrdersDomain
{
    public class Orders
    {
        [Key]
        public Guid Id_order { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderItem> GetItems{ get; set; }

        [JsonIgnore]
        public virtual Partiner GetPartiner{ get; set; }

        [JsonIgnore]
        public virtual Publisher GetPublisher{ get; set; }
        public DateTime Date_order{ get; set; }
        public string Site{ get; set; }
        public string Order_id{ get; set; }
        public string Customer{ get; set; }
        public string Reference{ get; set; }
        public string Status{ get; set; }
        public StatusOder StatusInternal { get; set; }
        public decimal TotalPay{ get; set; }
        public decimal ProductsPay{ get; set; }
        public decimal ShippingPay{ get; set; }
        public string Payment_type{ get; set; }

        public bool Valid { get; set; }

        public DateTime Date_create { get; set; }

        [DefaultValue(0)]
        public enum StatusOder
        {
            Aguardando,     // 0
            Aprovado,       // 1
            Cancelado,      // 2
            Devolvido,      // 3
            Bloqueado,      // 4
        }
    }
}
