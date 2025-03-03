using UnityEngine;

namespace Assets.Scripts.Game.Equipment.Gear
{
    [CreateAssetMenu(fileName = "NewRobe", menuName = "Game Entities/Equipment/Gear/Robe")]
    public class Robe : ScriptableObject, IGear
    {
        public string Name;
        public string Description;
        public string DisplayText => this.Name;
    }
}
