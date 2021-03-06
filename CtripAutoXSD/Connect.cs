using System;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using EnvDTE90;
using EnvDTE100;
using Microsoft.VisualStudio.CommandBars;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Windows.Forms;
using RazorEngine.Templating;
using System.IO;
using RazorEngine.Configuration;
using System.Collections.Generic;
using SOAModel;

namespace CtripAutoXSD
{
    /// <summary>用于实现外接程序的对象。</summary>
    /// <seealso class='IDTExtensibility2' />
    public class Connect : IDTExtensibility2, IDTCommandTarget
    {
        /// <summary>实现外接程序对象的构造函数。请将您的初始化代码置于此方法内。</summary>
        public Connect()
        {
        }

        /// <summary>实现 IDTExtensibility2 接口的 OnConnection 方法。接收正在加载外接程序的通知。</summary>
        /// <param term='application'>宿主应用程序的根对象。</param>
        /// <param term='connectMode'>描述外接程序的加载方式。</param>
        /// <param term='addInInst'>表示此外接程序的对象。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
        {
            _applicationObject = (DTE2)application;
            _addInInstance = (AddIn)addInInst;
            if (connectMode == ext_ConnectMode.ext_cm_UISetup)
            {
                object[] contextGUIDS = new object[] { };
                Commands2 commands = (Commands2)_applicationObject.Commands;
                string toolsMenuName = "Tools";

                //将此命令置于“工具”菜单上。
                //查找 MenuBar 命令栏，该命令栏是容纳所有主菜单项的顶级命令栏:
                Microsoft.VisualStudio.CommandBars.CommandBar menuBarCommandBar = ((Microsoft.VisualStudio.CommandBars.CommandBars)_applicationObject.CommandBars)["MenuBar"];

                //在 MenuBar 命令栏上查找“工具”命令栏:
                CommandBarControl toolsControl = menuBarCommandBar.Controls[toolsMenuName];
                CommandBarPopup toolsPopup = (CommandBarPopup)toolsControl;

                //如果希望添加多个由您的外接程序处理的命令，可以重复此 try/catch 块，
                //  只需确保更新 QueryStatus/Exec 方法，使其包含新的命令名。
                try
                {
                    
                    System.Drawing.Bitmap bp = new System.Drawing.Bitmap(AddInHelper.CtripICO);
                    //将一个命令添加到 Commands 集合:
                    Command command = commands.AddNamedCommand2(_addInInstance, "CtripAutoXSD", "CtripAutoXSD", "Executes the command for CtripAutoXSD", false, bp, ref contextGUIDS, (int)vsCommandStatus.vsCommandStatusSupported + (int)vsCommandStatus.vsCommandStatusEnabled, (int)vsCommandStyle.vsCommandStylePictAndText, vsCommandControlType.vsCommandControlTypeButton);

                    //将对应于该命令的控件添加到“工具”菜单:
                    if ((command != null) && (toolsPopup != null))
                    {
                        command.AddControl(toolsPopup.CommandBar, 1);
                    }
                }
                catch (System.ArgumentException)
                {
                    //如果出现此异常，原因很可能是由于具有该名称的命令
                    //  已存在。如果确实如此，则无需重新创建此命令，并且
                    //  可以放心忽略此异常。
                }
            }
        }

        /// <summary>实现 IDTExtensibility2 接口的 OnDisconnection 方法。接收正在卸载外接程序的通知。</summary>
        /// <param term='disconnectMode'>描述外接程序的卸载方式。</param>
        /// <param term='custom'>特定于宿主应用程序的参数数组。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
        {
        }

        /// <summary>实现 IDTExtensibility2 接口的 OnAddInsUpdate 方法。当外接程序集合已发生更改时接收通知。</summary>
        /// <param term='custom'>特定于宿主应用程序的参数数组。</param>
        /// <seealso class='IDTExtensibility2' />		
        public void OnAddInsUpdate(ref Array custom)
        {
        }

        /// <summary>实现 IDTExtensibility2 接口的 OnStartupComplete 方法。接收宿主应用程序已完成加载的通知。</summary>
        /// <param term='custom'>特定于宿主应用程序的参数数组。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnStartupComplete(ref Array custom)
        {
        }

        /// <summary>实现 IDTExtensibility2 接口的 OnBeginShutdown 方法。接收正在卸载宿主应用程序的通知。</summary>
        /// <param term='custom'>特定于宿主应用程序的参数数组。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnBeginShutdown(ref Array custom)
        {
        }

