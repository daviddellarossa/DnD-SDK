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

        [MenuItem("D&D Game/Game Data Initializer/Generate Abilities Data")]
        public static void InitializeAbilities()
        {
            try
            {
                AssetDatabase.StartAssetEditing();
            
                Common.EnsureFolderExists(AbilitiesPath);
                Common.EnsureFolderExists(SkillsPath);

                // Create Abilities

                var charisma = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Charisma, AbilitiesPath);
                charisma.Name = NameHelper.Abilities.Charisma;
                charisma.AbilityType = AbilityEnum.Charisma;

                var constitution = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Constitution, AbilitiesPath);
                constitution.Name = NameHelper.Abilities.Constitution;
                constitution.AbilityType = AbilityEnum.Constitution;

                var dexterity = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Dexterity, AbilitiesPath);
                dexterity.Name = NameHelper.Abilities.Dexterity;
                dexterity.AbilityType = AbilityEnum.Dexterity;

                var intelligence = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Intelligence, AbilitiesPath);
                intelligence.Name = NameHelper.Abilities.Intelligence;
                intelligence.AbilityType = AbilityEnum.Intelligence;

                var strength = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Strength, AbilitiesPath);
                strength.Name = NameHelper.Abilities.Strength;
                strength.AbilityType = AbilityEnum.Strength;

                var wisdom = Common.CreateScriptableObject<Ability>(NameHelper.Abilities.Wisdom, AbilitiesPath);
                wisdom.Name = NameHelper.Abilities.Wisdom;
                wisdom.AbilityType = AbilityEnum.Wisdom;

                // Create Skills

                var acrobatics = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Acrobatics, SkillsPath);
                acrobatics.Name = NameHelper.Skills.Acrobatics;
                acrobatics.Ability = AbilityEnum.Dexterity;
            
                var animalHandling = Common.CreateScriptableObject<Skill>(NameHelper.Skills.AnimalHandling, SkillsPath);
                animalHandling.Name = NameHelper.Skills.AnimalHandling;
                animalHandling.Ability = AbilityEnum.Wisdom;

                var arcana = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Arcana, SkillsPath);
                arcana.Name = NameHelper.Skills.Arcana;
                arcana.Ability = AbilityEnum.Intelligence;

                var athletics = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Athletics, SkillsPath);
                athletics.Name = NameHelper.Skills.Athletics;
                athletics.Ability = AbilityEnum.Strength;

                var deception = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Deception, SkillsPath);
                deception.Name = NameHelper.Skills.Deception;
                deception.Ability = AbilityEnum.Charisma;

                var history = Common.CreateScriptableObject<Skill>(NameHelper.Skills.History, SkillsPath);
                history.Name = NameHelper.Skills.History;
                history.Ability = AbilityEnum.Intelligence;

                var insight = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Insight, SkillsPath);
                insight.Name = NameHelper.Skills.Insight;
                insight.Ability = AbilityEnum.Wisdom;

                var intimidation = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Intimidation, SkillsPath);
                intimidation.Name = NameHelper.Skills.Intimidation;
                intimidation.Ability = AbilityEnum.Charisma;

                var investigation = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Investigation, SkillsPath);
                investigation.Name = NameHelper.Skills.Investigation;
                investigation.Ability = AbilityEnum.Intelligence;

                var medicine = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Medicine, SkillsPath);
                medicine.Name = NameHelper.Skills.Medicine;
                medicine.Ability = AbilityEnum.Wisdom;

                var nature = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Nature, SkillsPath);
                nature.Name = NameHelper.Skills.Nature;
                nature.Ability = AbilityEnum.Intelligence;

                var perception = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Perception, SkillsPath);
                perception.Name = NameHelper.Skills.Perception;
                perception.Ability = AbilityEnum.Wisdom;

                var performance = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Performance, SkillsPath);
                performance.Name = NameHelper.Skills.Performance;
                performance.Ability = AbilityEnum.Charisma;

                var persuasion = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Persuasion, SkillsPath);
                persuasion.Name = NameHelper.Skills.Persuasion;
                persuasion.Ability = AbilityEnum.Charisma;

                var religion = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Religion, SkillsPath);
                religion.Name = NameHelper.Skills.Religion;
                religion.Ability = AbilityEnum.Intelligence;

                var sleightOfHand = Common.CreateScriptableObject<Skill>(NameHelper.Skills.SleightOfHand, SkillsPath);
                sleightOfHand.Name = NameHelper.Skills.SleightOfHand;
                sleightOfHand.Ability = AbilityEnum.Dexterity;

                var stealth = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Stealth, SkillsPath);
                stealth.Name = NameHelper.Skills.Stealth;
                stealth.Ability = AbilityEnum.Dexterity;

                var survival = Common.CreateScriptableObject<Skill>(NameHelper.Skills.Survival, SkillsPath);
                survival.Name = NameHelper.Skills.Survival;
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
