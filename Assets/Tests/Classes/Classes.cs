using DnD.Code.Scripts.Helpers.NameHelper;

namespace Tests.Classes
{
    public class ClassTestModel
    {
        public string Name { get; set; }
        public string DisplayName => $"{NameHelper.Naming.Species}.{Name}";
        public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
        public string Ability { get; set; }
        public string Die { get; set; }
        public string[] SavingThrowProficiencies { get; set; }
        public string[] SkillProficienciesAvailable { get; set; }
        public string[] WeaponProficiencies { get; set; }
        public string[] ArmorTraining { get; set; }
        public StartingEquipmentTestModel[] StartingEquipmentOptions { get; set; }
        public LevelTestModel[] Levels { get; set; }
        public SubClassTestModel[] SubClasses { get; set; }
    }

    public class StartingEquipmentTestModel
    {
        public ItemWithAmountTestModel[] Items { get; set; }
    }

    public class ItemWithAmountTestModel
    {
        public int Amount { get; set; }
        public string ItemName { get; set; }
    }

    public class LevelTestModel
    {
        public string Name  { get; set; }
        public string DisplayName => $"{NameHelper.Naming.Levels}.{Name}";
        public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
        public int LevelNum { get; set; }
        public int ProficiencyBonus { get; set; }
        public string[] ClassFeatures { get; set; }
        public string[] ClassFeatureTraits { get; set; }
    }

    public class SubClassTestModel
    {
        public int Name  { get; set; }
        public string DisplayName => $"{NameHelper.Naming.Levels}.{Name}";
        public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
        public LevelTestModel Level03 { get; set; }
        public LevelTestModel Level06 { get; set; }
        public LevelTestModel Level10 { get; set; }
        public LevelTestModel Level14 { get; set; }
    }
}