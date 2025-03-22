using DnD.Code.Scripts.Characters.Classes.ClassFeatures;
using DnD.Code.Scripts.Common;

namespace DnD.Code.Scripts.Characters.Classes.Barbarian.ClassFeatures
{
    public class BrutalStrike : ClassFeature, IBarbarianClassFeature
    {
        public string Name => Helpers.NameHelper.NameHelper.ClassFeatures_Barbarian.BrutalStrike;
    }
}