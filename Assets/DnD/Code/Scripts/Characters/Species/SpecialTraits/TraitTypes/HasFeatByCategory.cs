using System;
using DnD.Code.Scripts.Characters.Feats;

namespace DnD.Code.Scripts.Characters.Species.SpecialTraits.TraitTypes
{
    [Serializable]
    public class HasFeatByCategory: TraitType
    {
        public FeatCategory FeatCategory;
    }
}
