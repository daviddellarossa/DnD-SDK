using System;
using System.Collections.Generic;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Species.SpecialTraits;
using UnityEngine;
using UnityEngine.Serialization;

namespace DnD.Code.Scripts.Species
{
    [CreateAssetMenu(fileName = "NewSpex", menuName = "Game Entities/Character/Species/Spex")]
    public class Spex : ScriptableObject, ILocalizable
    {
        [SerializeField]
        private string displayName;
        [SerializeField]
        private string displayDescription;
        
        [SerializeField]
        private Spex inheritFrom;
        
        [SerializeField]
        private CreatureType creatureType;
        
        [SerializeField]
        private Size size;
        
        [SerializeField]
        private float speed;
        
        [SerializeField]
        private List<SpecialTrait> specialTraits = new ();
        
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
        
        public Spex InheritFrom
        {
            get => inheritFrom;
            set => inheritFrom = value;
        }
        
        public CreatureType CreatureType
        {
            get => creatureType;
            set => creatureType = value;
        }
        
        public Size Size
        {
            get => size;
            set => size = value;
        }
        
        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public List<SpecialTrait> SpecialTraits
        {
            get => specialTraits;
            set => specialTraits = value;
        }
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