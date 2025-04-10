using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Abilities;
using NUnit.Framework;
using UnityEditor;
using NameHelper = DnD.Code.Scripts.Helpers.NameHelper.NameHelper;

namespace Tests.DnD
{
    [TestFixture]
    public class SkillsUnitTests
    {
        private Skill[] _skills;
        
        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(Skill)}");
            _skills =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Skill>)
                .Where(asset => asset != null)
                .ToArray();
        }
        
        [TestCaseSource(typeof(AbilitiesData), nameof(AbilitiesData.DamageTypeTestCases))]
        public void TestAllSkills(SkillTestModel expected)
        {
            var skill = _skills.SingleOrDefault(d => d.name == expected.Name);
            Assert.That(skill, Is.Not.Null, Common.GetNotFoundLogInfo(NameHelper.Naming.Skills, expected.Name));
            Assert.That(skill.DisplayName, Is.EqualTo(expected.DisplayName), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayName), expected.DisplayName));
            Assert.That(skill.DisplayDescription, Is.EqualTo(expected.DisplayDescription), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.DisplayDescription), expected.DisplayDescription));
            Assert.That(skill.Ability.name, Is.EqualTo(expected.Ability), Common.GetUnexpectedValueLogInfo(expected.DisplayName, nameof(expected.Ability), expected.Ability));
        }
        
        private class AbilitiesData
        {
            public static IEnumerable DamageTypeTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new SkillTestModel()
                        {
                            Name = NameHelper.Skills.Acrobatics,
                            Ability = NameHelper.Abilities.Dexterity,
                        });
                    yield return new TestCaseData(
                        new SkillTestModel()
                        {
                            Name = NameHelper.Skills.AnimalHandling,
                            Ability = NameHelper.Abilities.Wisdom,
                        });
                    yield return new TestCaseData(
                        new SkillTestModel()
                        {
                            Name = NameHelper.Skills.Arcana,
                            Ability = NameHelper.Abilities.Intelligence,
                        });
                    yield return new TestCaseData(
                        new SkillTestModel()
                        {
                            Name = NameHelper.Skills.Athletics,
                            Ability = NameHelper.Abilities.Strength,
                        });
                    yield return new TestCaseData(
                        new SkillTestModel()
                        {
                            Name = NameHelper.Skills.Deception,
                            Ability = NameHelper.Abilities.Charisma,
                        });
                    yield return new TestCaseData(
                        new SkillTestModel()
                        {
                            Name = NameHelper.Skills.History,
                            Ability = NameHelper.Abilities.Intelligence,
                        });
                    yield return new TestCaseData(
                        new SkillTestModel()
                        {
                            Name = NameHelper.Skills.Insight,
                            Ability = NameHelper.Abilities.Wisdom,
                        });
                    yield return new TestCaseData(
                        new SkillTestModel()
                        {
                            Name = NameHelper.Skills.Intimidation,
                            Ability = NameHelper.Abilities.Charisma,
                        });
                    yield return new TestCaseData(
                        new SkillTestModel()
                        {
                            Name = NameHelper.Skills.Investigation,
                            Ability = NameHelper.Abilities.Intelligence,
                        });
                    yield return new TestCaseData(
                        new SkillTestModel()
                        {
                            Name = NameHelper.Skills.Medicine,
                            Ability = NameHelper.Abilities.Wisdom,
                        });
                    yield return new TestCaseData(
                        new SkillTestModel()
                        {
                            Name = NameHelper.Skills.Nature,
                            Ability = NameHelper.Abilities.Intelligence,
                        });
                    yield return new TestCaseData(
                        new SkillTestModel()
                        {
                            Name = NameHelper.Skills.Perception,
                            Ability = NameHelper.Abilities.Wisdom,
                        });
                    yield return new TestCaseData(
                        new SkillTestModel()
                        {
                            Name = NameHelper.Skills.Performance,
                            Ability = NameHelper.Abilities.Charisma,
                        });
                    yield return new TestCaseData(
                        new SkillTestModel()
                        {
                            Name = NameHelper.Skills.Persuasion,
                            Ability = NameHelper.Abilities.Charisma,
                        });
                    yield return new TestCaseData(
                        new SkillTestModel()
                        {
                            Name = NameHelper.Skills.Religion,
                            Ability = NameHelper.Abilities.Intelligence,
                        });
                    yield return new TestCaseData(
                        new SkillTestModel()
                        {
                            Name = NameHelper.Skills.SleightOfHand,
                            Ability = NameHelper.Abilities.Dexterity,
                        });
                    yield return new TestCaseData(
                        new SkillTestModel()
                        {
                            Name = NameHelper.Skills.Stealth,
                            Ability = NameHelper.Abilities.Dexterity,
                        });
                    yield return new TestCaseData(
                        new SkillTestModel()
                        {
                            Name = NameHelper.Skills.Survival,
                            Ability = NameHelper.Abilities.Wisdom,
                        });
                }
            }
        }
        
        public class SkillTestModel
        {
            public string Name { get; set; }
            public string DisplayName => $"{NameHelper.Naming.Skills}.{Name}";
            public string DisplayDescription => $"{DisplayName}.{NameHelper.Naming.Description}";
            public string Ability { get; set; }
            
        }
    }
}