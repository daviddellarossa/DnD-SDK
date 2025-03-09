using System.Collections.Generic;
using UnityEngine;

namespace DnD.Code.Scripts.Characters.Abilities
{
    [CreateAssetMenu(fileName = "NewAbility", menuName = "Game Entities/Character/Ability")]
    public class Ability : ScriptableObject
    {
        public string Name;
        public AbilityEnum AbilityType;
        public List<Skill> SkillList = new List<Skill>();
    }
}