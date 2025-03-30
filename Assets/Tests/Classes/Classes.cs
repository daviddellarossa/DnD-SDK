using DnD.Code.Scripts.Helpers.NameHelper;

namespace Tests.Classes
{
    public class ClassTestModel
    {
        public string Name { get; set; }
        public string DisplayName => $"{NameHelper.Naming.Classes}.{Name}";
        public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
        public string PrimaryAbility { get; set; }
        public string HitPointDie { get; set; }
        
        public int NumberOfSkillProficienciesToChoose { get; set; }
    }

    public class StartingEquipmentTestModel
    {
        public string Name { get; set; }
        public ItemWithAmountTestModel[] Items { get; set; }
    }

    public class ItemWithAmountTestModel
    {
        public int Amount { get; set; }
        public string ItemName { get; set; }
    }

    public class LevelTestModel
    {
        private string name;

        public string Name
        {
            get => $"{name}.{LevelNum:00}";
            set => name = value;
        }

        public virtual string DisplayName => $"{Name}";
        public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
        public int LevelNum { get; set; }
        public int ProficiencyBonus { get; set; }
        public string[] ClassFeatures { get; set; }
        public IClassFeatureTraitsTestModel ClassFeatureTraits { get; set; }
    }

    public class SubLevelTestModel : LevelTestModel
    {
        public string SubClassName  { get; set; }
    }

    public class SubClassTestModel
    {
        public string Name  { get; set; }
        public string ClassName { get; set; }
        public string DisplayName => $"{ClassName}.{Name}";
        public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
    }

    public interface IClassFeatureTraitsTestModel
    {
    }
}