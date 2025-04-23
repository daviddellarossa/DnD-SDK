using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace MessageBroker.Editor
{
    public static partial class MessageBrokerGenerator
    {
        private static readonly string PackageName = "com.deedeer.messagebroker";
        private static readonly string ClassName = "MessageBroker";
        private static string _packageVersion = String.Empty;
        private static string _namespace;
        private static string _outputFolder;
        private static string _categoryPrefix;
        private static string _defaultCategoryName;
        
        [MenuItem("DeeDeeR/MessageBroker/Generate Message Broker")]
        static async Task GenerateMessageBroker()
        {
            _packageVersion = GetPackageVersion();

            var userInput = await MessageBrokerGeneratorUI.ShowDialog();
            
            if (userInput == null)
            {
                Debug.Log("Generation canceled.");
                return;
            }
            
            _namespace = userInput.Namespace;
            _outputFolder = userInput.OutputFolder;
            _categoryPrefix = userInput.CategoryPrefix;
            _defaultCategoryName = userInput.DefaultCategoryName;
            
            var messageInfos = GetAllMessages().ToArray();

            try
            {
                AddDefaultInputParametersToMessages(messageInfos);

                var messageBrokerCategoryGenerator = new MessageBrokerCategoryGenerator(messageInfos);
                messageBrokerCategoryGenerator.Generate();

                var messageBrokerInterfaceGenerator = new MessageBrokerInterfaceGenerator(messageInfos);
                messageBrokerInterfaceGenerator.Generate();

                var messageBrokerCommonGenerator = new MessageBrokerCommonGenerator(messageInfos);
                messageBrokerCommonGenerator.Generate();

            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
            finally
            {
                RemoveDefaultInputParametersToMessages(messageInfos);
            }
        }

        private static void RemoveDefaultInputParametersToMessages(IEnumerable<MessageInfo> messageInfos)
        {
            foreach (var messageInfo in messageInfos)
            {
                messageInfo.Message.InputParameters.RemoveAt(0);
                messageInfo.Message.InputParameters.RemoveAt(0);
            }
        }

        private static void AddDefaultInputParametersToMessages(IEnumerable<MessageInfo> messageInfos)
        {
            foreach(var messageInfo in messageInfos)
            {
                if (messageInfo.Message.InputParameters is null)
                {
                    messageInfo.Message.InputParameters = new List<InputParameter>();
                }
                
                messageInfo.Message.InputParameters.Insert(0,
                    new InputParameter()
                    {
                        multiplicity = Multiplicity.Single,
                        parameterName = "target",
                        parameterType = ParameterType.ObjectType,
                        parameterComment = "The target of the message. Optional."
                    });

                messageInfo.Message.InputParameters.Insert(0,
                    new InputParameter()
                    {
                        multiplicity = Multiplicity.Single,
                        parameterName = "sender",
                        parameterType = ParameterType.ObjectType,
                        parameterComment = "The sender of the message. Required."
                    }
                    );
            }
        }

        private static string GetPackageVersion()
        {
            var packages = UnityEditor.PackageManager.PackageInfo.GetAllRegisteredPackages();
            var package = packages.SingleOrDefault(x => x.name == PackageName);
            if (package is not null)
            {
                return package.version;
            }
            else
            {
                return "<version undefined>";
            }
        }

        private static IEnumerable<MessageInfo> GetAllMessages()
        {
            var currentScriptGuid = AssetDatabase.FindAssets($"t:Script {nameof(global::MessageBroker.Editor.MessageBrokerGenerator)}");
            var currentScriptLocation = AssetDatabase.GUIDToAssetPath(currentScriptGuid[0]);
            var currentFolder = currentScriptLocation.Substring(0, currentScriptLocation.LastIndexOf('/'));

            string[] guids = AssetDatabase.FindAssets("t:" + nameof(Message), new[] { currentFolder });
            var messageInfos = new List<MessageInfo>();
            for (int i = 0; i < guids.Length; i++) 
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                messageInfos.Add(new MessageInfo()
                    {
                        Message = AssetDatabase.LoadAssetAtPath<Message>(path),
                        Path = path
                    });
            }
            return messageInfos.ToArray();
        }

        private static string CleanName(string name)
        {
            return System.Text.RegularExpressions.Regex.Replace(name, @"[^a-zA-Z0-9_]", string.Empty);
        }

    }
    internal class MessageInfo
    {
        public Message Message;
        public string Path;
    }
}