using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AbandonedCart.Controllers
{
    public class GetMVCController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public GetMVCController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
       

        public IActionResult Index()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;

            string result = @"var $guid = " + Guid.NewGuid().ToString();
            result += System.IO.File.ReadAllText(webRootPath + "initcustomer/initfile.js");

            return new JavaScriptResult(result);
        }
    }

    public class JavaScriptResult : ContentResult
    {
        public JavaScriptResult(string script)
        {
            this.Content = script;
            this.ContentType = "application/javascript";
        }
    }
}