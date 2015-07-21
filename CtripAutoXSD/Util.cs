using System;
using System.Collections.Generic;
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
                typeName = typeName.Substring(typeName.IndexOf('<') + 1, (typeName.IndexOf('>') - typeName.IndexOf('<')) - 1);
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
        /// 用于程序集反射
        /// </summary>
        /// <param name="typeName">类型名，可能是List类型</param>
        /// <returns></returns>
        public static string GetTypeFullName(string typeName)
        {
            if (!typeName.Contains("List"))
            {
                return typeName;
            }
            else
            {
                return typeName.Substring(typeName.IndexOf('<') + 1, (typeName.IndexOf('>') - typeName.IndexOf('<')) - 1);   
            }
        }
    }
}
