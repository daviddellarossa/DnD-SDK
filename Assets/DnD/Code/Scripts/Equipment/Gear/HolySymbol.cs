using Assets.Scripts.Game.Equipment.Gear;
using UnityEngine;

namespace DnD.Code.Scripts.Equipment.Gear
{
    [CreateAssetMenu(fileName = "NewHolySymbol", menuName = "Game Entities/Equipment/Gear/Holy Symbol")]
    public class HolySymbol : ScriptableObject, IGear
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
