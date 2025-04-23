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
    public class SaveGameDataToEntityConverter : ISaveGameDataToEntityConverter
    {
        private Ability[] _abilities;
        private Skill[] _skills;
        
        public Ability[] Abilities
        {
            get
            {
                return _abilities ??= Helpers.ScriptableObjectHelper.GetAllScriptableObjects<Ability>(
                    DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Abilities.AbilitiesPath);
            }    
        }
        
        public Skill[] Skills
        {
            get
            {
                return _skills ??= Helpers.ScriptableObjectHelper.GetAllScriptableObjects<Skill>(
                    DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Abilities.SkillsPath);
            }    
        }

        public SaveGameDataToEntityConverter(Ability[] abilities = null, Skill[] skills = null)
        {
            this._abilities = abilities;
            this._skills = skills;
        }
        
        public CharacterStats Convert(CharacterStatsGameData characterStatsGameData)
        {
            var characterStats = new CharacterStats()
            {
                Background = GetBackground(),
                CharacterName = characterStatsGameData.CharacterName,
                Class = GetClass(),
                SubClass = GetSubClass(),
                Spex = GetSpex(),
                Level = characterStatsGameData.Level,
                Xp = characterStatsGameData.Xp,
                HitPoints = characterStatsGameData.HitPoints,
                TemporaryHitPoints = characterStatsGameData.TemporaryHitPoints,
                DeathSaves =  new DeathSaves()
                {
                    Failures = characterStatsGameData.DeathSavesSaveGameData.Failures,
                    Successes = characterStatsGameData.DeathSavesSaveGameData.Successes,
                },
                ClassFeatureStats = this.Convert(characterStatsGameData.ClassFeatureStats),
            };

            // characterStats.SetInventory(GetInventory()); Not implemented
            characterStats.SetArmourTraining(GetArmourTypes());
            characterStats.SetWeaponProficiencies(GetWeaponTypes());
            characterStats.SetToolProficiencies(GetToolProficiencies());
            characterStats.SetSavingThrowProficiencies(GetSavingThrowProficiencies());
            characterStats.SetLanguages(GetLanguages());
            characterStats.ResetAbilityStats(this.Convert(characterStatsGameData.AbilitiesSaveGameData));
            return characterStats;

            Background GetBackground()
            {
                var background = Helpers.ScriptableObjectHelper.GetScriptableObject<Background>(
                    DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Backgrounds.BackgroundsPath, 
                    characterStatsGameData.BackgroundName);
                return background;
            }

            Class GetClass()
            {
                var @class = Helpers.ScriptableObjectHelper.GetScriptableObject<Class>(
                    DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Classes.ClassesPath, 
                    characterStatsGameData.ClassName);
                return @class;
            }

            SubClass GetSubClass()
            {
                var subClass = Helpers.ScriptableObjectHelper.GetScriptableObject<SubClass>(
                    DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Classes.ClassesPath, 
                    characterStatsGameData.SubClassName);
                return subClass;
            }

            Spex GetSpex()
            {
                var spex  = Helpers.ScriptableObjectHelper.GetScriptableObject<Spex>(
                    DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Species.SpeciesPath, 
                    characterStatsGameData.SpexName);
                return spex;
            }

            IEnumerable<BaseArmourType> GetArmourTypes()
            {
                var baseArmourTypes = Helpers.ScriptableObjectHelper.GetAllScriptableObjects<BaseArmourType>(
                        DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Armours.ArmoursPath)
                    .Join(
                        characterStatsGameData.ArmourTraining,
                        armour => armour.name,
                        selected => selected,
                        (armour, _) => armour);
                return baseArmourTypes;
            }

            IEnumerable<WeaponType> GetWeaponTypes()
            {
                var weaponType = Helpers.ScriptableObjectHelper.GetAllScriptableObjects<WeaponType>(
                        DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Weapons.WeaponTypesPath)
                    .Join(
                        characterStatsGameData.WeaponProficiencies,
                        weaponType => weaponType.name,
                        selected => selected,
                        (weaponType, _) => weaponType);
                return weaponType;
            }

            IEnumerable<Proficient> GetToolProficiencies()
            {
                var proficients = characterStatsGameData.ToolProficiencies.Select(x => new Proficient(x.ProficiencyFullName, x.ProficiencyName));
                return proficients;
            }

            IEnumerable<Ability> GetSavingThrowProficiencies()
            {
                var savingThrowProficiencies = Helpers.ScriptableObjectHelper.GetAllScriptableObjects<Ability>(
                        DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Abilities.AbilitiesPath)
                    .Join(
                        characterStatsGameData.SavingThrowProficiencies,
                        ability => ability.name,
                        selected => selected,
                        (ability, _) => ability);
                return savingThrowProficiencies;
            }

            IEnumerable<Language> GetLanguages()
            {
                var languages = Helpers.ScriptableObjectHelper.GetAllScriptableObjects<Language>(
                        DnD.Code.Scripts.Helpers.PathHelper.PathHelper.Languages.LanguagesPath)
                    .Join(
                        characterStatsGameData.Languages,
                        language => language.name,
                        selected => selected,
                        (language, _) => language);
                return languages;
            }
        }

        public AbilityStats[] Convert(IEnumerable<AbilityStatsSaveGameData> abilityStatsSaveGameData)
        {
            var abilityStats = new List<AbilityStats>();
            foreach (var ability in abilityStatsSaveGameData)
            {
                abilityStats.Add(this.Convert(ability));
            }
            return abilityStats.ToArray();
        }

        public AbilityStats Convert(AbilityStatsSaveGameData abilityStatsSaveGameData)
        {
            var abilityStats = new AbilityStats()
            {
                Score = abilityStatsSaveGameData.Score,
                Ability = this.Abilities.Single(x => x.name == abilityStatsSaveGameData.AbilityName),
                SavingThrow = abilityStatsSaveGameData.SavingThrow,
            };

            foreach (var skillSaveGameData in abilityStatsSaveGameData.SkillsSaveGameData)
            {
                abilityStats.SkillProficiencies.Add(skillSaveGameData.SkillName, new SkillStats()
                {
                    Skill = this.Skills.Single(x => x.name == skillSaveGameData.SkillName),
                    IsExpert =  skillSaveGameData.IsExpert,
                } );
            }
            
            return abilityStats;
        }

        public IClassFeatureStats Convert(ClassFeatureStatsGameDataBase classFeatureStatsGameDataBase)
        {
            return classFeatureStatsGameDataBase switch
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