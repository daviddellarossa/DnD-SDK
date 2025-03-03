using System.Collections.Generic;
using DnD.Code.Scripts.Combat;
using DnD.Code.Scripts.Items;
using DnD.Code.Scripts.Weapons.MasteryProperties;
using DnD.Code.Scripts.Weapons.Properties;
using UnityEngine;

namespace DnD.Code.Scripts.Weapons
{
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "Game Entities/Weapons/Weapon")]
    public class Weapon : ScriptableObject, IItem, IWeapon
    {
        public string Name;
        public WeaponType Type;
        public int NumOfDamageDice = 1;
        public Die DamageDie;
        public DamageType DamageType;
        public List<Property> Properties = new();
        public MasteryProperty MasteryProperty;
        public float Weight;
        public float Cost;

        public string DisplayText => this.Name;
    }
}
