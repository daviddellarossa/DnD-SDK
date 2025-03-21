using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Characters.Abilities;
using DnD.Code.Scripts.Common;
using NUnit.Framework;
using UnityEditor;

namespace Tests
{
    public class Abilities
    {
        private Ability[] _abilities;
        
        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(Ability)}");
            _abilities =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Ability>)
                .Where(asset => asset != null)
                .ToArray();
        }

        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.TestCases))]
        public void TestAllAbilities(string name, AbilityEnum abilityEnum, string[] skillNames)
        {
            var ability = _abilities.SingleOrDefault(ability => ability.name == name && ability.AbilityType == abilityEnum);
            Assert.That(ability, Is.Not.Null,  $"Ability {name} not found");
            
            Assert.That(ability.SkillList.Count, Is.EqualTo(skillNames.Length));
            
            foreach (var skillName in skillNames)
            {
                Assert.IsTrue(ability.SkillList.Any(skill => skill.name == skillName) , $"{ability.name}: Skill {skillName} not found");
            }
        }

        private class AbilitiesData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(
                        NameHelper.Abilities.Charisma,
                        AbilityEnum.Charisma,
                        new string[]
                        {
                            NameHelper.Skills.Deception,
                            NameHelper.Skills.Intimidation,
                            NameHelper.Skills.Performance,
                            NameHelper.Skills.Persuasion,
                        });
                    yield return new TestCaseData(
                        NameHelper.Abilities.Constitution,
                        AbilityEnum.Constitution,
                        new string[] {}
                        );
                    yield return new TestCaseData(
                        NameHelper.Abilities.Dexterity,
                        AbilityEnum.Dexterity,
                        new string[]
                        {
                            NameHelper.Skills.Acrobatics,
                            NameHelper.Skills.SleightOfHand,
                            NameHelper.Skills.Stealth,
                        });
                    yield return new TestCaseData(
                        NameHelper.Abilities.Intelligence,
                        AbilityEnum.Intelligence,
                        new string[]
                        {
                            NameHelper.Skills.Arcana,
                            NameHelper.Skills.History,
                            NameHelper.Skills.Investigation,
                            NameHelper.Skills.Nature,
                            NameHelper.Skills.Religion,
                        });
                    yield return new TestCaseData(
                        NameHelper.Abilities.Strength,
                        AbilityEnum.Strength,
                        new string[]
                        {
                            NameHelper.Skills.Athletics,
                        });
                    yield return new TestCaseData(
                        NameHelper.Abilities.Wisdom,
                        AbilityEnum.Wisdom,
                        new string[]
                        {
                            NameHelper.Skills.AnimalHandling,
                            NameHelper.Skills.Insight,
                            NameHelper.Skills.Medicine,
                            NameHelper.Skills.Perception,
                            NameHelper.Skills.Survival,
                        });
                }
            }
        }
    }
}