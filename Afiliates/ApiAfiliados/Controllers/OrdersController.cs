using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InfraAfiliados;
using OrdersDomain;
using Microsoft.AspNetCore.Authorization;
using ApiAfiliados.Models.Order;
using HashesLibrary.Classes;
using BusinessDomain;
using System.Data.Common;
using static HashesLibrary.Classes.HashCrypty;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using ApiAfiliados.Classes;
using IntefaceDomains;

namespace ApiAfiliados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
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

        public OrdersController(AfiliadosContext context, IConfiguration configuration, IWebHostEnvironment env)
        {
            _context = context;
            _configuration = configuration; //instancia a configuração para pegar o secret KEY
            _context = context;
            _env = env;
            error = new ErrorReturn()
            {
                Controller = "Orders",
            };
        }
    

        // GET: api/Orders
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Orders>>> GetOrder()
        //{
        //    return await _context.Order.ToListAsync();
        //}

        // GET: api/Orders/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Orders>> GetOrders(Guid id)
        //{
        //    var orders = await _context.Order.FindAsync(id);

        //    if (orders == null)
        //    {
        //        return NotFound();
        //    }

        //    return orders;
        //}

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("change/{id}/{status}/{valid}/{internalstatus}")]
        public async Task<IActionResult> PutOrders(Guid id, string status, int valid, int internalstatus)
        {
            await _context.Database.BeginTransactionAsync();

            try
            {
                var order = await _context.Order.FindAsync(id);

                error.Code = "OD0004-01";
               
                if(order == null)
                {
                    return BadRequest(error);
                }

                order.Status = status;
                order.Valid = (valid == 1) ? true : false;
                order.StatusInternal =(Orders.StatusOder)internalstatus;

                _context.Entry(order).State = EntityState.Modified;

                if(internalstatus == 1)
                {
                    var cart = await _context.ItemsOrder.Where(x => x.GetOrders.Id_order == id).ToListAsync();

                    foreach (var its in cart)
                    {

                        var comission = new Comissions()
                        {
                            Id_comission = Guid.NewGuid(),
                            Blocked = false,
                            Date_create = DateTime.Now,
                            Date_pay = DateTime.Now,
                            GetOrders = order,
                            GetProducts = its.GetProduct,
                            Payed = false,
                            Values = its.Rate
                        };

                        _context.Comission.Add(comission);
                    }
                    
                }

                if (internalstatus == 2 || internalstatus == 3)
                {
                    var comission = await _context.Comission.Where(x => x.GetOrders.Id_order == id).ToListAsync();

                    foreach (var its in comission)
                    {

                        its.Blocked = true;
                        its.Blocked_decription = "Problemas com pedido";
                        _context.Entry(its).State = EntityState.Modified;
                    }

                }

                await _context.SaveChangesAsync();
                await _context.Database.CurrentTransaction.CommitAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                await _context.Database.CurrentTransaction.RollbackAsync();
            }
            

            return NoContent();
        }

        // GET: api/Orders/5
        [HttpGet("publisher")]
        [Authorize(Roles = "super, publisher")]
        public async Task<ActionResult> GetOrdersPublisher()
        {
            tks = new InitializeToken(HttpContext);

            var orders = await _context.Order.Where(x=>x.GetPublisher.Id_publisher == tks._Customer_id).ToListAsync();

            if (orders == null)
            {
                return NotFound();
            }

            Dictionary<string, List<Orders>> _order = new Dictionary<string, List<Orders>>();

            _order.Add("aguardando", orders.Where(x => x.StatusInternal == (Orders.StatusOder)0).ToList());
            _order.Add("aprovado", orders.Where(x => x.StatusInternal == (Orders.StatusOder)1).ToList());
            _order.Add("cancelado", orders.Where(x => x.StatusInternal == (Orders.StatusOder)2).ToList());
            _order.Add("devolvido", orders.Where(x => x.StatusInternal == (Orders.StatusOder)3).ToList());
            _order.Add("bloqueado", orders.Where(x => x.StatusInternal == (Orders.StatusOder)4).ToList());

            return Ok(_order);
        }

        // GET: api/Orders/5
        [HttpGet("partiner")]
        [Authorize(Roles = "super, partiner")]
        public async Task<ActionResult> GetOrdersPartiner()
        {
            tks = new InitializeToken(HttpContext);

            var orders = await _context.Order.Where(x => x.GetPartiner.Id_partiner == tks._Customer_id && x.StatusInternal == 0).ToListAsync();

            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("create/{dvisit}")]
        [AllowAnonymous]
        public async Task<ActionResult<Orders>> PostOrders(int dvisit, MVOrders orders)
        {


            if (orders.orders.Status.ToLower() == "cancelado" || orders.orders.Status.ToLower() == "pagamento_devolvido")
            {
                return Ok();
            }
                // _context.Order.Add(orders);
                // await _context.SaveChangesAsync();
                await _context.Database.BeginTransactionAsync();
            try
            {
                var _partiner = await _context.Partiners.FindAsync(Guid.Parse(orders.seller));

                if(this.OrdersExists(orders.orders.Reference, _partiner.Id_partiner))
                {
                    await _context.Database.CurrentTransaction.RollbackAsync();
                    return Ok();
                }
                var _publisher = await _context.Publishers.FindAsync(Guid.Parse(orders.m8a_sid));
                //var _ofers = await _context.Ofer.Where(x => x.Partiners.Id_partiner == _partiner.Id_partiner && x.Product.Id == productsview.m8a_idproduct).FirstOrDefaultAsync();
                var _device = new DetectDevice(HttpContext);
                var totalproductspay = 0;

                var window = DateTimeOffset.FromUnixTimeMilliseconds(dvisit).DateTime;
                
                
                int daysvisity = DateTime.Compare(window, DateTime.Now);
                
                var cart = new List<OrderItem>();

                foreach (var items in orders.orders.Cart)
                {
                    Ofers _cpd = await  _context.Ofer
                                                .Include(x => x.Product)
                                                .Where(
                                                        x => x.Product.Sku == items.Sku
                                                        && x.Partiners.Id_partiner == _partiner.Id_partiner
                                                        && x.Active == true
                                                        && x.Date_start.Date <= DateTime.Now.Date
                                                        && x.Date_end.Date >= DateTime.Now.Date
                                                        )
                                                .FirstOrDefaultAsync();

                    Console.WriteLine(_cpd);
                    //return Ok(_cpd);
                    var cpc = await _context.Ofer
                                                .Include(x => x.Product)
                                                .Where(
                                                        x => x.Product.Sku == items.Sku
                                                        && x.Partiners.Id_partiner == _partiner.Id_partiner
                                                        && x.Active == true && x.Window <= 0
                                                        && x.Date_start.Date <= DateTime.Now.Date
                                                        && x.Date_end.Date >= DateTime.Now.Date
                                                        )
                                                .FirstOrDefaultAsync();

                    if (_cpd == null)
                    {
                        continue;
                    }

                    //if (daysvisity <= _cpd.Window)
                    //    continue;
                    //Console.Write(cpc);
                    decimal _rate = (_cpd.Comission / 100); //Console.Write(_rate); Console.WriteLine(items.price * _rate) ;
                    cart.Add(new OrderItem()
                    {
                        GetOfers = _cpd,
                        GetProduct = _cpd.Product,
                        GetPublisher = _publisher,
                        Price = items.price,
                        Quantity = items.Quantity,
                        Rate =(items.Quantity * items.price) * _rate,
                        Id_item = Guid.NewGuid()
                    });
                }

                var order = new Orders()
                {
                    Customer = orders.orders.Customer,
                    Date_order = DateTime.Now,
                    GetPartiner = _partiner,
                    Order_id = orders.orders.Reference,
                    GetPublisher = _publisher,
                    Payment_type = orders.orders.Paymode,
                    ProductsPay = totalproductspay,
                    Reference = orders.orders.Reference,
                    ShippingPay = decimal.Parse(orders.orders.Shipping.Replace(".",",")),
                    Site = _device.Refer,
                    Status = orders.orders.Status,
                    TotalPay = decimal.Parse(orders.orders.Totalpay.Replace(".", ",")),
                    GetItems = cart,
                    Id_order = Guid.NewGuid(),
                    Date_create = DateTime.Now,
                    Valid = (orders.orders.Status.ToLower() == "aprovado" ? true : false),
                    StatusInternal = (orders.orders.Status.ToLower() == "aprovado" ? (Orders.StatusOder)1 : (Orders.StatusOder)0)
                };

                _context.Order.Add(order);

                if (order.StatusInternal == (Orders.StatusOder) 1)
                {
                    
                    foreach (var its in cart) {
                       
                        var comission = new Comissions()
                        {
                            Id_comission = Guid.NewGuid(),
                            Blocked = false,
                            Date_create = DateTime.Now,
                            Date_pay = DateTime.Now,
                            GetOrders = order,
                            GetProducts = its.GetProduct,
                            Payed = false,
                            Values = its.Rate
                        };

                        _context.Comission.Add(comission);
                    }
                }
                //Console.WriteLine(order);
                
                await _context.SaveChangesAsync();
                await _context.Database.CurrentTransaction.CommitAsync();
                return Ok(order);
            }catch(DbException ex)
            {
                await _context.Database.CurrentTransaction.RollbackAsync();
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> DeleteOrders(Guid id)
        {
            var orders = await _context.Order.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }

            _context.Order.Remove(orders);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdersExists(string referece, Guid partiner)
        {
            return _context.Order.Any(e => e.Reference == referece && e.GetPartiner.Id_partiner == partiner);
        }
    }
}
