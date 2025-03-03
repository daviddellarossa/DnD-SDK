using System;
using System.ComponentModel;
using DnD.Code.Scripts.Characters.Classes.FeatureProperties;
using UnityEngine;

namespace DnD.Code.Scripts.Characters.Classes.Barbarian.FeatureProperties
{
    [Serializable]
    public class BarbarianFP : IClassFeatureTraits
    {
        [SerializeField] private int rages;
        [SerializeField] private int rageDamage;
        [SerializeField] private int weaponMastery;

        [DisplayName("Rages")]
        public int Rages => this.rages;
        [DisplayName("Rage Damage")]
        public int RageDamage => this.rageDamage;
        [DisplayName("Weapon Mastery")]
        public int WeaponMastery => this.weaponMastery;

        public string DisplayText => "Barbarian Class Features";
    }
}