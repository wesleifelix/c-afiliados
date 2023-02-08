using BusinessDomain;
using PublisherDomain;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StatisticsSales
{
    public class ProductsViews
    {
        [Key]
        public int Id_productview { get; set; }
        public virtual Products GetProducts { get; set; }
        public virtual Ofers GetOfer { get; set; }
        public virtual Partiner GetPartiner { get; set; }
        public virtual Publisher GetPublisher { get; set; }
        public DateTime Date_access { get; set; }
        public string Refer { get; set; }
        public string Dispositive { get; set; }

        [DefaultValue(false)]
        public bool Checkout { get; set; }
    }
}
