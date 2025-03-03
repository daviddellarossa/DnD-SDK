using Game.Characters.Species.SpecialTraits;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Characters.Species
{
    [CreateAssetMenu(fileName = "NewSpecies", menuName = "Game Entities/Character/Species/Species")]
    public class Species : ScriptableObject
    {
        public string Name;
        public Species InheritFrom;
        public CreatureType CreatureType;
        public Size Size;
        public float Speed;

        public List<SpecialTrait> Traits;
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