using System;
using System.Collections.Generic;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Species.SpecialTraits;
using UnityEngine;

namespace DnD.Code.Scripts.Species
{
    [CreateAssetMenu(fileName = "NewSpex", menuName = "Game Entities/Character/Species/Spex")]
    public class Spex : ScriptableObject, ILocalizable
    {
        public Spex InheritFrom;
        public CreatureType CreatureType;
        public Size Size;
        public float Speed;
        
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

        public List<SpecialTrait> Traits = new ();
    }

    [Flags]
    public enum Size
    {
        None    = 0,
        Small   = 1 << 0,
        Medium  = 1 << 1,
        Large   = 1 << 2,
    }
}