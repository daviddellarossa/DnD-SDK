using DnD.Code.Scripts.Characters.Classes.ClassFeatures;
using DnD.Code.Scripts.Common;

namespace DnD.Code.Scripts.Characters.Classes.Barbarian.ClassFeatures
{
    public class PrimalKnowledge : ClassFeature, IBarbarianClassFeature
    {
        public string Name => Helpers.NameHelper.NameHelper.ClassFeatures_Barbarian.PrimalKnowledge;
    }
}