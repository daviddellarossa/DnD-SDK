using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace MessageBroker.Editor
{
    public static partial class MessageBrokerGenerator
    {
        /// <summary>
        /// Create the MessageBroker sub-class per each category of Messages.
        /// </summary>
        internal class MessageBrokerCategoryGenerator
        {
            private readonly IEnumerable<MessageInfo> _messageInfosByCategory;

            public MessageBrokerCategoryGenerator(IEnumerable<MessageInfo> messageInfos)
            {
                this._messageInfosByCategory = messageInfos;
            }

            internal void Generate()
            {
                var messageInfosGroupedByCategory = this._messageInfosByCategory.GroupBy(x => x.Message.MessageCategory);

                foreach (var group in messageInfosGroupedByCategory)
                {
                    this.GenerateFilePerCategory(group);
                }
            }

            private void GenerateFilePerCategory(IGrouping<string, MessageInfo> messageGroup)
            {
                Indent.Init();

                var messageInfoGroup = messageGroup.ToArray();

                var categoryName = MessageBrokerGenerator.CleanName(string.IsNullOrWhiteSpace(messageGroup.Key) ? MessageBrokerGenerator._defaultCategoryName : messageGroup.Key);

                var stringBuilder = new StringBuilder();
                
                
                using (new HeaderHandler(stringBuilder)) { /* Deliberately empty */ }
                using (new UsingHandler(stringBuilder)) { /* Deliberately empty */ }
                using (new NamespaceHandler(stringBuilder))
                {
                    using (new ClassHandler(categoryName, stringBuilder))
                    {
                        using (new RegionHandler("Event declaration", stringBuilder))
                        {
                            foreach (var messageInfo in messageInfoGroup)
                            {
                                using (new MessageEventHandler(messageInfo, stringBuilder)) { /* Deliberately empty */ }
                            }
                        }

                        using (new RegionHandler("Send methods", stringBuilder))
                        {
                            
                            foreach (var messageInfo in messageInfoGroup)
                            {
                                using (new InvokeMethodHandler(messageInfo, stringBuilder)) { /* Deliberately empty */ }
                            }
                        }
                        
                    }
                }
                using (new CreateFileWithCategoryHandler(categoryName, stringBuilder)) { /* Deliberately empty */ }
            }
            
            internal static object ReturnNull(MessageInfo messageInfo)
            {
                if(messageInfo.Message.ReturnParameter.parameterType == ParameterType.VoidType)
                {
                    return "return;";
                }
                else
                {
                    return "return default;";
                }
            }

            internal static string IsNullableAsString(MessageInfo messageInfo)
            {
                return IsReturnValueType(messageInfo) ? "" : "?";
            }

            private static bool IsReturnValueType(MessageInfo _)
            {
                return false;
            }

            internal static string GetInputParametersNamesAsString(MessageInfo messageInfo)
            {
                return string.Join(", ", messageInfo.Message.InputParameters.Select(x => MessageBrokerGenerator.CleanName(x.parameterName)));
            }

            internal static string GetInputParametersTypeNameAsString(MessageInfo messageInfo)
            {
                return string.Join(", ", messageInfo.Message.InputParameters.Select(x => $"{GetParameterType(x)} {MessageBrokerGenerator.CleanName(x.parameterName)}"));
            }

            internal static string GetOutputParameter(MessageInfo messageInfo)
            {
                return GetReturnType(messageInfo.Message.ReturnParameter);
            }
            
            internal static string GetParameterType(InputParameter inputParameter)
            {
                var parameterType = inputParameter.parameterType.ToParameterTypeString(System.Data.ParameterDirection.Input, false, inputParameter.otherType);

                var multiplicity = inputParameter.multiplicity.ToTypeString();

                return string.Format(multiplicity, parameterType);
            }

            internal static string GetReturnType(ReturnParameter inputParameter)
            {
                var parameterType = inputParameter.parameterType.ToParameterTypeString(ParameterDirection.ReturnValue, true, inputParameter.otherType);
                
                var multiplicity = inputParameter.multiplicity.ToTypeString();

                return string.Format(multiplicity, parameterType);
            }
        }

        abstract class BaseHandler : IDisposable
        {
            protected readonly StringBuilder StringBuilder;

            protected BaseHandler(StringBuilder stringBuilder)
            {
                this.StringBuilder = stringBuilder;
            }
            public virtual void Dispose() { }
        }

        class HeaderHandler : BaseHandler
        {
            public HeaderHandler(StringBuilder stringBuilder) : base(stringBuilder)
            {
                stringBuilder.AppendLine("//------------------------------------------------------------------------------");
                stringBuilder.AppendLine("// <auto-generated>");
                stringBuilder.AppendLine($"// Code auto-generated by {nameof(MessageBrokerGenerator)} version {MessageBrokerGenerator._packageVersion}.");
                stringBuilder.AppendLine($"// Re-run the generator every time a new {nameof(Message)} is added or removed.");
                stringBuilder.AppendLine("// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.");
                stringBuilder.AppendLine("// </auto-generated>");
                stringBuilder.AppendLine("//------------------------------------------------------------------------------");

                stringBuilder.AppendLine();
            }
        }

        class UsingHandler : BaseHandler
        {
            public UsingHandler(StringBuilder stringBuilder) : base(stringBuilder)
            {
                stringBuilder.AppendLine($"using System;");
                stringBuilder.AppendLine($"using UnityEngine;");
                stringBuilder.AppendLine($"using UnityEditor;");
                stringBuilder.AppendLine();
            }
        }

        class NamespaceHandler : BaseHandler
        {
            public NamespaceHandler(StringBuilder stringBuilder) : base(stringBuilder)
            {
                stringBuilder.AppendLine($"namespace {MessageBrokerGenerator._namespace}");
                stringBuilder.AppendLine($"{{");
                Indent.Push();
            }
            
            public override void Dispose()
            {
                StringBuilder.AppendLine($"{Indent.Pop()}}}");
            }
        }

        class ClassHandler : BaseHandler
        {
            public ClassHandler(string categoryName, StringBuilder stringBuilder) : base(stringBuilder)
            {
                stringBuilder.AppendLine($"{Indent.Get()}/// <summary>");
                stringBuilder.AppendLine($"{Indent.Get()}/// MessageBroker publisher for {categoryName} category.");
                stringBuilder.AppendLine($"{Indent.Get()}/// </summary>");
                stringBuilder.AppendLine($"{Indent.Get()} public class {MessageBrokerGenerator._categoryPrefix}{categoryName}");
                stringBuilder.AppendLine($"{Indent.Get()}{{");
                Indent.Push();
            }
            public override void Dispose()
            {
                StringBuilder.AppendLine($"{Indent.Pop()}}}");
            }
        }

        class RegionHandler : BaseHandler
        {
            public RegionHandler(string regionName, StringBuilder stringBuilder) : base(stringBuilder)
            {
                StringBuilder.AppendLine($"{Indent.Get()}#region {regionName}");
                StringBuilder.AppendLine();
            }

            public override void Dispose()
            {
                StringBuilder.AppendLine($"{Indent.Get()}#endregion");
                StringBuilder.AppendLine();
            }
        }

        class MessageEventHandler : BaseHandler
        {
            public MessageEventHandler(MessageInfo messageInfo, StringBuilder stringBuilder) : base(stringBuilder)
            {
                stringBuilder.AppendLine($"{Indent.Get()}/// <summary>");
                stringBuilder.AppendLine($"{Indent.Get()}/// { messageInfo.Message.EventComment }");
                stringBuilder.AppendLine($"{Indent.Get()}/// </summary>");
                stringBuilder.Append($"{Indent.Get()}public event ");
                if(messageInfo.Message.ReturnParameter.parameterType == ParameterType.VoidType)
                {
                    stringBuilder.Append($"Action<");
                    for(int i = 0; i < messageInfo.Message.InputParameters.Count; ++i )
                    {
                        var inputParameter = messageInfo.Message.InputParameters[i];

                        //add parameter
                        string parameterType = MessageBrokerCategoryGenerator.GetParameterType(inputParameter);

                        stringBuilder.Append(parameterType);
                        if (i < messageInfo.Message.InputParameters.Count - 1)
                        {
                            stringBuilder.Append(", ");
                        }
                    }
                }
                else
                {
                    stringBuilder.Append($"Func<");
                    foreach (var inputParameter in messageInfo.Message.InputParameters)
                    {
                        //add parameter

                        string parameterType = MessageBrokerCategoryGenerator.GetParameterType(inputParameter);

                        stringBuilder.Append(parameterType);
                        stringBuilder.Append(", ");

                    }
                    //add return parameter
                    string returnType = MessageBrokerCategoryGenerator.GetReturnType(messageInfo.Message.ReturnParameter);
                    stringBuilder.Append(returnType);

                }
                stringBuilder.Append($"> {MessageBrokerGenerator.CleanName(messageInfo.Message.GetName())};");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine();
            }
        }

        class InvokeMethodHandler : BaseHandler
        {
            public InvokeMethodHandler(MessageInfo messageInfo, StringBuilder stringBuilder) : base(stringBuilder)
            {
                var name = MessageBrokerGenerator.CleanName(messageInfo.Message.GetName());

                stringBuilder.AppendLine($"{Indent.Get()}/// <summary>");
                stringBuilder.AppendLine($"{Indent.Get()}/// Send a message of type {name}.");
                foreach(var inputParameter in messageInfo.Message.InputParameters)
                {
                    stringBuilder.AppendLine($"{Indent.Get()}/// <param name=\"{inputParameter.parameterName}\">{inputParameter.parameterComment}</param>");
                }
                if(messageInfo.Message.ReturnParameter.parameterType != ParameterType.VoidType)
                {
                    stringBuilder.AppendLine($"{Indent.Get()}/// <returns>{messageInfo.Message.ReturnParameter.parameterComment}</returns>");
                }
                stringBuilder.AppendLine($"{Indent.Get()}/// </summary>");

                stringBuilder.AppendLine($"{Indent.Get()}public {MessageBrokerCategoryGenerator.GetOutputParameter(messageInfo)} Send_{name}({MessageBrokerCategoryGenerator.GetInputParametersTypeNameAsString(messageInfo)})");
                stringBuilder.AppendLine($"{Indent.Get()}{{");
                Indent.Push();

                stringBuilder.AppendLine($"{Indent.Get()}if (sender == null)");
                stringBuilder.AppendLine($"{Indent.Get()}{{");
                stringBuilder.AppendLine($"{Indent.Push()}Debug.LogError(\"sender is required.\");");
                stringBuilder.AppendLine($"{Indent.Get()}{ MessageBrokerCategoryGenerator.ReturnNull(messageInfo) }");

                stringBuilder.AppendLine($"{Indent.Pop()}}}");
                stringBuilder.AppendLine();

                stringBuilder.AppendLine($"{Indent.Get()}{ CheckForReturn() }{ name }{ MessageBrokerCategoryGenerator.IsNullableAsString(messageInfo) }.Invoke({ MessageBrokerCategoryGenerator.GetInputParametersNamesAsString(messageInfo) });");

                stringBuilder.AppendLine($"{Indent.Pop()}}}");
                stringBuilder.AppendLine();

                string CheckForReturn()
                {
                    return messageInfo.Message.ReturnParameter.parameterType != ParameterType.VoidType ? "return " : string.Empty;
                }
            }
        }

        class CreateFileWithCategoryHandler : BaseHandler
        {
            public CreateFileWithCategoryHandler(string categoryName, StringBuilder stringBuilder) : base(stringBuilder)
            {
                var outputPath = System.IO.Path.Combine(MessageBrokerGenerator._outputFolder, $"{MessageBrokerGenerator.ClassName}.{MessageBrokerGenerator._categoryPrefix}{categoryName}.cs");

                this.CreateFile(stringBuilder, outputPath);
            }
            
            private void CreateFile(StringBuilder stringBuilder, string outputPath)
            {
                if (!System.IO.Directory.Exists(MessageBrokerGenerator._outputFolder))
                {
                    System.IO.Directory.CreateDirectory(MessageBrokerGenerator._outputFolder);
                }

                System.IO.File.WriteAllText(outputPath, stringBuilder.ToString());
            }
        }
    }
}