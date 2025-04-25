using System.Collections.Generic;
using DnD.Code.Scripts.Characters;
using Infrastructure.Helpers;
using UnityEditor;
using UnityEngine;

namespace MessageBroker.Editor
{
    public static partial class MessageBrokerMessagesInitializer
    {
        public static class LoggerMessagesInitializer
        {
            private static readonly string MessagesCategory = "Logger";
            public static readonly string MessagesPath = $"{MessageBrokerMessagesPath}/{MessagesCategory}";
            public static void InitializeLoggerMessages()
            {
                try
                {
                    AssetDatabase.StartAssetEditing();

                    FileSystemHelper.EnsureFolderExists(MessagesPath);

                    {
                        var messageName = "Log";
                        var message = ScriptableObjectHelper.CreateScriptableObject<Message>(messageName, MessagesPath);
                        message.MessageName = messageName;
                        message.MessageCategory = MessagesCategory;
                        message.SendMethodComment = string.Empty;
                        message.EventComment = string.Empty;
                        message.InputParameters = new List<InputParameter>()
                        {
                            new InputParameter()
                            {
                                parameterName = "message",
                                multiplicity = Multiplicity.Single,
                                parameterType = ParameterType.StringType,
                            },
                            
                            new InputParameter()
                            {
                            parameterName = "logLevel",
                            multiplicity = Multiplicity.Single,
                            parameterType = ParameterType.OtherType,
                            otherType = typeof(LogType).FullName,
                        }
                        };
                        message.ReturnParameter = new ReturnParameter()
                        {
                            multiplicity = Multiplicity.Single,
                            parameterType = ParameterType.VoidType,
                        };
                        EditorUtility.SetDirty(message);
                    }
                    
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
                finally
                {
                    AssetDatabase.StopAssetEditing();
                }
            }
        }
    }
}