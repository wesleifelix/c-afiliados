using PublisherDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static HashesLibrary.Classes.HashCrypty;

namespace HashesLibrary.Classes
{
    public class Tokens
    {
        
        public Guid _Customer_id { get; private set; }
        public Guid _Contract_id { get; private set; }
        public Guid _Adv_id { get; private set; }
        public string _Name { get; private set; }

        public string RoleClaimToken;

        public Tokens()
        {

        }

        public void initializerToken(ClaimsIdentity identity)
        {

            if (identity.Claims.Count() > 0)
            {
                IEnumerable<Claim> claims = identity.Claims;

                HashAes _has = new HashAes();

                string key = _has.Base64Decode(identity.FindFirst(ClaimTypes.Uri).Value.ToString());

                HasAES _aes = new HasAES(key);

                try
                {
                    _Customer_id = Guid.Parse(_aes.DecriptarAES(identity.FindFirst(ClaimTypes.PrimarySid).Value.ToString()));
                }
                catch
                {
                    _Customer_id = Guid.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value.ToString());
                }
                try
                {
                    _Contract_id = Guid.Parse(identity.FindFirst(ClaimTypes.GroupSid).Value.ToString());
                }
                catch { } 
                try
                {
                    _Adv_id = Guid.Parse(_aes.DecriptarAES(identity.FindFirst(ClaimTypes.PrimaryGroupSid).Value.ToString()));
                }
                catch { } 
                try
                {
                    _Name = _aes.DecriptarAES(identity.FindFirst(ClaimTypes.Name).Value.ToString());
                }
                catch {
                    _Name = (identity.FindFirst(ClaimTypes.Name).Value.ToString());
                }
                try
                {
                    //if(String.IsNullOrEmpty(identity.FindFirst(ClaimTypes.Role).Value.ToString()))
                    //   RoleClaimToken = identity.FindFirst(ClaimTypes.Role).Value.ToString();

                   // if (String.IsNullOrEmpty(identity.Claims.Where(x=>x.Type == "Store").FirstOrDefault().ToString()))
                        RoleClaimToken = identity.Claims.Where(x => x.Type == "Store").FirstOrDefault().Value.ToString();
                    Console.WriteLine(RoleClaimToken);
                }
                catch { }

            }
        }
        public Publisher decodeUsers(Publisher user)
        {
            HashAes _has = new HashAes();


            HasAES _aes = new HasAES(user.Id_publisher.ToString());

            if (!String.IsNullOrEmpty(user.Name))
                user.Name = _aes.DecriptarAES(user.Name);

            user.Email = _has.Base64Decode(user.Email);

            return user;
        }

        public Publisher encondeUsers(Publisher user)
        {
            HashAes _has = new HashAes();


            HasAES _aes = new HasAES(user.Id_publisher.ToString());

            if (!String.IsNullOrEmpty(user.Name))
                user.Name = _aes.EncriptarAES(user.Name);

            user.Email = _has.Base64Encode(user.Email);

            return user;
        }
    }
}
