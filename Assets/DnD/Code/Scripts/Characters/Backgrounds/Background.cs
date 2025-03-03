using Assets.Scripts.Game.Characters.Feats;
using DnD.Code.Scripts.Characters.Abilities;
using DnD.Code.Scripts.Tools;
using UnityEngine;

namespace DnD.Code.Scripts.Characters.Backgrounds
{
    [CreateAssetMenu(fileName = "NewSpecies", menuName = "Game Entities/Character/Background/Background")]
    public class Background : ScriptableObject
    {
        public string Name;
        public AbilityEnum[] Abilities = new AbilityEnum[3];
        public Skill[] SkillProficiencies = new Skill[2];
        public ITool ToolProficiency;
        public StartingEquipment[] StartingEquipment = new StartingEquipment[2];
        public Feat Feat; // TODO: this must be a Origin Feat. Add a filter on the UI.
        public string Note = "Tool proficiency still not shown in the UI. TODO.";
    }
}
