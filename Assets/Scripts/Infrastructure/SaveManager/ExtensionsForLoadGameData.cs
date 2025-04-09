using System.Linq;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Backgrounds;
using DnD.Code.Scripts.Characters;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Classes.Barbarian.FeatureProperties;
using DnD.Code.Scripts.Classes.FeatureProperties;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Equipment;
using DnD.Code.Scripts.Species;
using DnD.Code.Scripts.Tools;
using DnD.Code.Scripts.Weapons;

namespace Infrastructure.SaveManager
{
    public static class ExtensionsForLoadGameData
    {
        public static CharacterStats ToGameData(this CharacterStatsGameData characterStats)
        {
            var background = Helpers.ScriptableObjectHelper.GetScriptableObject<Background>(
                DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Backgrounds.BackgroundsPath, 
                characterStats.BackgroundName);
            
            var @class = Helpers.ScriptableObjectHelper.GetScriptableObject<Class>(
                DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Classes.ClassesPath, 
                characterStats.ClassName);
            
            var subClass = Helpers.ScriptableObjectHelper.GetScriptableObject<SubClass>(
                DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Classes.ClassesPath, 
                characterStats.SubClassName);

            var spex  = Helpers.ScriptableObjectHelper.GetScriptableObject<Spex>(
                DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Species.SpeciesPath, 
                characterStats.SpexName);

            var armours = Helpers.ScriptableObjectHelper.GetAllScriptableObjects<BaseArmourType>(
                    DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Armours.ArmoursPath)
                    .Join(
                        characterStats.ArmourTraining,
                        armour => armour.name,
                        selected => selected,
                        (armour, _) => armour);
            
            var weaponTypes = Helpers.ScriptableObjectHelper.GetAllScriptableObjects<WeaponType>(
                    DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Weapons.WeaponTypesPath)
                    .Join(
                        characterStats.WeaponProficiencies,
                        weaponType => weaponType.name,
                        selected => selected,
                        (weaponType, _) => weaponType);
            
            var toolProficiencies = Helpers.ScriptableObjectHelper.GetAllScriptableObjects<Tool>(
                    DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Equipments.EquipmentToolsPath)
                    .Join(
                        characterStats.ToolProficiencies,
                        tool => tool.name,
                        selected => selected,
                        (tool, _) => Proficient.Of(tool.GetType()));
            
            var characterStat = new CharacterStats()
            {
                Background = background,
                CharacterName = characterStats.CharacterName,
                Class = @class,
                SubClass = subClass,
                Spex = spex,
                Level = characterStats.Level,
                Xp = characterStats.Xp,
                // SavingThrowsProficiencies = new(characterStats.SavingThrowProficiencies.Select(x => x.name)),
                // HitPoints = characterStats.HitPoints,
                // TemporaryHitPoints = characterStats.TemporaryHitPoints,
                // Languages = new(characterStats.Languages.Select(x => x.DisplayName)),
                // ClassFeatureStats = characterStats.ClassFeatureStats.ToSaveGameData(),
            };

            characterStat.SetArmourTraining(armours);
            characterStat.SetWeaponProficiencies(weaponTypes);
            characterStat.SetToolProficiencies(toolProficiencies);
            
            return characterStat;
        }
        
        private static IClassFeatureStats  ToSaveGameData(this ClassFeatureStatsGameDataBase stats)
        {
            return stats switch
            {
                BarbarianFeatureStatsGameData barbarian =>
                    new BarbarianFeatureStats()
                    {
                        Rages = barbarian.Rages,
                        RageDamage = barbarian.RageDamage,
                        WeaponMastery = barbarian.WeaponMastery
                    },
                _ => throw new System.NotImplementedException(),
            };
        }
    }
}