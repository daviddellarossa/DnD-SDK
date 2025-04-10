using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Backgrounds;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Classes.ClassFeatures;
using DnD.Code.Scripts.Classes.FeatureProperties;
using DnD.Code.Scripts.Common;
using DnD.Code.Scripts.Equipment;
using DnD.Code.Scripts.Feats;
using DnD.Code.Scripts.Helpers.NameHelper;
using DnD.Code.Scripts.Languages;
using DnD.Code.Scripts.Species;
using DnD.Code.Scripts.Species.SpecialTraits;
using DnD.Code.Scripts.Weapons;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace DnD.Code.Scripts.Characters
{
    public partial class CharacterStats
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
        private readonly Dictionary<string, AbilityStats> abilities = new ();
        
        [SerializeField]
        private readonly List<BaseArmourType> armorTraining = new List<BaseArmourType>();
        
        [SerializeField]
        private readonly List<WeaponType> weaponProficiencies = new ();
        
        [SerializeField]
        private readonly List<Proficient> toolProficiencies = new List<Proficient>();
        
        [SerializeField]
        private readonly List<Ability> savingThrowProficiencies = new ();
        
        [SerializeField]
        private readonly List<StartingEquipment.EquipmentWithAmount> inventory = new ();
        
        [SerializeField]
        private int hitPoints;
        
        [SerializeField]
        private int temporaryHitPoints;
        
        [SerializeField]
        private DeathSaves deathSaves = new DeathSaves();

        [SerializeField]
        private HashSet<Language> languages = new ();
        
        [SerializeReference]
        private IClassFeatureStats classFeatureStats;
        
        [SerializeField]
        private List<ClassFeature> classFeatures = new List<ClassFeature>();
        
        public string CharacterName
        {
            get => characterName;
            internal set => characterName = value;
        }

        public Class Class
        {
            get => @class;
            internal set => @class = value;
        }

        public SubClass SubClass
        {
            get => subClass;
            internal set => subClass = value;
        }

        public Background Background
        {
            get => background;
            internal set => background = value;
        }

        public Species.Spex Spex
        {
            get => spex;
            internal set => spex = value;
        }

        public int Level
        {
            get => level;
            internal set => level = value;
        }

        public int Xp
        {
            get => xp;
            internal set => xp = value;
        }
        
        public int ProficiencyBonus => Constants.BaseProficiencyBonus + (this.level - 1) / 4;
        
        public ImmutableDictionary<string, AbilityStats> Abilities => abilities.ToImmutableDictionary();

        public void SetAbilityStats(AbilityStats abilityStats)
        {
            abilities[abilityStats.Ability.name] = abilityStats;
        }
        internal void ResetAbilityStats(AbilityStats[] abilityStats)
        {
            abilities.Clear();
            foreach (var abilityStat in abilityStats)
            {
                abilities.Add(abilityStat.Ability.name, abilityStat);
            }
        }
        
        public BaseArmourType[] ArmorTraining => armorTraining.ToArray();

        internal void SetArmourTraining(IEnumerable<BaseArmourType> armourTraining)
        {
            this.armorTraining.Clear();
            this.armorTraining.AddRange(armourTraining);
        }
        public WeaponType[] WeaponProficiencies => weaponProficiencies.ToArray();
        
        internal void SetWeaponProficiencies(IEnumerable<WeaponType> weaponTypes)
        {
            this.weaponProficiencies.Clear();
            this.weaponProficiencies.AddRange(weaponTypes);
        }
        
        public Proficient[] ToolProficiencies => toolProficiencies.ToArray();

        internal void SetToolProficiencies(IEnumerable<Proficient> proficiencies)
        {
            this.toolProficiencies.Clear();
            this.toolProficiencies.AddRange(proficiencies);
        }
        
        public Ability[] SavingThrowProficiencies =>  savingThrowProficiencies.ToArray();
        internal void SetSavingThrowProficiencies(IEnumerable<Ability> proficiencies)
        {
            this.savingThrowProficiencies.Clear();
            this.savingThrowProficiencies.AddRange(proficiencies);
        }

        public StartingEquipment.EquipmentWithAmount[] Inventory => inventory.ToArray();
        
        public Language[] Languages => languages.ToArray();

        internal void SetLanguages(IEnumerable<Language> languages)
        {
            this.languages.Clear();
            foreach (var language in languages)
            {
                this.languages.Add(language);
            }
        }

        public int HitPoints
        {
            get => hitPoints;
            internal set => hitPoints = value;
        }

        public int TemporaryHitPoints
        {
            get => temporaryHitPoints;
            internal set => temporaryHitPoints = value;
        }

        public DeathSaves DeathSaves
        {
            get => deathSaves;
            internal set => deathSaves = value;
        }
        
        public Ability PrimaryAbility => this.@class.PrimaryAbility;

        public Die HitPointDie => this.@class.HitPointDie;
        
        public Feat Feat => this.background.Feat;
        
        public CreatureType CreatureType => this.spex.CreatureType;
        
        public Size Size => this.spex.Size;
        
        public float Speed => this.spex.Speed;
        
        /// <summary>
        /// Special traits are defined in the Species
        /// </summary>
        public SpecialTrait[] SpecialTrait => this.spex.SpecialTraits.ToArray();

        public IClassFeatureStats ClassFeatureStats
        {
            get => classFeatureStats;
            set => classFeatureStats = value;
        }

        public List<ClassFeature> ClassFeatures
        {
            get => classFeatures;
            set => classFeatures = value;
        }

        public int PassivePerception
        {
            get
            {
                var abilityStats = this.abilities[NameHelper.Abilities.Wisdom];
                return Constants.BasePassivePerception
                       + abilityStats.Modifier
                       + (abilityStats.SkillProficiencies.ContainsKey(NameHelper.Skills.Perception) ? this.ProficiencyBonus : 0);
            }
        }

        public int Initiative => this.abilities[NameHelper.Abilities.Dexterity].Modifier;

        public int MaxHitPoints => this.HitPointDie.NumOfFaces * this.level + this.abilities[NameHelper.Abilities.Constitution].Modifier;

        public int ArmourClass => Constants.BaseArmourClass + this.abilities[NameHelper.Abilities.Dexterity].Modifier;

        // public Dictionary<CoinValue, int> Coins = new Dictionary<CoinValue, int>();
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
        // [SerializeField]
        // public string WeaponsDamageCantrips;

    }
}