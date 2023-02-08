using ApiAfiliados.Classes;
using HashesLibrary.Classes;
using InfraAfiliados;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HashesLibrary.Classes.HashCrypty;

namespace ApiAfiliados.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class FilesController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        private readonly AfiliadosContext _context;

        public FilesController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment, AfiliadosContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _context = context;
        }

        [Route("files/{id}.js")]
        public IActionResult Index(string id, string plataforma = "m8")
        {
            string result = "";
            var _shorts = new HashAes();


            string d64 = "";
            try { d64 = _shorts.Base64Decode(id + "="); } catch { d64 = _shorts.Base64Decode(id + "=="); }

            string encode = _shorts.Md5Decode(d64);

            var ids = encode.Split("/");

            try
            {
                //var _partiner = _context.Partiners.Find(Guid.Parse(id));
                plataforma = ids[0];
                string webRootPath = _hostingEnvironment.WebRootPath;

                //string referer = Request.Headers["Referer"].ToString();
                string _hostAddr = "http" + ((HttpContext.Request.IsHttps) ? "s" : "") + "://" + HttpContext.Request.Host.ToString() + "";
                //_hostAddr = "http"+ (( HttpContext.Request.IsHttps)? "s":"" ) + "://"+ "localhost:5001";
                var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                result = @"$m8a_guid = '" + ids[1] + @"'; $m8a_loader = '" + _hostAddr + "'; $m8a_type = 'cpc'; $m8a_page = 'inicio'; $dvidity='"+ Timestamp.ToString()+ "';";
                //string plataforma = _partiner.Platform.ToLower();

           
                result += System.IO.File.ReadAllText(webRootPath + "/js/" + plataforma + ".js");
                result += System.IO.File.ReadAllText(webRootPath + "/js/m8afiliados.js");
            }
            catch(Exception ex)
            {
                result += ex.Message;
                result += ex.InnerException;
            }
            return new JavaScriptResults(result, (_configuration["live"].ToLower() == "true" ? true : false));
        }
            
        
    }
}
