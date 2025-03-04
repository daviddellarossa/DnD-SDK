using Game.Characters.Species.SpecialTraits.TraitTypes;
using System;
using DnD.Code.Scripts.Characters.Feats;


namespace Assets.Scripts.Game.Characters.Species.SpecialTraits.TraitTypes
{
    [Serializable]
    public class HasFeatByCategory: TraitType
    {
        public FeatCategory FeatCategory;
    }
}
