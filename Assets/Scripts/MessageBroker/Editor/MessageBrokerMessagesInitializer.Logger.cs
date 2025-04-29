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
                                ParameterName = "message",
                                Multiplicity = Multiplicity.Single,
                                ParameterType = ParameterType.StringType,
                            },
                            
                            new InputParameter()
                            {
                            ParameterName = "logLevel",
                            Multiplicity = Multiplicity.Single,
                            ParameterType = ParameterType.OtherType,
                            OtherType = typeof(LogType).FullName,
                        }
                        };
                        message.ReturnParameter = new ReturnParameter()
                        {
                            Multiplicity = Multiplicity.Single,
                            ParameterType = ParameterType.VoidType,
                        };
                        EditorUtility.SetDirty(message);
                    }
                    
                    {
                        var messageName = "LogException";
                        var message = ScriptableObjectHelper.CreateScriptableObject<Message>(messageName, MessagesPath);
                        message.MessageName = messageName;
                        message.MessageCategory = MessagesCategory;
                        message.SendMethodComment = string.Empty;
                        message.EventComment = string.Empty;
                        message.InputParameters = new List<InputParameter>()
                        {
                            new InputParameter()
                            {
                                ParameterName = "sender",
                                Multiplicity = Multiplicity.Single,
                                ParameterType = ParameterType.ObjectType,
                                IsNullable = true
                            },
                            new InputParameter()
                            {
                                ParameterName = "target",
                                Multiplicity = Multiplicity.Single,
                                ParameterType = ParameterType.ObjectType,
                                IsNullable = true
                            },
                            new InputParameter()
                            {
                                ParameterName = "exceptionMessageEventArgs",
                                Multiplicity = Multiplicity.Single,
                                ParameterType = ParameterType.OtherType,
                                OtherType = typeof(DeeDeeR.MessageBroker.ExceptionMessageBrokerEventArgs).FullName,
                                IsNullable = true
                            }
                        };
                        message.ReturnParameter = new ReturnParameter()
                        {
                            Multiplicity = Multiplicity.Single,
                            ParameterType = ParameterType.VoidType,
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