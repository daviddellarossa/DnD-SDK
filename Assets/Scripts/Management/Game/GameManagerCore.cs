using Infrastructure.SaveManager;
using Management.Game.StateMachine;
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
            ReplaceState(stateFactory.PlayGameState);
        }
    }
}
