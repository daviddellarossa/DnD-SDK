using System;
using System.Collections.Generic;
using DnD.Code.Scripts.Characters;
using DnD.Code.Scripts.Classes.Barbarian.FeatureProperties;
using ProtoBuf;

namespace Infrastructure.SaveManager
{
    [ProtoContract]
    public class SaveGameData
    {
        [ProtoMember(1)]
        public CharacterStatsGameData CharacterStats;
    }
    
    [ProtoContract]
    public class CharacterStatsGameData
    {
        [ProtoMember(1)]
        public string CharacterName;
        [ProtoMember(2)]
        public string ClassName;
        [ProtoMember(3)]
        public string SubClassName;
        [ProtoMember(4)]
        public string BackgroundName;
        [ProtoMember(5)]
        public string SpexName;
        [ProtoMember(6)]
        public int Level;
        [ProtoMember(7)]
        public int Xp;
        [ProtoMember(8)]
        public List<AbilitySaveGameData> AbilitiesSaveGameData;
        [ProtoMember(9)]
        public List<string> InventoryItems;
        [ProtoMember(10)]
        public List<string> ArmourTraining;
        [ProtoMember(11)]
        public List<string> WeaponProficiencies;
        [ProtoMember(12)]
        public List<string> ToolProficiencies;
        [ProtoMember(13)]
        public List<string> SavingThrowsProficiencies;
        [ProtoMember(14)]
        public List<EquipmentSaveGameData> InventorySaveGameData;
        [ProtoMember(15)]
        public int HitPoints;
        [ProtoMember(16)]
        public int TemporaryHitPoints;
        [ProtoMember(17)]
        public DeathSavesSaveGameData DeathSavesSaveGameData;
        [ProtoMember(18)]
        public List<string> Languages;
        [ProtoMember(19)]
        public ClassFeatureStatsGameDataBase ClassFeatureStats;

    }
    
    [ProtoContract]
    [ProtoInclude(1, typeof(BarbarianFeatureStatsGameData))]
    public abstract class ClassFeatureStatsGameDataBase {}

    [ProtoContract]
    public class BarbarianFeatureStatsGameData : ClassFeatureStatsGameDataBase
    {
        [ProtoMember(1)]
        public int Rages;
        [ProtoMember(2)]
        public int RageDamage;
        [ProtoMember(3)]
        public int WeaponMastery;
    }

    [Serializable]
    public class AbilitySaveGameData
    {
        public string abilityName;
        public int score;
        public bool savingThrow;
        public List<SkillSaveGameData> skillsSaveGameData = new List<SkillSaveGameData>();
    }

    [Serializable]
    public class SkillSaveGameData
    {
        public string skillName;
        public bool isExpert;
    }

    [Serializable]
    public class EquipmentSaveGameData
    {
        public string equipmentName;
        public float amount;
    }

    [ProtoContract]
    public class DeathSavesSaveGameData
    {
        [ProtoMember(1)]
        public int Successes;
        [ProtoMember(2)]
        public int Failures;
    }
}