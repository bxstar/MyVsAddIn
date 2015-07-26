using System;
using System.Windows.Forms;
using EnvDTE;
using System.Reflection;
using System.IO;


namespace CtripAutoXSD
{
    public partial class VSConfig : UserControl, IDTToolsOptionsPage
    {
        public static string ConfigFullName = Assembly.GetExecutingAssembly().Location + ".config";

        public VSConfig()
        {
            InitializeComponent();
        }

        public void GetProperties(ref object PropertiesObject)
        {
            return;
        }

        /// <summary>
        /// 界面加载完后，加载配置文件内容
        /// </summary>
        /// <param name="DTEObject"></param>
        public void OnAfterCreated(DTE DTEObject)
        {
            using (StreamReader sr = new StreamReader(ConfigFullName))
            {
                txtCopyright.Text = sr.ReadToEnd();
                sr.Close();
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        public void OnCancel()
        {
            return;
        }

        /// <summary>
        /// 帮助
        /// </summary>
        public void OnHelp()
        {
            return;
        }

        /// <summary>
        /// 保存配置内容
        /// </summary>
        public void OnOK()
        {
            using (StreamWriter sw = new StreamWriter(ConfigFullName))
            {
                sw.Write(txtCopyright.Text);
                sw.Close();
            }
        }
    }
}
