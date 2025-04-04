﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessageBroker.Editor
{
    public static partial class MessageBrokerGenerator
    {

        internal class MessageBrokerCommonGenerator
        {
            private readonly IEnumerable<MessageInfo> messageInfos;

            public MessageBrokerCommonGenerator(IEnumerable<MessageInfo> messageInfos)
            {
                this.messageInfos = messageInfos;
            }

            internal void Generate()
            {
                var messageInfosGroupedByCategory = messageInfos.GroupBy(x => x.Message.MessageCategory);

                var stringBuilder = new StringBuilder();

                Indent.Init();

                AddHeader(stringBuilder);

                AddUsings(stringBuilder);

                OpenNamespace(stringBuilder);

                OpenClassDeclaration(stringBuilder);

                AddPublicVariables(messageInfosGroupedByCategory, stringBuilder);

                //AddAwakeBlock(groups, sb);

                CloseClassDeclaration(stringBuilder);

                CloseNamespace(stringBuilder);

                CreateFile(stringBuilder);
            }

            private void AddHeader(StringBuilder stringBuilder)
            {

                stringBuilder.AppendLine("//------------------------------------------------------------------------------");
                stringBuilder.AppendLine("// <auto-generated>");
                stringBuilder.AppendLine($"// Code auto-generated by {nameof(global::MessageBroker.Editor.MessageBrokerGenerator)}");
                stringBuilder.AppendLine($"// Re-run the generator every time a new {nameof(Message)} is added or removed.");
                stringBuilder.AppendLine("// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.");
                stringBuilder.AppendLine("// </auto-generated>");
                stringBuilder.AppendLine("//------------------------------------------------------------------------------");

                stringBuilder.AppendLine();
            }

            private void AddUsings(StringBuilder stringBuilder)
            {
                stringBuilder.AppendLine($"using UnityEngine;");
                stringBuilder.AppendLine();
            }

            private void OpenNamespace(StringBuilder stringBuilder)
            {
                stringBuilder.AppendLine($"namespace {MessageBrokerGenerator._namespace}");
                stringBuilder.AppendLine($"{{");
                Indent.Push();
            }

            private void CloseNamespace(StringBuilder stringBuilder)
            {
                stringBuilder.AppendLine($"{Indent.Pop()}}}");
            }

            private void OpenClassDeclaration(StringBuilder stringBuilder)
            {
                stringBuilder.AppendLine($"{Indent.Get()}/// <summary>");
                stringBuilder.AppendLine($"{Indent.Get()}/// Message Broker component exposing methods to send messages defined as ScriptableObjects of type <see cref=\"{nameof(Message)}\"/>.");
                stringBuilder.AppendLine($"{Indent.Get()}/// </summary>");
                stringBuilder.AppendLine($"{Indent.Get()}[AddComponentMenu(\"DeeDeeR/Message Broker/Message Broker\")]");
                stringBuilder.AppendLine($"{Indent.Get()}public sealed partial class {MessageBrokerGenerator.ClassName} : MonoBehaviour, I{MessageBrokerGenerator.ClassName}");
                stringBuilder.AppendLine($"{Indent.Get()}{{");
            }

            private void AddPublicVariables(IEnumerable<IGrouping<string, MessageInfo>> groups, StringBuilder stringBuilder)
            {
                Indent.Push();
                foreach (var group in groups)
                {
                    var categoryName = MessageBrokerGenerator.CleanName(string.IsNullOrWhiteSpace(group.Key) ? "Unnamed" : group.Key);

                    stringBuilder.AppendLine($"{Indent.Get()}/// <inheritdoc/>");
                    stringBuilder.AppendLine($"{Indent.Get()}public {MessageBrokerGenerator._categoryPrefix}{categoryName} {categoryName} {{ get; }} = new {MessageBrokerGenerator._categoryPrefix}{categoryName}();");
                    stringBuilder.AppendLine();
                }
                Indent.Pop();
            }

            private void AddAwakeBlock(IEnumerable<IGrouping<string, MessageInfo>> groups, StringBuilder stringBuilder)
            {
                stringBuilder.AppendLine($"{Indent.Push()}void Awake()");
                stringBuilder.AppendLine($"{Indent.Get()}{{");
                Indent.Push();
                foreach (var group in groups)
                {
                    var categoryName = MessageBrokerGenerator.CleanName(string.IsNullOrWhiteSpace(group.Key) ? "Unnamed" : group.Key);
                    var className = $"{MessageBrokerGenerator._categoryPrefix}{categoryName}";

                    stringBuilder.AppendLine($"{Indent.Get()}this.{categoryName} = new {className}();");
                }
                Indent.Pop();
                stringBuilder.AppendLine($"{Indent.Get()}}}");
                Indent.Pop();
            }

            private void CloseClassDeclaration(StringBuilder stringBuilder)
            {
                stringBuilder.AppendLine($"{Indent.Get()}}}");
            }

            private void CreateFile(StringBuilder stringBuilder)
            {
                var outputPath = System.IO.Path.Combine(MessageBrokerGenerator._outputFolder, $"{MessageBrokerGenerator.ClassName}.cs");

                System.IO.File.WriteAllText(outputPath, stringBuilder.ToString());
            }

        }
    }
}