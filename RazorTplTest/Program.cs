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

            ReturnListParameterExample();
        }

        /// <summary>
        /// 复杂参数，参数是用户自定义类型
        /// </summary>
        public static void ReturnListParameterExample()
        {
            var config = new TemplateServiceConfiguration();
            config.DisableTempFileLocking = true;
            config.CachingProvider = new DefaultCachingProvider(t => { });
            var service = RazorEngineService.Create(config);

            StreamReader sr = new StreamReader("simple_function_xsd_tpl.cshtml");

            string template = sr.ReadToEnd();

            FunctionModel data = new FunctionModel() { FunctionName = "GetAllPerson", FunctionSummary = "获取人员信息", ReturnSummary = "人员信息的数量和列表" };

            ParameterModel intput_p1 = new ParameterModel();
            intput_p1.ParameterName = "num1";
            intput_p1.ParameterTypeFullName = "int";
            intput_p1.ParameterSummary = "数字1";

            data.LstInputPara = new List<ParameterModel>();
            data.LstInputPara.Add(intput_p1);

            ParameterModel output_p1 = new ParameterModel();
            output_p1.ParameterName = "Result";
            output_p1.ParameterTypeFullName = "int";
            output_p1.ParameterSummary = "人员数量";

            ParameterModel output_p2 = new ParameterModel();
            output_p2.ParameterName = "PersonList";
            output_p2.ParameterTypeFullName = "PersonModel";
            output_p2.ParameterSummary = "人员信息列表";
            output_p2.IsBasicCSharpType = false;
            output_p2.IsSignleElement = false;

            ParameterModel output_p2_sub1 = new ParameterModel();
            output_p2_sub1.ParameterName = "Name";
            output_p2_sub1.ParameterTypeFullName = "string";
            output_p2_sub1.ParameterSummary = "姓名";
            ParameterModel output_p2_sub2 = new ParameterModel();
            output_p2_sub2.ParameterName = "Age";
            output_p2_sub2.ParameterTypeFullName = "int";
            output_p2_sub2.ParameterSummary = "年龄";

            output_p2.LeafPara = new List<ParameterModel>();
            output_p2.LeafPara.Add(output_p2_sub1);
            output_p2.LeafPara.Add(output_p2_sub2);



            data.LstOutputPara = new List<ParameterModel>();
            data.LstOutputPara.Add(output_p1);
            data.LstOutputPara.Add(output_p2);

            string result = service.RunCompile(template, "templateKey", typeof(FunctionModel), data);

            Console.WriteLine(result);
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

            FunctionModel data = new FunctionModel() { FunctionName = "AddPersonAge", FunctionSummary = "增加人员的年龄", ReturnSummary = "增加后的人员信息" };

            ParameterModel intput_p1 = new ParameterModel();
            intput_p1.ParameterName = "num1";
            intput_p1.ParameterTypeFullName = "int";
            intput_p1.ParameterSummary = "数字1";

            ParameterModel intput_p2 = new ParameterModel();
            intput_p2.ParameterName = "Person";
            intput_p2.ParameterTypeFullName = "PersonModel";
            intput_p2.ParameterSummary = "人员实体";
            intput_p2.IsBasicCSharpType = false;

            ParameterModel intput_p2_sub1 = new ParameterModel();
            intput_p2_sub1.ParameterName = "Name";
            intput_p2_sub1.ParameterTypeFullName = "string";
            intput_p2_sub1.ParameterSummary = "姓名";
            ParameterModel intput_p2_sub2 = new ParameterModel();
            intput_p2_sub2.ParameterName = "Age";
            intput_p2_sub2.ParameterTypeFullName = "int";
            intput_p2_sub2.ParameterSummary = "年龄";
            
            intput_p2.LeafPara = new List<ParameterModel>();
            intput_p2.LeafPara.Add(intput_p2_sub1);
            intput_p2.LeafPara.Add(intput_p2_sub2);

            data.LstInputPara = new List<ParameterModel>();
            data.LstInputPara.Add(intput_p1);
            data.LstInputPara.Add(intput_p2);


            ParameterModel output_p1 = new ParameterModel();
            output_p1.ParameterName = "IsPersonOlder";
            output_p1.ParameterTypeFullName = "boolean";
            output_p1.ParameterSummary = "是否老年人";

            ParameterModel output_p2 = new ParameterModel();
            output_p2.ParameterName = "Person";
            output_p2.ParameterTypeFullName = "PersonModel";
            output_p2.ParameterSummary = "人员实体";
            output_p2.IsBasicCSharpType = false;

            //输入参数已经给PersonModel作了定义，输出参数不用在定义
            data.LstOutputPara = new List<ParameterModel>();
            data.LstOutputPara.Add(output_p1);
            data.LstOutputPara.Add(output_p2);

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

            FunctionModel data = new FunctionModel() { FunctionName = "AddNumber", FunctionSummary = "将两个数字相加" };

            ParameterModel p1 = new ParameterModel();
            p1.ParameterName = "num1";
            p1.ParameterTypeFullName = "int";
            p1.ParameterSummary = "数字1";

            ParameterModel p2 = new ParameterModel();
            p2.ParameterName = "num2";
            p2.ParameterTypeFullName = "int";
            p2.ParameterSummary = "数字2";


            data.LstInputPara = new List<ParameterModel>();
            data.LstInputPara.Add(p1);
            data.LstInputPara.Add(p2);



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
