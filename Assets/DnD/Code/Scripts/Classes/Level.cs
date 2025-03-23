using System.Collections.Generic;
using DnD.Code.Scripts.Classes.ClassFeatures;
using DnD.Code.Scripts.Classes.FeatureProperties;
using UnityEngine;

namespace DnD.Code.Scripts.Classes
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Game Entities/Character/Classes/Level")]
    public class Level : ScriptableObject
    {
        public int LevelNum;
        public int ProficiencyBonus;
        
        [SerializeReference]
        public List<ClassFeature> ClassFeatures = new ();
        
        [SerializeReference]
        public IClassFeatureTraits ClassFeatureTraits;
    }
}