using IntefaceDomains;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;


namespace BusinessDomain
{
    public class Products 
    {
        [Key]
        public Guid Id_product { get; set; }

        [JsonIgnore]
        public virtual Partiner Partiner_id { get; set; }

        [MinLength(5)]
        [MaxLength(500)]
        public string Name { get; set; }

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
        [DataType(DataType.Url)]
        public string Link { get; set; }

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
        public decimal Stock { get; set; }

        [MaxLength(128)]
        public string Ean { get; set; }

     
        protected internal DateTime _create { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateCreate
        {
            get => _create;
            set
            {
                if (value.Date.Year == 1 && value.Date.Day == 1)
                {
                    _create = DateTime.Now;
                }
                else
                {
                    _create = value;
                }
            }
        }

        protected internal DateTime _update { get; set; }
        public DateTime DateUpdate
        {
            get => _update;
            set
            {
                _update = DateTime.Now;
            }
        }

        [DefaultValue(false)]
        protected internal DateTime _deleted { get; set; }
        public DateTime DateDeleted
        {
            get => _deleted;
            set
            {
                _deleted = DateTime.Now;
            }
        }

        [DefaultValue(true)]
        protected internal bool _actived { get; set; }

        [DefaultValue(true)]
        public bool Active
        {
            get => _actived;
            set
            {
                _actived = value;
            }
        }

       

        protected internal bool _delete { get; set; }

        [DefaultValue(true)]
        public bool Deleted
        {
            get => _delete;
            set
            {
                _delete = value;
            }
        }

    }
}
