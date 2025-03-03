using Assets.Scripts.Game.Characters.Feats;
using Game.Characters.Species.SpecialTraits.TraitTypes;
using System;


namespace Assets.Scripts.Game.Characters.Species.SpecialTraits.TraitTypes
{
    [Serializable]
    public class HasFeatByCategory: TraitType
    {
        public FeatCategory FeatCategory;
    }
}
