using DnD.Code.Scripts.Common;
using UnityEngine;

namespace DnD.Code.Scripts.Feats
{
    [CreateAssetMenu(fileName = "NewFeatCategory", menuName = "Game Entities/Character/Feats/FeatCategory")]
    public class FeatCategory : ScriptableObject, ILocalizable
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
