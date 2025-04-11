using System.Collections.Generic;
using DnD.Code.Scripts.Characters;
using DnD.Code.Scripts.Classes.FeatureProperties;

namespace Infrastructure.SaveManager
{
    public interface ISaveGameDataToEntityConverter
    {
        CharacterStats Convert(CharacterStatsGameData characterStatsGameData);

        AbilityStats[] Convert(IEnumerable<AbilityStatsSaveGameData> abilityStatsSaveGameData);

        AbilityStats Convert(AbilityStatsSaveGameData abilityStatsStats);

        IClassFeatureStats Convert(ClassFeatureStatsGameDataBase classFeatureStatsGameDataBase);
    }
}