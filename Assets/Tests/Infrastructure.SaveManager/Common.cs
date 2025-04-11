using System.Linq;
using DnD.Code.Scripts.Characters;
using DnD.Code.Scripts.Classes.Barbarian.FeatureProperties;
using DnD.Code.Scripts.Classes.FeatureProperties;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Equipment;
using Infrastructure.SaveManager;
using NUnit.Framework;

namespace Tests.Infrastructure.SaveManager
{
    public static class Common
    {
        public static void AssertBarbarianClassFeatureStats(IClassFeatureStats classFeatureStats, BarbarianFeatureStatsGameData classFeatureStatsGameData)
        {
            Assert.That(classFeatureStats, Is.Not.Null);
            Assert.That(classFeatureStatsGameData, Is.Not.Null);
            Assert.That(classFeatureStats, Is.InstanceOf<BarbarianFeatureStats>());
            var barbarianClassFeatureStats = (BarbarianFeatureStats)classFeatureStats;
            Assert.That(barbarianClassFeatureStats.Rages, Is.EqualTo(barbarianClassFeatureStats.Rages));
            Assert.That(barbarianClassFeatureStats.RageDamage, Is.EqualTo(barbarianClassFeatureStats.RageDamage));
            Assert.That(barbarianClassFeatureStats.WeaponMastery, Is.EqualTo(barbarianClassFeatureStats.WeaponMastery));
        }
    
        public static void AssertEquipment(StartingEquipment.EquipmentWithAmount equipmentWithAmount, EquipmentSaveGameData proficientGameData)
        {
            Assert.That(equipmentWithAmount, Is.Not.Null);
            Assert.That(proficientGameData, Is.Not.Null);
            Assert.That(proficientGameData.amount, Is.EqualTo(equipmentWithAmount.Amount));
            Assert.That(proficientGameData.equipmentName, Is.EqualTo(equipmentWithAmount.Equipment.name));
        }
        
        public static void AssertProficient(Proficient proficient, ProficientGameData proficientGameData)
        {
            Assert.That(proficient, Is.Not.Null);
            Assert.That(proficientGameData, Is.Not.Null);
            Assert.That(proficientGameData.ProficiencyName, Is.EqualTo(proficient.ProficiencyName));
            Assert.That(proficientGameData.ProficiencyFullName, Is.EqualTo(proficient.ProficiencyFullName));
        }
        public static void AssertAbilityStats(AbilityStats abilityStats, AbilityStatsSaveGameData abilityStatsSaveGameData)
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