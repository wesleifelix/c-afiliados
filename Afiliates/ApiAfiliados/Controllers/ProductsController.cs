using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessDomain;
using InfraAfiliados;
using Microsoft.AspNetCore.Authorization;
using ApiAfiliados.Models.Products;
using HashesLibrary.Classes;
using StatisticsSales;
using static HashesLibrary.Classes.HashCrypty;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using IntefaceDomains;
using ApiAfiliados.Classes;

namespace ApiAfiliados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
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

        public ProductsController(AfiliadosContext context, IConfiguration configuration, IWebHostEnvironment env)
        {
            _context = context;
            _configuration = configuration; //instancia a configuração para pegar o secret KEY
            _context = context;
            _env = env;
            error = new ErrorReturn()
            {
                Controller = "Product",
            };
        }


      

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProduct()
        {
            return await _context.Product.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProducts(Guid id)
        {
            var products = await _context.Product.FindAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "partiner,super")]
        public async Task<IActionResult> PutProducts(Guid id, Products products)
        {
            if (id != products.Id_product)
            {
                return BadRequest();
            }

            _context.Entry(products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "partiner,super")]
        public async Task<ActionResult<Products>> PostProducts(Products products)
        {
            await _context.Database.BeginTransactionAsync();
            tks = new InitializeToken(HttpContext);
            try
            {
                var partiner = await _context.Partiners.FindAsync(tks._Customer_id);
                products.Partiner_id = partiner;
                _context.Product.Add(products);
                await _context.SaveChangesAsync();
                await _context.Database.CurrentTransaction.CommitAsync();

            }
            catch(Exception ex)
            {
                await _context.Database.CurrentTransaction.RollbackAsync();
                return BadRequest(ex.Message);
            }
          

            return CreatedAtAction("GetProducts", new { id = products.Id_product }, products);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("view")]
        public async Task<ActionResult<Products>> PostViewProducts(MVProductsViews productsview)
        {
            var _device = new DetectDevice(HttpContext);
            await _context.Database.BeginTransactionAsync();

            Console.Write(_device.Device);

            try
            {
                if (!String.IsNullOrEmpty(productsview.publisher))
                {
                    var _partiner = await _context.Partiners.FindAsync(Guid.Parse(productsview.seller));
                    var _publisher = await _context.Publishers.FindAsync(Guid.Parse(productsview.m8a_sid));
                    var _ofers = await _context.Ofer.Where( x=>x.Partiners.Id_partiner == _partiner.Id_partiner && x.Product.Id == productsview.m8a_idproduct).FirstOrDefaultAsync();

                    var _views = new ProductsViews()
                    {
                        Refer = productsview.m8a_page,
                        Dispositive = _device.Device,
                        Checkout = ( productsview.m8a_page == "purschase") ? true : false,
                        Date_access = DateTime.Now,
                        GetPartiner = _partiner,
                        GetProducts = await _context.Product.FirstOrDefaultAsync(x => x.Id == productsview.m8a_idproduct && x.Partiner_id.Id_partiner == Guid.Parse(productsview.seller)),
                        GetOfer = _ofers,
                        GetPublisher = _publisher
                    };

                    _context.ViewsProducts.Add(_views);

                    await _context.SaveChangesAsync();
                    await _context.Database.CurrentTransaction.CommitAsync();
                    return Ok();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                await _context.Database.CurrentTransaction.RollbackAsync();
                return BadRequest(ex.InnerException);
            }

            return Ok();
        }


        [HttpGet]
        [Authorize(Roles = "publisher,super")]
        [Route("publisher/view")]
        public async Task<ActionResult<int>> PublisherViewProducts([FromHeader] DateTime dateini, [FromHeader] DateTime dateend)
        {
            
            await _context.Database.BeginTransactionAsync();
            tks = new InitializeToken(HttpContext);

            DateTime _initdate = (dateini.ToString("yyyy-dd-MM") == "0001-01-01") ? DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")) : dateini;

            
            DateTime _enddate = (dateend.ToString("yyyy-dd-MM") == "0001-01-01") ? DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(1).AddDays(-1) : dateend;


            try
            {

                var view = await _context.ViewsProducts.CountAsync(x => x.GetPublisher.Id_publisher == tks._Customer_id && x.Date_access.Date >= _initdate.Date && x.Date_access.Date <= _enddate.Date);
                return Ok(view);
              
            }
            catch (Exception ex)
            {
                await _context.Database.CurrentTransaction.RollbackAsync();
                return BadRequest(ex.InnerException);
            }

            return Ok(0);
        }


        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts(Guid id)
        {
            var products = await _context.Product.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            _context.Product.Remove(products);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductsExists(Guid id)
        {
            return _context.Product.Any(e => e.Id_product == id);
        }
    }
}
