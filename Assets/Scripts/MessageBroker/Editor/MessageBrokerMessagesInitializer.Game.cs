using Infrastructure.Helpers;
using UnityEditor;

namespace MessageBroker.Editor
{
    public static partial class MessageBrokerMessagesInitializer
    {
        public static class GameMessagesInitializer
        {
            private static readonly string MessagesCategory = "Game";
            public static readonly string MessagesPath = $"{MessageBrokerMessagesPath}/{MessagesCategory}";
            public static void InitializeGameMessages()
            {
                try
                {
                    AssetDatabase.StartAssetEditing();

                    FileSystemHelper.EnsureFolderExists(MessagesPath);

                    {
                        var messageName = "GameOver";
                        var message = ScriptableObjectHelper.CreateScriptableObject<Message>(messageName, MessagesPath);
                        message.MessageName = messageName;
                        message.MessageCategory = MessagesCategory;
                        message.SendMethodComment = string.Empty;
                        message.EventComment = string.Empty;
                        message.ReturnParameter = new ReturnParameter()
                        {
                            multiplicity = Multiplicity.Single,
                            parameterType = ParameterType.VoidType,
                        };
                        EditorUtility.SetDirty(message);
                    }
                    
                    {
                        var messageName = "GamePaused";
                        var message = ScriptableObjectHelper.CreateScriptableObject<Message>(messageName, MessagesPath);
                        message.MessageName = messageName;
                        message.MessageCategory = MessagesCategory;
                        message.SendMethodComment = string.Empty;
                        message.EventComment = string.Empty;
                        message.ReturnParameter = new ReturnParameter()
                        {
                            multiplicity = Multiplicity.Single,
                            parameterType = ParameterType.VoidType,
                        };
                        EditorUtility.SetDirty(message);
                    }
                    
                    {
                        var messageName = "GameResumed";
                        var message = ScriptableObjectHelper.CreateScriptableObject<Message>(messageName, MessagesPath);
                        message.MessageName = messageName;
                        message.MessageCategory = MessagesCategory;
                        message.SendMethodComment = string.Empty;
                        message.EventComment = string.Empty;
                        message.ReturnParameter = new ReturnParameter()
                        {
                            multiplicity = Multiplicity.Single,
                            parameterType = ParameterType.VoidType,
                        };
                        EditorUtility.SetDirty(message);
                    }
                    
                    {
                        var messageName = "GameStarted";
                        var message = ScriptableObjectHelper.CreateScriptableObject<Message>(messageName, MessagesPath);
                        message.MessageName = messageName;
                        message.MessageCategory = MessagesCategory;
                        message.SendMethodComment = string.Empty;
                        message.EventComment = string.Empty;
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