using System;
using System.Collections.Generic;
using ProtoBuf;

namespace Infrastructure.SaveManager
{
    [ProtoContract]
    public class SaveGameData
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
        // public List<AbilitySaveGameData> abilitiesSaveGameData = new List<AbilitySaveGameData>();
        // public List<string> inventoryItems;
        // public List<string> armourTraining;
        // public List<string> weaponProficiencies;
        // public List<string> toolProficiencies;
        // public List<string> savingThrowsProficiencies;
        // public List<EquipmentSaveGameData> inventorySaveGameData = new List<EquipmentSaveGameData>();
        [ProtoMember(8)]
        public int HitPoints;
        [ProtoMember(9)]
        public int TemporaryHitPoints;
        // public DeathSavesSaveGameData deathSavesSaveGameData;
        // public List<string> languages = new List<string>();
        // public List<string> classFeatureStats = new List<string>();
        

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

    [Serializable]
    public class DeathSavesSaveGameData
    {
        public int successes;
        public int Failures;
    }
}