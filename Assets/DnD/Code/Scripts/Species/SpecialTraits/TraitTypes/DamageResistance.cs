using System;
using DnD.Code.Scripts.Combat;
using UnityEngine;

namespace DnD.Code.Scripts.Species.SpecialTraits.TraitTypes
{
    [Serializable]
    public class DamageResistance : TypeTrait
    {
        public DamageType DamageType;
        public float Percent;
    }
}