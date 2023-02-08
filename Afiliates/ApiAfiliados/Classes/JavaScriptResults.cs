using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAfiliados.Classes
{
    public class JavaScriptResults : ContentResult
    {
        
        public JavaScriptResults(string script, bool minify = false)
        {
            //this.Content = NUglify.Uglify.Js(script).ToString();
            

            if (minify)
            {
                this.Content = NUglify.Uglify.Js(script).ToString();
            }
            else{
                this.Content =script;
            }
            this.ContentType = "application/javascript";
        }
    }
}
