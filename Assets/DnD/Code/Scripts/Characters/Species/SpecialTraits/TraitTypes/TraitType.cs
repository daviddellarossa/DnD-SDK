using System;
using UnityEngine;

namespace Game.Characters.Species.SpecialTraits.TraitTypes
{
    public class TraitType : ScriptableObject
    {
        public string Name;
        public virtual void ApplyTrait(GameObject target) { }

        protected TraitType()
        {
            this.Name = this.GetType().Name;
        }
    }
}