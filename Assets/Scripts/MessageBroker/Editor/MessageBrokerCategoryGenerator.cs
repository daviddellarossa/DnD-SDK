using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            private readonly IEnumerable<MessageInfo> messageInfosByCategory;

            public MessageBrokerCategoryGenerator(IEnumerable<MessageInfo> messageInfos)
            {
                this.messageInfosByCategory = messageInfos;
            }

            internal void Generate()
            {
                var messageInfosGroupedByCategory = this.messageInfosByCategory.GroupBy(x => x.Message.MessageCategory);

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

                AddHeader(stringBuilder);

                AddUsings(stringBuilder);

                OpenNamespace(stringBuilder);

                OpenClassDeclaration(stringBuilder, categoryName);

                AddEventDeclarations(messageInfoGroup, stringBuilder);

                AddInvokeMethods(messageInfoGroup, stringBuilder);

                CloseClassDeclaration(stringBuilder);

                CloseNamespace(stringBuilder);

                CreateFileWithCategory(stringBuilder, categoryName);

            }

            private void AddInvokeMethods(IEnumerable<MessageInfo> messageInfos, StringBuilder stringBuilder)
            {
                stringBuilder.AppendLine($"{Indent.Get()}#region Send methods");
                stringBuilder.AppendLine();
                foreach (var messageInfo in messageInfos)
                {
                    AddInvokeMethod(messageInfo, stringBuilder);
                }
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"{Indent.Get()}#endregion");
                stringBuilder.AppendLine();
            }

            private void AddInvokeMethod(MessageInfo messageInfo, StringBuilder stringBuilder)
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

                stringBuilder.AppendLine($"{Indent.Get()}public {GetOutputParameter(messageInfo)} Send_{name}({GetInputParametersTypeNameAsString(messageInfo)})");
                stringBuilder.AppendLine($"{Indent.Get()}{{");
                Indent.Push();

                stringBuilder.AppendLine($"{Indent.Get()}if (sender == null)");
                stringBuilder.AppendLine($"{Indent.Get()}{{");
                stringBuilder.AppendLine($"{Indent.Push()}Debug.LogError(\"sender is required.\");");
                stringBuilder.AppendLine($"{Indent.Get()}{ ReturnNull(messageInfo) }");

                stringBuilder.AppendLine($"{Indent.Pop()}}}");
                stringBuilder.AppendLine();

                stringBuilder.AppendLine($"{Indent.Get()}{ CheckForReturn() }{ name }{ IsNullableAsString(messageInfo) }.Invoke({ GetInputParametersNamesAsString(messageInfo) });");

                stringBuilder.AppendLine($"{Indent.Pop()}}}");
                stringBuilder.AppendLine();

                string CheckForReturn()
                {
                    return messageInfo.Message.ReturnParameter.parameterType != ParameterType.VoidType ? "return " : String.Empty;
                }
            }

            private object ReturnNull(MessageInfo messageInfo)
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

            private string IsNullableAsString(MessageInfo messageInfo)
            {
                return IsReturnValueType(messageInfo) ? "" : "?";
            }

            private bool IsReturnValueType(MessageInfo _)
            {
                return false;
            }

            private object GetInputParametersNamesAsString(MessageInfo messageInfo)
            {
                return string.Join(", ", messageInfo.Message.InputParameters.Select(x => MessageBrokerGenerator.CleanName(x.parameterName)));
            }

            private object GetInputParametersTypeNameAsString(MessageInfo messageInfo)
            {
                return string.Join(", ", messageInfo.Message.InputParameters.Select(x => $"{GetParameterType(x)} {MessageBrokerGenerator.CleanName(x.parameterName)}"));
            }

            private object GetOutputParameter(MessageInfo messageInfo)
            {
                return GetReturnType(messageInfo.Message.ReturnParameter);
            }

            private void AddEventDeclarations(IEnumerable<MessageInfo> messageInfos, StringBuilder stringBuilder)
            {
                stringBuilder.AppendLine($"{Indent.Get()}#region Event declaration");
                stringBuilder.AppendLine();
                foreach (var messageInfo in messageInfos)
                {
                    AddMessageEvent(stringBuilder, messageInfo);
                }
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"{Indent.Get()}#endregion");
                stringBuilder.AppendLine();

            }

            private void AddMessageEvent(StringBuilder stringBuilder, MessageInfo messageInfo)
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
                        string parameterType = GetParameterType(inputParameter);

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

                        string parameterType = GetParameterType(inputParameter);

                        stringBuilder.Append(parameterType);
                        stringBuilder.Append(", ");

                    }
                    //add return parameter
                    string returnType = GetReturnType(messageInfo.Message.ReturnParameter);
                    stringBuilder.Append(returnType);

                }
                stringBuilder.Append($"> {MessageBrokerGenerator.CleanName(messageInfo.Message.GetName())};");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine();
            }

            private static string GetParameterType(InputParameter inputParameter)
            {
                var parameterType = inputParameter.parameterType.ToParameterTypeString(System.Data.ParameterDirection.Input, false, inputParameter.otherType);

                var multiplicity = inputParameter.multiplicity.ToTypeString();

                return string.Format(multiplicity, parameterType);
            }

            private static string GetReturnType(ReturnParameter inputParameter)
            {
                var parameterType = inputParameter.parameterType.ToParameterTypeString(ParameterDirection.ReturnValue, true, inputParameter.otherType);
                
                var multiplicity = inputParameter.multiplicity.ToTypeString();

                return string.Format(multiplicity, parameterType);
            }

            private void AddHeader(StringBuilder sb)
            {

                sb.AppendLine("//------------------------------------------------------------------------------");
                sb.AppendLine("// <auto-generated>");
                sb.AppendLine($"// Code auto-generated by {nameof(MessageBrokerGenerator)} version {MessageBrokerGenerator._packageVersion}.");
                sb.AppendLine($"// Re-run the generator every time a new {nameof(Message)} is added or removed.");
                sb.AppendLine("// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.");
                sb.AppendLine("// </auto-generated>");
                sb.AppendLine("//------------------------------------------------------------------------------");

                sb.AppendLine();
            }

            private void AddUsings(StringBuilder sb)
            {
                sb.AppendLine($"using System;");
                sb.AppendLine($"using UnityEngine;");
                sb.AppendLine($"using UnityEditor;");
                sb.AppendLine();
            }

            private void OpenNamespace(StringBuilder sb)
            {
                sb.AppendLine($"namespace {MessageBrokerGenerator._namespace}");
                sb.AppendLine($"{{");
                Indent.Push();
            }

            private void CloseNamespace(StringBuilder sb)
            {
                sb.AppendLine($"{Indent.Pop()}}}");
            }

            private void OpenClassDeclaration(StringBuilder sb, string categoryName)
            {
                sb.AppendLine($"{Indent.Get()}/// <summary>");
                sb.AppendLine($"{Indent.Get()}/// MessageBroker publisher for {categoryName} category.");
                sb.AppendLine($"{Indent.Get()}/// </summary>");
                sb.AppendLine($"{Indent.Get()} public class {MessageBrokerGenerator._categoryPrefix}{categoryName}");
                sb.AppendLine($"{Indent.Get()}{{");
                Indent.Push();
            }

            private void CloseClassDeclaration(StringBuilder sb)
            {
                sb.AppendLine($"{Indent.Pop()}}}");
            }

            private void CreateFileWithCategory(StringBuilder sb, string categoryName)
            {
                var outputPath = System.IO.Path.Combine(MessageBrokerGenerator._outputFolder, $"{MessageBrokerGenerator.ClassName}.{MessageBrokerGenerator._categoryPrefix}{categoryName}.cs");

                this.CreateFile(sb, outputPath);
            }

            private void CreateFile(StringBuilder sb, string outputPath)
            {
                if (!System.IO.Directory.Exists(MessageBrokerGenerator._outputFolder))
                {
                    System.IO.Directory.CreateDirectory(MessageBrokerGenerator._outputFolder);
                }

                System.IO.File.WriteAllText(outputPath, sb.ToString());
            }

        }
    }
}