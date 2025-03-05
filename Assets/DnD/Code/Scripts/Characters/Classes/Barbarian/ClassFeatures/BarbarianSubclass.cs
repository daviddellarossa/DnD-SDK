using DnD.Code.Scripts.Characters.Classes.ClassFeatures;
using DnD.Code.Scripts.Common;

namespace DnD.Code.Scripts.Characters.Classes.Barbarian.ClassFeatures
{
    public class BarbarianSubclass : ClassFeature, IBarbarianClassFeature
    {
        public string Name => NameHelper.ClassFeatures_Barbarian.BarbarianSubclass;
    }
}