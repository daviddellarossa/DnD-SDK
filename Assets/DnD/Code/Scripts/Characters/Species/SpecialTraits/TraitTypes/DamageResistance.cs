using System;
using DnD.Code.Scripts.Combat;
using UnityEngine;

namespace Game.Characters.Species.SpecialTraits.TraitTypes
{
    [Serializable]
    public class DamageResistance : TraitType
    {
        public DamageType DamageType;
        public float Percent;

        public override void ApplyTrait(GameObject target)
        {
            throw new NotImplementedException();
        }
    }
}