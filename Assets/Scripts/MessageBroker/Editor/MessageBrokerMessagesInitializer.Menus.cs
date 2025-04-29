using System.Collections.Generic;
using Infrastructure.Helpers;
using UnityEditor;

namespace MessageBroker.Editor
{
    public static partial class MessageBrokerMessagesInitializer
    {
        public static class MenusMessagesInitializer
        {
            private static readonly string MessagesCategory = "Menus";
            public static readonly string MessagesPath = $"{MessageBrokerMessagesPath}/{MessagesCategory}";

            public static void InitializeMenusMessages()
            {
                try
                {
                    AssetDatabase.StartAssetEditing();

                    FileSystemHelper.EnsureFolderExists(MessagesPath);
                    
                    {
                        var messageName = "StartGame";
                        var message = ScriptableObjectHelper.CreateScriptableObject<Message>(messageName, MessagesPath);
                        message.MessageName = messageName;
                        message.MessageCategory = MessagesCategory;
                        message.SendMethodComment = string.Empty;
                        message.EventComment = string.Empty;
                        message.ReturnParameter = new ReturnParameter()
                        {
                            Multiplicity = Multiplicity.Single,
                            ParameterType = ParameterType.VoidType,
                        };
                        EditorUtility.SetDirty(message);
                    }
                    
                    {
                        var messageName = "LoadGame";
                        var message = ScriptableObjectHelper.CreateScriptableObject<Message>(messageName, MessagesPath);
                        message.MessageName = messageName;
                        message.MessageCategory = MessagesCategory;
                        message.SendMethodComment = string.Empty;
                        message.EventComment = string.Empty;
                        message.InputParameters = new List<InputParameter>()
                        {
                            new InputParameter()
                            {
                                Multiplicity = Multiplicity.Single,
                                ParameterType = ParameterType.OtherType,
                                ParameterName = "saveGameData",
                                ParameterComment = "The savegame data structure containing the game data to load",
                                OtherType = "Infrastructure.SaveManager.Models.SaveGameData"
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
                        var messageName = "LoadLatestGame";
                        var message = ScriptableObjectHelper.CreateScriptableObject<Message>(messageName, MessagesPath);
                        message.MessageName = messageName;
                        message.MessageCategory = MessagesCategory;
                        message.SendMethodComment = string.Empty;
                        message.EventComment = string.Empty;
                        
                        message.ReturnParameter = new ReturnParameter()
                        {
                            Multiplicity = Multiplicity.Single,
                            ParameterType = ParameterType.VoidType,
                        };
                        EditorUtility.SetDirty(message);
                    }
                    
                    {
                        var messageName = "BackToMainMenu";
                        var message = ScriptableObjectHelper.CreateScriptableObject<Message>(messageName, MessagesPath);
                        message.MessageName = messageName;
                        message.MessageCategory = MessagesCategory;
                        message.SendMethodComment = string.Empty;
                        message.EventComment = string.Empty;
                        message.ReturnParameter = new ReturnParameter()
                        {
                            Multiplicity = Multiplicity.Single,
                            ParameterType = ParameterType.VoidType,
                        };
                        EditorUtility.SetDirty(message);
                    }
                    
                    {
                        var messageName = "QuitGame";
                        var message = ScriptableObjectHelper.CreateScriptableObject<Message>(messageName, MessagesPath);
                        message.MessageName = messageName;
                        message.MessageCategory = MessagesCategory;
                        message.SendMethodComment = string.Empty;
                        message.EventComment = string.Empty;
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