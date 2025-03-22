using DnD.Code.Scripts.Common;
using UnityEngine;

namespace DnD.Code.Scripts.Weapons.MasteryProperties
{
    [CreateAssetMenu(fileName = "NewMasteryProperty", menuName = "Game Entities/Weapons/MasteryProperty")]
    public class MasteryProperty : ScriptableObject, ILocalizable
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