        /// <summary>实现 IDTCommandTarget 接口的 QueryStatus 方法。此方法在更新该命令的可用性时调用</summary>
        /// <param term='commandName'>要确定其状态的命令的名称。</param>
        /// <param term='neededText'>该命令所需的文本。</param>
        /// <param term='status'>该命令在用户界面中的状态。</param>
        /// <param term='commandText'>neededText 参数所要求的文本。</param>
        /// <seealso class='Exec' />
        public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
        {
            if (neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
            {
                if (commandName == "CtripAutoXSD.Connect.CtripAutoXSD")
                {
                    status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
                    return;
                }
            }
        }

        /// <summary>实现 IDTCommandTarget 接口的 Exec 方法。此方法在调用该命令时调用。</summary>
        /// <param term='commandName'>要执行的命令的名称。</param>
        /// <param term='executeOption'>描述该命令应如何运行。</param>
        /// <param term='varIn'>从调用方传递到命令处理程序的参数。</param>
        /// <param term='varOut'>从命令处理程序传递到调用方的参数。</param>
        /// <param term='handled'>通知调用方此命令是否已被处理。</param>
        /// <seealso class='Exec' />
        public void Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
        {
            //参考：http://edc.tversu.ru/elib/inf/0056/0735618747_idaw44u.html
            //https://msdn.microsoft.com/en-us/library/vstudio/tz746te4%28v=vs.110%29.aspx 递归获取CodeElement
            //https://msdn.microsoft.com/en-us/library/vstudio/0ykehh3k%28v=vs.110%29.aspx

            handled = false;
            if (executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
            {
                if (commandName == "CtripAutoXSD.Connect.CtripAutoXSD")
                {
                    FunctionModel data = new FunctionModel();

                    //获取选择的文本对象，方法名称和参数列表
                    TextSelection selectedText = _applicationObject.ActiveDocument.Selection as TextSelection;
                    string resultFuncTpl = "methodName:{0},{1}";
                    CodeElement func = selectedText.ActivePoint.get_CodeElement(vsCMElement.vsCMElementFunction);

                    if (func != null)
                    {//选中的对象，不为空
                        string result = string.Format(resultFuncTpl, func.Name, func.FullName + func.ProjectItem.Name);
                        data.FunctionName = func.Name;

                        string resultParamTpl = "paramName:{0},{1}";
                        CodeFunction funcType = func as CodeFunction;

                        //输入参数
                        foreach (CodeElement param in funcType.Parameters)
                        {
                            CodeParameter paramType = param as CodeParameter;
                            result = string.Format(resultParamTpl, param.Name, paramType.Type.AsFullName);
                            System.Diagnostics.Debug.WriteLine(result);

                            ParameterModel input = new ParameterModel();
                            input.ParameterName = param.Name;
                            string typeName = paramType.Type.AsFullName;
                            if (Utils.IsCSharpBasicType(typeName))
                            {//基本CSharp类型
                                input.IsBasicCSharpType = true;
                            }
                            else
                            {//自定义类型，需要反射计算其定义
                                input.IsBasicCSharpType = false;
                                Type t = Utils.GetType(typeName);
                                if (t == null)
                                {
                                    MessageBox.Show("未找到类型：" + typeName + "，请重新设置Model程序集路径");
                                    return;
                                }
                                foreach (PropertyInfo pi in t.GetProperties())
                                {
                                    System.Diagnostics.Debug.WriteLine(pi.PropertyType.Name + ":" + pi.Name);
                                    ParameterModel input_sub = new ParameterModel();
                                    input_sub.ParameterName = pi.Name;
                                    if (!Utils.IsCSharpBasicType(pi.PropertyType.Name))
                                        input_sub.IsBasicCSharpType = false;
                                    else
                                        input_sub.IsBasicCSharpType = true;
                                    input_sub.IsNillable = Utils.IsNillableType(pi.PropertyType.Name);
                                    if (!input_sub.IsNillable)
                                        input_sub.ParameterTypeFullName = Utils.GetXSDTypeFromCSharpType(pi.PropertyType.Name);
                                    else
                                        input_sub.ParameterTypeFullName = Utils.GetXSDTypeFromCSharpType(pi.PropertyType.FullName);
                                    if (pi.PropertyType.Name.Contains("List"))
                                    {
                                        input_sub.IsSignleElement = false;
                                    }
                                    else
                                    {
                                        input_sub.IsSignleElement = true;
                                    }
                                    if (input.LeafPara == null) input.LeafPara = new List<ParameterModel>();
                                    input.LeafPara.Add(input_sub);
                                }
                            }

                            input.IsNillable = Utils.IsNillableType(typeName);
                            input.ParameterTypeFullName = Utils.GetXSDTypeFromCSharpType(typeName);
                            if (typeName.Contains("List"))
                            {
                                input.IsSignleElement = false;
                            }
                            else
                            {
                                input.IsSignleElement = true;
                            }
                            if (data.LstInputPara == null) data.LstInputPara = new List<ParameterModel>();

                            data.LstInputPara.Add(input);
                        }

                        #region 输出参数，使用Tuple支持多个

                        List<string> lstOutPutTypeName = new List<string>();
                        Boolean isTuple = funcType.Type.AsFullName.Contains("Tuple");

                        if (!isTuple)
                        {//一个参数
                            lstOutPutTypeName.Add(funcType.Type.AsFullName);
                        }
                        else
                        {//多个参数
                            string tupleName = funcType.Type.AsFullName;
                            //注意Tuple有两个>>
                            lstOutPutTypeName.AddRange(Utils.GetTypeFullName(tupleName).Split(','));
                        }
                        int outputParaIndex = 1;
                        foreach (string outputTypeName in lstOutPutTypeName)
                        {
                            ParameterModel output = new ParameterModel();
                            output.IsNillable = Utils.IsNillableType(outputTypeName);
                            output.ParameterTypeFullName = Utils.GetXSDTypeFromCSharpType(outputTypeName);

                            if (Utils.IsCSharpBasicType(outputTypeName))
                            {//C#基本类型
                                output.IsBasicCSharpType = true;
                                if (outputTypeName.Contains("List"))
                                {
                                    output.IsSignleElement = false;
                                }
                                else
                                {
                                    output.IsSignleElement = true;
                                }
                                if (!isTuple)
                                    output.ParameterName = "ReturnValue";
                                else
                                    output.ParameterName = "ReturnValue" + outputParaIndex++;
                            }
                            else
                            {//C#自定义类型
                                output.IsBasicCSharpType = false;
                                if (outputTypeName.Contains("List"))
                                {
                                    output.IsSignleElement = false;
                                    output.ParameterName = "Return" + output.ParameterTypeFullName.Replace("Model", "").Replace("Entity", "") + "List";
                                }
                                else
                                {
                                    output.IsSignleElement = true;
                                    output.ParameterName = "Return" + output.ParameterTypeFullName.Replace("Model", "").Replace("Entity", "");
                                }

                                Type tOutPut = Utils.GetType(outputTypeName);
                                if (tOutPut == null)
                                {
                                    MessageBox.Show("未找到类型：" + outputTypeName + "，请重新设置Model程序集路径");
                                    return;
                                }
                                foreach (PropertyInfo pi in tOutPut.GetProperties())
                                {
                                    System.Diagnostics.Debug.WriteLine(pi.PropertyType.Name + ":" + pi.Name);
                                    ParameterModel output_sub = new ParameterModel();
                                    output_sub.ParameterName = pi.Name;
                                    if (!Utils.IsCSharpBasicType(pi.PropertyType.Name))
                                        output_sub.IsBasicCSharpType = false;
                                    else
                                        output_sub.IsBasicCSharpType = true;
                                    output_sub.IsNillable = Utils.IsNillableType(pi.PropertyType.Name);
                                    if (!output_sub.IsNillable)
                                        output_sub.ParameterTypeFullName = Utils.GetXSDTypeFromCSharpType(pi.PropertyType.Name);
                                    else
                                        output_sub.ParameterTypeFullName = Utils.GetXSDTypeFromCSharpType(pi.PropertyType.FullName);
                                    if (pi.PropertyType.Name.Contains("List"))
                                    {
                                        output_sub.IsSignleElement = false;
                                    }
                                    else
                                    {
                                        output_sub.IsSignleElement = true;
                                    }
                                    if (output.LeafPara == null) output.LeafPara = new List<ParameterModel>();
                                    output.LeafPara.Add(output_sub);
                                }

                            }

                            if (data.LstOutputPara == null)
                                data.LstOutputPara = new List<ParameterModel>();
                            data.LstOutputPara.Add(output);

                        }



                        #endregion
                    }
                    else
                    {//选中的对象有可能为空，如果为空则获取选择的文件，按照命名空间，方法名称和参数列表的结构来分析
                        FileCodeModel fcm = _applicationObject.ActiveDocument.ProjectItem.FileCodeModel;

                        // Find the FileCodeModel's children.
                        string functionName = "";
                        foreach (CodeElement2 elemNameSpace in fcm.CodeElements)
                        {
                            if (elemNameSpace is CodeNamespace)
                            {
                                CodeElements members = ((CodeNamespace)elemNameSpace).Members;
                                foreach (var elemClass in members)
                                {
                                    if (elemClass is CodeInterface)
                                    {
                                        foreach (var elemFunction in ((CodeInterface)elemClass).Members)
                                        {
                                            if (elemFunction is CodeFunction)
                                                functionName += ((CodeFunction)elemFunction).FullName + Environment.NewLine;
                                        }
                                    }
                                    else if (elemClass is CodeClass)
                                    {
                                        foreach (var elemFunction in ((CodeClass)elemClass).Members)
                                        {
                                            if (elemFunction is CodeFunction)
                                                functionName += ((CodeFunction)elemFunction).FullName + Environment.NewLine;
                                        }
                                    }

                                    //break; //多个Class或Interface
                                }

                                //break;    //多个nameSpace
                            }
                        }

                        MessageBox.Show(fcm.Parent.Name +
                            " has the following top-level code elements:" +
                            Environment.NewLine + Environment.NewLine + functionName);

                    }

                    string outPutResult = Utils.WriteTemplateData(data);
                    if (string.IsNullOrEmpty(outPutResult))
                        MessageBox.Show("生成XSD完成");
                    else
                        MessageBox.Show(outPutResult);

                    handled = true;
                    return;
                }
            }
        }
        private DTE2 _applicationObject;
        private AddIn _addInInstance;
    }
}