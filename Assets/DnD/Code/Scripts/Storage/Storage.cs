using DnD.Code.Scripts.Common;
using UnityEngine;

namespace DnD.Code.Scripts.Storage
{
    [CreateAssetMenu(fileName = "NewStorage", menuName = "Game Entities/Storage")]
    public class Storage : ScriptableObject, ILocalizable
    {
        [SerializeField]
        private string displayName;
        [SerializeField]
        private string displayDescription;

        public string DisplayName
        {
            get => displayName;
            set => displayName = value;
        }

        public string DisplayDescription
        {
            get => displayDescription;
            set => displayDescription = value;
        }
    }
}
