using DnD.Code.Scripts.Classes.ClassFeatures;
using DnD.Code.Scripts.Helpers.NameHelper;

namespace DnD.Code.Scripts.Classes.Barbarian.ClassFeatures
{
    public class Retaliation : ClassFeature, IBarbarianClassFeature
    {
        public override string ClassFeatureName => Helpers.NameHelper.NameHelper.ClassFeaturesBarbarian.Retaliation;
        
        public override string DisplayName => $"{NameHelper.Naming.ClassFeatures}.{NameHelper.Classes.Barbarian}.{ClassFeatureName}";

        public override string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
    }
}