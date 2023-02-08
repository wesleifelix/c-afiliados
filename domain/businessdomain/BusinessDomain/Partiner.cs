using IntefaceDomains;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;


namespace BusinessDomain
{
    public class Partiner : PadraoPublisher
    {
        public Partiner()
        {
            if(this.Id_partiner.ToString() == "00000000-0000-0000-0000-000000000000" || this.Id_partiner.ToString() == "00000000000000000000000000000000")
            {
                this.Id_partiner = Guid.NewGuid();
            }
        }

        [Key]
        public Guid Id_partiner { get; set; }


        private string _name { get; set; }
        [MinLength(5)]
        [MaxLength(500)]
        [Required(ErrorMessage = "O NOME e SOBRENOME devem ser informados")]
        public string Name {get;set;}

        private string _pwd { get; set; }
        [MinLength(8, ErrorMessage = "A senha deve ao Menos 8 caracteres")]
        [MaxLength(500)]
        [Required(ErrorMessage = "A senha deve ser informada")]
        [DataType(DataType.Password)]
        public string Password
        {
            get => _pwd;
            set
            {
                if (value.Length >= 8)
                {
                    _pwd = value;
                }
                else
                    throw new ArgumentException($"Invalid {nameof(value)} format",
                                  nameof(Password));
            }
        }


        [MinLength(14)]
        [MaxLength(600)]
        [Required]
        public string Document { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        public string Email { get; set; }


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

        [MaxLength(350)]
        [DefaultValue("m8")]
        public string Platform { get; set; }

        [DefaultValue(true)]
        public bool Terms { get; set; }

        DateTime PadraoPublisher._create { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DateTime PadraoPublisher._update { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DateTime PadraoPublisher._deleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool PadraoPublisher._actived { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

        DateTime PadraoPublisher.DateCreate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DateTime PadraoPublisher.DateUpdate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DateTime PadraoPublisher.DateDeleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool PadraoPublisher.Active { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool PadraoPublisher._delete { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

        [NotMapped]
        public string Token { get; internal set; }

        public void setToken(string tokens)
        {
            this.Token = tokens;
        }

        public PaymentDays Dayscomission { get; set; }


        //public virtual ICollection<Products> Product { get; set; }
        //public virtual ICollection<Ofers> Ofer { get; set; }

        [DefaultValue(2)]
        public enum PaymentDays
        {
            Diario,     // 0
            Semanal,    // 1
            Quinzenal,  // 2
            Mensal      // 3
        }

    }
}
