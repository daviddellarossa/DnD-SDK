using System.Collections.Generic;
using System.Linq;
using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Backgrounds;
using DnD.Code.Scripts.Characters;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Classes.Barbarian.FeatureProperties;
using DnD.Code.Scripts.Classes.FeatureProperties;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Equipment;
using DnD.Code.Scripts.Languages;
using DnD.Code.Scripts.Species;
using DnD.Code.Scripts.Weapons;
using UnityEditor;
using Tool = DnD.Code.Scripts.Tools.Tool;

namespace Infrastructure.SaveManager
{
    public static class ExtensionsForLoadGameData
    {
        public static CharacterStats ToGameData(this CharacterStatsGameData characterStatsGameData)
        {
            var background = Helpers.ScriptableObjectHelper.GetScriptableObject<Background>(
                DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Backgrounds.BackgroundsPath, 
                characterStatsGameData.BackgroundName);
            
            var @class = Helpers.ScriptableObjectHelper.GetScriptableObject<Class>(
                DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Classes.ClassesPath, 
                characterStatsGameData.ClassName);
            
            var subClass = Helpers.ScriptableObjectHelper.GetScriptableObject<SubClass>(
                DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Classes.ClassesPath, 
                characterStatsGameData.SubClassName);

            var spex  = Helpers.ScriptableObjectHelper.GetScriptableObject<Spex>(
                DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Species.SpeciesPath, 
                characterStatsGameData.SpexName);

            var armours = Helpers.ScriptableObjectHelper.GetAllScriptableObjects<BaseArmourType>(
                    DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Armours.ArmoursPath)
                    .Join(
                        characterStatsGameData.ArmourTraining,
                        armour => armour.name,
                        selected => selected,
                        (armour, _) => armour);
            
            var weaponTypes = Helpers.ScriptableObjectHelper.GetAllScriptableObjects<WeaponType>(
                    DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Weapons.WeaponTypesPath)
                    .Join(
                        characterStatsGameData.WeaponProficiencies,
                        weaponType => weaponType.name,
                        selected => selected,
                        (weaponType, _) => weaponType);
            
            var toolProficiencies = characterStatsGameData.ToolProficiencies.Select(x => new Proficient(x.ProficiencyFullName, x.ProficiencyName));
            
            var savingThrowProficiencies = Helpers.ScriptableObjectHelper.GetAllScriptableObjects<Ability>(
                    DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Abilities.AbilitiesPath)
                    .Join(
                        characterStatsGameData.SavingThrowsProficiencies,
                        ability => ability.name,
                        selected => selected,
                        (ability, _) => ability);
            
            var languages = Helpers.ScriptableObjectHelper.GetAllScriptableObjects<Language>(
                    DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Languages.LanguagesPath)
                    .Join(
                        characterStatsGameData.Languages,
                        language => language.name,
                        selected => selected,
                        (language, _) => language);

            var abilities = Helpers.ScriptableObjectHelper.GetAllScriptableObjects<Ability>(
                DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Abilities.AbilitiesPath);
            
            var skills = Helpers.ScriptableObjectHelper.GetAllScriptableObjects<Skill>(
                DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Abilities.SkillsPath);
            
            var characterStats = new CharacterStats()
            {
                Background = background,
                CharacterName = characterStatsGameData.CharacterName,
                Class = @class,
                SubClass = subClass,
                Spex = spex,
                Level = characterStatsGameData.Level,
                Xp = characterStatsGameData.Xp,
                HitPoints = characterStatsGameData.HitPoints,
                TemporaryHitPoints = characterStatsGameData.TemporaryHitPoints,
                DeathSaves =  new DeathSaves()
                {
                    Failures = characterStatsGameData.DeathSavesSaveGameData.Failures,
                    Successes = characterStatsGameData.DeathSavesSaveGameData.Successes,
                },
                ClassFeatureStats = characterStatsGameData.ClassFeatureStats.ToGameData(),
            };

            characterStats.SetArmourTraining(armours);
            characterStats.SetWeaponProficiencies(weaponTypes);
            characterStats.SetToolProficiencies(toolProficiencies);
            characterStats.SetSavingThrowProficiencies(savingThrowProficiencies);
            characterStats.SetLanguages(languages);
            characterStats.ResetAbilityStats(characterStatsGameData.AbilitiesSaveGameData.ToGameData(abilities, skills));
            return characterStats;
        }

        private static AbilityStats[] ToGameData(this IEnumerable<AbilitySaveGameData> abilitySaveGameData, Ability[] abilities, Skill[] skills)
        {
            var abilityStats = new List<AbilityStats>();
            foreach (var ability in abilitySaveGameData)
            {
                abilityStats.Add(ability.ToGameData(abilities, skills));
            }
            return abilityStats.ToArray();
        }

        private static AbilityStats ToGameData(this AbilitySaveGameData abilitySaveGameData, Ability[] abilities, Skill[] skills)
        {
            var abilityStats = new AbilityStats()
            {
                Score = abilitySaveGameData.Score,
                Ability = abilities.Single(x => x.name == abilitySaveGameData.AbilityName),
                SavingThrow = abilitySaveGameData.SavingThrow,
            };

            foreach (var skillSaveGameData in abilitySaveGameData.SkillsSaveGameData)
            {
                abilityStats.SkillProficiencies.Add(skillSaveGameData.SkillName, new SkillStats()
                {
                    Skill = skills.Single(x => x.name == skillSaveGameData.SkillName),
                    IsExpert =  skillSaveGameData.IsExpert,
                } );
            }
            
            return abilityStats;
        }
        
        private static IClassFeatureStats ToGameData(this ClassFeatureStatsGameDataBase stats)
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