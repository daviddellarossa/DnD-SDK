using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Characters;
using DnD.Code.Scripts.Classes.Barbarian.FeatureProperties;
using Infrastructure.SaveManager;
using NUnit.Framework;
using UnityEditor;

namespace Tests.Infrastructure.SaveManager
{
    [TestFixture]
    public class EntityToSaveGameDataConverterUnitTests
    {
        private EntityToSaveGameDataConverter entityToSaveGameDataConverter;
        private Ability[] _abilities;
        private Skill[] _skills;

        private Fixture _fixture = new Fixture();
        
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
            this.entityToSaveGameDataConverter = new EntityToSaveGameDataConverter();
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
            
            var abilityStatsSaveGameData = entityToSaveGameDataConverter.Convert(abilityStats);
            
            AssertAbilityStats(abilityStats, abilityStatsSaveGameData);
        }
        
        [Test]
        public void Convert_BarbarianFeatureStats_To_BarbarianClassFeatureStatsGameData_IsSuccessful()
        {
            var barbarianFeatureStats = _fixture.Create<BarbarianFeatureStats>();
            
            var classFeatureStatsGameDataBase = entityToSaveGameDataConverter.Convert(barbarianFeatureStats);
            
            Assert.That(classFeatureStatsGameDataBase, Is.Not.Null);
            Assert.That(classFeatureStatsGameDataBase, Is.InstanceOf<BarbarianFeatureStatsGameData>());
            var barbarianClassFeatureStatsGameData = (BarbarianFeatureStatsGameData)classFeatureStatsGameDataBase;
            Assert.That(barbarianFeatureStats.Rages, Is.EqualTo(barbarianClassFeatureStatsGameData.Rages));
            Assert.That(barbarianFeatureStats.RageDamage, Is.EqualTo(barbarianClassFeatureStatsGameData.RageDamage));
            Assert.That(barbarianFeatureStats.WeaponMastery, Is.EqualTo(barbarianClassFeatureStatsGameData.WeaponMastery));
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