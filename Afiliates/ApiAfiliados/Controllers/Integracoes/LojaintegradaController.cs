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
using RestSharp;
using ApiAfiliados.Models.HubsIntegracao.LojaIntegrada;
using System.Threading;
using Newtonsoft.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiAfiliados.Controllers.Integracoes
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class LojaintegradaController : ControllerBase
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

        public LojaintegradaController(AfiliadosContext context, IConfiguration configuration, IWebHostEnvironment env)
        {
            _context = context;
            _configuration = configuration; //instancia a configuração para pegar o secret KEY
            _context = context;
            _env = env;
            error = new ErrorReturn()
            {
                Controller = "LojaIntegrada",
            };
        }

        // GET: api/<LojaintegradaController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LojaintegradaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LojaintegradaController>
        [HttpPost]
        [Route("create/{orderid}")]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromRoute] string orderid, [FromBody] MVOrders orders)
        {

            if (orders.orders.Status.ToLower() == "cancelado" || orders.orders.Status.ToLower() == "pagamento_devolvido")
            {
                return Ok();
            }
                

            await _context.Database.BeginTransactionAsync();
            try
            {
                var _publisher = await _context.Publishers.FindAsync(Guid.Parse(orders.m8a_sid));

                if(_publisher == null)
                {
                    await _context.Database.CurrentTransaction.RollbackAsync();
                    return Ok();
                }
                var _partiner = await _context.Partiners.FindAsync(Guid.Parse(orders.seller));
                if(_partiner == null)
                {
                    await _context.Database.CurrentTransaction.RollbackAsync();
                    return Ok();
                }
                        
                if (this.OrdersExists(orders.orders.Reference, _partiner.Id_partiner))
                {
                    //update
                    await _context.Database.CurrentTransaction.RollbackAsync();
                    return Ok();
                }

                    
                //var _ofers = await _context.Ofer.Where(x => x.Partiners.Id_partiner == _partiner.Id_partiner && x.Product.Id == productsview.m8a_idproduct).FirstOrDefaultAsync();
                var _device = new DetectDevice(HttpContext);
                var totalproductspay = 0;

                var window = DateTimeOffset.FromUnixTimeMilliseconds( long.Parse(orders.m8a_visity)).DateTime;

                //*********//

                var _lojaintegrada = await _context.HubsPartinerSite.Where(x => x.GetPartiner.Id_partiner == Guid.Parse(orders.seller)).FirstOrDefaultAsync();
                if(_lojaintegrada == null)
                {
                    await _context.Database.CurrentTransaction.RollbackAsync();
                    return Ok();
                }

                string _url = String.Format("{0}pedido/{1}?format=json&chave_api={2}&chave_aplicacao={3}", _lojaintegrada.Url, orderid, _lojaintegrada.Client_key, _lojaintegrada.App_key);
                Console.WriteLine(_url);
                var client = new RestClient(_url);
                RestRequest request = new RestRequest();

                var response = await client.ExecuteGetAsync<PedidosLojaIntegrada>(request);
                var cart = new List<OrderItem>();
                int daysvisity = DateTime.Compare(window, DateTime.Now);

                if (response.IsSuccessful)
                {
                    try
                    {
                       var ret = JsonConvert.DeserializeObject<PedidosLojaIntegrada>(response.Content);
                        decimal totalorder = 0;
                        foreach (var items in ret.itens)
                        {
                            string __idproduct = items.produto.Replace("api/v1/produto/", "");
                            var cpc = await _context.Ofer
                                                    .Include(x => x.Product)
                                                    .Where(
                                                            x => (x.Product.Id == __idproduct || x.Product.Sku == items.sku )
                                                            && x.Partiners.Id_partiner == _partiner.Id_partiner
                                                            && x.Active == true 
                                                           
                                                            )
                                                    .FirstOrDefaultAsync();

                            if (cpc == null)
                            {
                                continue;
                            }

                            decimal _rate = (cpc.Comission / 100); //Console.Write(_rate); Console.WriteLine(items.price * _rate) ;
                            cart.Add(new OrderItem()
                            {
                                GetOfers = cpc,
                                GetProduct = cpc.Product,
                                GetPublisher = _publisher,
                                Price = decimal.Parse(items.preco_venda.Replace(".", ",")),
                                Quantity = decimal.Parse(items.quantidade.Replace(".",",")),
                                Rate = (decimal.Parse(items.quantidade.Replace(".", ",")) * decimal.Parse(items.preco_venda.Replace(".", ","))) * _rate,
                                Id_item = Guid.NewGuid()
                            });

                            totalorder += (decimal.Parse(items.quantidade.Replace(".", ",")) * decimal.Parse(items.preco_venda.Replace(".", ",")));

                        }

                        if(cart == null)
                        {
                            await _context.Database.CurrentTransaction.RollbackAsync();
                            return Ok();
                        }
                        
                        if (cart.Count() == 0)
                        {
                            await _context.Database.CurrentTransaction.RollbackAsync();
                            return Ok();
                        }

                        var _status = (Orders.StatusOder)(ret.situacao.aprovado ? 1 : (ret.situacao.nome == "aguardando_pagamento" ? 0 : 2) );
                        _status = (Orders.StatusOder)(ret.situacao.cancelado ? (Orders.StatusOder)3 : _status);

                        if (ret.situacao.cancelado)
                        {
                            await _context.Database.CurrentTransaction.RollbackAsync();
                            return Ok();
                        }

                        var order = new Orders()
                        {
                            Customer = ret.cliente.nome,
                            Date_order = DateTime.Now,
                            GetPartiner = _partiner,
                            Order_id = orderid,
                            GetPublisher = _publisher,
                            Payment_type = ret.pagamentos.LastOrDefault().forma_pagamento.nome,
                            ProductsPay = totalorder,
                            Reference = orderid,
                            ShippingPay = decimal.Parse(ret.valor_envio.Replace(".", ",")),
                            Site = _device.Refer,
                            Status = ret.situacao.nome,
                            TotalPay = decimal.Parse(ret.valor_total.Replace(".", ",")),
                            GetItems = cart,
                            Id_order = Guid.NewGuid(),
                            Date_create = DateTime.Now,
                            Valid = (ret.situacao.aprovado ? true : false),
                            StatusInternal = _status
                        };

                        _context.Order.Add(order);

                        if (order.StatusInternal == (Orders.StatusOder)1)
                        {

                            foreach (var its in cart)
                            {

                                var comission = new Comissions()
                                {
                                    Id_comission = Guid.NewGuid(),
                                    Blocked = false,
                                    Date_create = DateTime.Now,
                                    //Date_pay = DateTime.Now,
                                    GetOrders = order,
                                    GetProducts = its.GetProduct,
                                    Payed = false,
                                    Values = its.Rate
                                };

                                _context.Comission.Add(comission);
                            }
                        }


                        await _context.SaveChangesAsync();
                        await _context.Database.CurrentTransaction.CommitAsync();
                       
                        return Ok();


                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        await _context.Database.CurrentTransaction.RollbackAsync();
                        return BadRequest(ex.Message);
                    }
                }
                //********//
                

                }
            catch (DbException ex)
            {
                await _context.Database.CurrentTransaction.RollbackAsync();
                return BadRequest(ex.Message);
            }

            return BadRequest();
        }

        // PUT api/<LojaintegradaController>/5
        [HttpPut("{id}/{partiner}")]
        [Authorize(Roles = "super")]
        public async Task Put(Guid id, Guid partiner)
        {
            error.Controller = "Update Order";
            error.Code = "HULI02-01";
            await _context.Database.BeginTransactionAsync();
            try
            {
                var _partiner = await _context.Partiners.FindAsync(partiner);
                if (_partiner == null)
                {
                    await _context.Database.CurrentTransaction.RollbackAsync();
                    return;
                }
                error.Code = "HULI02-02";
                var _lojaintegrada = await _context.HubsPartinerSite.Where(x => x.GetPartiner.Id_partiner == partiner).FirstOrDefaultAsync();
                if (_lojaintegrada == null)
                {
                    await _context.Database.CurrentTransaction.RollbackAsync();
                    return;
                }
                error.Code = "HULI02-03";
                var orders = await _context.Order.Include(x => x.GetPublisher).Where(x => x.Id_order == id).FirstOrDefaultAsync();
                if (orders == null)
                {
                    await _context.Database.CurrentTransaction.RollbackAsync();
                    return;
                }



                var totalproductspay = 0;



                //*********//
                string _url = String.Format("{0}pedido/{1}?format=json&chave_api={2}&chave_aplicacao={3}", _lojaintegrada.Url, orders.Order_id, _lojaintegrada.Client_key, _lojaintegrada.App_key);
                Console.WriteLine(_url);
                var client = new RestClient(_url);
                RestRequest request = new RestRequest();

                var response = await client.ExecuteGetAsync<PedidosLojaIntegrada>(request);
                error.Code = "HULI02-04";
                var cart = await _context.ItemsOrder.Include(x => x.GetProduct).Where(x => x.GetOrders.Id_order == orders.Id_order).ToListAsync();
                var cartupdate = new List<OrderItem>();
                var cartadd = new List<OrderItem>();
                error.Code = "HULI02-05";
                if (response.IsSuccessful)
                {
                    try
                    {
                        error.Code = "HULI02-06";
                        var ret = JsonConvert.DeserializeObject<PedidosLojaIntegrada>(response.Content);

                        if (ret.situacao.codigo == "aguardando_pagamento")
                        {
                            await _context.Database.CurrentTransaction.RollbackAsync();
                            return;
                        }
                        error.Code = "HULI02-07";
                        List<Ofers> _cpc = new List<Ofers>();
                        decimal totalorder = 0;

                        var _status = (Orders.StatusOder)(ret.situacao.aprovado ? 1 : (ret.situacao.codigo == "aguardando_pagamento" ? 0 : 2));
                        _status = (Orders.StatusOder)(ret.situacao.cancelado ? (Orders.StatusOder)2 : _status);


                        foreach (var items in ret.itens)
                        {

                            error.Code = "HULI02-08";
                            string __idproduct = items.produto.Replace("api/v1/produto/", "");
                            var cpc = await _context.Ofer
                                                    .Include(x => x.Product)
                                                    .Where(
                                                            x => (x.Product.Id == __idproduct || x.Product.Sku == items.sku)
                                                            && x.Partiners.Id_partiner == _partiner.Id_partiner
                                                            && x.Active == true

                                                            )
                                                    .FirstOrDefaultAsync();

                            error.Code = "HULI02-09";
                            if (cpc == null)
                            {
                                continue;
                            }

                            error.Code = "HULI02-10";
                            _cpc.Add(cpc);
                            decimal _rate = (cpc.Comission / 100); //Console.Write(_rate); Console.WriteLine(items.price * _rate) ;

                            error.Code = "HULI02-11";
                            if (!cart.Any(x => x.GetProduct.Id_product == cpc.Product.Id_product))
                            {
                                error.Code = "HULI02-12";
                                cartadd.Add(new OrderItem()
                                {
                                    GetOfers = cpc,
                                    GetProduct = cpc.Product,
                                    GetPublisher = orders.GetPublisher,
                                    Price = decimal.Parse(items.preco_venda.Replace(".", ",")),
                                    Quantity = decimal.Parse(items.quantidade.Replace(".", ",")),
                                    Rate = (decimal.Parse(items.quantidade.Replace(".", ",")) * decimal.Parse(items.preco_venda.Replace(".", ","))) * _rate,
                                    Id_item = Guid.NewGuid()
                                });
                                error.Code = "HULI02-13";
                                totalorder += (decimal.Parse(items.quantidade.Replace(".", ",")) * decimal.Parse(items.preco_venda.Replace(".", ",")));
                            }
                            else
                            {
                                error.Code = "HULI02-14";
                                var tmp = cart.Where(x => x.GetProduct.Id_product == cpc.Product.Id_product).FirstOrDefault();

                                tmp.Price = decimal.Parse(items.preco_venda.Replace(".", ","));
                                tmp.Quantity = decimal.Parse(items.quantidade.Replace(".", ","));
                                tmp.Rate = (decimal.Parse(items.quantidade.Replace(".", ",")) * decimal.Parse(items.preco_venda.Replace(".", ","))) * _rate;

                                totalorder += (decimal.Parse(items.quantidade.Replace(".", ",")) * decimal.Parse(items.preco_venda.Replace(".", ",")));
                                error.Code = "HULI02-15";
                                cartupdate.Add(tmp);

                            }

                            error.Code = "HULI02-16";
                            if (ret.situacao.cancelado)
                            {
                                error.Code = "HULI02-17";
                                var comission = await _context.Comission.Where(x => x.GetOrders.Id_order == id).ToListAsync();
                                _status = (Orders.StatusOder)1;
                                if (comission != null && comission.Count() > 0)
                                {
                                    error.Code = "HULI02-18";
                                    foreach (var coms in comission)
                                    {
                                        coms.Blocked = true;
                                        coms.Blocked_decription = "Pedido cancelado";

                                    }
                                    error.Code = "HULI02-19";
                                    _context.Entry(comission).State = EntityState.Modified;
                                }
                            }

                            error.Code = "HULI02-20";
                            //if (ret.situacao.aprovado)
                            if (ret.situacao.cancelado)
                            {
                                error.Code = "HULI02-21";
                                var comission = await _context.Comission.Where(x => x.GetOrders.Id_order == id).ToListAsync();

                                error.Code = "HULI02-22";
                                if (comission == null || comission.Count() == 0)
                                {
                                    error.Code = "HULI02-23";
                                    if (cartadd != null)
                                    {
                                        foreach (var its in cartadd)
                                        {

                                            var tmp = new Comissions()
                                            {
                                                Id_comission = Guid.NewGuid(),
                                                Blocked = false,
                                                Date_create = DateTime.Now,
                                                //Date_pay = DateTime.Now,
                                                GetOrders = orders,
                                                GetProducts = its.GetProduct,
                                                Payed = false,
                                                Values = its.Rate
                                            };
                                            error.Code = "HULI02-24";
                                            _context.Comission.Add(tmp);
                                        }
                                    }
                                    error.Code = "HULI02-25";
                                    if (cartupdate != null)
                                    {
                                        foreach (var its in cartupdate)
                                        {
                                            error.Code = "HULI02-26";
                                            var tmp = new Comissions()
                                            {
                                                Id_comission = Guid.NewGuid(),
                                                Blocked = false,
                                                Date_create = DateTime.Now,
                                                //Date_pay = DateTime.Now,
                                                GetOrders = orders,
                                                GetProducts = its.GetProduct,
                                                Payed = false,
                                                Values = its.Rate
                                            };
                                            error.Code = "HULI02-27";
                                            _context.Comission.Add(tmp);
                                        }
                                    }
                                }
                                else
                                {
                                    error.Code = "HULI02-28";
                                    if (comission != null && comission.Count() > 0)
                                    {
                                        error.Code = "HULI02-29";
                                        foreach (var item in comission)
                                        {
                                            error.Code = "HULI02-30";
                                            if (cartupdate.Any(x => x.GetProduct.Id_product == item.GetProducts.Id_product))
                                                item.Values = cartupdate.Where(x => x.GetProduct.Id_product == item.GetProducts.Id_product).FirstOrDefault().Rate;
                                            error.Code = "HULI02-31";
                                            if (cartadd.Any(x => x.GetProduct.Id_product == item.GetProducts.Id_product))
                                            {
                                                item.Values = cartupdate.Where(x => x.GetProduct.Id_product == item.GetProducts.Id_product).FirstOrDefault().Rate;
                                                error.Code = "HULI02-32";
                                            }
                                            else if (!cartadd.Any(x => x.GetProduct.Id_product == item.GetProducts.Id_product))
                                            {

                                                error.Code = "HULI02-33";
                                                foreach (var its in cartadd)
                                                {
                                                    error.Code = "HULI02-34";
                                                    if (!comission.Any(x => x.GetProducts.Id_product == its.GetProduct.Id_product))
                                                    {
                                                        var tmp = new Comissions()
                                                        {
                                                            Id_comission = Guid.NewGuid(),

                                                            Blocked = false,
                                                            Date_create = DateTime.Now,
                                                            //Date_pay = DateTime.Now,
                                                            GetOrders = orders,
                                                            GetProducts = its.GetProduct,
                                                            Payed = false,
                                                            Values = its.Rate
                                                        };
                                                        error.Code = "HULI02-35";
                                                        _context.Comission.Add(tmp);
                                                    }
                                                }


                                            }


                                        }
                                        error.Code = "HULI02-36";
                                        _context.Entry(comission).State = EntityState.Modified;
                                    }
                                }
                            }
                        }







                        error.Code = "HULI02-37";
                        orders.Customer = ret.cliente.nome;
                        orders.Payment_type = ret.pagamentos.LastOrDefault().forma_pagamento.nome;
                        orders.ProductsPay = totalorder;
                        orders.ShippingPay = decimal.Parse(ret.valor_envio.Replace(".", ","));
                        orders.Status = ret.situacao.nome;
                        orders.TotalPay = decimal.Parse(ret.valor_total.Replace(".", ","));
                        orders.Valid = (ret.situacao.aprovado ? true : false);
                        orders.StatusInternal = _status;





                        error.Code = "HULI02-38";
                        await _context.SaveChangesAsync();

                        error.Code = "HULI02-39";
                        await _context.Database.CurrentTransaction.CommitAsync();

                        return;


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(error.Code);
                        await _context.Database.CurrentTransaction.RollbackAsync();
                        return;
                    }
                }
                //********//


            }
            catch (DbException ex)
            {
                Console.WriteLine(error.Code);
                Console.WriteLine(ex.Message);
                await _context.Database.CurrentTransaction.RollbackAsync();
                return;
            }

            return;
        }

        // DELETE api/<LojaintegradaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private bool OrdersExists(string referece, Guid partiner)
        {
            return _context.Order.Any(e => e.Reference == referece && e.GetPartiner.Id_partiner == partiner);
        }
    }
}
