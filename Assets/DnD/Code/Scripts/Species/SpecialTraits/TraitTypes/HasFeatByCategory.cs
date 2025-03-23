using System;
using DnD.Code.Scripts.Feats;

namespace DnD.Code.Scripts.Species.SpecialTraits.TraitTypes
{
    [Serializable]
    public class HasFeatByCategory: TraitType
    {
        public FeatCategory FeatCategory;
    }
}
