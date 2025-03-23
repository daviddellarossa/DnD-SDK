using DnD.Code.Scripts.Characters.Abilities;
using DnD.Code.Scripts.Characters.Classes;
using DnD.Code.Scripts.Common;
using System.Threading;
using DnD.Code.Scripts.Helpers.PathHelper;
using Unity.VisualScripting;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;


namespace DnD.Editor.Initializer
{
    public static class AbilitiesInitializer
    {
        public static Ability[] GetAllAbilities()
        {
            return Common.GetAllScriptableObjects<Ability>(PathHelper.Abilities.AbilitiesPath);
        }
        
        public static Skill[] GetAllSkills()
        {
            return Common.GetAllScriptableObjects<Skill>(PathHelper.Abilities.SkillsPath);
        }
        
        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Abilities Data")]
        public static void InitializeAbilities()
        {
            try
            {
                AssetDatabase.StartAssetEditing();
            
                Common.EnsureFolderExists(PathHelper.Abilities.AbilitiesPath);
                Common.EnsureFolderExists(PathHelper.Abilities.SkillsPath);

                // Create Abilities

                {
                    var charisma = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Charisma, PathHelper.Abilities.AbilitiesPath);
                
                    charisma.DisplayName = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Charisma}";
                    charisma.DisplayDescription = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Charisma}.{NameHelper.Naming.Description}";

