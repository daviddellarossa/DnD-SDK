using System;
using System.Collections.Generic;
using DnD.Code.Scripts.Species.SpecialTraits;
using UnityEngine;

namespace DnD.Code.Scripts.Species
{
    [CreateAssetMenu(fileName = "NewSpecies", menuName = "Game Entities/Character/Species/Species")]
    public class Species : ScriptableObject
    {
        public string Name;
        public Species InheritFrom;
        public CreatureType CreatureType;
        public Size Size;
        public float Speed;

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