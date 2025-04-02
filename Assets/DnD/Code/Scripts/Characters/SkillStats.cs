using System;
using DnD.Code.Scripts.Abilities;
using UnityEngine;

namespace DnD.Code.Scripts.Characters
{
    [Serializable]
    public class SkillStats
    {
        [SerializeField]
        private Skill skill;
        
        [SerializeField]
        private bool isExpert;

        public Skill Skill
        {
            get => skill;
            set => skill = value;
        }

        public bool IsExpert
        {
            get => isExpert;
            set => isExpert = value;
        }
    }
}
