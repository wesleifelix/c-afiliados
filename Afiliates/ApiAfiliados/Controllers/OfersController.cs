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
using Microsoft.AspNetCore.Hosting;
using IntefaceDomains;
using ApiAfiliados.Classes;
using HashesLibrary.Classes;
using Microsoft.AspNetCore.Authorization;
using ApiAfiliados.Models.Ofers;

namespace ApiAfiliados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OfersController : ControllerBase
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

        public OfersController(AfiliadosContext context, IConfiguration configuration, IWebHostEnvironment env)
        {
            _context = context;
            _configuration = configuration; //instancia a configuração para pegar o secret KEY
            _context = context;
            _env = env;
            error = new ErrorReturn()
            {
                Controller = "Ofers",
            };
        }


        // GET: api/Ofers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ofers>>> GetOfer()
        {
            return await _context.Ofer.ToListAsync();
        }

        // GET: api/Ofers
        [HttpGet]
        [Route("publisher")]
        [Authorize(Roles = "publisher,super")]
        public async Task<ActionResult<IEnumerable<Ofers>>> GetOferPublishers()
        {
            await _context.Database.BeginTransactionAsync();
            tks = new InitializeToken(HttpContext);
            try
            {
                var ofers = await _context.Ofer.Include(x=>x.Product).Include(x=>x.Banner).Include(x=>x.Partiners).Where(x => x.Active == true && x.Partiners.Active == true && x.Deleted == false).ToListAsync();

                List<OfersLinks> links = new List<OfersLinks>();


                if(ofers != null)
                {
                    var _order = await _context.Order.Include(x => x.GetItems).Where(x => x.GetPublisher.Id_publisher == tks._Customer_id).ToListAsync();

                    

                    foreach (var item in ofers)
                    {
                        string linkprod = item.Product.Link;
                       

                        linkprod += (!item.Product.Link.Contains("?")  ? "?" : "&") +"utm_source=afiliadosm8&utm_medium=afiliado" ;
                        linkprod += "&utm_campaign="+((int)item.TypeComission == 0 ? "cpc" : "cpa") ;
                        linkprod += "&m8psid=" + tks._Customer_id.ToString();

                        _aes = new HasAES(item.Partiners.Id_partiner.ToString());
                        try
                        {
                            var userEncoder = new PartinerInternalEncoder(item.Partiners, item.Partiners.Id_partiner.ToString());
                            item.Partiners = userEncoder.DecodePartiner();
                        }
                        catch { }


                        int order = _order.Sum(x=>x.GetItems.Count(x=>x.GetProduct.Id_product == item.Product.Id_product));
                        var tmp = new OfersLinks()
                        {
                            GetOfers = item,
                            Category = item.Product.Category,
                            Description = item.Product.Description,
                            Id = item.Product.Id,
                            Id_product = item.Product.Id_product,
                            Name_product = item.Product.Name,
                            PriceOriginal = item.Product.PriceOriginal,
                            PriceSale = item.Product.PriceSale,
                            Sku = item.Product.Sku,
                            Link = linkprod,
                            UrlImage = item.Product.UrlImage,
                            Name_partiner = item.Partiners.Name,
                            Views = await _context.ViewsProducts.CountAsync(x=>x.GetProducts.Id_product == item.Product.Id_product && x.GetPublisher.Id_publisher == tks._Customer_id),
                            Orders = order
                        };

                        tmp.GetOfers.Partiners = null;
                        tmp.GetOfers.Product = null;
                        links.Add(tmp);
                    }
                }

                return Ok(links);

            }
            catch (Exception ex)
            {
                await _context.Database.CurrentTransaction.RollbackAsync();
                return BadRequest(ex.Message);
            }

           
        }

        // GET: api/Ofers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ofers>> GetOfers(Guid id)
        {
            var ofers = await _context.Ofer.FindAsync(id);

            if (ofers == null)
            {
                return NotFound();
            }

            return ofers;
        }

        // PUT: api/Ofers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "partiner,super")]
        public async Task<IActionResult> PutOfers(Guid id, Ofers ofers)
        {
            if (id != ofers.Id_ofers)
            {
                return BadRequest();
            }

            _context.Entry(ofers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfersExists(id))
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

        // POST: api/Ofers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create/{product}")]
        [Authorize(Roles = "partiner,super")]
        public async Task<ActionResult<Ofers>> PostOfers(Ofers ofers, Guid product)
        {
            
            await _context.Database.BeginTransactionAsync();
            tks = new InitializeToken(HttpContext);
            try
            {
                var products = await _context.Product.FirstOrDefaultAsync(x => x.Id_product == product && x.Partiner_id.Id_partiner == tks._Customer_id);
                var partiner = await _context.Partiners.FindAsync(tks._Customer_id);
                
                if(products == null)
                {
                    return BadRequest();
                }

                ofers.Active = true;
                ofers.DateCreate = DateTime.Now;
                ofers.Partiners = partiner;
                ofers.Product = products;
                
                _context.Ofer.Add(ofers);
                await _context.SaveChangesAsync();
                await _context.Database.CurrentTransaction.CommitAsync();

            }
            catch (Exception ex)
            {
                await _context.Database.CurrentTransaction.RollbackAsync();
                return BadRequest(ex.Message);
            }


            return CreatedAtAction("GetOfers", new { id = ofers.Id_ofers }, ofers);
        }

        // DELETE: api/Ofers/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "super")]
        public async Task<IActionResult> DeleteOfers(Guid id)
        {
            var ofers = await _context.Ofer.FindAsync(id);
            if (ofers == null)
            {
                return NotFound();
            }

            _context.Ofer.Remove(ofers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OfersExists(Guid id)
        {
            return _context.Ofer.Any(e => e.Id_ofers == id);
        }
    }
}
