using UnityEngine;

namespace DnD.Code.Scripts.Species.SpecialTraits.TraitTypes
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