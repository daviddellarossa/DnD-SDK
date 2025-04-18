﻿using System.Collections.Generic;
using DnD.Code.Scripts.Characters;
using Infrastructure.Helpers;
using UnityEditor;

namespace MessageBroker.Editor
{
    public static partial class MessageBrokerMessagesInitializer
    {
        public static class CharacterMessagesInitializer
        {
            private static readonly string MessagesCategory = "Character";
            public static readonly string MessagesPath = $"{MessageBrokerMessagesPath}/{MessagesCategory}";
            public static void InitializeCharacterMessages()
            {
                try
                {
                    AssetDatabase.StartAssetEditing();

                    FileSystemHelper.EnsureFolderExists(MessagesPath);

                    {
                        var messageName = "CharacterCreated";
                        var message = ScriptableObjectHelper.CreateScriptableObject<Message>(messageName, MessagesPath);
                        message.MessageName = messageName;
                        message.MessageCategory = MessagesCategory;
                        message.SendMethodComment = string.Empty;
                        message.EventComment = string.Empty;
                        message.InputParameters = new List<InputParameter>()
                        {
                            new InputParameter()
                            {
                                parameterName = "characterStatsInstance",
                                multiplicity = Multiplicity.Single,
                                parameterType = ParameterType.OtherType,
                                otherType = typeof(DnD.Code.Scripts.Characters.CharacterStats).FullName,
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