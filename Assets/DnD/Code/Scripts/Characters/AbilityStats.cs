using System;
using System.Collections.Generic;
using DnD.Code.Scripts.Abilities;
using UnityEngine;

namespace DnD.Code.Scripts.Characters
{
    [Serializable]
    public class AbilityStats
    {
        [SerializeField]
        private Ability ability;

        [SerializeField]
        private int score;
        
        [SerializeField]
        private bool savingThrow;

        private Dictionary<string, SkillStats> skillProficiencies = new ();

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
        
        public int Modifier => Mathf.FloorToInt((score - Constants.BaseScoreModifierThreshold) /  (float)Constants.BaseScoreModifierStep);

        public bool SavingThrow
        {
            get => savingThrow;
            set => savingThrow = value;
        }

        public Dictionary<string, SkillStats> SkillProficiencies
        {
            get => skillProficiencies;
            set => skillProficiencies = value;
        }
    }
}
