using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AbandonedCart_Models
{
    public class Contracts
    {
        public Contracts()
        {
            this.Id_contracts = (this.Id_contracts == null) ? Guid.NewGuid() : this.Id_contracts;
        }

        public Guid Id_contracts { get; set; }

        
        [MaxLength(200)]
        public string Name { get; set; }
        
        [DataType(DataType.Url)]
        [MaxLength(300)]
        public string Site { get; set; }

        [DataType(DataType.Date)]
        public DateTime Create_dt { get; set; }
    }
}
