﻿using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Equipment;
using DnD.Code.Scripts.Feats;
using DnD.Code.Scripts.Tools;
using UnityEngine;
using UnityEngine.Serialization;

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
        private Proficient toolProficiency;
        
        [FormerlySerializedAs("startingEquipmentOptions")] [SerializeField]
        private StartingEquipment[] startingEquipmentOptionsOptions = new StartingEquipment[2];
        
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

        public Proficient ToolProficiency
        {
            get => toolProficiency;
            set => toolProficiency = value;
        }

        public StartingEquipment[] StartingEquipmentOptions
        {
            get => startingEquipmentOptionsOptions;
            set => startingEquipmentOptionsOptions = value;
        }

        public Feat Feat
        {
            get => feat;
            set => feat = value;
        }
    }
}
