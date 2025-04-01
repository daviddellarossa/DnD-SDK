using System;
using DnD.Code.Scripts.Classes.FeatureProperties;
using UnityEngine;

namespace DnD.Code.Scripts.Classes.Barbarian.FeatureProperties
{
    [Serializable]
    public class BarbarianFeatureStats : IClassFeatureStats
    {
        [SerializeField]
        private int rages;
        [SerializeField]
        private int rageDamage;
        [SerializeField]
        private int weaponMastery;

        public int Rages
        {
            get => rages;
            set => rages = value;
        }

        public int RageDamage
        {
            get => rageDamage;
            set => rageDamage = value;
        }

        public int WeaponMastery
        {
            get => weaponMastery;
            set => weaponMastery = value;
        }
    }
}