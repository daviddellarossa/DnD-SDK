using System;
using System.Collections.Generic;
using System.Linq;
using Codice.Client.Common;
using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Backgrounds;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Helpers.NameHelper;
using DnD.Code.Scripts.Languages;
using DnD.Code.Scripts.Species;
using Infrastructure.Helpers;
using UnityEditor;
using UnityEngine;
using PathHelper = DnD.Code.Scripts.Helpers.PathHelper.PathHelper;

namespace DnD.Code.Scripts.Characters
{
    public partial class CharacterStats
    {
        public class Builder
        {
            public static readonly string CharacterStatsPath = "Assets/CharacterStats.asset";
            public static readonly int DefaultLevel = 1;
            public static readonly int DefaultXp = 0;
            public static readonly int DefaultNumberOfLanguages = 3;
            
            private string _name;
            private Class _class;
            private SubClass _subClass;
            private Spex _spex;
            private Background _background;
            private int _level = DefaultLevel;
            private int _xp = DefaultXp;
            private List<Skill> _skillProficienciesFromClass = new ();
            private Equipment.StartingEquipment _startingEquipmentFromClass;
            private Equipment.StartingEquipment _startingEquipmentFromBackground;
            private List<AbilityStats> _abilityStats = new();
            private HashSet<StandardLanguage> _languages = new();
            
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

            public Builder SetSkillProficienciesFromClass(Skill[] skillProficiencies)
            {
                this._skillProficienciesFromClass.AddRange(skillProficiencies);
                return this;
            }

            public Builder SetStartingEquipmentFromClass(Equipment.StartingEquipment startingEquipment)
            {
                this._startingEquipmentFromClass = startingEquipment;
                return this;
            }
            
            public Builder SetStartingEquipmentFromBackground(Equipment.StartingEquipment startingEquipment)
            {
                this._startingEquipmentFromBackground = startingEquipment;
                return this;
            }

            public Builder SetSpex(Spex spex)
            {
                this._spex = spex;
                return this;
            }

            public Builder SetAbilityStats(AbilityStats abilityStats)
            {
                this._abilityStats.Add(abilityStats);
                return this;
            }

            public Builder SetLanguage(StandardLanguage language)
            {
                this._languages.Add(language);
                return this;
            }
            
            public CharacterStats Build()
            {
                if (!CheckAll())
                {
                    Debug.Log("Some checks failed. Cannot continue with the CharacterStats creation.");
                    return null;
                }
                
                FileSystemHelper.EnsureFolderExists(PathHelper.CharacterStatsPath);

                var fileName = $"{NameHelper.Naming.CharacterStats}.{this._name}.{Guid.NewGuid().ToString()}";
                //var characterStats = ScriptableObjectHelper.CreateScriptableObject<CharacterStats>(fileName, PathHelper.CharacterStatsPath);
                var characterStats = new CharacterStats
                {
                    characterName = this._name,
                    @class = this._class,
                    subClass = this._subClass,
                    spex = this._spex,
                    background = this._background,
                    level = this._level,
                    xp = this._xp
                };

                foreach (var standardLanguage in this._languages)
                {
                    characterStats.languages.Add(standardLanguage);
                }
                
                // from class
                characterStats.armorTraining.AddRange(this._class.ArmourTraining);
                characterStats.weaponProficiencies.AddRange(this._class.WeaponProficiencies);
                characterStats.skillProficiencies.AddRange(this._skillProficienciesFromClass);
                characterStats.inventory.AddRange(this._startingEquipmentFromClass.EquipmentsWithAmountList);
                characterStats.savingThrowProficiencies.AddRange(this._class.SavingThrowProficiencies);
                
                var currentLevel = this._class.Levels.Single(lvl => lvl.LevelNum == this._level);
                characterStats.ClassFeatureStats = currentLevel.ClassFeatureStats;
                characterStats.classFeatures.AddRange(currentLevel.ClassFeatures);
                
                // from background
                characterStats.skillProficiencies.AddRange(this._background.SkillProficiencies);
                characterStats.toolProficiencies.Add(this._background.ToolProficiency);
                characterStats.inventory.AddRange(this._startingEquipmentFromBackground.EquipmentsWithAmountList);

                // from species
                
                
                // others
                foreach (var abilityStat in this._abilityStats)
                {
                    abilityStat.SavingThrow = this._class.SavingThrowProficiencies.Contains(abilityStat.Ability);

                    foreach (var skillProficiency in this._skillProficienciesFromClass.Union(this._background.SkillProficiencies).Where(x => x.Ability == abilityStat.Ability))
                    {
                        abilityStat.SkillProficiencies[skillProficiency.name] = new SkillStats()
                        {
                            Skill = skillProficiency,
                        };
                    }
                    characterStats.abilities.Add(abilityStat.Ability.name, abilityStat);
                }
                
                characterStats.hitPoints = characterStats.MaxHitPoints;
                
                return characterStats;
            }

            public virtual bool CheckName()
            {
                if (string.IsNullOrEmpty(this._name))
                {
                    Debug.Log($"{nameof(Builder)}: {nameof(Builder._name)} cannot be null or empty.");
                    return false;
                }

                return true;
            }
            
