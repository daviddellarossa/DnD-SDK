using DnD.Code.Scripts.Characters;
using Infrastructure.SaveManager;
using Infrastructure.SaveManager.Models;
using Management.Game.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Management.Game
{
    public partial class GameManagerCore
    {
        public GameManagerCore(GameManager parent)
        {
            this.Parent = parent;
            
            stateFactory = new StateFactory(this);

            sceneManagerWrapper = SceneManagerWrapper.Instance;
        }

        internal readonly GameManager Parent;
        private readonly SceneManagerWrapper sceneManagerWrapper;
        
        private readonly StateStack stateStack = new ();

        private readonly IStateFactory stateFactory;
        
        private void LoadScene(string sceneName, LoadSceneMode loadSceneMode)
        {
            sceneManagerWrapper.LoadSceneAsync(sceneName, loadSceneMode);
        }

        private void UnloadScene(string sceneName)
        {
            sceneManagerWrapper.UnloadSceneAsync(sceneName);
        }
        
        public void OnAwake()
        {
            stateStack.PoppingStateEvent += StateStack_PoppingStateEvent;
            stateStack.PushingStateEvent += StateStack_PushingStateEvent;
        }

        public void OnEnable() { }

        public void OnStart()
        {
            sceneManagerWrapper.SceneLoaded += SceneLoaded;
            sceneManagerWrapper.SceneUnloaded += SceneUnloaded;

            PushState(stateFactory.MainMenuState);
        }
        
        protected virtual void StateStack_PushingStateEvent(object sender, GameManagerCore.State state)
        {
        }

        protected virtual void StateStack_PoppingStateEvent(object sender, GameManagerCore.State state)
        {
        }

        private void SceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode loadSceneMode)
        {
            var currentState = stateStack.Peek();
            if (currentState == null)
            {
                return;
            }
            currentState.SceneLoaded(scene, loadSceneMode);

        }

        private void SceneUnloaded(UnityEngine.SceneManagement.Scene scene)
        {
            var currentState = stateStack.Peek();
            if (currentState == null)
            {
                return;
            }
            currentState.SceneUnloaded(scene);

        }
        
        #region Handler methods for StateStack

        protected virtual void ReplaceState(GameManagerCore.State state)
        {
            PopState();

            PushState(state);
        }

        protected virtual void PushState(GameManagerCore.State state)
        {
            stateStack.Push(state);
        }

        protected virtual void PopState()
        {
            var state = stateStack.Pop();
        }

        #endregion
        
        public void StartNewGame_EventHandler(object sender, object target)
        {
            ReplaceState(stateFactory.CharacterBuildState);
        }

        public void StartGame_EventHandler(object sender, object target, SaveGameData saveGameData)
        {
            // NOTE: Review how the saveGameData is passed to the PlayGameState.
            // Currently, the states are reused and they can share state among calls.
            // Evaluate if PlayGameState is an exception and needs to be instantiated every time.
            stateFactory.PlayGameState.SetSaveGameData(saveGameData);
            ReplaceState(stateFactory.PlayGameState);
        }
        
        public void BackToMainMenu_EventHandler(object sender, object target)
        {
            ReplaceState(stateFactory.MainMenuState);
        }

        public void QuitGame_EventHandler(object sender, object target)
        {
            Application.Quit();
            
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        public void Character_OnCharacterCreated_EventHandler(object sender, object target, CharacterStats characterStats)
        {
            var entityToSaveGameDataConverter = new EntityToSaveGameDataConverter();
            
            SaveGameData savegameData = new SaveGameData()
            {
                CharacterStats =  entityToSaveGameDataConverter.Convert(characterStats),
            };

            SaveManager.Save(savegameData);
            
            DeeDeeR.MessageBroker.MessageBroker.Instance.Logger.Send_OnLog(this, nameof(Logger), $"Savegame created.", LogType.Log);
            
            this.StartGame_EventHandler(sender, target, savegameData);
        }

        public void LoadLatestGame_EventHandler(object sender, object target)
        {
            var loadedObject = SaveManager.Load();

            if (loadedObject == null)
            {
                DeeDeeR.MessageBroker.MessageBroker.Instance.Logger.Send_OnLog(this, nameof(Logger), $"Savegame not found.", LogType.Error);
                return;
            }
            
            DeeDeeR.MessageBroker.MessageBroker.Instance.Logger.Send_OnLog(this, nameof(Logger), $"Savegame loaded.", LogType.Log);
            
            this.StartGame_EventHandler(sender, target, loadedObject);
        }
    }
}
