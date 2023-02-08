using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IntefaceDomains
{
    public interface PadraoPublisher
    {
        protected internal DateTime _create { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateCreate { 
            get => _create; 
            set 
            {
                if(value.Date.Year == 1 && value.Date.Day == 1)
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

        [JsonIgnore]
        public DateTime DateDeleted
        {
            get => _deleted;
            set
            {
                _deleted = DateTime.Now;
            }
        }

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

        [DefaultValue(false)]
        [JsonIgnore]
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
