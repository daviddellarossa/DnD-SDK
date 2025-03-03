using Assets.Scripts.Game.Character;
using UnityEngine;

namespace DnD.Code.Scripts.Weapons
{
    [CreateAssetMenu(fileName = "NewAmmunitionType", menuName = "Game Entities/Weapons/AmmunitionType")]
    public class AmmunitionType : ScriptableObject
    {
        public string Name;
        public int Amount;
        public Storage Storage;
        public float Weight;
        public float Cost;
    }
}
