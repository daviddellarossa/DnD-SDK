using DnD.Code.Scripts.Characters.Classes.ClassFeatures;
using DnD.Code.Scripts.Common;

namespace DnD.Code.Scripts.Characters.Classes.Barbarian.ClassFeatures
{
    public class PersistentRage : ClassFeature, IBarbarianClassFeature
    {
        public string Name => NameHelper.ClassFeatures_Barbarian.PersistentRage;
    }
}