﻿using System;
using System.Collections.Generic;
using ProtoBuf;

namespace Infrastructure.SaveManager.Models
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
        public List<AbilityStatsSaveGameData> AbilitiesSaveGameData;
        [ProtoMember(9)]
        public ClassFeatureStatsGameDataBase ClassFeatureStats;
        [ProtoMember(10)]
        public List<string> ArmourTraining;
        [ProtoMember(11)]
        public List<string> WeaponProficiencies;
        [ProtoMember(12)]
        public List<ProficientGameData> ToolProficiencies;
        [ProtoMember(13)]
        public List<string> SavingThrowProficiencies;
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

    [ProtoContract]
    public class ProficientGameData
    {
        [ProtoMember(1)]
        public string ProficiencyFullName;
        [ProtoMember(2)]
        public string ProficiencyName;
    }

    [ProtoContract]
    public class AbilityStatsSaveGameData
    {
        [ProtoMember(1)]
        public string AbilityName;
        [ProtoMember(2)]
        public int Score;
        [ProtoMember(3)]
        public bool SavingThrow;
        [ProtoMember(4)]
        public List<SkillStatsSaveGameData> SkillsSaveGameData = new List<SkillStatsSaveGameData>();
    }

    [ProtoContract]
    public class SkillStatsSaveGameData
    {
        [ProtoMember(1)]
        public string SkillName;
        [ProtoMember(2)]
        public bool IsExpert;
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