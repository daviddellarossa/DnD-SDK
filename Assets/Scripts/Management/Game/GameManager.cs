using System.Linq;
using DnD.Code.Scripts.Characters;
using Infrastructure;
using Infrastructure.SaveManager;
using UnityEngine;
using Scene_SceneManager = Management.Scene.SceneManager;

namespace Management.Game
{
    public class GameManager : Scene_SceneManager
    {
        public GameManagerCore Core { get; protected set; }
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            OnStart();
        }

        private void OnStart()
        {
            Core.OnStart();
        }

        // Update is called once per frame
        void Awake()
        {
            RegisterMessageBrokerHandlers();
            
            OnAwake();
        }

        private void OnAwake()
        {
            Core = new GameManagerCore(this);
            Core.OnAwake();
        }
        
        void OnApplicationQuit()
        {
            UnRegisterMessageBrokerHandlers();
        }
        
        private void RegisterMessageBrokerHandlers()
        {
            if (DeeDeeR.MessageBroker.MessageBroker.Instance != null)
            {
                DeeDeeR.MessageBroker.MessageBroker.Instance.Menus.StartGame += StartNewGame_EventHandler;
                DeeDeeR.MessageBroker.MessageBroker.Instance.Menus.LoadLatestGame += LoadLatestGame_EventHandler;
                DeeDeeR.MessageBroker.MessageBroker.Instance.Menus.BackToMainMenu += BackToMainMenu_EventHandler;
                DeeDeeR.MessageBroker.MessageBroker.Instance.Menus.QuitGame += QuitGame_EventHandler;

                DeeDeeR.MessageBroker.MessageBroker.Instance.Character.CharacterCreated += Character_OnCharacterCreated_EventHandler;
            }
            else
            {
                Debug.LogError("MessageBroker instance is null");
            }
        }

        private void LoadLatestGame_EventHandler(object sender, object target)
        {
            this.Core.LoadLatestGame_EventHandler(sender, target);
        }

        private void QuitGame_EventHandler(object sender, object target)
        {
            this.Core.QuitGame_EventHandler(sender, target);
        }

        private void BackToMainMenu_EventHandler(object sender, object target)
        {
            this.Core.BackToMainMenu_EventHandler(sender, target);
        }

        private void UnRegisterMessageBrokerHandlers()
        {
            if (DeeDeeR.MessageBroker.MessageBroker.Instance != null)
            {
                DeeDeeR.MessageBroker.MessageBroker.Instance.Menus.StartGame -= StartNewGame_EventHandler;
                DeeDeeR.MessageBroker.MessageBroker.Instance.Menus.BackToMainMenu -= BackToMainMenu_EventHandler;
                DeeDeeR.MessageBroker.MessageBroker.Instance.Menus.QuitGame -= QuitGame_EventHandler;

                DeeDeeR.MessageBroker.MessageBroker.Instance.Character.CharacterCreated -= Character_OnCharacterCreated_EventHandler;
            }
            else
            {
                Debug.LogWarning("MessageBroker instance is null");
            }
        }
        
        public void StartNewGame_EventHandler(object sender, object target)
        {
            this.Core.StartNewGame_EventHandler(sender, target);
        }
        
        private void Character_OnCharacterCreated_EventHandler(object sender, object target, CharacterStats characterStats)
        {
            this.Core.Character_OnCharacterCreated_EventHandler(sender, target, characterStats);
        }
        
        //private void LoadGame
    }
}
