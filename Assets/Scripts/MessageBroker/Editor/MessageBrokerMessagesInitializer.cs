﻿using System;
using Infrastructure.Helpers;
using UnityEditor;
using UnityEngine;

namespace MessageBroker.Editor
{
    public static partial class MessageBrokerMessagesInitializer
    {
        public static readonly string MessageBrokerMessagesPath = "Assets/Scripts/MessageBroker/Editor/Messages";
        
        [MenuItem("DeeDeeR/MessageBroker/Generate Messages")]
        public static void InitializeMessages()
        {
            Debug.Log("Initializing MessageBroker Messages");

            try
            {
                FileSystemHelper.EnsureFolderExists(MessageBrokerMessagesPath, true);
                
                GameMessagesInitializer.InitializeGameMessages();
                MenusMessagesInitializer.InitializeMenusMessages();
                CharacterMessagesInitializer.InitializeCharacterMessages();

            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
                        
            Debug.Log("MessageBroker Messages initialization completed");
        }
    }
}