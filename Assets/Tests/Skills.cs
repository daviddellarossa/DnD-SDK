using System;
using System.Collections;
using System.Linq;
using DnD.Code.Scripts;
using DnD.Code.Scripts.Characters.Abilities;
using NUnit.Framework;
using UnityEditor;

namespace Tests
{
    [TestFixture]
    public class Skills
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
        public void TestAllSkills(SkillModel expected)
        {
            var skill = _skills.SingleOrDefault(d => d.name == expected.Name);
            
            Assert.That(skill, Is.Not.Null);
            Assert.That(skill.Ability, Is.EqualTo(expected.Ability));
        }
        
        private class AbilitiesData
        {
            public static IEnumerable DamageTypeTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new SkillModel(
                            "Acrobatics",
                            AbilityEnum.Dexterity
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            "AnimalHandling",
                            AbilityEnum.Wisdom
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            "Arcana",
                            AbilityEnum.Intelligence
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            "Athletics",
                            AbilityEnum.Strength
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            "Deception",
                            AbilityEnum.Charisma
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            "History",
                            AbilityEnum.Intelligence
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            "Insight",
                            AbilityEnum.Wisdom
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            "Intimidation",
                            AbilityEnum.Charisma
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            "Investigation",
                            AbilityEnum.Intelligence
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            "Medicine",
                            AbilityEnum.Wisdom
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            "Nature",
                            AbilityEnum.Intelligence
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            "Perception",
                            AbilityEnum.Wisdom
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            "Performance",
                            AbilityEnum.Charisma
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            "Persuasion",
                            AbilityEnum.Charisma
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            "Religion",
                            AbilityEnum.Intelligence
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            "SleightOfHand",
                            AbilityEnum.Dexterity
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            "Stealth",
                            AbilityEnum.Dexterity
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            "Survival",
                            AbilityEnum.Wisdom
                        ));
                }
            }
        }
        
        public class SkillModel
        {
            public string Name { get; set; }
            public AbilityEnum Ability { get; set; }

            public SkillModel(string name,  AbilityEnum ability)
            {
                this.Name = name;
                this.Ability = ability;
            }
        }
    }
}