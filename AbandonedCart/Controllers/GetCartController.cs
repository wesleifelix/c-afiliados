using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AbandonedCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetCartController : ControllerBase
    {
        public ActionResult MyScript()
        {
            return Content("var greeting = \"Hello World!\";");
        }


    }
}