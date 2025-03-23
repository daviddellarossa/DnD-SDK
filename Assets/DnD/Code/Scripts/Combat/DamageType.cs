using DnD.Code.Scripts.Common;
using UnityEngine;

namespace DnD.Code.Scripts.Combat
{
    [CreateAssetMenu(fileName = "NewDamageType", menuName = "Game Entities/DamageType")]
    public class DamageType : ScriptableObject, ILocalizable
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
