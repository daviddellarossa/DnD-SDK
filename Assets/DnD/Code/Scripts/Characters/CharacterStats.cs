using System.Collections.Generic;
using System.Linq;
using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Backgrounds;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Equipment;
using DnD.Code.Scripts.Feats;
using DnD.Code.Scripts.Languages;
using DnD.Code.Scripts.Species;
using DnD.Code.Scripts.Species.SpecialTraits;
using DnD.Code.Scripts.Weapons;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace DnD.Code.Scripts.Characters
{
    public partial class CharacterStats : ScriptableObject
    {
        [SerializeField]
        private string characterName;
        
        [SerializeReference]
        private Class @class;
        
        [SerializeReference]
        private SubClass subClass;
        
        [SerializeField]
        private Background background;
        
        [SerializeField]
        private Species.Spex spex;
        
        [SerializeField]
        private int level;
        
        [SerializeField]
        private int xp;
        
        [SerializeField]
        private int proficiencyBonus;
        
        [SerializeField]
        private Dictionary<Ability, int> abilityScores;
        
        [SerializeField]
        private List<BaseArmourType> armorTraining = new List<BaseArmourType>();
        
        [SerializeField]
        private List<WeaponType> weaponProficiencies = new ();

        [SerializeField]
        private List<Skill> skillProficiencies = new List<Skill>();
        
        [SerializeField]
        private List<Proficient> toolProficiencies = new List<Proficient>();
        
        [SerializeField]
        private List<Ability> savingThrowProficiencies = new ();
        
        [SerializeField]
        private List<StartingEquipment.EquipmentWithAmount> inventory = new ();
        
        [SerializeField]
        private HitPoints hitPoints;
        
        [SerializeField]
        private string armorClass;
        
        [SerializeField]
        private DeathSaves deathSaves;

        [SerializeField]
        private HashSet<ILanguage> languages = new ();
        
        public string CharacterName => characterName;
        
        public Class Class => @class;
        
        public SubClass SubClass => subClass;
        
        public Background Background => background;
        
        public Species.Spex Spex => spex;
        
        public int Level => level;

        public int Xp => xp;
        public int ProficiencyBonus => proficiencyBonus;
        
        public KeyValuePair<Ability, int>[] AbilityScores => abilityScores.ToArray();
        
        public BaseArmourType[] ArmorTraining => armorTraining.ToArray();

        public WeaponType[] WeaponProficiencies => weaponProficiencies.ToArray();

        public Skill[] SkillProficiencies => skillProficiencies.ToArray();
        public Proficient[] ToolProficiencies => toolProficiencies.ToArray();

        public Ability[] SavingThrowProficiencies =>  savingThrowProficiencies.ToArray();

        public StartingEquipment.EquipmentWithAmount[] Inventory => inventory.ToArray();
        
        public ILanguage[] Languages => languages.ToArray();

        public HitPoints HitPoints => hitPoints;

        public string ArmorClass => armorClass;

        public DeathSaves DeathSaves => deathSaves;
        
        public Ability PrimaryAbility => this.@class.PrimaryAbility;
        
        public Die HitPointDie => this.@class.HitPointDie;
        
        public Feat Feat => this.background.Feat;
        
        public CreatureType CreatureType => this.spex.CreatureType;
        
        public Size Size => this.spex.Size;
        
        public float Speed => this.spex.Speed;
        
        public SpecialTrait[] SpecialTrait => this.spex.SpecialTraits.ToArray();
        
        
        // public Dictionary<AbilityEnum, AbilityStats> Abilities = new Dictionary<AbilityEnum, AbilityStats>
        //     {
        //         // TODO: fix this
        //         // [AbilityEnum.Strength] = new AbilityStats(AbilityEnum.Strength),
        //         // [AbilityEnum.Dexterity] = new AbilityStats(AbilityEnum.Dexterity),
        //         // [AbilityEnum.Constitution] = new AbilityStats(AbilityEnum.Constitution),
        //         // [AbilityEnum.Intelligence] = new AbilityStats(AbilityEnum.Intelligence),
        //         // [AbilityEnum.Wisdom] = new AbilityStats(AbilityEnum.Wisdom),
        //         // [AbilityEnum.Charisma] = new AbilityStats(AbilityEnum.Charisma),
        // };
        //
        // public int ProficiencyBonus => 2 + (this.level - 1) / 4;
        //
        // public HashSet<BaseArmourType> ArmourTypeProficiencies;
        //
        // public HashSet<ITool> ToolProficiencies; //TODO: Not sure about this ITool
        //
        // public List<Feat> Feats = new List<Feat>();
        //
        // public List<IEquipment> Equipment = new List<IEquipment>();
        //
        // public Dictionary<CoinValue, int> Coins = new Dictionary<CoinValue, int>();
        //
        // public int Speed;
        //
        // public Size Size;
        //
        // public List<TypeTrait> SpeciesTraits = new List<TypeTrait>();
        //
        // public bool HeroicInspiration
        // {
        //     get
        //     {
        //         var heroicInspirationTrait = this.SpeciesTraits.FirstOrDefault(trait => trait is HeroicInspiration) as HeroicInspiration;
        //
        //         return false;
        //         //return heroicInspirationTrait?.IsInspired ?? false;
        //     }
        // }
        //
        // [SerializeReference]
        // public HashSet<ILanguage> Languages = new HashSet<ILanguage>();
        //
        //
        // public int GetSavingThrowProficiency(AbilityEnum ability)
        // {
        //     AbilityStats abilityStats = this.Abilities[ability];
        //
        //     return abilityStats.Modifier + (abilityStats.SavingThrow ? this.ProficiencyBonus : 0); // TODO: implement expertise
        // }
        //
        // public int GetSkillProficiency(Skill skill)
        // {
        //     // TODO: Fix this
        //     //AbilityStats abilityStats = this.Abilities[skill.Ability];
        //
        //     //var proficiencyScore = abilityStats.Modifier;
        //
        //     // if (abilityStats.SkillProficiencies.TryGetValue(skill, out var proficiencySkill))
        //     // {
        //     //     var multiplier = proficiencySkill.IsExpert ? 2 : 1; // Here 2 is the multiplier for expertise.
        //     //
        //     //     proficiencyScore += multiplier * this.ProficiencyBonus;
        //     // }
        //
        //     //return proficiencyScore;
        //
        //     throw new NotImplementedException();
        // }
        //
        // public string PassivePerception;
        //
        //
        //
        // public string Initiative;
        //
        // [SerializeField]
        // public string HitDice;
        //
        // [SerializeField]
        // public string WeaponsDamageCantrips;
        //
        // [SerializeField]
        // public string ClassFeatures;
        //
        // [SerializeField]
        // public string Appearance;
        //
        // [SerializeField]
        // public string BackstoryAndPersonality;


    }
}