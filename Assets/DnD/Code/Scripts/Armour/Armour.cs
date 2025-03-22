using DnD.Code.Scripts.Items;
using UnityEngine;

namespace DnD.Code.Scripts.Armour
{
    [CreateAssetMenu(fileName = "NewArmour", menuName = "Game Entities/Armours/Armour")]
    public class Armour : ScriptableObject, IItem, IArmour
    {
        // public string Name;
        public ArmourType Type;
        public int ArmourClass;
        public bool AddDexModifier;
        public bool CapDexModifier;
        public int MaxDexModifier;
        public bool HasDisadvantageOnDexterityChecks;

        public int Strength;
        public float Weight;
        public float Cost;

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
