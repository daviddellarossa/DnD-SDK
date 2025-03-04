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
                        "Charisma",
                        AbilityEnum.Charisma,
                        new string[]
                        {
                            "Deception",
                            "Intimidation",
                            "Performance",
                            "Persuasion",
                        });
                    yield return new TestCaseData(
                        "Constitution",
                        AbilityEnum.Constitution,
                        new string[] {}
                        );
                    yield return new TestCaseData(
                        "Dexterity",
                        AbilityEnum.Dexterity,
                        new string[]
                        {
                            "Acrobatics",
                            "SleightOfHand",
                            "Stealth",
                        });
                    yield return new TestCaseData(
                        "Intelligence",
                        AbilityEnum.Intelligence,
                        new string[]
                        {
                            "Arcana",
                            "History",
                            "Investigation",
                            "Nature",
                            "Religion",
                        });
                    yield return new TestCaseData(
                        "Strength",
                        AbilityEnum.Strength,
                        new string[]
                        {
                            "Athletics",
                        });
                    yield return new TestCaseData(
                        "Wisdom",
                        AbilityEnum.Wisdom,
                        new string[]
                        {
                            "AnimalHandling",
                            "Insight",
                            "Medicine",
                            "Perception",
                            "Survival",
                        });
                }
            }
        }
    }
}