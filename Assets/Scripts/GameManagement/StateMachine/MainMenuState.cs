using UnityEngine.SceneManagement;

// ReSharper disable once CheckNamespace
namespace GameManagement
{
    public partial class GameManagerCore
    {
        public class SampleSceneState : State
        {
            private readonly string sceneName = "MainMenu";

            //protected IPreRollManager _menuManager;

            internal SampleSceneState(GameManagerCore core) : base(core)
            {
            }

            public override void OnEnter()
            {
                base.OnEnter();

                Core.LoadScene(sceneName, LoadSceneMode.Additive);
            }

            public override void OnExit()
            {
                base.OnExit();

                Core.UnloadScene(sceneName);
            }

            // public override void PauseResumeGame()
            // {
            //     base.PauseResumeGame();
            //
            //     this.Core.Parent.StaticObjects.MessageBroker.Menus.Send_PreRollFinished(this, null);
            // }


            public override void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
            {
                // _menuManager = GetMenuManagerFromScene(scene);
                // if (_menuManager == null)
                //    return;
            }

            // protected virtual IPreRollManager GetMenuManagerFromScene(Scene scene)
            // {
            //     if (scene.name != _sceneName)
            //         return null;
            //
            //     var rootGameObjects = scene.GetRootGameObjects();
            //     var sceneManagerGo = rootGameObjects.Single(x => x.name == "SceneManager");
            //     var menuManager = sceneManagerGo.GetComponent<PreRollManager>();
            //     return menuManager;
            // }

        }
    }
}