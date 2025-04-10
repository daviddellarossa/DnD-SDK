using System;
using System.Collections.Generic;
using System.Linq;
using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Characters;
using DnD.Code.Scripts.Classes.Barbarian.FeatureProperties;
using DnD.Code.Scripts.Classes.FeatureProperties;

namespace Infrastructure.SaveManager
{
    public class EntityToSaveGameDataConverter : ISaveGameDataConverter
    {
        public CharacterStatsGameData Convert(CharacterStats characterStats)
        {
            return new CharacterStatsGameData()
            {
                BackgroundName = characterStats.Background.name,
                CharacterName = characterStats.CharacterName,
                ClassName = characterStats.Class.name,
                SubClassName = characterStats.SubClass.name,
                SpexName = characterStats.Spex.name,
                Level = characterStats.Level,
                Xp = characterStats.Xp,
                ArmourTraining = new(characterStats.ArmorTraining.Select(x => x.name)),
                WeaponProficiencies = new(characterStats.WeaponProficiencies.Select(x => x.name)),
                ToolProficiencies = new(characterStats.ToolProficiencies.Select(x => new ProficientGameData()
                {
                    ProficiencyName = x.ProficiencyName,
                    ProficiencyFullName = x.ProficiencyFullName,
                })),
                SavingThrowsProficiencies = new(characterStats.SavingThrowProficiencies.Select(x => x.name)),
                HitPoints = characterStats.HitPoints,
                TemporaryHitPoints = characterStats.TemporaryHitPoints,
                Languages = new(characterStats.Languages.Select(x => x.name)),
                ClassFeatureStats = Convert(characterStats.ClassFeatureStats),
                DeathSavesSaveGameData = new DeathSavesSaveGameData()
                {
                    Failures = characterStats.DeathSaves.Failures,
                    Successes = characterStats.DeathSaves.Successes,
                },
                AbilitiesSaveGameData = this.Convert(characterStats.Abilities.Values)
            };
        }
        
        public List<AbilityStatsSaveGameData> Convert(IEnumerable<AbilityStats> abilityStats)
        {
            var abilitySaveGameDataList = new List<AbilityStatsSaveGameData>();
            foreach (var abilityStat in abilityStats)
            {
                abilitySaveGameDataList.Add(this.Convert(abilityStat));
            }
            return abilitySaveGameDataList;
        }
        
        public AbilityStatsSaveGameData Convert(AbilityStats abilityStats)
        {
            var abilitySaveGameData = new AbilityStatsSaveGameData()
            {
                Score = abilityStats.Score,
                AbilityName = abilityStats.Ability.name,
                SavingThrow = abilityStats.SavingThrow,
            };

            foreach (var skillProficiency in abilityStats.SkillProficiencies.Values)
            {
                abilitySaveGameData.SkillsSaveGameData.Add(
                    new  SkillStatsSaveGameData()
                    {
                        IsExpert = skillProficiency.IsExpert,
                        SkillName = skillProficiency.Skill.name,
                    });
            }
            
            return abilitySaveGameData;
        }

        public ClassFeatureStatsGameDataBase Convert(IClassFeatureStats stats)
        {
            return stats switch
            {
                BarbarianFeatureStats barbarian =>
                    new BarbarianFeatureStatsGameData()
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