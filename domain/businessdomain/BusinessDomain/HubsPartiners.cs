using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain
{
    public class HubsPartiners
    {
        [Key]
        public Guid Id_hubs { get; set; }

        public virtual Partiner GetPartiner { get; set; }

        public TypeAuth Auth { get; set; }

        public string Url { get; set; }

        public string TokenBearer { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string App_key { get; set; }
        public string Secret_key { get; set; }
        public string Client_key { get; set; }

        [DefaultValue(2)]
        public enum TypeAuth
        {
            Basic,      // 0
            Bearer,     // 1
            Oath,       // 2
            Oath2,      // 3
            None,       // 4
        }
    }
}
