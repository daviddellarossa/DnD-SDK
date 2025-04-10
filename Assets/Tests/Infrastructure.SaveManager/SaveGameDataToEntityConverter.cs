using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Characters;
using DnD.Code.Scripts.Classes.Barbarian.FeatureProperties;
using DnD.Code.Scripts.Helpers.NameHelper;
using Infrastructure.SaveManager;
using NUnit.Framework;
using UnityEditor;

namespace Tests.Infrastructure.SaveManager
{
    [TestFixture]
    public class SaveGameDataToEntityConverterUnitTests
    {
        private Fixture _fixture = new Fixture();
        private SaveGameDataToEntityConverter saveGameDataToEntityConverter;

        private Ability[] _abilities;
        private Skill[] _skills;
        
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(Ability)}");
            _abilities =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Ability>)
                .Where(asset => asset != null)
                .ToArray();
            
            guids = AssetDatabase.FindAssets($"t:{nameof(Skill)}");
            _skills =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Skill>)
                .Where(asset => asset != null)
                .ToArray();
        }

        [SetUp]
        public void Setup()
        {
            this.saveGameDataToEntityConverter = new SaveGameDataToEntityConverter();    
        }
        
        [Test]
        public void ConvertBarbarianFeatureStatsGameData_To_BarbarianClassFeatureStats_IsSuccessful()
        {
            var barbarianFeatureStats = _fixture.Create<BarbarianFeatureStatsGameData>();
            
            var classFeatureStats = saveGameDataToEntityConverter.Convert(barbarianFeatureStats);
            
            Assert.That(classFeatureStats, Is.Not.Null);
            Assert.That(classFeatureStats, Is.InstanceOf<BarbarianFeatureStats>());
            var barbarianClassFeatureStats = (BarbarianFeatureStats)classFeatureStats;
            Assert.That(barbarianFeatureStats.Rages, Is.EqualTo(barbarianClassFeatureStats.Rages));
            Assert.That(barbarianFeatureStats.RageDamage, Is.EqualTo(barbarianClassFeatureStats.RageDamage));
            Assert.That(barbarianFeatureStats.WeaponMastery, Is.EqualTo(barbarianClassFeatureStats.WeaponMastery));
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
            
            var abilityStats = saveGameDataToEntityConverter.Convert(abilityStatsSaveGameData);
            
            AssertAbilityStats(abilityStats, abilityStatsSaveGameData);
        }

        private void AssertAbilityStats(AbilityStats abilityStats, AbilityStatsSaveGameData abilityStatsSaveGameData)
        {
            Assert.That(abilityStats, Is.Not.Null);
            Assert.That(abilityStatsSaveGameData, Is.Not.Null);
            Assert.That(abilityStats.Modifier, Is.EqualTo(abilityStats.Modifier));
            Assert.That(abilityStats.Score, Is.EqualTo(abilityStats.Score));
            Assert.That(abilityStats.SavingThrow, Is.EqualTo(abilityStats.SavingThrow));
            Assert.That(abilityStats.Ability.name, Is.EqualTo(abilityStatsSaveGameData.AbilityName));
            Assert.That(
                abilityStats.SkillProficiencies.Values.Select(x => new { SkillName = x.Skill.name, IsExpert = x.IsExpert}), 
                Is.EquivalentTo(abilityStatsSaveGameData.SkillsSaveGameData.Select(x => new { SkillName = x.SkillName, IsExpert = x.IsExpert })));
        }
    }
}