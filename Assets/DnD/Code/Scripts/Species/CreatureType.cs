using DnD.Code.Scripts.Common;
using UnityEngine;

namespace DnD.Code.Scripts.Species
{
    [CreateAssetMenu(fileName = "NewCreatureType", menuName = "Game Entities/Character/Species/CreatureType")]
    public class CreatureType : ScriptableObject, ILocalizable
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