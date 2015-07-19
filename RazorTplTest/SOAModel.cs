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
        /// 返回注释
        /// </summary>
        public string ReturnSummary { get; set; }

        /// <summary>
        /// 输入参数列表
        /// </summary>
        public List<ParameterModel> LstInputPara { get; set; }

        /// <summary>
        /// 输出参数列表
        /// </summary>
        public List<ParameterModel> LstOutputPara { get; set; }


        /// <summary>
        /// 输入或输出参数中所有自定义类型的参数
        /// </summary>
        public List<ParameterModel> LstAllCustomPara
        {
            get
            {
                if (LstInputPara == null)
                    LstInputPara = new List<ParameterModel>();
                if (LstOutputPara == null)
                    LstOutputPara = new List<ParameterModel>();

                Dictionary<string, int> dicParaGen = new Dictionary<string, int>();//已生成的自定义类型不再生成
                List<ParameterModel> lst = new List<ParameterModel>();
                foreach (ParameterModel para in LstInputPara.Union(LstOutputPara))
                {
                    if (!para.IsBasicCSharpType && !dicParaGen.ContainsKey(para.ParameterTypeFullName))
                    {
                        dicParaGen.Add(para.ParameterTypeFullName, 1);
                        lst.Add(para);
                    }
                }

                return lst;
            }
        }
    }

    /// <summary>
    /// 参数定义
    /// </summary>
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
        /// 默认该参数是单个的
        /// </summary>
        private Boolean _isSignleElement = true;

        /// <summary>
        /// 是否是单个元素
        /// </summary>
        public Boolean IsSignleElement
        {
            get
            {
                return _isSignleElement;
            }
            set
            {
                _isSignleElement = value;
            }
        }

        /// <summary>
        /// 自定义参数类型的参数列表
        /// </summary>
        public List<ParameterModel> LeafPara { get; set; }
    }

}
