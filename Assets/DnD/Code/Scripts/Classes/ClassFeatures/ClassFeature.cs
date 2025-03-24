using DnD.Code.Scripts.Common;
using UnityEngine;

namespace DnD.Code.Scripts.Classes.ClassFeatures
{
    [CreateAssetMenu(fileName = "NewClassFeature", menuName = "Game Entities/Character/Classes/Class Feature")]
    public abstract class ClassFeature : ScriptableObject, ILocalizable
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