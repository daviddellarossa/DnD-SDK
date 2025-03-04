using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Characters.Abilities;
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
            Assert.IsNotNull(ability);
            
            Assert.AreEqual(ability.SkillList.Count, skillNames.Length);

            foreach (var skillName in skillNames)
            {
                Assert.IsTrue(ability.SkillList.Any(skill => skill.name == skillName));
            }
        }

        private class AbilitiesData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(
                        Helper.Abilities.Charisma,
                        AbilityEnum.Charisma,
                        new string[]
                        {
                            Helper.Skills.Deception,
                            Helper.Skills.Intimidation,
                            Helper.Skills.Performance,
                            Helper.Skills.Persuasion,
                        });
                    yield return new TestCaseData(
                        Helper.Abilities.Constitution,
                        AbilityEnum.Constitution,
                        new string[] {}
                        );
                    yield return new TestCaseData(
                        Helper.Abilities.Dexterity,
                        AbilityEnum.Dexterity,
                        new string[]
                        {
                            Helper.Skills.Acrobatics,
                            Helper.Skills.SleightOfHand,
                            Helper.Skills.Stealth,
                        });
                    yield return new TestCaseData(
                        Helper.Abilities.Intelligence,
                        AbilityEnum.Intelligence,
                        new string[]
                        {
                            Helper.Skills.Arcana,
                            Helper.Skills.History,
                            Helper.Skills.Investigation,
                            Helper.Skills.Nature,
                            Helper.Skills.Religion,
                        });
                    yield return new TestCaseData(
                        Helper.Abilities.Strength,
                        AbilityEnum.Strength,
                        new string[]
                        {
                            Helper.Skills.Athletics,
                        });
                    yield return new TestCaseData(
                        Helper.Abilities.Wisdom,
                        AbilityEnum.Wisdom,
                        new string[]
                        {
                            Helper.Skills.AnimalHandling,
                            Helper.Skills.Insight,
                            Helper.Skills.Medicine,
                            Helper.Skills.Perception,
                            Helper.Skills.Survival,
                        });
                }
            }
        }
    }
}