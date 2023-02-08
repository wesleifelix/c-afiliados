using BusinessDomain;
using PublisherDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HashesLibrary.Classes.HashCrypty;

namespace HashesLibrary.Classes
{
    public class PartinerInternalEncoder
    {
       
        HashAes _has = new HashAes();
        private Tokens tks = new Tokens();
        private HasAES _aes;
        private string _key;

        private Partiner user;

        public PartinerInternalEncoder()
        {

        }

        public PartinerInternalEncoder(Partiner _user, string key)
        {
            this.user = _user;
            this._key = key;
            _aes = new HasAES(key);
        }

        public Partiner EncodePartiner(bool pass = false)
        {

            if (this.user.Name != null)
                this.user.Name = _aes.EncriptarAES(this.user.Name);

            if (this.user.Email != null)
                this.user.Email = _has.Base64Encode(this.user.Email);

            if (this.user.Document != null)
                this.user.Document = _has.Base64Encode(this.user.Document);

            if (pass)
            {
                if (!String.IsNullOrEmpty(this.user.Password))
                {
                    this.user.Password = EncodePartinerPassword(this.user.Password);
                }
            }

            user.DateCreate = (user.DateCreate.Date.ToString() == "01-01-0001" || user.DateCreate.Date.ToString() == "0001-01-01" || user.DateCreate.Date.ToShortDateString() == "01/01/0001") ? DateTime.Now : user.DateCreate;
            user.DateUpdate = (user.DateUpdate.Date.ToString() == "01-01-0001" || user.DateUpdate.Date.ToString() == "0001-01-01" || user.DateUpdate.Date.ToShortDateString() == "01/01/0001") ? DateTime.Now : user.DateUpdate;

            return this.user;
        }
        public string EncodePartinerPassword(string password)
        {
            return _has.Codifica(password);
        }
        public Partiner DecodePartiner()
        {

            if (this.user.Name != null)
                this.user.Name = _aes.DecriptarAES(this.user.Name);

            if (this.user.Email != null)
                this.user.Email = _has.Base64Decode(this.user.Email);
            if (this.user.Document != null)
                this.user.Document = _has.Base64Decode(this.user.Document);
            return this.user;
        }


    }
}