using DnD.Code.Scripts.Classes.ClassFeatures;
using DnD.Code.Scripts.Helpers.NameHelper;
using UnityEngine;

namespace DnD.Code.Scripts.Classes.Barbarian.ClassFeatures
{
    public class AbilityScoreImprovement : ClassFeature, IBarbarianClassFeature
    {
        public override string ClassFeatureName => NameHelper.ClassFeaturesBarbarian.AbilityScoreImprovement;
        
        public override string DisplayName => $"{NameHelper.Naming.ClassFeatures}.{NameHelper.Classes.Barbarian}.{ClassFeatureName}";

        public override string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
    }
}