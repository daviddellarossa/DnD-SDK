using DnD.Code.Scripts.Helpers;
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