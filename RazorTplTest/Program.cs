using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RazorEngine;
using RazorEngine.Templating;
using RazorEngine.Configuration;
using System.IO;
using SOAModel;


namespace RazorTplTest
{
    /// <summary>
    /// Razor模板解析
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            AddRechargeOrderExample();
        }

        public static void AddRechargeOrderExample()
        {
            var config = new TemplateServiceConfiguration();
            config.EncodedStringFactory = new RazorEngine.Text.RawStringFactory();
            config.DisableTempFileLocking = true;
            config.CachingProvider = new DefaultCachingProvider(t => { });
            var service = RazorEngineService.Create(config);

            StreamReader sr = new StreamReader("simple_function_xsd_tpl.cshtml");

            string template = sr.ReadToEnd();

            FunctionModel data = new FunctionModel() { FunctionName = "AddBalanceRechargeOrder", FunctionSummary = "保存余额充值订单信息", ReturnSummary = "带订单号的余额充值订单及详情信息" };

            ParameterModel intput_p1 = new ParameterModel();
            intput_p1.ParameterName = "BalanceRechargeOrder";
            intput_p1.ParameterTypeFullName = "BalanceRechargeOrderModel";
            intput_p1.ParameterSummary = "余额充值订单信息";
            intput_p1.IsBasicCSharpType = false;
            intput_p1.IsSignleElement = true;

            ParameterModel input_p1_sub1 = new ParameterModel();
            input_p1_sub1.ParameterName = "BalanceRechargeID";
            input_p1_sub1.ParameterTypeFullName = "long";
            input_p1_sub1.ParameterSummary = "余额充值订单主键";
            ParameterModel input_p1_sub2 = new ParameterModel();
            input_p1_sub2.ParameterName = "LocalOrderID";
            input_p1_sub2.ParameterTypeFullName = "long";
            input_p1_sub2.ParameterSummary = "订单ID，显示给客户使用，由自增表生成";
            ParameterModel input_p1_sub3 = new ParameterModel();
            input_p1_sub3.ParameterName = "UID";
            input_p1_sub3.ParameterTypeFullName = "string";
            input_p1_sub3.ParameterSummary = "携程大系统UID";
            ParameterModel input_p1_sub4 = new ParameterModel();
            input_p1_sub4.ParameterName = "UserID";
            input_p1_sub4.ParameterTypeFullName = "long";
            input_p1_sub4.ParameterSummary = "福利内部用户ID";
            ParameterModel input_p1_sub5 = new ParameterModel();
            input_p1_sub5.ParameterName = "AccountID";
            input_p1_sub5.ParameterTypeFullName = "long";
            input_p1_sub5.ParameterSummary = "余额账户ID";
            ParameterModel input_p1_sub6 = new ParameterModel();
            input_p1_sub6.ParameterName = "OrderType";
            input_p1_sub6.ParameterTypeFullName = "byte";
            input_p1_sub6.ParameterSummary = "订单类型";
            ParameterModel input_p1_sub7 = new ParameterModel();
            input_p1_sub7.ParameterName = "OrderStatus";
            input_p1_sub7.ParameterTypeFullName = "byte";
            input_p1_sub7.ParameterSummary = "订单状态";
            ParameterModel input_p1_sub8 = new ParameterModel();
            input_p1_sub8.ParameterName = "PayType";
            input_p1_sub8.ParameterTypeFullName = "byte";
            input_p1_sub8.ParameterSummary = "订单支付方式";
            ParameterModel input_p1_sub9 = new ParameterModel();
            input_p1_sub9.ParameterName = "TotalCount";
            input_p1_sub9.ParameterTypeFullName = "int";
            input_p1_sub9.ParameterSummary = "总数量（子单）";
            ParameterModel input_p1_sub10 = new ParameterModel();
            input_p1_sub10.ParameterName = "TotalFaceAmount";
            input_p1_sub10.ParameterTypeFullName = "decimal";
            input_p1_sub10.ParameterSummary = "总面额";
            ParameterModel input_p1_sub11 = new ParameterModel();
            input_p1_sub11.ParameterName = "OrderAmount";
            input_p1_sub11.ParameterTypeFullName = "decimal";
            input_p1_sub11.ParameterSummary = "订单金额";
            ParameterModel input_p1_sub12 = new ParameterModel();
            input_p1_sub12.ParameterName = "PaidAmount";
            input_p1_sub12.ParameterTypeFullName = "decimal";
            input_p1_sub12.ParameterSummary = "已支付金额";
            ParameterModel input_p1_sub13 = new ParameterModel();
            input_p1_sub13.ParameterName = "IsInvoiced";
            input_p1_sub13.ParameterTypeFullName = "boolean";
            input_p1_sub13.ParameterSummary = "是否有发票";
            ParameterModel input_p1_sub14 = new ParameterModel();
            input_p1_sub14.ParameterName = "ContactName";
            input_p1_sub14.ParameterTypeFullName = "string";
            input_p1_sub14.ParameterSummary = "联系人姓名";
            ParameterModel input_p1_sub15 = new ParameterModel();
            input_p1_sub15.ParameterName = "CredentialType";
            input_p1_sub15.ParameterTypeFullName = "byte";
            input_p1_sub15.ParameterSummary = "证件类型";
            ParameterModel input_p1_sub16 = new ParameterModel();
            input_p1_sub16.ParameterName = "CredentialNo";
            input_p1_sub16.ParameterTypeFullName = "string";
            input_p1_sub16.ParameterSummary = "证件编号";
            ParameterModel input_p1_sub17 = new ParameterModel();
            input_p1_sub17.ParameterName = "MobileNo";
            input_p1_sub17.ParameterTypeFullName = "string";
            input_p1_sub17.ParameterSummary = "手机号码";
            ParameterModel input_p1_sub18 = new ParameterModel();
            input_p1_sub18.ParameterName = "ClientIP";
            input_p1_sub18.ParameterTypeFullName = "string";
            input_p1_sub18.ParameterSummary = "客户端IP";
            ParameterModel input_p1_sub19 = new ParameterModel();
            input_p1_sub19.ParameterName = "RewardBID";
            input_p1_sub19.ParameterTypeFullName = "string";
            input_p1_sub19.ParameterSummary = "奖励BID";
            ParameterModel input_p1_sub20 = new ParameterModel();
            input_p1_sub20.ParameterName = "DataChange_CreateTime";
            input_p1_sub20.ParameterTypeFullName = "dateTime";
            input_p1_sub20.ParameterSummary = "记录创建时间";
            ParameterModel input_p1_sub21 = new ParameterModel();
            input_p1_sub21.ParameterName = "DataChange_LastTime";
            input_p1_sub21.ParameterTypeFullName = "dateTime";
            input_p1_sub21.ParameterSummary = "最后更新时间";

            intput_p1.LeafPara = new List<ParameterModel>();
            intput_p1.LeafPara.Add(input_p1_sub1); intput_p1.LeafPara.Add(input_p1_sub2); intput_p1.LeafPara.Add(input_p1_sub3); intput_p1.LeafPara.Add(input_p1_sub4); intput_p1.LeafPara.Add(input_p1_sub5);
            intput_p1.LeafPara.Add(input_p1_sub6); intput_p1.LeafPara.Add(input_p1_sub7); intput_p1.LeafPara.Add(input_p1_sub8); intput_p1.LeafPara.Add(input_p1_sub9); intput_p1.LeafPara.Add(input_p1_sub10);
            intput_p1.LeafPara.Add(input_p1_sub11); intput_p1.LeafPara.Add(input_p1_sub12); intput_p1.LeafPara.Add(input_p1_sub13); intput_p1.LeafPara.Add(input_p1_sub14); intput_p1.LeafPara.Add(input_p1_sub15);
            intput_p1.LeafPara.Add(input_p1_sub16); intput_p1.LeafPara.Add(input_p1_sub17); intput_p1.LeafPara.Add(input_p1_sub18); intput_p1.LeafPara.Add(input_p1_sub19); intput_p1.LeafPara.Add(input_p1_sub20);
            intput_p1.LeafPara.Add(input_p1_sub21); 

            data.LstInputPara = new List<ParameterModel>();
            data.LstInputPara.Add(intput_p1);

            //输出参数

            ParameterModel output_p1 = new ParameterModel();
            output_p1.ParameterName = "BalanceRechargeOrder";
            output_p1.ParameterTypeFullName = "BalanceRechargeOrderModel";
            output_p1.ParameterSummary = "余额充值订单信息";
            output_p1.IsBasicCSharpType = false;
            output_p1.IsSignleElement = true;

            ParameterModel output_p2 = new ParameterModel();
            output_p2.ParameterName = "BalanceRechargeOrderDetailList";
            output_p2.ParameterTypeFullName = "BalanceRechargeOrderDetailModel";
            output_p2.ParameterSummary = "余额充值订单详情列表";
            output_p2.IsBasicCSharpType = false;
            output_p2.IsSignleElement = false;

            ParameterModel output_p2_sub1 = new ParameterModel();
            output_p2_sub1.ParameterName = "BalanceRechargeDetailID";
            output_p2_sub1.ParameterTypeFullName = "long";
            output_p2_sub1.ParameterSummary = "余额充值订单详情ID";

            ParameterModel output_p2_sub2 = new ParameterModel();
            output_p2_sub2.ParameterName = "BalanceRechargeID";
            output_p2_sub2.ParameterTypeFullName = "long";
            output_p2_sub2.ParameterSummary = "关联余额充值订单ID";

            ParameterModel output_p2_sub3 = new ParameterModel();
            output_p2_sub3.ParameterName = "FaceAmount";
            output_p2_sub3.ParameterTypeFullName = "decimal";
            output_p2_sub3.ParameterSummary = "拆分后的面额";

            ParameterModel output_p2_sub4 = new ParameterModel();
            output_p2_sub4.ParameterName = "OrderAmount";
            output_p2_sub4.ParameterTypeFullName = "decimal";
            output_p2_sub4.ParameterSummary = "拆分后的订单金额";

            ParameterModel output_p2_sub5 = new ParameterModel();
            output_p2_sub5.ParameterName = "DataChange_CreateTime";
            output_p2_sub5.ParameterTypeFullName = "dateTime";
            output_p2_sub5.ParameterSummary = "记录创建时间";

            ParameterModel output_p2_sub6 = new ParameterModel();
            output_p2_sub6.ParameterName = "DataChange_LastTime";
            output_p2_sub6.ParameterTypeFullName = "dateTime";
            output_p2_sub6.ParameterSummary = "最后更新时间";

            output_p2.LeafPara = new List<ParameterModel>();
            output_p2.LeafPara.Add(output_p2_sub1); output_p2.LeafPara.Add(output_p2_sub2); output_p2.LeafPara.Add(output_p2_sub3); output_p2.LeafPara.Add(output_p2_sub4); output_p2.LeafPara.Add(output_p2_sub5); output_p2.LeafPara.Add(output_p2_sub6);


            data.LstOutputPara = new List<ParameterModel>();
            data.LstOutputPara.Add(output_p1);
            data.LstOutputPara.Add(output_p2);

            string result = service.RunCompile(template, "templateKey", typeof(FunctionModel), data);

            Console.WriteLine(result);
        }


        public static void InputListParameterExample()
        {
            var config = new TemplateServiceConfiguration();
            config.DisableTempFileLocking = true;
            config.CachingProvider = new DefaultCachingProvider(t => { });
            var service = RazorEngineService.Create(config);

            StreamReader sr = new StreamReader("simple_function_xsd_tpl.cshtml");

            string template = sr.ReadToEnd();

            FunctionModel data = new FunctionModel() { FunctionName = "SavePersonList", FunctionSummary = "批量保存人员信息", ReturnSummary = "人员的平均年龄和列表" };

            ParameterModel intput_p1 = new ParameterModel();
            intput_p1.ParameterName = "PersonList";
            intput_p1.ParameterTypeFullName = "PersonModel";
            intput_p1.ParameterSummary = "人员信息列表";
            intput_p1.IsBasicCSharpType = false;
            intput_p1.IsSignleElement = false;

            ParameterModel input_p2_sub1 = new ParameterModel();
            input_p2_sub1.ParameterName = "Name";
            input_p2_sub1.ParameterTypeFullName = "string";
            input_p2_sub1.ParameterSummary = "姓名";
            ParameterModel input_p2_sub2 = new ParameterModel();
            input_p2_sub2.ParameterName = "Age";
            input_p2_sub2.ParameterTypeFullName = "int";
            input_p2_sub2.ParameterSummary = "年龄";

            intput_p1.LeafPara = new List<ParameterModel>();
            intput_p1.LeafPara.Add(input_p2_sub1);
            intput_p1.LeafPara.Add(input_p2_sub2);

            data.LstInputPara = new List<ParameterModel>();
            data.LstInputPara.Add(intput_p1);

            ParameterModel output_p1 = new ParameterModel();
            output_p1.ParameterName = "AvgAge";
            output_p1.ParameterTypeFullName = "decimal";
            output_p1.ParameterSummary = "人员的年龄平均值";

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
        /// 复杂参数，输出参数是用户自定义类型
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
        /// 复杂参数，输入参数是用户自定义类型
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
