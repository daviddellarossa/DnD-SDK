using System;
using System.ComponentModel;
using DnD.Code.Scripts.Characters.Classes.FeatureProperties;
using UnityEngine;

namespace DnD.Code.Scripts.Characters.Classes.Barbarian.FeatureProperties
{
    [Serializable]
    public class BarbarianFP : IClassFeatureTraits
    {
        public int rages;
        public int rageDamage;
        public int weaponMastery;

        public string DisplayText => "Barbarian Class Features";
    }
}