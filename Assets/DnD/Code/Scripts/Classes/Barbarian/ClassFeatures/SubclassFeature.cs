using DnD.Code.Scripts.Classes.ClassFeatures;

namespace DnD.Code.Scripts.Classes.Barbarian.ClassFeatures
{
    public class SubclassFeature : ClassFeature, IBarbarianClassFeature
    {
        public string Name => Helpers.NameHelper.NameHelper.ClassFeatures_Barbarian.SubclassFeature;
    }
}