using System.Collections.Generic;
using DnD.Code.Scripts.Characters.Species.SpecialTraits.TraitTypes;
using UnityEngine;

namespace DnD.Code.Scripts.Characters.Species.SpecialTraits
{
    [CreateAssetMenu(fileName = "NewSpecialTrait", menuName = "Game Entities/Character/Species/SpecialTrait")]
    public class SpecialTrait : ScriptableObject
    {
        public string Name;

        [SerializeReference]
        public List<TraitType> TraitTypes = new();
        
    }
}
