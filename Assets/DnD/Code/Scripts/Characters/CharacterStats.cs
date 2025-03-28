using System;
using System.Collections.Generic;
using System.Linq;
using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Armour;
using DnD.Code.Scripts.Backgrounds;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Equipment;
using DnD.Code.Scripts.Equipment.Coins;
using DnD.Code.Scripts.Feats;
using DnD.Code.Scripts.Languages;
using DnD.Code.Scripts.Species;
using DnD.Code.Scripts.Species.SpecialTraits.TraitTypes;
using DnD.Code.Scripts.Tools;
using UnityEngine;
using UnityEngine.Serialization;

namespace DnD.Code.Scripts.Characters
{
    public partial class CharacterStats : ScriptableObject
    {
        [SerializeField]
        private string name;

        [SerializeReference]
        private Class @class;
        
        [SerializeReference]
        private SubClass subClass;
        
        [SerializeField]
        private Background background;
        
        [SerializeField]
        private Species.Spex spex;
        
        
        
        [SerializeField]
        private int level = 1;

        [SerializeField]
        private int xp = 0;

        [SerializeField]
        private string armorClass;

        [SerializeField]
        private HitPoints hitPoints;

        [SerializeField]
        private DeathSaves deathSaves;

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