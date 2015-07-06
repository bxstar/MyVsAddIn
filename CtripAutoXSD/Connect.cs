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
			if(connectMode == ext_ConnectMode.ext_cm_UISetup)
			{
				object []contextGUIDS = new object[] { };
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
					//将一个命令添加到 Commands 集合:
					Command command = commands.AddNamedCommand2(_addInInstance, "CtripAutoXSD", "CtripAutoXSD", "Executes the command for CtripAutoXSD", true, 59, ref contextGUIDS, (int)vsCommandStatus.vsCommandStatusSupported+(int)vsCommandStatus.vsCommandStatusEnabled, (int)vsCommandStyle.vsCommandStylePictAndText, vsCommandControlType.vsCommandControlTypeButton);

					//将对应于该命令的控件添加到“工具”菜单:
					if((command != null) && (toolsPopup != null))
					{
						command.AddControl(toolsPopup.CommandBar, 1);
					}
				}
				catch(System.ArgumentException)
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
			if(neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
			{
				if(commandName == "CtripAutoXSD.Connect.CtripAutoXSD")
				{
					status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported|vsCommandStatus.vsCommandStatusEnabled;
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
			if(executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
			{
				if(commandName == "CtripAutoXSD.Connect.CtripAutoXSD")
				{
                    //获取选择的文本对象，方法名称和参数列表
                    TextSelection selectedText = _applicationObject.ActiveDocument.Selection as TextSelection; 
                    string resultFuncTpl = "methodName:{0},{1}";
                    CodeElement func = selectedText.ActivePoint.get_CodeElement(vsCMElement.vsCMElementFunction);
                    string result = string.Format(resultFuncTpl, func.Name, func.FullName + func.ProjectItem.Name);
                    MessageBox.Show(result);

                    string resultParamTpl = "paramName:{0},{1}";
                    CodeFunction funcType = func as CodeFunction;
                    foreach (CodeElement param in funcType.Parameters)
                    {
                        CodeParameter paramType = param as CodeParameter;
                        result = string.Format(resultParamTpl, param.Name, paramType.Type.AsFullName);
                        MessageBox.Show(result);
                    }
                    
                    //获取选择的文件中，方法名称和参数列表
                    FileCodeModel fcm = _applicationObject.ActiveDocument.ProjectItem.FileCodeModel;

                    // Find the FileCodeModel's children.
                    string functionName = "";
                    foreach (CodeElement2 elemNameSpace in fcm.CodeElements)
                    {
                        if (elemNameSpace is CodeNamespace)
                        {
                            CodeElements members = ((CodeNamespace)elemNameSpace).Members;
                            foreach(var elemClass in members)
                            {
                                if (elemClass is CodeClass)
                                {
                                    foreach (var elemFunction in ((CodeClass)elemClass).Members)
                                    {
                                        if (elemFunction is CodeFunction)
                                            functionName += ((CodeFunction)elemFunction).FullName + Environment.NewLine;
                                    }
                                }

                                //break; //多个Class
                            }

                            //break;    //多个nameSpace
                        }
                    }

                    MessageBox.Show(fcm.Parent.Name +
                        " has the following top-level code elements:" +
                        Environment.NewLine + Environment.NewLine + functionName);
                    

                    

					handled = true;
					return;
				}
			}
		}
		private DTE2 _applicationObject;
		private AddIn _addInInstance;
	}
}