using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Backgrounds;
using DnD.Code.Scripts.Characters;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Languages;
using DnD.Code.Scripts.Species;
using DnD.Code.Scripts.Weapons;
using Infrastructure.SaveManager;
using Infrastructure.SaveManager.Models;
using soHelper = Infrastructure.Helpers.ScriptableObjectHelper;
using pathHelper = DnD.Code.Scripts.Helpers.PathHelper.PathHelper;
using NUnit.Framework;
using Tool = DnD.Code.Scripts.Tools.Tool;

namespace Tests.Infrastructure.SaveManager
{
    [TestFixture]
    public class SaveGameDataToEntityConverterUnitTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly SaveGameDataToEntityConverter _saveGameDataToEntityConverter = new SaveGameDataToEntityConverter();

        private Background[] _backgrounds;
        private Class[] _classes;
        private Spex[] _species;
        private Ability[] _abilities;
        private Skill[] _skills;
        private BaseArmourType[] _armourTypes;
        private Language[] _languages;
        private WeaponType[] _weaponTypes;

        private CharacterStatsGameData _characterStatsGameData;
        private CharacterStats _characterStats;
        
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _backgrounds = soHelper.GetAllScriptableObjects<Background>(pathHelper.Backgrounds.BackgroundsPath);
            _classes = soHelper.GetAllScriptableObjects<Class>(pathHelper.Classes.ClassesPath);
            _species = soHelper.GetAllScriptableObjects<Spex>(pathHelper.Species.SpeciesPath);
            _abilities = soHelper.GetAllScriptableObjects<Ability>(pathHelper.Abilities.AbilitiesPath);
            _skills = soHelper.GetAllScriptableObjects<Skill>(pathHelper.Abilities.SkillsPath);
            _armourTypes = soHelper.GetAllScriptableObjects<BaseArmourType>(pathHelper.Armours.ArmoursPath);
            _languages = soHelper.GetAllScriptableObjects<Language>(pathHelper.Languages.LanguagesPath);
            _weaponTypes = soHelper.GetAllScriptableObjects<WeaponType>(pathHelper.Weapons.WeaponsPath);
            
            var characterStatTestData = new CharacterStatsTestData()
            {
                Background = _backgrounds[0],
                Class = _classes[0],
                SubClass = _classes[0].SubClasses[0],
                Spex = _species[0],
                Tools = soHelper.GetAllScriptableObjects<Tool>(pathHelper.Backgrounds.AcolyteToolsPath).ToList(),
            };
            
