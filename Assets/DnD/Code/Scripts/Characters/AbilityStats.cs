using System;
using System.Collections.Generic;
using DnD.Code.Scripts.Abilities;
using UnityEngine;

namespace DnD.Code.Scripts.Characters
{
    [Serializable]
    public class AbilityStats
    {
        public Ability Ability;

        public int Score;
        public int Modifier => Mathf.FloorToInt((Score - 10) / 2f);

        public bool SavingThrow;

        public Dictionary<Skill, Proficiency<Skill>> SkillProficiencies = new Dictionary<Skill, Proficiency<Skill>>();

        public AbilityStats(Ability ability)
        {
            this.Ability = ability;
        }
    }
}
