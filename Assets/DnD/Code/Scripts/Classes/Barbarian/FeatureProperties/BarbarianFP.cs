using System;
using DnD.Code.Scripts.Classes.FeatureProperties;

namespace DnD.Code.Scripts.Classes.Barbarian.FeatureProperties
{
    [Serializable]
    public class BarbarianFp : IClassFeatureTraits
    {
        public int rages;
        public int rageDamage;
        public int weaponMastery;

        public string DisplayText => "Barbarian Class Features";
    }
}