using System;
using UnityEngine;

namespace DnD.Code.Scripts.Characters
{
    [Serializable]
    public class DeathSaves
    {
        [SerializeField]
        private int successes;
        [SerializeField]
        private int failures;

        public int Successes
        {
            get => successes;
            internal set => successes = value;
        }

        public int Failures
        {
            get => failures;
            internal set => failures = value;
        }
    }
}
