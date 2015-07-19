using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RazorEngine;
using RazorEngine.Templating;
using RazorEngine.Configuration;
using System.IO;


namespace RazorTplTest
{
    /// <summary>
    /// Razor模板解析
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            ComplexParameterExample();
        }

        /// <summary>
        /// 复杂参数，参数是用户自定义类型
        /// </summary>
        public static void ComplexParameterExample()
        {
            var config = new TemplateServiceConfiguration();
            config.DisableTempFileLocking = true;
            config.CachingProvider = new DefaultCachingProvider(t => { });
            var service = RazorEngineService.Create(config);

            StreamReader sr = new StreamReader("simple_function_xsd_tpl.cshtml");

            string template = sr.ReadToEnd();

            FunctionModel data = new FunctionModel() { FunctionName = "AddPersonAge", FunctionSummary = "增加人员的年龄", ReturnTypeFullName = "int", ReturnSummary = "增加年龄后的人员信息" };

            ParameterModel p1 = new ParameterModel();
            p1.ParameterName = "num1";
            p1.ParameterTypeFullName = "int";
            p1.ParameterSummary = "数字1";

            ParameterModel p2 = new ParameterModel();
            p2.ParameterName = "person";
            p2.ParameterTypeFullName = "PersonModel";
            p2.ParameterSummary = "人员实体";
            p2.IsBasicCSharpType = false;

            ParameterModel p2_sub1 = new ParameterModel();
            p2_sub1.ParameterName = "Name";
            p2_sub1.ParameterTypeFullName = "string";
            p2_sub1.ParameterSummary = "姓名";
            ParameterModel p2_sub2 = new ParameterModel();
            p2_sub2.ParameterName = "Age";
            p2_sub2.ParameterTypeFullName = "int";
            p2_sub2.ParameterSummary = "年龄";
            
            p2.LeafPara = new List<ParameterModel>();
            p2.LeafPara.Add(p2_sub1);
            p2.LeafPara.Add(p2_sub2);

            data.LstPara = new List<ParameterModel>();
            data.LstPara.Add(p1);
            data.LstPara.Add(p2);



            string result = service.RunCompile(template, "templateKey", typeof(FunctionModel), data);

            Console.WriteLine(result);
        }

        /// <summary>
        /// 简单参数示例
        /// </summary>
        public static void SimpleParameterExample()
        {
            var config = new TemplateServiceConfiguration();
            config.DisableTempFileLocking = true;
            config.CachingProvider = new DefaultCachingProvider(t => { });
            var service = RazorEngineService.Create(config);

            StreamReader sr = new StreamReader("simple_function_xsd_tpl.cshtml");

            string template = sr.ReadToEnd();

            FunctionModel data = new FunctionModel() { FunctionName = "AddNumber", FunctionSummary = "将两个数字相加", ReturnTypeFullName = "int", ReturnSummary = "数字相加之和" };

            ParameterModel p1 = new ParameterModel();
            p1.ParameterName = "num1";
            p1.ParameterTypeFullName = "int";
            p1.ParameterSummary = "数字1";

            ParameterModel p2 = new ParameterModel();
            p2.ParameterName = "num2";
            p2.ParameterTypeFullName = "int";
            p2.ParameterSummary = "数字2";


            data.LstPara = new List<ParameterModel>();
            data.LstPara.Add(p1);
            data.LstPara.Add(p2);



            string result = service.RunCompile(template, "templateKey", typeof(FunctionModel), data);

            Console.WriteLine(result);
        }

        /// <summary>
        /// 基本模版示例
        /// </summary>
        public static void BasicExample()
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
