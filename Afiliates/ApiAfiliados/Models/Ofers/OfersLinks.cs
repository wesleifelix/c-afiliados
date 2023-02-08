using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessDomain;

namespace ApiAfiliados.Models.Ofers
{
    public class OfersLinks
    {
        public virtual BusinessDomain.Ofers GetOfers { get; set; }
        //public virtual BusinessDomain.Products GetProduct { get; set; }
        public string Link { get; set; }

        public Guid Id_product { get; set; }

       
        [MinLength(5)]
        [MaxLength(500)]
        public string Name_product { get; set; }

        [MinLength(1)]
        [MaxLength(500)]
        public string Name_partiner { get; set; }

        [MinLength(2)]
        [MaxLength(500)]
        public string Sku { get; set; }


        [MinLength(1)]
        [MaxLength(500)]
        public string Id { get; set; }


        [MaxLength(5000)]
        public string Description { get; set; }


        [MaxLength(500)]
        public string Category { get; set; }


        [MinLength(10)]
        [MaxLength(500)]
        [DataType(DataType.ImageUrl)]
        public string UrlImage { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C2}")]
        public decimal PriceOriginal { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C2}")]
        public decimal PriceSale { get; set; }

        public int Views { get; set; }
        public int Orders { get; set; }

    }
}
