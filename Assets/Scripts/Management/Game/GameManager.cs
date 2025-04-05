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
        
        void OnDestroy()
        {
            //UnRegisterMessageBrokerHandlers();
        }
        
        private void RegisterMessageBrokerHandlers()
        {
            // this.StaticObjects.MessageBroker.Menus.BackToMain += BackToMain_EventHandler;
            // this.StaticObjects.MessageBroker.Menus.OpenHelp += OpenHelp_EventHandler;
            // this.StaticObjects.MessageBroker.Menus.OpenCredits += OpenCredits_EventHandler;
            // this.StaticObjects.MessageBroker.Menus.QuitGame += QuitGame_EventHandler;
            // this.StaticObjects.MessageBroker.Menus.QuitCurrentGame += QuitCurrentGame_EventHandler;
            // this.StaticObjects.MessageBroker.Menus.ResumeGame += ResumeGame_EventHandler;
            // this.StaticObjects.MessageBroker.Menus.StartGame += StartGame_EventHandler;
            // this.StaticObjects.MessageBroker.Menus.PauseGame += PauseGame_EventHandler;
            // this.StaticObjects.MessageBroker.Menus.PreRollFinished += PreRollFinished_EventHandler;
            //this.StaticObjects.MessageBroker.Input.TogglePause += TogglePause_EventHandler;
            
            if (DeeDeeR.MessageBroker.MessageBroker.Instance != null)
            {
                DeeDeeR.MessageBroker.MessageBroker.Instance.Menus.StartGame += StartGame_EventHandler;
            }
            else
            {
                Debug.LogWarning("MessageBroker instance is null");
            }
        }

        private void UnRegisterMessageBrokerHandlers()
        {
            if (DeeDeeR.MessageBroker.MessageBroker.Instance != null)
            {
                DeeDeeR.MessageBroker.MessageBroker.Instance.Menus.StartGame -= StartGame_EventHandler;

            }
            else
            {
                Debug.LogWarning("MessageBroker instance is null");
            }
            
            // this.StaticObjects.MessageBroker.Menus.BackToMain -= BackToMain_EventHandler;
            // this.StaticObjects.MessageBroker.Menus.OpenHelp -= OpenHelp_EventHandler;
            // this.StaticObjects.MessageBroker.Menus.OpenCredits -= OpenCredits_EventHandler;
            // this.StaticObjects.MessageBroker.Menus.QuitGame -= QuitGame_EventHandler;
            // this.StaticObjects.MessageBroker.Menus.QuitCurrentGame -= QuitCurrentGame_EventHandler;
            // this.StaticObjects.MessageBroker.Menus.ResumeGame -= ResumeGame_EventHandler;
            // this.StaticObjects.MessageBroker.Menus.StartGame -= StartGame_EventHandler;
            // this.StaticObjects.MessageBroker.Menus.PauseGame -= PauseGame_EventHandler;
            // this.StaticObjects.MessageBroker.Menus.PreRollFinished -= PreRollFinished_EventHandler;
            //this.StaticObjects.MessageBroker.Input.TogglePause -= TogglePause_EventHandler;
        }
        
        public void StartGame_EventHandler(object sender, object target)
        {
            this.Core.StartGame_EventHandler(sender, target);
        }
    }
}
