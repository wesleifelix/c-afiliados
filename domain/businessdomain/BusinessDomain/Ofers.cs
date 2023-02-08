using IntefaceDomains;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessDomain
{
    public class Ofers : PadraoPublisher
    {
        [Key]
        public Guid Id_ofers { get; set; }

        [JsonIgnore]
        public virtual Partiner Partiners { get; set; }

        [JsonIgnore]
        public virtual Products Product { get; set; }

        public string Name { get; set; }
        public int Type { get; set; }

        public TypeOfer TypeComission { get; set; }

        public decimal Comission { get; set; }

        public int Window { get; set; }
        public virtual ICollection<Banners> Banner { get; set; }

        public DateTime Date_start { get; set; }
        public DateTime Date_end { get; set; }

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

        [DefaultValue(false)]
        public bool Deleted
        {
            get => _delete;
            set
            {
                _delete = value;
            }
        }

        [DefaultValue(0)]
        public enum TypeOfer
        {
            Produto,     // 0
            Loja,    // 1
        }

    }
}
