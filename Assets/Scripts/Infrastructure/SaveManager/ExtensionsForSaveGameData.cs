using System.Linq;
using DnD.Code.Scripts.Characters;
using DnD.Code.Scripts.Classes.Barbarian.FeatureProperties;
using DnD.Code.Scripts.Classes.FeatureProperties;

namespace Infrastructure.SaveManager
{
    public static class ExtensionsForSaveGameData
    {
        public static CharacterStatsGameData ToSaveGameData(this CharacterStats characterStats)
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
                ToolProficiencies = new(characterStats.ToolProficiencies.Select(x => x.ProficiencyFullName)),
                SavingThrowsProficiencies = new(characterStats.SavingThrowProficiencies.Select(x => x.name)),
                HitPoints = characterStats.HitPoints,
                TemporaryHitPoints = characterStats.TemporaryHitPoints,
                Languages = new(characterStats.Languages.Select(x => x.DisplayName)),
                ClassFeatureStats = characterStats.ClassFeatureStats.ToSaveGameData(),
            };
        }
        
        private static ClassFeatureStatsGameDataBase ToSaveGameData(this IClassFeatureStats stats)
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