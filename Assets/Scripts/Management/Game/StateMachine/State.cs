using System;
using UnityEngine.SceneManagement;

namespace Management.Game
{
    partial class GameManagerCore
    {
        [Serializable]
        public abstract class State
        {
            public Management.Game.GameManagerCore Core { get; private set; }

            public StateStateEnum StateState { get; protected set; } = StateStateEnum.NotInStack;


            protected State(Management.Game.GameManagerCore core)
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

            public virtual void SceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode loadSceneMode)
            {
            }

            public virtual void SceneUnloaded(UnityEngine.SceneManagement.Scene scene)
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