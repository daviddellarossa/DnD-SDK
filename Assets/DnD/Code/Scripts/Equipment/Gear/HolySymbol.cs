using UnityEngine;

namespace Assets.Scripts.Game.Equipment.Gear
{
    [CreateAssetMenu(fileName = "NewHolySymbol", menuName = "Game Entities/Equipment/Gear/Holy Symbol")]
    public class HolySymbol : ScriptableObject, IGear
    {
        public string Name;
        public string Description;
        public string DisplayText => this.Name;
    }
}
