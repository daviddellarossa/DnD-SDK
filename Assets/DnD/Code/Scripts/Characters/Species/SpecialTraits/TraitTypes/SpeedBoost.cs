using System;
using UnityEngine;

namespace Game.Characters.Species.SpecialTraits.TraitTypes
{
    [Serializable]
    public class SpeedBoost : TraitType
    {
        public float speedMultiplier;

        public override void ApplyTrait(GameObject target)
        {
            Debug.Log($"Applied speed boost ({speedMultiplier}x) to {target.name}");
        }
    }
}