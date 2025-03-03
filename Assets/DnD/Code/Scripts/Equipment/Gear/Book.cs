using UnityEngine;

namespace Assets.Scripts.Game.Equipment.Gear
{
    [CreateAssetMenu(fileName = "NewBook", menuName = "Game Entities/Equipment/Gear/Book")]

    public class Book : ScriptableObject, IGear
    {
        public string Name;
        public string Description;
        public string DisplayText => this.Name;
    }
}
