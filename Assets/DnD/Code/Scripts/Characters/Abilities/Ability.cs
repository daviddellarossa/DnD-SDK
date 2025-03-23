using System.Collections.Generic;
using DnD.Code.Scripts.Common;
using UnityEngine;

namespace DnD.Code.Scripts.Characters.Abilities
{
    [CreateAssetMenu(fileName = "NewAbility", menuName = "Game Entities/Character/Ability")]
    public class Ability : ScriptableObject, ILocalizable
    {
        [SerializeField]
        private string displayName;
        [SerializeField]
        private string displayDescription;

        //public AbilityEnum AbilityType;
        public List<Skill> SkillList = new List<Skill>();
        
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