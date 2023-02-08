using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InfraAfiliados;
using PublisherDomain;
using ApiAfiliados.Models.Publishers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using IntefaceDomains;
using static HashesLibrary.Classes.HashCrypty;
using HashesLibrary.Classes;
using ApiAfiliados.Classes;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Data.Common;
using Microsoft.AspNetCore.Authorization;
using OrdersDomain;

namespace ApiAfiliados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PublishersController : ControllerBase
    {
        private readonly AfiliadosContext _context;

        HashAes _has = new HashAes();

        private HasAES _aes;
        private Guid _Customer_id { get; set; }
        private Guid _Contract_id { get; set; }
        private string RoleClaimToken { get; set; }
        private string errors;
        IConfiguration _configuration;
        IWebHostEnvironment _env;
        private ErrorReturn error;
        private InitializeToken tks;

        public PublishersController(AfiliadosContext context, IConfiguration configuration, IWebHostEnvironment env)
        {
            _context = context;
            _configuration = configuration; //instancia a configuração para pegar o secret KEY
            _context = context;
            _env = env;
            error = new ErrorReturn()
            {
                Controller = "Publisher",
            };
        }

        // GET: api/Publishers
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Roles = "super,partiner")]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
        {
            return await _context.Publishers.ToListAsync();
        }

        // GET: api/Publishers/5
        [HttpGet("details/{id}")]
        [Authorize(Roles = "super,partiner")]
        public async Task<ActionResult<Publisher>> GetPublisher(Guid id)
        {
            var publisher = await _context.Publishers.FindAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return publisher;
        }


        // GET: api/Publishers/5
        [HttpGet("me")]
        [Authorize(Roles = "super,publisher")]
        public async Task<ActionResult<Publisher>> GetPublisherMe()
        {
            tks = new InitializeToken(HttpContext);
            var publisher = await _context.Publishers.FindAsync(tks._Customer_id);

            if (publisher == null)
            {
                return NotFound();
            }

            var userEncoder = new UsersInternalEncoder(publisher, tks._Contract_id.ToString());
            publisher = userEncoder.DecodeUser();
            publisher.Password = "********";

            return publisher;
        }

        // GET: api/Publishers/5
        [HttpPut("me")]
        [Authorize(Roles = "super,publisher")]
        public async Task<ActionResult> PutPublisherMe([FromBody] MVUpdatePublisher publisher)
        {
            error.Action = "UPDATE";
            _context.Database.BeginTransaction();

            try
            {


                tks = new InitializeToken(HttpContext);
                var publisherinner = await _context.Publishers.FindAsync(tks._Customer_id);
                var _verifyDocument = new ValidDocument();

                var partiner = _context.Publishers
                                                .Include(x => x.GetPartiner)
                                                .Where(x => x.Id_publisher == publisherinner.Id_publisher)
                                                    .FirstOrDefaultAsync().Result.GetPartiner.Id_partiner;

                _aes = new HasAES(partiner.ToString());


                error.Code = "PU0005-01";
                if (publisherinner == null)
                {
                    return BadRequest(error);
                }

                error.Code = "PU0005-02";
                if (String.IsNullOrEmpty(publisher.Document))
                {
                    return BadRequest(error);
                }

                error.Code = "PU0005-03";
                if (!_verifyDocument.ValidateDocument(publisher.Document))
                {
                    return BadRequest(error);
                }


                error.Code = "PU0005-05";
                if (String.IsNullOrEmpty(publisher.Postcode))
                {
                    return BadRequest(error);
                }

                error.Code = "PU0005-04";
                if (publisher.Postcode.Length < 8)
                {
                    return BadRequest(error);
                }





                publisherinner.Name = (String.IsNullOrEmpty(publisher.Name)) ? publisherinner.Name : _aes.EncriptarAES(publisher.Name);

                publisherinner.Address = (String.IsNullOrEmpty(publisher.Address)) ? publisherinner.Address : publisher.Address;
                publisherinner.City = (String.IsNullOrEmpty(publisher.City)) ? publisherinner.City : publisher.City;
                publisherinner.Complement = (String.IsNullOrEmpty(publisher.Complement)) ? publisherinner.Complement : publisher.Complement;
                publisherinner.State = (String.IsNullOrEmpty(publisher.State)) ? publisherinner.State : publisher.State;

                publisherinner.Numaddress = (String.IsNullOrEmpty(publisher.Numaddress)) ? publisherinner.Numaddress : publisher.Numaddress;
                publisherinner.Postcode = (String.IsNullOrEmpty(publisher.Postcode)) ? publisherinner.Postcode : publisher.Postcode;
                publisherinner.Neighbor = (String.IsNullOrEmpty(publisher.Neighbor)) ? publisherinner.Neighbor : publisher.Neighbor;

                publisherinner.Document = (String.IsNullOrEmpty(publisher.Document)) ? publisherinner.Document : _has.Base64Encode(publisher.Document);

                publisherinner.Facebook = (String.IsNullOrEmpty(publisher.Facebook)) ? null : publisher.Facebook;
                publisherinner.Instagram = (String.IsNullOrEmpty(publisher.Instagram)) ? null : publisher.Instagram;
                publisherinner.Linkedin = (String.IsNullOrEmpty(publisher.Linkedin)) ? null : publisher.Linkedin;
                publisherinner.Phone = (String.IsNullOrEmpty(publisher.Phone)) ? publisherinner.Phone : publisher.Phone;
                publisherinner.Tiktok = (String.IsNullOrEmpty(publisher.Tiktok)) ? null : publisher.Tiktok;
                publisherinner.Twitter = (String.IsNullOrEmpty(publisher.Twitter)) ? null : publisher.Twitter;
                publisherinner.Youtube = (String.IsNullOrEmpty(publisher.Youtube)) ? null : publisher.Youtube;
                publisherinner.Site = (String.IsNullOrEmpty(publisher.Site)) ? null : publisher.Site;

                publisherinner.ChavePix = (String.IsNullOrEmpty(publisher.ChavePix)) ? null : _aes.EncriptarAES(publisher.ChavePix);
                publisherinner.User_mercadopago = (String.IsNullOrEmpty(publisher.User_mercadopago)) ? null : _aes.EncriptarAES(publisher.User_mercadopago);



                publisherinner.DateUpdate = DateTime.Now;

                /* var userEncoder = new UsersInternalEncoder(publisher, tks._Contract_id.ToString());
                 publisher = userEncoder.DecodeUser();*/

                _context.Entry(publisherinner).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                await _context.Database.CurrentTransaction.CommitAsync();
                //return publisher;
                return NoContent();
            }
            catch (DbException ex)
            {
                await _context.Database.CurrentTransaction.RollbackAsync();
                return BadRequest(error);
            }
        }

        // GET: api/Publishers/5
        [HttpGet]
        [Route("comissions")]
        [Authorize(Roles = "publisher,super")]
        public async Task<ActionResult<List<ApiAfiliados.Models.Comissions.MVComissions>>> GetComissions()
        {
            tks = new InitializeToken(HttpContext);
            var comissions = await _context.Comission.Include(x => x.GetOrders).Where(x => x.GetOrders.GetPublisher.Id_publisher == tks._Customer_id).ToListAsync();

            if (comissions == null)
            {
                return NotFound();
            }

            var mvcom = new List<ApiAfiliados.Models.Comissions.MVComissions>();

            foreach (var item in comissions)
            {
                string customer = item.GetOrders.Customer.ToUpper();
                string reference = (String.IsNullOrEmpty(item.GetOrders.Reference)) ? item.GetOrders.Order_id : item.GetOrders.Reference;
                string status = item.GetOrders.Status;
                item.GetOrders = null;
                mvcom.Add(new Models.Comissions.MVComissions()
                {
                    Customer = customer,
                    Reference = reference,
                    DateCreate = item.Date_create,
                    comissions = item,
                    Status = status,
                    Payed = item.Payed,
                });
            }


            return mvcom;
        }

        // PUT: api/Publishers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("update/{id}")]
        [Authorize(Roles = "super,publisher")]
        public async Task<IActionResult> PutPublisher(Guid id, Publisher publisher)
        {
            if (id != publisher.Id_publisher)
            {
                return BadRequest();
            }

            _context.Entry(publisher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // PUT: api/Publishers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Atualizar senha do divulgador
        /// </summary>
        /// <param name="oldpass">Senha atual</param>
        /// <param name="newpass">Nova senha</param>
        /// <returns></returns>
        [HttpPut("updatepassword")]
        [Authorize(Roles = "super,publisher")]
        public async Task<IActionResult> PutPublisherPassword([FromBody] ChangePass pass)
        {
            error.Action = "PASSWORD";
            await _context.Database.BeginTransactionAsync();
            tks = new InitializeToken(HttpContext);
            try {
                error.Code = "PU0006-01";
                var publisher = await _context.Publishers.FindAsync(tks._Customer_id);

                error.Code = "PU0006-02";
                if (publisher == null)
                {
                    return NotFound();
                }
                error.Code = "PU0006-03";
                if (!_has.Compara(pass.Oldpass, publisher.Password))
                {
                    return BadRequest(error);
                }

                error.Code = "PU0006-04";
                var userEncoder = new UsersInternalEncoder(publisher, tks._Customer_id.ToString());

                error.Code = "PU0006-05";
                publisher.Password = userEncoder.EncodeUserPassword(pass.Newpass);
                publisher.DateUpdate = DateTime.Now;

                error.Code = "PU0006-06";
                _context.Entry(publisher).State = EntityState.Modified;

                error.Code = "PU0006-07";
                await _context.SaveChangesAsync();
                await _context.Database.CurrentTransaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException.Message);
                await _context.Database.CurrentTransaction.RollbackAsync();
                return BadRequest(error);
            }

            return NoContent();
        }

        /// <summary>
        /// Mudar o email do divuldador
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPut("updateemail")]
        [Authorize(Roles = "super,publisher")]
        public async Task<IActionResult> PutPublisherEmail([FromBody] ChangeEmail email)
        {
            error.Action = "EMAIL";
            await _context.Database.BeginTransactionAsync();
            tks = new InitializeToken(HttpContext);
            try
            {
                error.Code = "PU0007-01";
                var publisher = await _context.Publishers.Include(x=>x.GetPartiner).Where(x=>x.Id_publisher ==  tks._Customer_id).FirstOrDefaultAsync();
                 

                error.Code = "PU0007-02";
                if (publisher == null)
                {
                    return NotFound();
                }
                error.Code = "PU0007-03";
                if (await UserExistsEmail(email.Email, publisher.GetPartiner.Id_partiner))
                {
                    return BadRequest(error);
                }

                error.Code = "PU0007-04";
                var userEncoder = new UsersInternalEncoder(publisher, tks._Customer_id.ToString());

                error.Code = "PU0007-05";
                publisher.Email = userEncoder.EncodeEmail(email.Email);

                publisher.DateUpdate = DateTime.Now;
                error.Code = "PU0007-06";
                _context.Entry(publisher).State = EntityState.Modified;

                error.Code = "PU0007-07";
                await _context.SaveChangesAsync();
                await _context.Database.CurrentTransaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException.Message);
                await _context.Database.CurrentTransaction.RollbackAsync();
                return BadRequest(error);
            }

            return NoContent();
        }

        // POST: api/Publishers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("Register/{partiner}")]
        [AllowAnonymous]
        public async Task<ActionResult<Publisher>> PostPublisher(Publisher publisher, [FromRoute] Guid partiner)
        {
            error.Action = "Register";
            _context.Database.BeginTransaction();
            string _email = publisher.Email;
            string _pwd = publisher.Password;
            try
            {
                var _verifyDocument = new ValidDocument();
                if (!_verifyDocument.ValidateDocument(publisher.Document))
                {
                    error.Code = "PU0003-02";
                    return BadRequest(error);

                }

                if (ContractExistsDocument(publisher.Document, partiner))
                {
                    error.Code = "PU0003-01";
                    return BadRequest(error);
                }



                if (String.IsNullOrEmpty(publisher.Password))
                {
                    error.Code = "PU0003-03";
                    return BadRequest(error);

                }

                if (await UserExistsEmail(publisher.Email, partiner))
                {
                    error.Code = "PU0003-04";
                    return BadRequest(error);
                }

                publisher.Id_publisher = Guid.NewGuid();
                publisher.Active = true;
                publisher.setPartiner(await _context.Partiners.FindAsync(partiner));
                _aes = new HasAES(partiner.ToString());
                try
                {
                    var userEncoder = new UsersInternalEncoder(publisher, partiner.ToString());
                    publisher = userEncoder.EncodeUser(true);
                }
                catch { }

                _context.Publishers.Add(publisher);

                //return publisher;
                await _context.SaveChangesAsync();

                await _context.Database.CurrentTransaction.CommitAsync();
                //return CreatedAtAction(nameof(Login), new { email = _email, password = _pwd });
                var _rest = await this.SetToken(publisher, partiner);

                // publisher.setToken(_rest);
                if (_rest != null)
                {
                    return new ObjectResult(_rest) { StatusCode = StatusCodes.Status201Created };

                    return Ok(_rest);

                }

                return new ObjectResult(_rest) { StatusCode = StatusCodes.Status201Created };
                return CreatedAtAction(nameof(Login), new { email = _email, password = _pwd });
            }
            catch (DbException ex)
            {
                await _context.Database.CurrentTransaction.RollbackAsync();
            }


            return CreatedAtAction("GetPublisher", new { id = publisher.Id_publisher }, publisher);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<Publisher>> Login(LoginPublisher user, [FromHeader] Guid partiner)
        {
            _context.Database.BeginTransaction();

            error.Action = "Login";
            error.Code = "LO0001-01";
            if (user == null)
            {
                return BadRequest(error);
            }

            try
            {
                string username = _has.Base64Encode(user.Email);
                var us = await _context.Publishers.Where(x => x.Email == username && x.Active == true && x.Deleted == false && x.GetPartiner.Id_partiner == partiner).FirstOrDefaultAsync();

                error.Code = "LO0001-02";
                if (us == null)
                {
                    return BadRequest(error);
                }

                error.Code = "LO0001-03";
                if (user.Password != null)
                {
                    error.Code = "LO0001-04";

                    if (_has.Compara(user.Password, us.Password))
                    {
                        var userEncoder = new UsersInternalEncoder(us, partiner.ToString());
                        us = userEncoder.DecodeUser();

                        var _rest = await this.SetToken(us, partiner);
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

        // DELETE: api/Publishers/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "super")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> DeletePublisher(Guid id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        /*
        ######################################################################
        ###############                                         ##############
        ###############             UPLOAD FILES                ##############
        ###############                                         ##############
        ######################################################################
        */



        /// <summary>
        /// Upload Files
        /// </summary>
        /// <param name="file"></param>
       
        /// <returns></returns>
        [HttpPost]
        [Route("uploadavatar")]
        [Authorize(Roles = "publisher, super")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<IActionResult> UpLoadFile(IFormFile file)
        {
            await _context.Database.BeginTransactionAsync();
            tks = new InitializeToken(HttpContext);

            error.Action = "UploadAvatar";
            error.Code = "PBUP01-01";



            error.Code = "PBUP01-02";
            var publisher = _context.Publishers.Find(tks._Customer_id);

            if (publisher == null)
            {
                return BadRequest(error);
            }
            error.Code = "PBUP01-03";



            //Criar TIMESPAN
            var dateTime = new DateTime(DateTime.Now.Ticks, DateTimeKind.Local);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var unixDateTime = (dateTime.ToUniversalTime() - epoch).TotalSeconds;

            //FIM DO TIMESPAN
            error.Code = "UPP000-07";
            string filename = tks._Customer_id.ToString();

            string patch = "publisher\\avatar";

            string[] extenion = file.FileName.Split('.');

            error.Code = "UPP000-08";
            try
            {
                var rnd = new Random();
                /* string fileNames =  (rnd.Next(0,99999))+ DateTime.UtcNow.ToString("yyMMddHHmmssfff",
                                            CultureInfo.InvariantCulture).ToString();*/

                string fileNames = filename + "." + extenion.Last();

                error.Code = "UPP000-09";
                bool retImg = false;

                error.Code = "UPP000-10";
                var ImageName = new UpLoadImage(_env);
                var ExtentionName = extenion.Last();

                if (ExtentionName != "jpg" && ExtentionName != "jpeg")
                {
                    error.Code = "UPP000-14";
                    return BadRequest(error);
                }


                bool _thumbs = false;
                //_thumbs = (extenion.Last() == "jpg" || extenion.Last() == "jpeg" || extenion.Last() == "png") ? true : false;

                //retImg = ImageName.UploadUpLoad(file, patch, fileNames, null, false, _thumbs, 300).ToString();
                
                retImg = ImageName.UploadUpLoad(file, patch, fileNames, width: 150);


                error.Code = "UPP000-11";
                if (retImg)
                {
                    error.Code = "UPP000-12";
                    try
                    {


                        error.Code = "UPP000-13";

                        publisher.Avatar = fileNames;


                        _context.Entry(publisher).State = EntityState.Modified;

                        error.Code = "UPP000-14";


                        error.Code = "UPP000-15";
                        await _context.SaveChangesAsync();
                        _context.Database.CurrentTransaction.Commit();

                    }
                    catch
                    {


                        _context.Database.CurrentTransaction.Rollback();
                        return BadRequest(error);
                    }

                }
                else
                {

                    _context.Database.CurrentTransaction.Rollback();
                    return BadRequest(error);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(error + ex.Message);
            }
        }



        //[HttpDelete]
        //[Route("DeleteFiles/{id_project}/{id}")]
        //[Authorize(Roles = "administrador, update_project, superm8")]
        //public async Task<IActionResult> DeleteFiles([FromRoute] Guid id_project, [FromRoute] int id)
        //{
        //    string patch = "buildersnew";

        //    try
        //    {
        //        _context.Database.BeginTransaction();
        //        var img = _context.ProjFiles.Find(id);

        //        var ImageName = new Uploads(_env);

        //        var tmp = ImageName.remove(patch, img.File_url.ToString());
        //        ImageName.remove(patch, "thumbs-" + img.File_url.ToString());

        //        if (tmp == true)
        //        {
        //            this.initializerToken();
        //            LogBuilders log = new LogBuilders();
        //            log.GetBuilders = _context.Builders.Find(id_project);
        //            log.Create_dt = DateTime.Now;
        //            log.GetUser = _context.Users.Find(this._Customer_id);

        //            log.Description = "Arquivo Removido" + img.File_url.ToString();

        //            _context.LogsBuilders.Add(log);
        //            _context.ProjFiles.Remove(img);

        //            await _context.SaveChangesAsync();
        //            _context.Database.CurrentTransaction.Commit();
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        _context.Database.CurrentTransaction.Rollback();
        //        return BadRequest(ex.Message);
        //    }

        //    return Ok();
        //}
        private bool PublisherExists(Guid id)
        {
            return _context.Publishers.Any(e => e.Id_publisher == id);
        }

        private async Task<Dictionary<string, object>> SetToken(Publisher us, Guid partiner, bool isPersist = false)
        {
            try
            {

                error.Code = "LO0001-06";
                //dados do usuario

                string setKey = us.Id_publisher.ToString();
                var aes = new HasAES(setKey);
                string contractID = partiner.ToString();

                var identity = new ClaimsIdentity(new[]
                {
                            new Claim(ClaimTypes.Actor, "Mercado8"),
                            new Claim(ClaimTypes.Country, "BR"),
                            new Claim(ClaimTypes.Webpage, "afiliadosm8.com.br"),
                            new Claim(ClaimTypes.Name, us.Name),
                            new Claim(ClaimTypes.NameIdentifier, aes.EncriptarAES(us.Id_publisher.ToString())),

                            new Claim(ClaimTypes.GroupSid, contractID),
                            new Claim(ClaimTypes.PrimarySid, aes.EncriptarAES(us.Id_publisher.ToString()).ToString()),
                            new Claim(ClaimTypes.Expiration, (isPersist) ? TimeSpan.FromDays(365).ToString() :TimeSpan.FromMinutes(600).ToString()),
                            new Claim(ClaimTypes.GivenName , _has.Base64Encode(us.Id_publisher.ToString()).ToString()),
                            new Claim(ClaimTypes.Uri , _has.Base64Encode(setKey).ToString()),
                            new Claim(ClaimTypes.PrimaryGroupSid,  aes.EncriptarAES(setKey.ToString()).ToString()),
                            new Claim("Store", "publisher"),
                            new Claim(ClaimTypes.Role, "publisher"),


                        },
                 "ApplicationCookie");

                error.Code = "LO0001-07";
                // identity.AddClaim(new Claim(ClaimTypes.Role, us.GetLevels.Name.ToLower()));
                /* remover -->          identity.AddClaim(new Claim(ClaimTypes.Role, "administrador"));*/  // <<-------  remover eta linha
                                                                                                           //foreach (var reg in us.GetRoles.ToList())
                                                                                                           //{
                                                                                                           //    identity.AddClaim(new Claim(ClaimTypes.Role, reg.Name.ToLower()));
                                                                                                           //}

                error.Code = "LO0001-08";
                //buscando a chave secreta
                var key = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_configuration["SecurityKey"])
                        );
                error.Code = "LO0001-09";
                int dayExpe = (isPersist) ? 365 : 1;

                //definir o tipo de critofica para geração do token
                var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                error.Code = "LO0001-10";
                var token = new JwtSecurityToken(
                                issuer: "Mercado 8",
                                audience: "M8_AFILIADOS",
                                claims: identity.Claims,
                                expires: DateTime.Now.AddDays(dayExpe),
                                signingCredentials: credential
                            );

                us.setToken(new JwtSecurityTokenHandler().WriteToken(token));

                error.Code = "LO0001-11";
                us.Password = "********";
                Dictionary<string, Object> _rest = new Dictionary<string, object>();
                _rest.Add("user", us);


                return _rest;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private bool ContractExistsDocument(string pDocument, Guid partiner)
        {
            pDocument = _has.Base64Encode(pDocument);
            var _ret = _context.Publishers.Any(e => e.Document == pDocument && e.GetPartiner.Id_partiner == partiner);
            return _ret;
            // return false;
        }
        private async Task<bool> UserExistsEmail(string pEmail, Guid partiner)
        {
            return await _context.Publishers.AnyAsync(x => x.Email == _has.Base64Encode(pEmail) && x.GetPartiner.Id_partiner == partiner);
        }
        

    }
}
