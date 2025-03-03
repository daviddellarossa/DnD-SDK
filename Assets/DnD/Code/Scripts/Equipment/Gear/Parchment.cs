using UnityEngine;

namespace Assets.Scripts.Game.Equipment.Gear
{
    [CreateAssetMenu(fileName = "NewParchment", menuName = "Game Entities/Equipment/Gear/Parchment")]
    public class Parchment : ScriptableObject, IGear
    {
        public string Name;
        public string Description;
        public string DisplayText => this.Name;
    }
}
