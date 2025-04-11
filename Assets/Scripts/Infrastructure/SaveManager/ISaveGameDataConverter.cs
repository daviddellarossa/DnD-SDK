using System.Collections.Generic;
using DnD.Code.Scripts.Characters;
using DnD.Code.Scripts.Classes.FeatureProperties;

namespace Infrastructure.SaveManager
{
    public interface ISaveGameDataConverter
    {
        CharacterStatsGameData Convert(CharacterStats characterStats);

        List<AbilityStatsSaveGameData> Convert(IEnumerable<AbilityStats> abilityStats);

        AbilityStatsSaveGameData Convert(AbilityStats abilityStats);

        ClassFeatureStatsGameDataBase Convert(IClassFeatureStats stats);
    }
}