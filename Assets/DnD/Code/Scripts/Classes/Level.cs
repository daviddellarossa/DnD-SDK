using System.Collections.Generic;
using DnD.Code.Scripts.Classes.ClassFeatures;
using DnD.Code.Scripts.Classes.FeatureProperties;
using UnityEngine;
using UnityEngine.Serialization;

namespace DnD.Code.Scripts.Classes
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Game Entities/Character/Classes/Level")]
    public class Level : ScriptableObject
    {
        [SerializeField]
        private string displayName;
        [SerializeField]
        private string displayDescription;
        
        [SerializeField]
        private int levelNum;
        [SerializeField]
        private int proficiencyBonus;

        [SerializeReference]
        [SerializeField]
        private List<ClassFeature> classFeatures = new ();
        
        [FormerlySerializedAs("classFeatureTraits")]
        [SerializeReference]
        [SerializeField]
        private IClassFeatureStats classFeatureStats;
        
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
        
        public int LevelNum
        {
            get => this.levelNum;
            set => this.levelNum = value;
        }

        public int ProficiencyBonus
        {
            get => this.proficiencyBonus;
            set => this.proficiencyBonus = value;
        }

        public List<ClassFeature> ClassFeatures
        {
            get => this.classFeatures;
            set => this.classFeatures = value;
        }

        public IClassFeatureStats ClassFeatureStats
        {
            get => this.classFeatureStats;
            set => this.classFeatureStats = value;
        }
    }
}