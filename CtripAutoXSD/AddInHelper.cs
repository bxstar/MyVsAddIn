using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.IO;

namespace CtripAutoXSD
{
    /// <summary>
    /// 插件辅助类
    /// </summary>
    public class AddInHelper
    {
        static AddInHelper()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(location);
            ModelPath = cfa.AppSettings.Settings["ModelFath"].Value;
            SimpleFunctionXSDTpl = cfa.AppSettings.Settings["SimpleFunctionXSD_Tpl_FullName"].Value;
            CtripXSDTpl = cfa.AppSettings.Settings["CtripXSD_Tpl_FullName"].Value;
            OutPutXSDFullName = cfa.AppSettings.Settings["OutPutXSDFullName"].Value;
        }

        /// <summary>
        /// 参数所属的Model路径，下面可以有多个dll，优先加载文件名有Model的dll
        /// </summary>
        public static string ModelPath
        {
            get;
            set;
        }

        /// <summary>
        /// 单个函数的模版带路径文件名
        /// </summary>
        public static string SimpleFunctionXSDTpl
        {
            get;
            set;
        }

        /// <summary>
        /// SOA定义文件的模版带路径名
        /// </summary>
        public static string CtripXSDTpl
        {
            get;
            set;
        }

        /// <summary>
        /// 输出的XSD文件名带路径
        /// </summary>
        public static string OutPutXSDFullName
        {
            get;
            set;
        }

        /// <summary>
        /// 图标
        /// </summary>
        public static string CtripICO
        {
            get
            {
                string location = Assembly.GetExecutingAssembly().Location;
                string baseDir = Path.GetDirectoryName(location);
                string str = Path.Combine(baseDir, "ctrip.png");
                return str;
            }
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        /// <returns></returns>
        public static string Read()
        {
            try
            {
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="content"></param>
        public static void Save(string content)
        {

        }
    }
}
