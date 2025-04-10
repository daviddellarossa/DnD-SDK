using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Abilities;
using NUnit.Framework;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace Tests.DnD
{
    [TestFixture]
    public class AbilitiesUnitTests
    {
        private Ability[] _abilities;
        
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(Ability)}");
            _abilities =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Ability>)
                .Where(asset => asset != null)
                .ToArray();
        }

        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.TestCases))]
        public void TestAllAbilities(AbilityTestModel expected)
        {
            var ability = _abilities.SingleOrDefault(ability => ability.name == expected.Name );
            Assert.That(ability, Is.Not.Null,  $"Ability {expected.Name} not found");
            Assert.That(ability.DisplayName, Is.EqualTo(expected.DisplayName),  $"{expected.DisplayName}: {nameof(expected.DisplayName)} not equal to {expected.DisplayName}.");
            Assert.That(ability.DisplayDescription, Is.EqualTo(expected.DisplayDescription),  $"{expected.DisplayName}: {nameof(expected.DisplayDescription)} not equal to {expected.DisplayDescription}.");

            Assert.That(ability.SkillList.Count, Is.EqualTo(expected.Skills.Length));
            Assert.That(ability.SkillList.Select(skill => skill.name), Is.EquivalentTo(expected.Skills), $"{expected.DisplayName}: {nameof(expected.Skills)} not equal to {expected.Skills}.");
        }

        private class AbilitiesData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new AbilityTestModel()
                        {
                            Name = NameHelper.Abilities.Charisma,
                            Skills = new[]
                            {
                                NameHelper.Skills.Deception,
                                NameHelper.Skills.Intimidation,
                                NameHelper.Skills.Performance,
                                NameHelper.Skills.Persuasion,
                            }
                        });
                    yield return new TestCaseData(
                        new AbilityTestModel()
                        {
                            Name = NameHelper.Abilities.Constitution,
                            Skills = new string[] { }
                        });
                    yield return new TestCaseData(
                        new AbilityTestModel()
                        {
                            Name = NameHelper.Abilities.Dexterity,
                            Skills = new[]
                            {
                                NameHelper.Skills.Acrobatics,
                                NameHelper.Skills.SleightOfHand,
                                NameHelper.Skills.Stealth,
                            }
                        });
                    yield return new TestCaseData(
                        new AbilityTestModel()
                        {
                            Name = NameHelper.Abilities.Intelligence,
                            Skills = new[]
                            {
                                NameHelper.Skills.Arcana,
                                NameHelper.Skills.History,
                                NameHelper.Skills.Investigation,
                                NameHelper.Skills.Nature,
                                NameHelper.Skills.Religion,
                            }
                        });
                    yield return new TestCaseData(
                        new AbilityTestModel()
                        {
                            Name = NameHelper.Abilities.Strength,
                            Skills = new[]
                            {
                                NameHelper.Skills.Athletics,
                            }
                        });
                    yield return new TestCaseData(
                        new AbilityTestModel()
                        {
                            Name = NameHelper.Abilities.Wisdom,
                            Skills = new[]
                            {
                                NameHelper.Skills.AnimalHandling,
                                NameHelper.Skills.Insight,
                                NameHelper.Skills.Medicine,
                                NameHelper.Skills.Perception,
                                NameHelper.Skills.Survival,
                            }
                        });
                }
            }
        }
        
        public class AbilityTestModel
        {
            public string Name { get; set; }
            public string DisplayName => $"{NameHelper.Naming.Abilities}.{Name}";
            public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
            public string[] Skills;
        }
    }
}