using DnD.Code.Scripts.Characters.Abilities;
using DnD.Code.Scripts.Characters.Classes;
using DnD.Code.Scripts.Common;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;

namespace DnD.Editor.Initializer
{
    public static class AbilitiesInitializer
    {
        public static readonly string AbilitiesPath = $"{Common.FolderPath}/Abilities";
        public static readonly string SkillsPath = $"{Common.FolderPath}/Skills";

        public static Ability[] GetAllAbilities()
        {
            return Common.GetAllScriptableObjects<Ability>(AbilitiesPath);
        }
        
        public static Skill[] GetAllSkills()
        {
            return Common.GetAllScriptableObjects<Skill>(SkillsPath);
        }
        
        [MenuItem("D&D Game/Game Data Initializer/Initializers/Initialize Abilities Data")]
        public static void InitializeAbilities()
        {
            try
            {
                AssetDatabase.StartAssetEditing();
            
                Common.EnsureFolderExists(AbilitiesPath);
                Common.EnsureFolderExists(SkillsPath);

                // Create Abilities

                var charisma = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Charisma, AbilitiesPath);
                
                charisma.Name = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Charisma}";
                charisma.AbilityType = AbilityEnum.Charisma;

                var constitution = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Constitution, AbilitiesPath);
                constitution.Name = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Constitution}";
                constitution.AbilityType = AbilityEnum.Constitution;

                var dexterity = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Dexterity, AbilitiesPath);
                dexterity.Name = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Dexterity}";
                dexterity.AbilityType = AbilityEnum.Dexterity;

                var intelligence = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Intelligence, AbilitiesPath);
                intelligence.Name = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Intelligence}";
                intelligence.AbilityType = AbilityEnum.Intelligence;

                var strength = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Strength, AbilitiesPath);
                strength.Name = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Strength}";
                strength.AbilityType = AbilityEnum.Strength;

                var wisdom = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Wisdom, AbilitiesPath);
                wisdom.Name = $"{nameof(NameHelper.Abilities)}.{NameHelper.Abilities.Wisdom}";
                wisdom.AbilityType = AbilityEnum.Wisdom;

                // Create Skills

                var acrobatics = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Acrobatics, SkillsPath);
                acrobatics.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Acrobatics}";
                acrobatics.Ability = AbilityEnum.Dexterity;
            
                var animalHandling = Common.CreateScriptableObject<Skill>(NameHelper.Skills.AnimalHandling, SkillsPath);
                animalHandling.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.AnimalHandling}";
                animalHandling.Ability = AbilityEnum.Wisdom;

                var arcana = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Arcana, SkillsPath);
                arcana.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Arcana}";
                arcana.Ability = AbilityEnum.Intelligence;

                var athletics = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Athletics, SkillsPath);
                athletics.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Athletics}";
                athletics.Ability = AbilityEnum.Strength;

                var deception = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Deception, SkillsPath);
                deception.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Deception}";
                deception.Ability = AbilityEnum.Charisma;

                var history = Common.CreateScriptableObject<Skill>(NameHelper.Skills.History, SkillsPath);
                history.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.History}";
                history.Ability = AbilityEnum.Intelligence;

                var insight = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Insight, SkillsPath);
                insight.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Insight}";
                insight.Ability = AbilityEnum.Wisdom;

                var intimidation = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Intimidation, SkillsPath);
                intimidation.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Intimidation}";
                intimidation.Ability = AbilityEnum.Charisma;

                var investigation = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Investigation, SkillsPath);
                investigation.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Investigation}";
                investigation.Ability = AbilityEnum.Intelligence;

                var medicine = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Medicine, SkillsPath);
                medicine.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Medicine}";
                medicine.Ability = AbilityEnum.Wisdom;

                var nature = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Nature, SkillsPath);
                nature.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Nature}";
                nature.Ability = AbilityEnum.Intelligence;

                var perception = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Perception, SkillsPath);
                perception.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Perception}";
                perception.Ability = AbilityEnum.Wisdom;

                var performance = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Performance, SkillsPath);
                performance.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Performance}";
                performance.Ability = AbilityEnum.Charisma;

                var persuasion = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Persuasion, SkillsPath);
                persuasion.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Persuasion}";
                persuasion.Ability = AbilityEnum.Charisma;

                var religion = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Religion, SkillsPath);
                religion.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Religion}";
                religion.Ability = AbilityEnum.Intelligence;

                var sleightOfHand = Common.CreateScriptableObject<Skill>(NameHelper.Skills.SleightOfHand, SkillsPath);
                sleightOfHand.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.SleightOfHand}";
                sleightOfHand.Ability = AbilityEnum.Dexterity;

                var stealth = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Stealth, SkillsPath);
                stealth.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Stealth}";
                stealth.Ability = AbilityEnum.Dexterity;

                var survival = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Survival, SkillsPath);
                survival.Name = $"{nameof(NameHelper.Skills)}.{NameHelper.Skills.Survival}";
                survival.Ability = AbilityEnum.Wisdom;
                
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
