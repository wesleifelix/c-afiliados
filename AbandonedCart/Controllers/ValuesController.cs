using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AbandonedCart_Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AbandonedCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHostingEnvironment _env;

        public ValuesController(IHttpClientFactory clientFactory, IHostingEnvironment env)
        {
            _clientFactory = clientFactory;
            _env = env;
            
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<Products_GoogleShop.Channel>> Get()
        {
            var listProducts = new Products_GoogleShop();

            var request = new HttpRequestMessage(HttpMethod.Get,
             "http://www.francaboots.com.br/googleshopping-s1-br-BRL.xml");
            
            var client = _clientFactory.CreateClient();
            //"productsjson"
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Stream requestStream =await response.Content.ReadAsStreamAsync();

                var teste = await response.Content.ReadAsStringAsync();
                TextReader tx = new StreamReader(await response.Content.ReadAsStreamAsync());
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Products_GoogleShop.Rss));
                var resp =  (Products_GoogleShop.Rss) serializer.Deserialize(requestStream);
                /*listProducts = await response.Content
                .ReadAsAsync<Products_GoogleShop.rss>();*/
                string name = resp.Channel.Author.Name.Replace(" ", "_");
                string directory = Path.Combine(_env.WebRootPath + "\\productsjson\\", name);
               

                if (!System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }

                foreach(var item in resp.Channel.Item)
                {
                    string file = Path.Combine(directory, item.Id.ToString()+  DateTime.Now.ToUniversalTime().ToString("HHmmss")+ ".json");

                    if (!System.IO.File.Exists(file))
                    {
                        var itemstring = item;
                        // Create a file to write to.
                        using (StreamWriter sw = System.IO.File.CreateText(file))
                        {
                            sw.WriteLine( JsonConvert.SerializeObject(item).ToString());
                        }

                        /*
                        // Open the file to read from.
                        using (StreamReader sr = System.IO.File.OpenText(file))
                        {
                            string s = "";
                            while ((s = sr.ReadLine()) != null)
                            {
                                Console.WriteLine(s);
                            }
                        }
                        */
                    }

                }



                return resp.Channel;
            }
           

            return null;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
