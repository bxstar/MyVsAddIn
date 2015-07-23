using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CtripAutoXSD
{
    /// <summary>
    /// 插件辅助类
    /// </summary>
    public class AddInHelper
    {
        static AddInHelper()
        {
            string ModelFath = ConfigurationManager.AppSettings["ModelFath"];
            //string location = Assembly.GetExecutingAssembly().Location;
            //string baseDir = Path.GetDirectoryName(location);
            //configXml = Path.Combine(baseDir, "VisualSPlusAddIn.xml");
        }

        /// <summary>
        /// 参数所属的Model路径，下面可以有多个dll，优先加载文件名有Model的dll
        /// </summary>
        public static string ModelPath
        {
            get
            {
                return ConfigurationManager.AppSettings["ModelFath"];
            }
            set
            {
                ConfigurationManager.AppSettings["ModelFath"] = value;
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
