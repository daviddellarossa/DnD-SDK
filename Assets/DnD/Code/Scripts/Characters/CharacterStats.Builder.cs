﻿using System;
using System.Collections.Generic;
using System.Linq;
using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Backgrounds;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Helpers;
using DnD.Code.Scripts.Helpers.NameHelper;
using DnD.Code.Scripts.Helpers.PathHelper;
using DnD.Code.Scripts.Species;
using UnityEngine;

namespace DnD.Code.Scripts.Characters
{
    public partial class CharacterStats
    {
        public class Builder
        {
            public static readonly string CharacterStatsPath = "Assets/CharacterStats.asset";
            private const int DefaultLevel = 1;
            private const int DefaultXp = 0;
            private const int DefaultProficiencyBonus = 2;
            
            private string _name;
            private Class _class;
            private SubClass _subClass;
            private Spex _spex;
            private Background _background;
            private int _level = DefaultLevel;
            private int _xp = DefaultXp;
            private int _proficiencyBonus = DefaultProficiencyBonus;
            private List<Skill> _skillProficienciesFromClass = new ();
            private StartingEquipment _startingEquipmentFromClass;
            private Dictionary<Ability, int> _abilityScores = new();
            
            public Builder SetName(string name)
            {
                this._name = name;
                return this;
            }

            public Builder SetClass(Class @class)
            {
                this._class = @class;
                return this;
            }
            
            public Builder SetSubClass(SubClass subClass)
            {
                this._subClass = subClass;
                
                return this;
            }

            public Builder SetBackground(Background background)
            {
                this._background = background;
                return this;
            }

            public Builder SetSkillProficiencies(Skill[] skillProficiencies)
            {
                this._skillProficienciesFromClass.AddRange(skillProficiencies);
                return this;
            }

            public Builder SetStartingEquipmentFromClass(StartingEquipment startingEquipment)
            {
                this._startingEquipmentFromClass = startingEquipment;
                return this;
            }
            
            public Builder SetSpex(Spex spex)
            {
                this._spex = spex;
                return this;
            }

            public Builder SetAbilityScore(Ability ability, int score)
            {
                this._abilityScores[ability] = score;
                return this;
            }
            
            public CharacterStats Build()
            {
                if (!CheckAll())
                {
                    Debug.LogError("Cannot continue with the CharacterStats creation.");
                    return null;
                }
                
                FileSystemHelper.EnsureFolderExists(PathHelper.CharacterStatsPath);

                var fileName = $"{NameHelper.Naming.CharacterStats}.{this._name}.{Guid.NewGuid().ToString()}";
                var characterStats = ScriptableObjectHelper.CreateScriptableObject<CharacterStats>(fileName, PathHelper.CharacterStatsPath);
                
                characterStats.name = this._name;
                characterStats.@class = this._class;
                characterStats.subClass = this._subClass;
                characterStats.spex = this._spex;
                characterStats.background = this._background;
                characterStats.level = this._level;
                characterStats.xp = this._xp;
                characterStats.proficiencyBonus = this._proficiencyBonus;
                
                // from class
                characterStats.armorTraining.AddRange(this._class.ArmorTraining);
                characterStats.weaponProficiencies.AddRange(this._class.WeaponProficiencies);
                characterStats.skillProficiencies.AddRange(this._skillProficienciesFromClass);
                characterStats.inventory.AddRange(this._startingEquipmentFromClass.Items);
                characterStats.savingThrowProficiencies.AddRange(this._class.SavingThrowProficiencies);
                
                // from background
                characterStats.skillProficiencies.AddRange(this._background.SkillProficiencies);
                characterStats.toolProficiencies.Add(this._background.ToolProficiency);
                
                // others
                characterStats.abilityScores = this._abilityScores;
                
                return characterStats;
            }

            private bool CheckName()
            {
                if (string.IsNullOrEmpty(this._name))
                {
                    Debug.LogError("Name cannot be null or empty.");
                    return false;
                }

                return true;
            }
            
            private bool CheckClass()
            {
                if (this._class is null)
                {
                    Debug.LogError("Class cannot be null");
                    return false;
                }
                
                return true;
            }
            
            private bool CheckSubClass()
            {
                if (_class.SubClasses.Count == 0)
                {
                    return true; // Class does not offer sub-classes
                }
                
                if (this._subClass is null)
                {
                    Debug.LogError("SubClass cannot be null");
                    return false;
                }
                
                if (!_class.SubClasses.Contains(this._subClass))
                {
                    Debug.LogError($"Class {_class.name} does not have a subclass with {this._subClass.name}");
                    return false;
                    
                }
                
                return true;
            }
            
            private bool CheckBackground()
            {
                if (this._background is null)
                {
                    Debug.LogError("Background cannot be null");
                    return false;
                }
                
                return true;
            }

            private bool CheckSpex()
            {
                if (this._spex is null)
                {
                    Debug.LogError("Spex cannot be null");
                    return false;
                }
                
                return true;
            }

            private bool CheckAbilityScores()
            {
                var abilities = Helpers.ScriptableObjectHelper.GetAllScriptableObjects<Ability>(PathHelper.Abilities.AbilitiesPath);

                foreach (var ability in abilities)
                {
                    if (!this._abilityScores.ContainsKey(ability))
                    {
                        Debug.LogError($"Ability {ability.name} does not have a score for {this._name}");
                        return false;
                    }
                }

                return true;
            }
            private bool CheckSkillProficienciesFromClass()
            {
                foreach (var skill in this._skillProficienciesFromClass)
                {
                    var skillAvailable = _class.SkillProficienciesAvailable.SingleOrDefault(skillAvailable =>
                        skillAvailable.name == skill.name);
                    if (skillAvailable is null)
                    {
                        Debug.LogError($"Skill ({skill}) is not among the available skills from the chosen class ({_class.name}).");
                        return false;
                    }
                }
                return true;
            }

            private bool CheckStartingEquipmentFromClass()
            {
                if (this._startingEquipmentFromClass is null)
                {
                    Debug.LogError("StartingEquipment cannot be null");
                    return false;
                }
                
                var startingEquipmentAvailable = this._class.StartingEquipmentOptions.SingleOrDefault(startingEquipment => startingEquipment.name == this._startingEquipmentFromClass.name);
                if (startingEquipmentAvailable is null)
                {
                    Debug.LogError($"The chosen starting equipment is not among those available from the chosen class ({_class.name}).");
                    return false;
                }
                return true;
            }
            
            private bool CheckAll()
            {
                return CheckName()
                    && CheckClass()
                    && CheckSubClass()
                    && CheckBackground()
                    && CheckSpex()
                    && CheckSkillProficienciesFromClass()
                    && CheckStartingEquipmentFromClass()
                    && CheckAbilityScores();
            }
        }
    }
}