            public virtual bool CheckClass()
            {
                if (this._class is null)
                {
                    Debug.Log($"{nameof(Builder)}: Class cannot be null");
                    return false;
                }
                
                return true;
            }
            
            public virtual bool CheckSubClass()
            {
                if (_class.SubClasses.Count == 0)
                {
                    return true; // Class does not offer sub-classes
                }
                
                if (this._subClass is null)
                {
                    Debug.Log($"{nameof(Builder)}: SubClass cannot be null");
                    return false;
                }
                
                if (!_class.SubClasses.Contains(this._subClass))
                {
                    Debug.Log($"{nameof(Builder)}: Class {_class.name} does not have a subclass with {this._subClass.name}");
                    return false;
                }
                
                return true;
            }
            
            public virtual bool CheckBackground()
            {
                if (this._background is null)
                {
                    Debug.Log($"{nameof(Builder)}: Background cannot be null");
                    return false;
                }
                
                return true;
            }

            public virtual bool CheckSpex()
            {
                if (this._spex is null)
                {
                    Debug.Log($"{nameof(Builder)}: Spex cannot be null");
                    return false;
                }
                
                return true;
            }
            
            public virtual bool CheckAbilityStats()
            {
                var abilities = ScriptableObjectHelper.GetAllScriptableObjects<Ability>(PathHelper.Abilities.AbilitiesPath);

                foreach (var ability in abilities)
                {
                    var abilityStat = this._abilityStats.SingleOrDefault(abilityStat => abilityStat.Ability == ability);
                    if (abilityStat is null)
                    {
                        Debug.Log($"{nameof(Builder)}: Ability stats for ability {ability.name} not set for {this._name}");
                        return false;
                    }

                    if (abilityStat.Score == 0)
                    {
                        Debug.Log($"{nameof(Builder)}: Ability score for ability {abilityStat.Ability.name} not set for {this._name}");
                        return false;
                    }
                }

                return true;
            }
            
            public virtual bool CheckSkillProficienciesFromClass()
            {
                if (this._skillProficienciesFromClass.Count != _class.NumberOfSkillProficienciesToChoose)
                {
                    Debug.Log($@"The number of Skill proficiencies from class does not match: {this._skillProficienciesFromClass.Count}; expected: {_class.NumberOfSkillProficienciesToChoose}");
                    return false;
                }
                foreach (var skill in this._skillProficienciesFromClass)
                {
                    var skillAvailable = _class.SkillProficienciesAvailable.SingleOrDefault(skillAvailable =>
                        skillAvailable.name == skill.name);
                    if (skillAvailable is null)
                    {
                        Debug.Log($"{nameof(Builder)}: Skill ({skill}) is not among the available skills from the chosen class ({_class.name}).");
                        return false;
                    }
                }
                return true;
            }

            public virtual bool CheckStartingEquipmentFromClass()
            {
                if (this._startingEquipmentFromClass is null)
                {
                    Debug.Log("StartingEquipment from Class cannot be null");
                    return false;
                }
                
                var startingEquipmentAvailable = this._class.StartingEquipmentOptions.SingleOrDefault(startingEquipment => startingEquipment.name == this._startingEquipmentFromClass.name);
                if (startingEquipmentAvailable is null)
                {
                    Debug.Log($"{nameof(Builder)}: The chosen starting equipment is not among those available from the chosen class ({_class.name}).");
                    return false;
                }
                return true;
            }
            
            public virtual bool CheckStartingEquipmentFromBackground()
            {
                if (this._startingEquipmentFromBackground is null)
                {
                    Debug.Log("StartingEquipment from Background cannot be null");
                    return false;
                }
                
                var startingEquipmentAvailable = this._background.StartingEquipmentOptions.SingleOrDefault(startingEquipment => startingEquipment.name == this._startingEquipmentFromBackground.name);
                if (startingEquipmentAvailable is null)
                {
                    Debug.Log($"{nameof(Builder)}: The chosen starting equipment is not among those available from the chosen background ({_background.name}).");
                    return false;
                }
                return true;
            }

            public virtual bool CheckLanguages()
            {
                if (this._languages.Any(language => language.name == NameHelper.Languages.Common) == false)
                {
                    var standardLanguages = ScriptableObjectHelper.GetAllScriptableObjects<StandardLanguage>(PathHelper.Languages.StandardLanguagesPath);
                    var commonLanguage = standardLanguages.SingleOrDefault(language => language.name == NameHelper.Languages.Common);
                    
                    this._languages.Add(commonLanguage);
                }

                if (this._languages.Count() != DefaultNumberOfLanguages)
                {
                    Debug.Log($"The character builder needs {DefaultNumberOfLanguages} languages (2 + Common).");
                    return false;
                }

                return true;
            }

            public virtual bool CheckAll()
            {
                return CheckName()
                    && CheckClass()
                    && CheckSubClass()
                    && CheckBackground()
                    && CheckSpex()
                    && CheckSkillProficienciesFromClass()
                    && CheckStartingEquipmentFromClass()
                    && CheckStartingEquipmentFromBackground()
                    && CheckAbilityStats()
                    && CheckLanguages();
            }
        }
    }
}