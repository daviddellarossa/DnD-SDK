using UnityEngine;

namespace Assets.Scripts.Game.Equipment.Gear
{
    [CreateAssetMenu(fileName = "NewParchment", menuName = "Game Entities/Equipment/Gear/Parchment")]
    public class Parchment : ScriptableObject, IGear
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
