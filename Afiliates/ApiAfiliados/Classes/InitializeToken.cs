using HashesLibrary.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static HashesLibrary.Classes.HashCrypty;

namespace ApiAfiliados.Classes
{
    public class InitializeToken : ControllerBase
    {
        private HasAES _aes;
        HashAes _has = new HashAes();
        public Tokens tks = new Tokens();


        public Guid _Customer_id { get; private set; }
        public Guid _Contract_id { get; private set; }
        public Guid _Adv_id { get; private set; }
        public bool isValid { get; private set; }

        public string RoleClaimToken;

        public InitializeToken(Object _identity)
        {
            var _context = (HttpContext)_identity;
            try
            {
                tks.initializerToken(_context.User.Identity as ClaimsIdentity);
                this._Customer_id = tks._Customer_id;
                this._Contract_id = tks._Contract_id;
                this._Adv_id = tks._Adv_id;
                this.RoleClaimToken = tks.RoleClaimToken;

                this.isValid = true;

                if (this._Contract_id.ToString() == "00000000-0000-0000-0000-000000000000")
                    this.isValid = false;
                //else if (this._Adv_id.ToString() == "00000000-0000-0000-0000-000000000000")
                //    this.isValid = false;
                else if (String.IsNullOrEmpty(RoleClaimToken))
                    this.isValid = false;
            }
            catch { }
        }

    }
}