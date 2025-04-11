using DnD.Code.Scripts.Helpers.NameHelper;
using DnD.Code.Scripts.Species;
using DnD.Code.Scripts.Species.SpecialTraits.TraitTypes;

namespace Tests.DnD.Species
{
    public class SpexTestModel
    {
        public string Name { get; set; }
        public string DisplayName => $"{NameHelper.Naming.Species}.{Name}";
        public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
        public string InheritFrom;
        public string CreatureType;
        public Size Size;
        public float Speed;
    }
    public abstract class TypeTraitTestModel
    {
        public string Name { get; set; }
        public string DisplayName => $"{NameHelper.Naming.TypeTraits}.{Name}";
        public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
            
        public abstract void AssertEqual(TypeTrait typeTrait);
    }
    
    public class SpecialTraitTestModel
    {
        public string Name { get; set; }
        public string DisplayName => $"{NameHelper.Naming.SpecialTraits}.{Name}";
        public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";

        public TypeTraitTestModel[] TraitTypes { get; set; }
    }
}