using UnityEngine;

namespace DnD.Code.Scripts.Characters.Abilities
{
    [CreateAssetMenu(fileName = "NewSkill", menuName = "Game Entities/Character/Skill")]
    public class Skill : ScriptableObject
    {
        public string Name;
        public AbilityEnum Ability;
    }
}