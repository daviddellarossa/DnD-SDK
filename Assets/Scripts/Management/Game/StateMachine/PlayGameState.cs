

using System.Linq;
using Infrastructure.SaveManager;
using Infrastructure.SaveManager.Models;
using UnityEngine;
// ReSharper disable once CheckNamespace
using UnityEngine.SceneManagement;

// ReSharper disable once CheckNamespace
namespace Management.Game
{
    public partial class GameManagerCore
    {
        public class PlayGameState : GameManagerCore.State
        {
            private readonly string sceneName = "TestCharacter";

            private SaveGameData saveGameData;

            public void SetSaveGameData(SaveGameData data) => this.saveGameData = data;
            
            internal PlayGameState(Management.Game.GameManagerCore core) : base(core)
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


            public override void SceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode loadSceneMode)
            {
                var sceneManager = GetSceneManagerFromScene(scene);
                if (sceneManager == null)
                {
                    DeeDeeR.MessageBroker.MessageBroker.Instance.Logger.Send_OnLog(this, nameof(Logger), $"SceneManager for scene \"{sceneName}\" not found", LogType.Error);
                }
                sceneManager.SetSaveGameData(saveGameData);
            }

            protected virtual Management.Levels.LevelManager GetSceneManagerFromScene(UnityEngine.SceneManagement.Scene scene)
            {
                var rootGameObjects = scene.GetRootGameObjects();
                var sceneManagerGo = rootGameObjects.Single(x => x.name == "SceneManager");
                var sceneManager = sceneManagerGo.GetComponent<Management.Levels.LevelManager>();
                
                return sceneManager;
            }

        }
    }
}