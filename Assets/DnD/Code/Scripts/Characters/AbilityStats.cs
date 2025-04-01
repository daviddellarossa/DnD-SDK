using System;
using System.Collections.Generic;
using DnD.Code.Scripts.Abilities;
using UnityEngine;

namespace DnD.Code.Scripts.Characters
{
    [Serializable]
    public class AbilityStats
    {
        private Ability ability;

        private int score;
        
        private bool savingThrow;

        //public Dictionary<Skill, Proficiency<Skill>> SkillProficiencies = new Dictionary<Skill, Proficiency<Skill>>();

        public Ability Ability
        {
            get => ability;
            set => ability = value;
        }

        public int Score
        {
            get => score;
            set => score = value;
        }
        
        public int Modifier => Mathf.FloorToInt((score - 10) / 2f);

        public bool SavingThrow
        {
            get => savingThrow;
            set => savingThrow = value;
        }
    }
}
