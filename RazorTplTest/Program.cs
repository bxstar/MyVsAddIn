using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RazorEngine;
using RazorEngine.Templating;
using RazorEngine.Configuration;


namespace RazorTplTest
{
    /// <summary>
    /// Razor模板解析
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //RazorEngine3.3版本写法
            //string template = "Hello @Model.Name, welcome to RazorEngine!";
            //string result = Razor.Parse(template, new { Name = "World" });

            //RazorEngine3.7版本以上写法
            var config = new TemplateServiceConfiguration();
            config.DisableTempFileLocking = true;
            config.CachingProvider = new DefaultCachingProvider(t => { });
            var service = RazorEngineService.Create(config);
            string template = "Hello @Model.Name, welcome to RazorEngine!";
            var result =
                service.RunCompile(template, "templateKey", null, new { Name = "World" });

            var result2 =
                service.Run("templateKey", null, new { Name = "Max" });

            Console.WriteLine(result);

            Console.WriteLine(result2);
        }
    }
}
