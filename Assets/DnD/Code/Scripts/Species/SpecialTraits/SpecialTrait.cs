using System.Collections.Generic;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Species.SpecialTraits.TraitTypes;
using UnityEngine;

namespace DnD.Code.Scripts.Species.SpecialTraits
{
    [CreateAssetMenu(fileName = "NewSpecialTrait", menuName = "Game Entities/Character/Species/SpecialTrait")]
    public class SpecialTrait : ScriptableObject, ILocalizable
    {
        [SerializeField]
        private string displayName;
        [SerializeField]
        private string displayDescription;

        public string DisplayName
        {
            get => displayName;
            set => displayName = value;
        }

        public string DisplayDescription
        {
            get => displayDescription;
            set => displayDescription = value;
        }

        [SerializeReference]
        public List<TraitType> TraitTypes = new();
        
    }
}
