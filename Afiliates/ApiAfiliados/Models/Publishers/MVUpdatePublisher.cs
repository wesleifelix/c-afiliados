using PublisherDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApiAfiliados.Models.Publishers
{
    public class MVUpdatePublisher
    {
       
        public Guid Id_publisher { get; set; }

        private string _name { get; set; }
        [MinLength(5)]
        [MaxLength(500)]
        [Required(ErrorMessage = "O NOME e SOBRENOME devem ser informados")]
        public string Name
        {
            get => _name;
            set
            {
                if (Regex.IsMatch(value, "^[a-zA-Z ]+$"))
                {
                    if (value.Split(" ").Length > 1)
                        _name = value;
                }
                else
                {
                    if (value.Length >= 20)
                        _name = value;
                    else
                        throw new ArgumentException($"Invalid {nameof(value)} format",
                                  nameof(Name));
                }

            }
        }

  

        [MinLength(10)]
        [MaxLength(500)]
        [Required]
        public string Document { get; set; }

     

        [MinLength(10)]
        [MaxLength(25)]
        [Required(ErrorMessage = "O Telefone é obrigatório")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:00 ?0000-0000}")]
        public string Phone { get; set; }

        [StringLength(9)]
        //[Required(ErrorMessage = "O CEP é obrigatório")]
        [DataType(DataType.PostalCode)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:00000-000}")]
        public string Postcode { get; set; }

        [MinLength(3)]
        [MaxLength(500)]
        public string Address { get; set; }
        public string Numaddress { get; set; }
        public string Neighbor { get; set; }
        internal string _complement { get; set; }
        public string Complement
        {
            get => _complement;
            set
            {
                if (value == null)
                    _complement = "";
                else if (Regex.IsMatch(value, "^[a-zA-Z ]+$"))
                {
                    if (value.Length > 20)
                        _complement = value.Substring(0, 20);
                    else
                        _complement = value;
                }
            }
        }
        public string City { get; set; }

        [MinLength(2)]
        [MaxLength(150)]
        public string State { get; set; }


        [MaxLength(350)]
        [DataType(DataType.Url)]
        public string Youtube { get; set; }


        [MaxLength(350)]
        [DataType(DataType.Url)]
        public string Facebook { get; set; }


        [MaxLength(350)]
        [DataType(DataType.Url)]
        public string Tiktok { get; set; }


        [MaxLength(350)]
        [DataType(DataType.Url)]
        public string Instagram { get; set; }


        [MaxLength(350)]
        [DataType(DataType.Url)]
        public string Linkedin { get; set; }

        [MaxLength(350)]
        [DataType(DataType.Url)]
        public string Twitter { get; set; }

        [MaxLength(350)]
        [DataType(DataType.Url)]
        public string Site { get; set; }

        [DefaultValue(true)]
        public bool Terms { get; set; }

        public string ChavePix { get; set; }
        public string User_mercadopago { get; set; }


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


    }
}
