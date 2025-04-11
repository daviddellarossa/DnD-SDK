using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Backgrounds;
using DnD.Code.Scripts.Characters;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Classes.Barbarian.FeatureProperties;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Equipment;
using DnD.Code.Scripts.Languages;
using DnD.Code.Scripts.Species;
using DnD.Code.Scripts.Weapons;
using Infrastructure.SaveManager;
using NUnit.Framework;
using UnityEditor;
using soHelper = Infrastructure.Helpers.ScriptableObjectHelper;
using pathHelper = DnD.Code.Scripts.Helpers.PathHelper.PathHelper;
using Tool = DnD.Code.Scripts.Tools.Tool;

namespace Tests.Infrastructure.SaveManager
{
    [TestFixture]
    public class EntityToSaveGameDataConverterUnitTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly EntityToSaveGameDataConverter _entityToSaveGameDataConverter = new EntityToSaveGameDataConverter();
        
        private Background[] _backgrounds;
        private Class[] _classes;
        private Spex[] _species;
        private Ability[] _abilities;
        private Skill[] _skills;
        private BaseArmourType[] _armourTypes;
        private StandardLanguage[] _languages;
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
            _languages = soHelper.GetAllScriptableObjects<StandardLanguage>(pathHelper.Languages.StandardLanguagesPath);
            _weaponTypes = soHelper.GetAllScriptableObjects<WeaponType>(pathHelper.Weapons.WeaponsPath);
            
            var characterStatTestData = new CharacterStatsTestData()
            {
                Background = _backgrounds[0],
                Class = _classes[0],
                SubClass = _classes[0].SubClasses[0],
                Spex = _species[0],
                Tools = soHelper.GetAllScriptableObjects<Tool>(pathHelper.Backgrounds.AcolyteToolsPath).ToList(),
            };
            
            _characterStats = CreateCharacterStats(characterStatTestData);
            _characterStatsGameData = _entityToSaveGameDataConverter.Convert(_characterStats);
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
        public void Convert_AbilityStats_To_AbilityStatsSaveGameData_IsSuccessful()
        {
            var ability = _abilities.First();
            var skillProficiencies = new Dictionary<string, SkillStats>();
            foreach (var skill in ability.SkillList)
            {
                skillProficiencies.Add(skill.name, new  SkillStats(){ IsExpert = true, Skill = skill });
            }
            
            var abilityStats = new AbilityStats()
            {
                Score = _fixture.Create<int>(),
                SavingThrow = _fixture.Create<bool>(),
                Ability = ability,
                SkillProficiencies = skillProficiencies
            };
            
            var abilityStatsSaveGameData = _entityToSaveGameDataConverter.Convert(abilityStats);
            
            Common.AssertAbilityStats(abilityStats, abilityStatsSaveGameData);
        }
        
        [Test]
        public void Convert_BarbarianFeatureStats_To_BarbarianClassFeatureStatsGameData_IsSuccessful()
        {
            var barbarianFeatureStats = _fixture.Create<BarbarianFeatureStats>();
            
            var classFeatureStatsGameDataBase = _entityToSaveGameDataConverter.Convert(barbarianFeatureStats);
            
            Assert.That(classFeatureStatsGameDataBase, Is.Not.Null);
            Assert.That(classFeatureStatsGameDataBase, Is.InstanceOf<BarbarianFeatureStatsGameData>());
            var barbarianClassFeatureStatsGameData = (BarbarianFeatureStatsGameData)classFeatureStatsGameDataBase;
            Assert.That(barbarianFeatureStats.Rages, Is.EqualTo(barbarianClassFeatureStatsGameData.Rages));
            Assert.That(barbarianFeatureStats.RageDamage, Is.EqualTo(barbarianClassFeatureStatsGameData.RageDamage));
            Assert.That(barbarianFeatureStats.WeaponMastery, Is.EqualTo(barbarianClassFeatureStatsGameData.WeaponMastery));
        }
        
        private CharacterStats CreateCharacterStats(CharacterStatsTestData characterStatsTestData)
        {
            var builder = new CharacterStats.Builder();
            var characterStats =
                builder.SetName(_fixture.Create<string>())
                    .SetBackground(characterStatsTestData.Background)
                    .SetClass(characterStatsTestData.Class)
                    .SetSubClass(characterStatsTestData.SubClass)
                    .SetSpex(characterStatsTestData.Spex)
                    .SetLanguage(_languages[0])
                    .SetLanguage(_languages[1])
                    .SetLanguage(_languages[3])
                    .SetAbilityStats(new AbilityStats()
                    {
                        Ability = _abilities[0],
                        Score = _fixture.Create<int>(),
                        SavingThrow = _fixture.Create<bool>(),
                        SkillProficiencies = _abilities[0].SkillList.ToDictionary(
                            x => x.name,
                            x => new SkillStats
                            {
                                IsExpert = _fixture.Create<bool>(),
                                Skill = x
                            }),
                    })
                    .SetAbilityStats(new AbilityStats()
                    {
                        Ability = _abilities[1],
                        Score = _fixture.Create<int>(),
                        SavingThrow = _fixture.Create<bool>(),
                        SkillProficiencies = new Dictionary<string, SkillStats>()
                    })
                    .SetAbilityStats(new AbilityStats()
                    {
                        Ability = _abilities[2],
                        Score = _fixture.Create<int>(),
                        SavingThrow = _fixture.Create<bool>(),
                        SkillProficiencies = new Dictionary<string, SkillStats>()
                    })
                    .SetAbilityStats(new AbilityStats()
                    {
                        Ability = _abilities[3],
                        Score = _fixture.Create<int>(),
                        SavingThrow = _fixture.Create<bool>(),
                        SkillProficiencies = new Dictionary<string, SkillStats>()
                    })
                    .SetAbilityStats(new AbilityStats()
                    {
                        Ability = _abilities[4],
                        Score = _fixture.Create<int>(),
                        SavingThrow = _fixture.Create<bool>(),
                        SkillProficiencies = new Dictionary<string, SkillStats>()
                    })
                    .SetAbilityStats(new AbilityStats()
                    {
                        Ability = _abilities[5],
                        Score = _fixture.Create<int>(),
                        SavingThrow = _fixture.Create<bool>(),
                        SkillProficiencies = new Dictionary<string, SkillStats>()
                    })
                    .SetSkillProficienciesFromClass(characterStatsTestData.Class.SkillProficienciesAvailable.Take(2)
                        .ToArray())
                    .SetStartingEquipmentFromBackground(characterStatsTestData.Background.StartingEquipmentOptions[0])
                    .SetStartingEquipmentFromClass(characterStatsTestData.Class.StartingEquipmentOptions[1])
                    .Build();
            if (characterStats == null)
            {
                Assert.Inconclusive("Something went wrong creating the character stats instance");
            }
            return characterStats;
        }
    }
}