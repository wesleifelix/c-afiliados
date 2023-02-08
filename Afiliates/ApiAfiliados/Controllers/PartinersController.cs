using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessDomain;
using InfraAfiliados;
using static HashesLibrary.Classes.HashCrypty;
using Microsoft.Extensions.Configuration;
using HashesLibrary.Classes;
using Microsoft.AspNetCore.Hosting;
using IntefaceDomains;
using ApiAfiliados.Classes;
using System.Data.Common;
using System.Security.Claims;
using ApiAfiliados.Models.Publishers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace ApiAfiliados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PartinersController : ControllerBase
    {
        private readonly AfiliadosContext _context;

        HashAes _has = new HashAes();

        private HasAES _aes;
        private Guid _Customer_id { get; set; }
        private Guid _Contract_id { get; set; }
        private string RoleClaimToken { get; set; }
        private string errors;
        IConfiguration _configuration;
        IHostingEnvironment _env;
        private ErrorReturn error;
        private InitializeToken tks;

        public PartinersController(AfiliadosContext context, IConfiguration configuration, IHostingEnvironment env)
        {
            _context = context;
            _configuration = configuration; //instancia a configuração para pegar o secret KEY
            _context = context;
            _env = env;
            error = new ErrorReturn()
            {
                Controller = "Partiner",
            };
        }

        // GET: api/Partiners
        [HttpGet]
        //[Authorize(Roles = "super")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<IEnumerable<Partiner>>> GetPartiners()
        {
            return await _context.Partiners.ToListAsync();
        }

        // GET: api/Partiners/5
        [HttpGet("details/{id}")]
        [Authorize(Roles = "partiner,super")]
        public async Task<ActionResult<Partiner>> GetPartiner(Guid id)
        {
            var partiner = await _context.Partiners.FindAsync(id);

            if (partiner == null)
            {
                return NotFound();
            }

            return partiner;
        }

        // GET: api/Partiners/5
        [HttpGet("embed")]
        [Authorize(Roles = "partiner,super")]
        public async Task<ActionResult<string>> GetEmbed()
        {
            tks = new InitializeToken(HttpContext);
            try
            {
                var partiner = await _context.Partiners.FindAsync(tks._Customer_id);

                if (!partiner.Active)
                    return "";

                var _shorts = new HashAes();
                var encode = _shorts.Md5Encode( String.Format("{0}/{1}", partiner.Platform, tks._Customer_id));
                var b64 = _shorts.Base64Encode(encode);

                if (partiner == null)
                {
                    return NotFound();
                }
                string _hostAddr = "http" + ((HttpContext.Request.IsHttps) ? "s" : "") + "://" + HttpContext.Request.Host.ToString() + "";
                return String.Format("{0}/files/{1}.js", _hostAddr, b64.Replace("=",""));
            }
            catch 
            {
                return null;
            }
        }
        
        // GET: api/Partiners/5
        [HttpGet("me")]
        [Authorize(Roles = "partiner,super")]
        public async Task<ActionResult<Partiner>> GetMe()
        {
            tks = new InitializeToken(HttpContext);
            var partiner = await _context.Partiners.FindAsync(tks._Customer_id);

            if (partiner == null)
            {
                return NotFound();
            }

            return partiner;
        }

        // PUT: api/Partiners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        [Authorize(Roles = "partiner,super")]
        public async Task<IActionResult> PutPartiner(Guid id, Partiner partiner)
        {
            if (id != partiner.Id_partiner)
            {
                return BadRequest();
            }

            _context.Entry(partiner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartinerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Partiners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<Partiner>> PostPublisher(Partiner partiner)
        {
            error.Action = "Register";
            _context.Database.BeginTransaction();
            string _email = partiner.Email;
            string _pwd = partiner.Password;
            try
            {
                var _verifyDocument = new ValidDocument();
                if (!_verifyDocument.ValidateDocument(partiner.Document))
                {
                    error.Code = "PU0003-02";
                    return BadRequest(error);

                }

                if (ContractExistsDocument(partiner.Document))
                {
                    error.Code = "PU0003-01";
                    return BadRequest(error);
                }



                if (String.IsNullOrEmpty(partiner.Password))
                {
                    error.Code = "PU0003-03";
                    return BadRequest(error);

                }

                if (await UserExistsEmail(partiner.Email))
                {
                    error.Code = "PU0003-04";
                    return BadRequest(error);
                }

                partiner.Id_partiner = Guid.NewGuid();
                partiner.Active = true;
                _aes = new HasAES(partiner.Id_partiner.ToString());
                try
                {
                    var userEncoder = new PartinerInternalEncoder(partiner, partiner.Id_partiner.ToString());
                    partiner = userEncoder.EncodePartiner(true);
                }
                catch { }

                _context.Partiners.Add(partiner);

                //return publisher;
                await _context.SaveChangesAsync();

                await _context.Database.CurrentTransaction.CommitAsync();
                return CreatedAtAction(nameof(Login), new { email = _email, password = _pwd });
                var _rest = await this.SetToken(partiner);
                if (_rest != null)
                {
                    return new ObjectResult(_rest) { StatusCode = StatusCodes.Status201Created };
                    return CreatedAtAction(nameof(Login), new { email = _email, password = _pwd });
                    return Ok(_rest);

                }

                return new ObjectResult(_rest) { StatusCode = StatusCodes.Status201Created };
                return CreatedAtAction(nameof(Login), new { email = _email, password = _pwd });
            }
            catch (DbException ex)
            {
                await _context.Database.CurrentTransaction.RollbackAsync();
            }


            return CreatedAtAction("GetPartiner", new { id = partiner.Id_partiner }, partiner);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<Partiner>> Login(LoginPublisher user)
        {
            _context.Database.BeginTransaction();

            error.Action = "Login";
            error.Code = "LA0001-01";
            if (user == null)
            {
                return BadRequest(error);
            }

            try
            {
                string username = _has.Base64Encode(user.Email);
                var us = await _context.Partiners.Where(x => x.Email == username && x.Active == true && x.Deleted == false).FirstOrDefaultAsync();

                error.Code = "LA0001-02";
                if (us == null)
                {
                    return BadRequest(error);
                }

                error.Code = "LA0001-03";
                if (user.Password != null)
                {
                    error.Code = "LA0001-04";

                    if (_has.Compara(user.Password, us.Password))
                    {
                        var userEncoder = new PartinerInternalEncoder(us, us.Id_partiner.ToString());
                        us = userEncoder.DecodePartiner();

                        var _rest = await this.SetToken(us);
                        if (_rest != null)
                            return Ok(_rest);
                    }
                    else
                    {
                        return Unauthorized(error);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return Unauthorized(error);
            }

            return BadRequest(error);
        }

        // DELETE: api/Partiners/5
        [HttpDelete("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Roles = "super")]
        public async Task<IActionResult> DeletePartiner(Guid id)
        {
            var partiner = await _context.Partiners.FindAsync(id);
            if (partiner == null)
            {
                return NotFound();
            }

            _context.Partiners.Remove(partiner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PartinerExists(Guid id)
        {
            return _context.Partiners.Any(e => e.Id_partiner == id);
        }

        private bool PublisherExists(Guid id)
        {
            return _context.Publishers.Any(e => e.Id_publisher == id);
        }

        private async Task<Dictionary<string, object>> SetToken(Partiner us, bool isPersist = false)
        {
            try
            {

                error.Code = "LA0001-06";
                //dados do usuario
                string setKey = us.Id_partiner.ToString();
                var aes = new HasAES(setKey);


                var identity = new ClaimsIdentity(new[]
                {
                            new Claim(ClaimTypes.Actor, "Mercado8"),
                            new Claim(ClaimTypes.Country, "BR"),
                            new Claim(ClaimTypes.Webpage, "afiliadosm8.com.br"),
                            new Claim(ClaimTypes.Name, us.Name),
                            new Claim(ClaimTypes.NameIdentifier, aes.EncriptarAES(us.Id_partiner.ToString())),
                            
                           // new Claim(ClaimTypes.PrimaryGroupSid, advID),
                            new Claim(ClaimTypes.PrimarySid, aes.EncriptarAES(us.Id_partiner.ToString()).ToString()),
                            new Claim(ClaimTypes.Expiration, (isPersist) ? TimeSpan.FromDays(365).ToString() :TimeSpan.FromMinutes(600).ToString()),
                            new Claim(ClaimTypes.GivenName , _has.Base64Encode(us.Id_partiner.ToString()).ToString()),
                            new Claim(ClaimTypes.Uri , _has.Base64Encode(setKey).ToString()),
                            new Claim(ClaimTypes.PrimaryGroupSid,  aes.EncriptarAES(setKey.ToString()).ToString()),
                            new Claim("Store", "partiner"),
                            new Claim("Store", "super"),
                            new Claim(ClaimTypes.Role, "partiner"),
                            new Claim(ClaimTypes.Role, "super"),
                            

                        },
                 "ApplicationCookie");

                error.Code = "LA0001-07";
                // identity.AddClaim(new Claim(ClaimTypes.Role, us.GetLevels.Name.ToLower()));
                /* remover -->          identity.AddClaim(new Claim(ClaimTypes.Role, "administrador"));*/  // <<-------  remover eta linha
                                                                                                           //foreach (var reg in us.GetRoles.ToList())
                                                                                                           //{
                                                                                                           //    identity.AddClaim(new Claim(ClaimTypes.Role, reg.Name.ToLower()));
                                                                                                           //}

                error.Code = "LA0001-08";
                //buscando a chave secreta
                var key = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_configuration["SecurityKey"])
                        );
                error.Code = "LA0001-09";
                int dayExpe = (isPersist) ? 365 : 1;

                //definir o tipo de critofica para geração do token
                var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                error.Code = "LA0001-10";
                var token = new JwtSecurityToken(
                                issuer: "Mercado 8",
                                audience: "M8_AFILIADOS",
                                claims: identity.Claims,
                                expires: DateTime.Now.AddDays(dayExpe),
                                signingCredentials: credential
                            );

                us.setToken(new JwtSecurityTokenHandler().WriteToken(token));

                error.Code = "LA0001-11";

                Dictionary<string, Object> _rest = new Dictionary<string, object>();
                _rest.Add("user", us);


                return _rest;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private bool ContractExistsDocument(string pDocument)
        {
            pDocument = _has.Base64Encode(pDocument);
            var _ret = _context.Partiners.Any(e => e.Document == pDocument);
            return _ret;
            // return false;
        }
        private async Task<bool> UserExistsEmail(string pEmail)
        {
            return await _context.Partiners.AnyAsync(x => x.Email == _has.Base64Encode(pEmail));
        }
    }
}
