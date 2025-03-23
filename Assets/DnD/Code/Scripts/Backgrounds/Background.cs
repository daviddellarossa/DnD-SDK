using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Feats;
using UnityEngine;

namespace DnD.Code.Scripts.Backgrounds
{
    [CreateAssetMenu(fileName = "NewSpecies", menuName = "Game Entities/Character/Background/Background")]
    public class Background : ScriptableObject, ILocalizable
    {
        // public string Name;
        public Ability[] Abilities = new Ability[3];
        public Skill[] SkillProficiencies = new Skill[2];
        public string ToolProficiency;
        public StartingEquipment[] StartingEquipment = new StartingEquipment[2];
        public Feat Feat; // TODO: this must be a Origin Feat. Add a filter on the UI.
        public string Note = "Tool proficiency still not shown in the UI. TODO.";
        
        [SerializeField]
        private string displayName;
        [SerializeField]
        private string displayDescription;

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
