using DnD.Code.Scripts.Common;
using UnityEngine;

namespace DnD.Code.Scripts.Species.SpecialTraits.TraitTypes
{
    public class TypeTrait : ScriptableObject, ILocalizable
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
    }
}