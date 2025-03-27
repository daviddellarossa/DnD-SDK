using DnD.Code.Scripts.Common;
using UnityEngine;

namespace DnD.Code.Scripts.Feats
{
    [CreateAssetMenu(fileName = "NewFeat", menuName = "Game Entities/Character/Feats/Feat")]
    public class Feat : ScriptableObject, ILocalizable
    {
        [SerializeField]
        private string displayName;
        [SerializeField]
        private string displayDescription;
        
        public FeatCategory Category;
        public Repeatable Repeatable;
        // public object Benefit; // TODO
        // public object Prerequisite; // TODO

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
