using RazorEngine.Configuration;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CtripAutoXSD
{
    public class Utils
    {
        /// <summary>
        /// 从CSharp类型转换为XSD类型
        /// </summary>
        public static string GetXSDTypeFromCSharpType(string typeName)
        {
            string result = string.Empty;
            if (typeName.Contains("<"))
            {//List<T>或Nullable<T>类型，取出T
                typeName = GetTypeFullName(typeName);
            }
            else if(typeName.Contains("[["))
            {//Nullable[[System.Int64,
                typeName = typeName.Substring(typeName.IndexOf('[') + 2, (typeName.IndexOf(',') - typeName.IndexOf('[')) - 2);
            }

            if (typeName.Contains("."))
            {//去掉前面的命名空间
                if (IsCSharpBasicType(typeName))
                    typeName = typeName.Substring(typeName.LastIndexOf('.') + 1).ToLower();
                else//自定义类型，不需要小写
                    typeName = typeName.Substring(typeName.LastIndexOf('.') + 1);
            }

            if (typeName.ToLower() == "int32")
            {
                result = "int";
            }
            else if (typeName.ToLower() == "int64")
            {
                result = "long";
            }
            else if (typeName.ToLower() == "datetime")
            {
                result = "dateTime";
            }
            else
            {
                if (IsCSharpBasicType(typeName))//基本类型，第一个字母小写，如：String -> string
                    result = typeName.First().ToString().ToLower() + typeName.Substring(1);
                else
                    result = typeName;
            }

            return result;
        }

        /// <summary>
        /// 是否是C#基本类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static Boolean IsCSharpBasicType(string typeName)
        {
            if (
                typeName.ToLower().EndsWith("model") || typeName.ToLower().EndsWith("entity")
                || typeName.ToLower().EndsWith("model>") || typeName.ToLower().EndsWith("entity>"))
            {
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// 是否是可为null的类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static Boolean IsNillableType(string typeName)
        {
            if (typeName.ToLower().Contains("nullable") || typeName.ToLower().Contains("nillable"))
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 在Dll的目录中获取指定类型
        /// </summary>
        /// <returns></returns>
        public static Type GetType(string typeName)
        {
            string typeFullName = GetTypeFullName(typeName);
            string dllPath = AddInHelper.ModelPath;
            var files = Directory.GetFiles(dllPath);
            List<string> lstFile = new List<string>();
            //先使用Model和Entity名称的dll
            lstFile.AddRange(files.Where(o => o.Substring(o.LastIndexOf('\\') + 1).Contains("Model")));
            lstFile.AddRange(files.Where(o => o.Substring(o.LastIndexOf('\\') + 1).Contains("Entity")));
            lstFile.AddRange(files.Where(o => !o.Substring(o.LastIndexOf('\\') + 1).Contains("Entity") && !o.Substring(o.LastIndexOf('\\') + 1).Contains("Model")));
            foreach (var item in lstFile)
            {
                var asm = System.Reflection.Assembly.LoadFile(item);
                Type t = asm.GetType(typeFullName);
                if (t != null)
                    return t;
            }
            return null;
        }

        /// <summary>
        /// 用于程序集反射
        /// </summary>
        /// <param name="typeName">类型名，可能是List类型</param>
        /// <returns></returns>
        public static string GetTypeFullName(string typeName)
        {
            if (!typeName.Contains("List") && !typeName.Contains("Tuple") && !typeName.Contains("<"))
            {
                return typeName;
            }
            else
            {
                if (typeName.Contains("<<") || typeName.Contains(">>"))
                    typeName.Replace("<<", "<").Replace(">>", ">");
                    
                return typeName.Substring(typeName.IndexOf('<') + 1, (typeName.IndexOf('>') - typeName.IndexOf('<')) - 1);
            }
        }

        /// <summary>
        /// 将数据写入模版
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string WriteTemplateData(Object data)
        {
            string strErrorMsg = null;
            string tplFile = AddInHelper.SimpleFunctionXSDTpl;

            try
            {
                //Razor引擎载入模版，不进行Html编码
                var config = new TemplateServiceConfiguration();
                config.EncodedStringFactory = new RazorEngine.Text.RawStringFactory();
                config.DisableTempFileLocking = true;
                config.CachingProvider = new DefaultCachingProvider(t => { });
                var service = RazorEngineService.Create(config);
                using (StreamReader sr = new StreamReader(tplFile))
                {
                    string template = sr.ReadToEnd();
                    sr.Close();
                    string xsd = service.RunCompile(template, "templateKey", data.GetType(), data);
                    using (StreamWriter sw = new StreamWriter("D:\\xsd.xml"))
                    {
                        sw.Write(xsd);
                        sw.Close();
                    }
                }
            }
            catch (Exception se)
            {
                strErrorMsg = se.Message;
            }
            return strErrorMsg;
        }
    }
}
