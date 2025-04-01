using System;
using DnD.Code.Scripts.Classes.FeatureProperties;

namespace DnD.Code.Scripts.Classes.Barbarian.FeatureProperties
{
    [Serializable]
    public class BarbarianFeatureStats : IClassFeatureStats
    {
        private int rages;
        private int rageDamage;
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