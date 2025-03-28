using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Feats;
using UnityEngine;

namespace DnD.Code.Scripts.Backgrounds
{
    [CreateAssetMenu(fileName = "NewSpecies", menuName = "Game Entities/Character/Background/Background")]
    public class Background : ScriptableObject, ILocalizable
    {
        [SerializeField]
        private string displayName;
        
        [SerializeField]
        private string displayDescription;
        
        [SerializeField]
        private Ability[] abilities = new Ability[3];
        
        [SerializeField]
        private Skill[] skillProficiencies = new Skill[2];
        
        [SerializeField]
        // TODO: use a more specific type for toolProficiency: string is not the safest type to use here
        private string toolProficiency;
        
        [SerializeField]
        private StartingEquipment[] startingEquipment = new StartingEquipment[2];
        
        [SerializeField]
        private Feat feat; // TODO: this must be a Origin Feat. Add a filter on the UI.
        
        
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

        public Ability[] Abilities
        {
            get => abilities;
            set => abilities = value;
        }

        public Skill[] SkillProficiencies
        {
            get => skillProficiencies;
            set => skillProficiencies = value;
        }

        public string ToolProficiency
        {
            get => toolProficiency;
            set => toolProficiency = value;
        }

        public StartingEquipment[] StartingEquipment
        {
            get => startingEquipment;
            set => startingEquipment = value;
        }

        public Feat Feat
        {
            get => feat;
            set => feat = value;
        }
    }
}
