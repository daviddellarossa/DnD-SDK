using DnD.Code.Scripts.Items;
using UnityEngine;

namespace DnD.Code.Scripts.Armour
{
    [CreateAssetMenu(fileName = "NewShield", menuName = "Game Entities/Armours/Shield")]
    public class Shield : ScriptableObject, IItem, IShield
    {
        public string Name;
        public ShieldType Type;
        public int IncrementArmourClassBy;
        public float Weight;
        public float Cost;

        public string DisplayText => this.Name;
    }
}
