using Autodesk.Revit.DB;
using Revit.Toolkit.Mvvm.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.CodeAnalysis;
using Microsoft.CSharp;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ChatRevit
{
    public partial class ChatRevitViewModel : ObservableObject
    {
        private IDataContext _revitDataContext;
        private readonly IExternalEventService _externalEventService;
        public ChatRevitViewModel(IDataContext dataContext, IExternalEventService externalEventService)
        {
            _revitDataContext = dataContext;
            _externalEventService = externalEventService;
        }

        [RelayCommand]
        private void AddAPIKEY()
        {
            ChatRevit.Default.GPT_API_KEY = input;
            ChatRevit.Default.Save();
        }

        [ObservableProperty]
        private string input;

        [RelayCommand]
        private async void Enter()
        {
            //const string OPENAI_TOKEN = "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";
            if (ChatRevit.Default.GPT_API_KEY == string.Empty)
            {
                MessageBox.Show("请先填写API_KEY", "提示");
                return;
            }
            OpenAIService service = new OpenAIService(new OpenAiOptions { ApiKey = ChatRevit.Default.GPT_API_KEY });
            List<ChatMessage> convMessages = new List<ChatMessage>();
            var sysMessage = new ChatMessage("system", systemPersonality);
            var userMessage = new ChatMessage("user", WrapPrompt(input));

            convMessages.Add(sysMessage);
            convMessages.Add(userMessage);
            var createRequest = new ChatCompletionCreateRequest()
            {
                Messages = convMessages,
                Model = Models.ChatGpt3_5Turbo,
                Temperature = 0.1f
            };
            var res = await service.ChatCompletion.CreateCompletion(createRequest);
            if (res.Successful)
            {
                var response = res.Choices[0].Message.Content;
                var request = await _externalEventService.Raise((app) =>
                {
                    var document = app.ActiveUIDocument.Document;
                    try
                    {
                        using var tran = new Transaction(document, "chat");
                        tran.Start();
                        execute(document, response);
                        tran.Commit();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                });
            }
        }

        private string GetPureCode(string response)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(response);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
            var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
            if (methods.Count() != 0)
            {
                return methods.FirstOrDefault().ToFullString();
            }
            else
            {
                MessageBox.Show("No method found");
                return "";
            }

        }

        string systemPersonality => "I want you to act as a Revit Developer. Now Write a Revit C# script of a method.\n" +
                 " - This method provides its functionality as a public function named 'RevitFunction'.\n " +
                 " - This method has only one parameter called 'document' which is passed in from the previous code " +
                 "and the type of parameter 'document' is Document.\n " +
                 " - This method only contains methods available in RevitAPI.\n" +
                 " - This method does not need Transaction.\n" +
                 " - This method immediately does the task when the function is invoked.\n" +
                 " - Don’t use using namesapce and implementation of IExternalCommand.\n" +
                 " - I only need the founction body. Don’t add any explanation.\n";

        string WrapPrompt(string input)
                =>
                 "The task is described as follows:\n" + input;

        private void execute(Document doc, string codeOutput)
        {
            var codeString = "using System;\n " +
                             "using System.IO;\n " +
                             "using System.Windows;\n" +
                             "using System.Collections.Generic;\n" +
                             "using System.Linq;\n" +
                             "using Autodesk.Revit.UI;\n" +
                             "using Autodesk.Revit.DB;\n" +
                             "namespace Chat {\n" +
                             "public class ChatWithRevit{ \n" + codeOutput + " }\n}\n";


            // 创建CodeDom编译器
            CodeDomProvider provider = new CSharpCodeProvider();

            // 定义编译参数
            CompilerParameters parameters = new CompilerParameters();
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Core.dll");
            parameters.ReferencedAssemblies.Add("System.Xml.Linq.dll");
            parameters.ReferencedAssemblies.Add("C:\\Program Files\\Autodesk\\Revit 2022\\RevitAPI.dll");
            parameters.ReferencedAssemblies.Add("C:\\Program Files\\Autodesk\\Revit 2022\\RevitAPIUI.dll");
            parameters.GenerateExecutable = false; // 不生成exe文件
            parameters.GenerateInMemory = true; // 在内存中编译
            parameters.WarningLevel = 4;
            parameters.CompilerOptions = "/target:library";

            // 编译代码
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, codeString);

            // 判断是否有编译错误
            if (results.Errors.HasErrors)
            {
                var errorStr = "编译出现错误：\n";
                foreach (CompilerError error in results.Errors)
                {
                    errorStr += String.Format("行{0}列{1}：{2}", error.Line, error.Column, error.ErrorText);
                }
                MessageBox.Show(errorStr);
                return;
            }

            // 获取编译后的程序集
            var assembly = results.CompiledAssembly;

            // 获取实例
            object obj = assembly.CreateInstance("Chat.ChatWithRevit");

            // 执行方法
            obj.GetType().GetMethod("RevitFunction").Invoke(obj, new object[] { doc });
        }
    }
}
