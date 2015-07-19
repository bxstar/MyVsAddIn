using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazorTplTest
{
    public class CtripXSDModel
    {
        /// <summary>
        /// 由多个方法组成的XSD文件模型
        /// </summary>
        List<FunctionModel> LstFunc { get; set; }
    }

    public class FunctionModel
    {
        /// <summary>
        /// 方法名称
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// 方法注释
        /// </summary>
        public string FunctionSummary { get; set; }

        /// <summary>
        /// 方法的参数列表
        /// </summary>
        public List<ParameterModel> LstPara { get; set; }

        /// <summary>
        /// 函数返回的类型
        /// </summary>
        public string ReturnTypeFullName { get; set; }

        /// <summary>
        /// 函数返回的注释
        /// </summary>
        public string ReturnSummary { get; set; }
    }

    public class ParameterModel
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// 参数类型，如果是复合类型（非C#中的基本类型），还需要反射生成下面的类型定义
        /// </summary>
        public string ParameterTypeFullName { get; set; }

        /// <summary>
        /// 参数的注释
        /// </summary>
        public string ParameterSummary { get; set; }

        /// <summary>
        /// 默认是基本类型
        /// </summary>
        private Boolean _isBasicCSharpType = true;

        /// <summary>
        /// 是否是C#中的基本类型
        /// </summary>
        public Boolean IsBasicCSharpType 
        {
            get
            {
                return _isBasicCSharpType;
            }
            set
            {
                _isBasicCSharpType = value;
            }
        }

        /// <summary>
        /// 自定义参数类型的参数列表
        /// </summary>
        public List<ParameterModel> LeafPara { get; set; }
    }

}