                    {
                        var deception = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Deception, PathHelper.Abilities.SkillsPath);
                        deception.DisplayName = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Deception}";
                        deception.DisplayDescription = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Deception}.{NameHelper.Naming.Description}";
                        deception.Ability = charisma;
                        EditorUtility.SetDirty(deception);
                        
                        charisma.SkillList.Add(deception);
                    }

                    {
                        var intimidation = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Intimidation, PathHelper.Abilities.SkillsPath);
                        intimidation.DisplayName = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Intimidation}";
                        intimidation.DisplayDescription = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Intimidation}.{NameHelper.Naming.Description}";
                        intimidation.Ability = charisma;
                        EditorUtility.SetDirty(intimidation);
                        
                        charisma.SkillList.Add(intimidation);
                    }

                    {
                        var performance = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Performance, PathHelper.Abilities.SkillsPath);
                        performance.DisplayName = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Performance}";
                        performance.DisplayDescription = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Performance}.{NameHelper.Naming.Description}";
                        performance.Ability = charisma;
                        EditorUtility.SetDirty(performance);
                        
                        charisma.SkillList.Add(performance);
                    }

                    {
                        var persuasion = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Persuasion, PathHelper.Abilities.SkillsPath);
                        persuasion.DisplayName = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Persuasion}";
                        persuasion.DisplayDescription = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Persuasion}.{NameHelper.Naming.Description}";
                        persuasion.Ability = charisma;
                        EditorUtility.SetDirty(persuasion);
                        
                        charisma.SkillList.Add(persuasion);
                    }
                    
                    EditorUtility.SetDirty(charisma);
                }

                {
                    var constitution = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Constitution, PathHelper.Abilities.AbilitiesPath);
                    constitution.DisplayName = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Constitution}";
                    constitution.DisplayDescription = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Constitution}.{NameHelper.Naming.Description}";
                    EditorUtility.SetDirty(constitution);
                }

                {
                    var dexterity = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Dexterity, PathHelper.Abilities.AbilitiesPath);
                    dexterity.DisplayName = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Dexterity}";
                    dexterity.DisplayDescription = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Dexterity}.{NameHelper.Naming.Description}";
                    
                    {
                        var acrobatics = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Acrobatics, PathHelper.Abilities.SkillsPath);
                        acrobatics.DisplayName = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Acrobatics}";
                        acrobatics.DisplayDescription = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Acrobatics}.{NameHelper.Naming.Description}";
                        acrobatics.Ability = dexterity;
                        EditorUtility.SetDirty(acrobatics);
                        
                        dexterity.SkillList.Add(acrobatics);
                    }

                    {
                        var sleightOfHand = Common.CreateScriptableObject<Skill>(NameHelper.Skills.SleightOfHand, PathHelper.Abilities.SkillsPath);
                        sleightOfHand.DisplayName = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.SleightOfHand}";
                        sleightOfHand.DisplayDescription = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.SleightOfHand}.{NameHelper.Naming.Description}";
                        sleightOfHand.Ability = dexterity;
                        EditorUtility.SetDirty(sleightOfHand);
                        
                        dexterity.SkillList.Add(sleightOfHand);
                    }

                    {
                        var stealth = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Stealth, PathHelper.Abilities.SkillsPath);
                        stealth.DisplayName = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Stealth}";
                        stealth.DisplayDescription = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Stealth}.{NameHelper.Naming.Description}";
                        stealth.Ability = dexterity;
                        EditorUtility.SetDirty(stealth);
                        
                        dexterity.SkillList.Add(stealth);
                    }
                    
                    EditorUtility.SetDirty(dexterity);
                }

                {
                    var intelligence = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Intelligence, PathHelper.Abilities.AbilitiesPath);
                    intelligence.DisplayName = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Intelligence}";
                    intelligence.DisplayDescription = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Intelligence}.{NameHelper.Naming.Description}";
                    
                    {
                        var arcana = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Arcana, PathHelper.Abilities.SkillsPath);
                        arcana.DisplayName = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Arcana}";
                        arcana.DisplayDescription = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Arcana}.{NameHelper.Naming.Description}";
                        arcana.Ability = intelligence;
                        EditorUtility.SetDirty(arcana);
                        
                        intelligence.SkillList.Add(arcana);
                    }

                    {
                        var history = Common.CreateScriptableObject<Skill>(NameHelper.Skills.History, PathHelper.Abilities.SkillsPath);
                        history.DisplayName = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.History}";
                        history.DisplayDescription = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.History}.{NameHelper.Naming.Description}";
                        history.Ability = intelligence;
                        EditorUtility.SetDirty(history);
                        
                        intelligence.SkillList.Add(history);
                    }

                    {
                        var investigation = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Investigation, PathHelper.Abilities.SkillsPath);
                        investigation.DisplayName = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Investigation}";
                        investigation.DisplayDescription = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Investigation}.{NameHelper.Naming.Description}";
                        investigation.Ability = intelligence;
                        EditorUtility.SetDirty(investigation);
                        
                        intelligence.SkillList.Add(investigation);
                    }

                    {
                        var nature = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Nature, PathHelper.Abilities.SkillsPath);
                        nature.DisplayName = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Nature}";
                        nature.DisplayDescription = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Nature}.{NameHelper.Naming.Description}";
                        nature.Ability = intelligence;
                        EditorUtility.SetDirty(nature);
                        
                        intelligence.SkillList.Add(nature);
                    }

                    {
                        var religion = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Religion, PathHelper.Abilities.SkillsPath);
                        religion.DisplayName = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Religion}";
                        religion.DisplayDescription = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Religion}.{NameHelper.Naming.Description}";
                        religion.Ability = intelligence;
                        EditorUtility.SetDirty(religion);
                        
                        intelligence.SkillList.Add(religion);
                    }
                    
                    EditorUtility.SetDirty(intelligence);
                }

                {
                    var strength = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Strength, PathHelper.Abilities.AbilitiesPath);
                    strength.DisplayName = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Strength}";
                    strength.DisplayDescription = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Strength}.{NameHelper.Naming.Description}";
                    
                    {
                        var athletics = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Athletics, PathHelper.Abilities.SkillsPath);
                        athletics.DisplayName = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Athletics}";
                        athletics.DisplayDescription = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Athletics}.{NameHelper.Naming.Description}";
                        athletics.Ability = strength;
                        EditorUtility.SetDirty(athletics);
                        
                        strength.SkillList.Add(athletics);
                    }
                    
                    EditorUtility.SetDirty(strength);
                }

                {
                    var wisdom = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Wisdom, PathHelper.Abilities.AbilitiesPath);
                    wisdom.DisplayName = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Wisdom}";
                    wisdom.DisplayDescription = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Wisdom}.{NameHelper.Naming.Description}";
                    
                    {
                        var animalHandling = Common.CreateScriptableObject<Skill>(NameHelper.Skills.AnimalHandling, PathHelper.Abilities.SkillsPath);
                        animalHandling.DisplayName = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.AnimalHandling}";
                        animalHandling.DisplayDescription = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.AnimalHandling}.{NameHelper.Naming.Description}";
                        animalHandling.Ability = wisdom;
                        EditorUtility.SetDirty(animalHandling);
                        
                        wisdom.SkillList.Add(animalHandling);
                    }

                    {
                        var insight = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Insight, PathHelper.Abilities.SkillsPath);
                        insight.DisplayName = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Insight}";
                        insight.DisplayDescription = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Insight}.{NameHelper.Naming.Description}";
                        insight.Ability = wisdom;
                        EditorUtility.SetDirty(insight);
                        
                        wisdom.SkillList.Add(insight);
                    }

                    {
                        var medicine = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Medicine, PathHelper.Abilities.SkillsPath);
                        medicine.DisplayName = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Medicine}";
                        medicine.DisplayDescription = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Medicine}.{NameHelper.Naming.Description}";
                        medicine.Ability = wisdom;
                        EditorUtility.SetDirty(medicine);
                        
                        wisdom.SkillList.Add(medicine);
                    }

                    {
                        var perception = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Perception, PathHelper.Abilities.SkillsPath);
                        perception.DisplayName = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Perception}";
                        perception.DisplayDescription = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Perception}.{NameHelper.Naming.Description}";
                        perception.Ability = wisdom;
                        EditorUtility.SetDirty(perception);
                        
                        wisdom.SkillList.Add(perception);
                    }

                    {
                        var survival = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Survival, PathHelper.Abilities.SkillsPath);
                        survival.DisplayName = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Survival}";
                        survival.DisplayDescription = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Survival}.{NameHelper.Naming.Description}";
                        survival.Ability = wisdom;
                        EditorUtility.SetDirty(survival);
                        
                        wisdom.SkillList.Add(survival);
                    }
                    
                    EditorUtility.SetDirty(wisdom);
                }
                
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }
    }
}
