using DnD.Code.Scripts.Common;
using UnityEngine;

namespace DnD.Code.Scripts.Characters.Abilities
{
    [CreateAssetMenu(fileName = "NewSkill", menuName = "Game Entities/Character/Skill")]
    public class Skill : ScriptableObject, ILocalizable
    {
        [SerializeField]
        private string displayName;
        [SerializeField]
        private string displayDescription;
        
        public Ability Ability;
        
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