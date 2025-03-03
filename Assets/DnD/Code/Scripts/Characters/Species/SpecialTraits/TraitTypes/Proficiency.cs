using Game.Characters.Species.SpecialTraits.TraitTypes;
using System;
using DnD.Code.Scripts.Characters.Abilities;

namespace Assets.Scripts.Game.Characters.Species.SpecialTraits.TraitTypes
{
    [Serializable]
    public class Proficiency: TraitType
    {
        public Skill Skill;
    }
}
