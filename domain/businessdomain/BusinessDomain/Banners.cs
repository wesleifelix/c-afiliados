using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessDomain
{
    public class Banners
    {
        [Key]
        public Guid Id_banner { get; set; }

        [MinLength(10)]
        [MaxLength(500)]
        [DataType(DataType.ImageUrl)]
        public string Url_image { get; set; }
        public TypeFormat Format { get; set; }
        public virtual Ofers GetOfers { get; set; }

        [DefaultValue(0)]
        public enum TypeFormat
        {
            Horizontal_350x100,     // 0
            Loja,    // 1
        }
    }
}