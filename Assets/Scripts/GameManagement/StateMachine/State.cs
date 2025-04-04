using System;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    partial class GameManagerCore
    {
        [Serializable]
        public abstract class State
        {
            public GameManagerCore Core { get; private set; }

            public StateStateEnum StateState { get; protected set; } = StateStateEnum.NotInStack;


            protected State(GameManagerCore core)
            {
                this.Core = core;
            }

            public virtual void OnEnter()
            {
                StateState = StateStateEnum.InStack;
            }

            public virtual void OnExit()
            {
                StateState = StateStateEnum.NotInStack;
            }

            public virtual void OnActivate()
            {
                StateState = StateStateEnum.Activated;
            }

            public virtual void OnDeactivate()
            {
                StateState = StateStateEnum.InStack;
            }

            public virtual void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
            {
            }

            public virtual void SceneUnloaded(Scene scene)
            {
            }

        }

        public enum StateStateEnum
        {
            NotInStack,
            InStack,
            Activated,
        }
    }
}