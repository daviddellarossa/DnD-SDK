using Game.Characters.Species.SpecialTraits.TraitTypes;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Species.SpecialTraits
{
    [CreateAssetMenu(fileName = "NewSpecialTrait", menuName = "Game Entities/Character/Species/SpecialTrait")]
    public class SpecialTrait : ScriptableObject
    {
        public string Name;

        [SerializeReference]
        public List<TraitType> TraitTypes = new();

        public SpecialTrait()
        {
            this.Name = nameof(SpecialTrait);
        }
    }
}
