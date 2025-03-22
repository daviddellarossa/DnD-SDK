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

                var charisma = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Charisma, PathHelper.Abilities.AbilitiesPath);
                
                charisma.Name = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Charisma}";
                charisma.AbilityType = AbilityEnum.Charisma;
                EditorUtility.SetDirty(charisma);
                
                var constitution = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Constitution, PathHelper.Abilities.AbilitiesPath);
                constitution.Name = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Constitution}";
                constitution.AbilityType = AbilityEnum.Constitution;
                EditorUtility.SetDirty(constitution);
                
                var dexterity = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Dexterity, PathHelper.Abilities.AbilitiesPath);
                dexterity.Name = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Dexterity}";
                dexterity.AbilityType = AbilityEnum.Dexterity;
                EditorUtility.SetDirty(dexterity);

                var intelligence = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Intelligence, PathHelper.Abilities.AbilitiesPath);
                intelligence.Name = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Intelligence}";
                intelligence.AbilityType = AbilityEnum.Intelligence;
                EditorUtility.SetDirty(intelligence);

                var strength = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Strength, PathHelper.Abilities.AbilitiesPath);
                strength.Name = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Strength}";
                strength.AbilityType = AbilityEnum.Strength;
                EditorUtility.SetDirty(strength);

                var wisdom = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Wisdom, PathHelper.Abilities.AbilitiesPath);
                wisdom.Name = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Wisdom}";
                wisdom.AbilityType = AbilityEnum.Wisdom;
                EditorUtility.SetDirty(wisdom);

                // Create Skills

                var acrobatics = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Acrobatics, PathHelper.Abilities.SkillsPath);
                acrobatics.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Acrobatics}";
                acrobatics.Ability = AbilityEnum.Dexterity;
                EditorUtility.SetDirty(acrobatics);
            
                var animalHandling = Common.CreateScriptableObject<Skill>(NameHelper.Skills.AnimalHandling, PathHelper.Abilities.SkillsPath);
                animalHandling.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.AnimalHandling}";
                animalHandling.Ability = AbilityEnum.Wisdom;
                EditorUtility.SetDirty(animalHandling);

                var arcana = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Arcana, PathHelper.Abilities.SkillsPath);
                arcana.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Arcana}";
                arcana.Ability = AbilityEnum.Intelligence;
                EditorUtility.SetDirty(arcana);

                var athletics = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Athletics, PathHelper.Abilities.SkillsPath);
                athletics.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Athletics}";
                athletics.Ability = AbilityEnum.Strength;
                EditorUtility.SetDirty(athletics);

                var deception = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Deception, PathHelper.Abilities.SkillsPath);
                deception.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Deception}";
                deception.Ability = AbilityEnum.Charisma;
                EditorUtility.SetDirty(deception);

                var history = Common.CreateScriptableObject<Skill>(NameHelper.Skills.History, PathHelper.Abilities.SkillsPath);
                history.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.History}";
                history.Ability = AbilityEnum.Intelligence;
                EditorUtility.SetDirty(history);

                var insight = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Insight, PathHelper.Abilities.SkillsPath);
                insight.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Insight}";
                insight.Ability = AbilityEnum.Wisdom;
                EditorUtility.SetDirty(insight);

                var intimidation = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Intimidation, PathHelper.Abilities.SkillsPath);
                intimidation.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Intimidation}";
                intimidation.Ability = AbilityEnum.Charisma;
                EditorUtility.SetDirty(intimidation);

                var investigation = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Investigation, PathHelper.Abilities.SkillsPath);
                investigation.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Investigation}";
                investigation.Ability = AbilityEnum.Intelligence;
                EditorUtility.SetDirty(investigation);

                var medicine = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Medicine, PathHelper.Abilities.SkillsPath);
                medicine.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Medicine}";
                medicine.Ability = AbilityEnum.Wisdom;
                EditorUtility.SetDirty(medicine);

                var nature = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Nature, PathHelper.Abilities.SkillsPath);
                nature.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Nature}";
                nature.Ability = AbilityEnum.Intelligence;
                EditorUtility.SetDirty(nature);

                var perception = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Perception, PathHelper.Abilities.SkillsPath);
                perception.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Perception}";
                perception.Ability = AbilityEnum.Wisdom;
                EditorUtility.SetDirty(perception);

                var performance = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Performance, PathHelper.Abilities.SkillsPath);
                performance.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Performance}";
                performance.Ability = AbilityEnum.Charisma;
                EditorUtility.SetDirty(performance);

                var persuasion = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Persuasion, PathHelper.Abilities.SkillsPath);
                persuasion.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Persuasion}";
                persuasion.Ability = AbilityEnum.Charisma;
                EditorUtility.SetDirty(persuasion);

                var religion = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Religion, PathHelper.Abilities.SkillsPath);
                religion.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Religion}";
                religion.Ability = AbilityEnum.Intelligence;
                EditorUtility.SetDirty(religion);

                var sleightOfHand = Common.CreateScriptableObject<Skill>(NameHelper.Skills.SleightOfHand, PathHelper.Abilities.SkillsPath);
                sleightOfHand.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.SleightOfHand}";
                sleightOfHand.Ability = AbilityEnum.Dexterity;
                EditorUtility.SetDirty(sleightOfHand);

                var stealth = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Stealth, PathHelper.Abilities.SkillsPath);
                stealth.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Stealth}";
                stealth.Ability = AbilityEnum.Dexterity;
                EditorUtility.SetDirty(stealth);

                var survival = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Survival, PathHelper.Abilities.SkillsPath);
                survival.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Survival}";
                survival.Ability = AbilityEnum.Wisdom;
                EditorUtility.SetDirty(survival);
                
                charisma.SkillList.AddRange(new []{ deception, intimidation, performance, persuasion });
            
                dexterity.SkillList.AddRange(new[] { acrobatics, sleightOfHand, stealth });
                intelligence.SkillList.AddRange(new[] { arcana, history, investigation, nature, religion });
                strength.SkillList.AddRange(new[] { athletics });
                wisdom.SkillList.AddRange(new[] { animalHandling, insight, medicine, perception, survival });

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