            _characterStatsGameData = CreateCharacterStatsGameData(characterStatTestData);
            _characterStats = _saveGameDataToEntityConverter.Convert(_characterStatsGameData);
        }
        
        [Test]
        public void Convert_CharacterStats_To_CharacterStats_CharacterName()
        {
            Assert.That(_characterStats.CharacterName, Is.EqualTo(_characterStatsGameData.CharacterName));
        }
        
        [Test]
        public void Convert_CharacterStats_To_CharacterStats_ClassName()
        {
            Assert.That(_characterStats.Class.name, Is.EqualTo(_characterStatsGameData.ClassName));
        }
        
        [Test]
        public void Convert_CharacterStats_To_CharacterStats_SubClassName()
        {
            Assert.That(_characterStats.SubClass.name, Is.EqualTo(_characterStatsGameData.SubClassName));
        }
        
        [Test]
        public void Convert_CharacterStats_To_CharacterStats_BackgroundName()
        {
            Assert.That(_characterStats.Background.name, Is.EqualTo(_characterStatsGameData.BackgroundName));
        }
        
        [Test]
        public void Convert_CharacterStats_To_CharacterStats_SpexName()
        {
            Assert.That(_characterStats.Spex.name, Is.EqualTo(_characterStatsGameData.SpexName));
        }
        
        [Test]
        public void Convert_CharacterStats_To_CharacterStats_Level()
        {
            Assert.That(_characterStats.Level, Is.EqualTo(_characterStatsGameData.Level));
        }
        
        [Test]
        public void Convert_CharacterStats_To_CharacterStats_Xp()
        {
            Assert.That(_characterStats.Xp, Is.EqualTo(_characterStatsGameData.Xp));
        }
        
        [Test]
        public void Convert_CharacterStats_To_CharacterStats_Abilities()
        {
            foreach (var abilityGameData in _characterStatsGameData.AbilitiesSaveGameData)
            {
                var ability = _characterStats.Abilities[abilityGameData.AbilityName];
                Assert.That(ability, Is.Not.Null);
                Common.AssertAbilityStats(ability, abilityGameData);
            }
        }
        
        [Test]
        public void Convert_CharacterStats_To_CharacterStats_ClassFeatureStats()
        {
            switch (_characterStatsGameData.ClassFeatureStats)
            {
                case BarbarianFeatureStatsGameData barbarianFeatureStatsGameData:
                {
                    Common.AssertBarbarianClassFeatureStats(_characterStats.ClassFeatureStats,
                        barbarianFeatureStatsGameData);
                    break;
                }
                default:
                    Assert.Fail("ClassFeatureStats type not recognized");
                    break;
            }
        }

        [Test]
        public void Convert_CharacterStats_To_CharacterStats_ArmourTraining()
        {
            Assert.That(_characterStats.ArmourTraining.Select(x => x.name), Is.EquivalentTo(_characterStatsGameData.ArmourTraining));

        }
        
        [Test]
        public void Convert_CharacterStats_To_CharacterStats_WeaponProficiencies()
        {
            Assert.That(_characterStats.WeaponProficiencies.Select(x => x.name), Is.EquivalentTo(_characterStatsGameData.WeaponProficiencies));

        }
        
        [Test]
        public void Convert_CharacterStats_To_CharacterStats_ToolProficiencies()
        {
            foreach (var toolProficiencyGameData in _characterStatsGameData.ToolProficiencies)      
            {
                var toolProficiency = _characterStats.ToolProficiencies.SingleOrDefault(x => x.ProficiencyFullName == toolProficiencyGameData.ProficiencyFullName);
                Assert.That(toolProficiency, Is.Not.Null);
                Common.AssertProficient(toolProficiency, toolProficiencyGameData);
            }
        }
        
        [Test]
        public void Convert_CharacterStats_To_CharacterStats_SavingThrowsProficiencies()
        {
            Assert.That(_characterStats.SavingThrowProficiencies.Select(x => x.name), Is.EquivalentTo(_characterStatsGameData.SavingThrowProficiencies));

        }
        
        [Test, Ignore("Not implemented")]
        public void Convert_CharacterStats_To_CharacterStats_Inventory()
        {
            // Not implemented
        }
        
        [Test]
        public void Convert_CharacterStats_To_CharacterStats_HitPoints()
        {
            Assert.That(_characterStats.HitPoints, Is.EqualTo(_characterStatsGameData.HitPoints));
        }
        
        [Test]
        public void Convert_CharacterStats_To_CharacterStats_TemporaryHitPoints()
        {
            Assert.That(_characterStats.TemporaryHitPoints, Is.EqualTo(_characterStatsGameData.TemporaryHitPoints));
        }
        
        [Test]
        public void Convert_CharacterStats_To_CharacterStats_DeathSaves()
        {
            Assert.That(_characterStats.DeathSaves.Failures, Is.EqualTo(_characterStatsGameData.DeathSavesSaveGameData.Failures));
            Assert.That(_characterStats.DeathSaves.Successes, Is.EqualTo(_characterStatsGameData.DeathSavesSaveGameData.Successes));
        }
        
        [Test]
        public void Convert_CharacterStats_To_CharacterStats_Languages()
        {
            Assert.That(_characterStats.Languages.Select(x => x.name), Is.EquivalentTo(_characterStatsGameData.Languages));
        }
        
        [Test]
        public void ConvertBarbarianFeatureStatsGameData_To_BarbarianClassFeatureStats_IsSuccessful()
        {
            var barbarianFeatureStats = _fixture.Create<BarbarianFeatureStatsGameData>();
            
            var classFeatureStats = _saveGameDataToEntityConverter.Convert(barbarianFeatureStats);
            
            Common.AssertBarbarianClassFeatureStats(classFeatureStats, barbarianFeatureStats);
        }
        
        [Test]
        public void Convert_AbilityStatsGameData_To_AbilityStatsSave_IsSuccessful()
        {
            var ability = _abilities.First();
            var abilityStatsSaveGameData = new AbilityStatsSaveGameData()
            {
                Score = _fixture.Create<int>(),
                SavingThrow = _fixture.Create<bool>(),
                AbilityName = ability.name,
                SkillsSaveGameData = ability.SkillList.Select(x => new SkillStatsSaveGameData()
                {
                    IsExpert = true,
                    SkillName =  x.name,
                }).ToList(),

            };
            
            var abilityStats = _saveGameDataToEntityConverter.Convert(abilityStatsSaveGameData);
            
            Common.AssertAbilityStats(abilityStats, abilityStatsSaveGameData);
        }
        
        private CharacterStatsGameData CreateCharacterStatsGameData(CharacterStatsTestData characterStatsTestData)
        {
            var characterStatsGameData = _fixture
                .Build<CharacterStatsGameData>()
                .With(x => x.BackgroundName, characterStatsTestData.Background.name)
                .With(x => x.ClassName, characterStatsTestData.Class.name)
                .With(x => x.SubClassName, characterStatsTestData.SubClass.name)
                .With(x => x.SpexName, characterStatsTestData.Spex.name)
                .With(x => x.ArmourTraining, _armourTypes.Take(2).Select(x => x.name).ToList())
                .With(x => x.Languages, _languages.Take(2).Select(x => x.name).ToList())
                .With(x => x.WeaponProficiencies, _weaponTypes.Take(2).Select(x => x.name).ToList())
                .With(x => x.SavingThrowProficiencies, _abilities.Take(2).Select(x => x.name).ToList())
                .With(x => x.ToolProficiencies, characterStatsTestData.Tools.Take(2).Select(x => new ProficientGameData(){ ProficiencyName = x.GetType().Name, ProficiencyFullName = x.GetType().FullName }).ToList())
                .With(x => x.ClassFeatureStats, this._fixture.Create<BarbarianFeatureStatsGameData>())
                .With(x => x.AbilitiesSaveGameData, _fixture.Build<IEnumerable<AbilityStatsSaveGameData>>()
                    .FromFactory(() =>
                    {
                        var ability = _abilities[0];
                        var skillList = new List<SkillStatsSaveGameData>();
                        skillList.AddRange(ability.SkillList.Select(x => new SkillStatsSaveGameData()
                        {
                            IsExpert = _fixture.Create<bool>(),
                            SkillName = x.name
                        }));
                        
                        return this._fixture
                            .Build<AbilityStatsSaveGameData>()
                            .With(x => x.AbilityName, ability.name)
                            .With(x => x.SkillsSaveGameData, skillList)
                            .CreateMany(1)
                            .ToList();
                    })
                    .Create()
                    .ToList()
                )
                .With(x => x.InventorySaveGameData, 
                    characterStatsTestData.Tools
                        .Take(2)
                        .Select(x => 
                            _fixture
                                .Build<EquipmentSaveGameData>()
                                .With(x => x.equipmentName, x.name)
                                .Create())
                        .ToList())
                .Create();
            return characterStatsGameData;
        }
    }
}