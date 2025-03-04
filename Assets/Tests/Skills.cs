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
                            Helper.Skills.Acrobatics,
                            AbilityEnum.Dexterity
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            Helper.Skills.AnimalHandling,
                            AbilityEnum.Wisdom
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            Helper.Skills.Arcana,
                            AbilityEnum.Intelligence
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            Helper.Skills.Athletics,
                            AbilityEnum.Strength
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            Helper.Skills.Deception,
                            AbilityEnum.Charisma
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            Helper.Skills.History,
                            AbilityEnum.Intelligence
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            Helper.Skills.Insight,
                            AbilityEnum.Wisdom
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            Helper.Skills.Intimidation,
                            AbilityEnum.Charisma
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            Helper.Skills.Investigation,
                            AbilityEnum.Intelligence
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            Helper.Skills.Medicine,
                            AbilityEnum.Wisdom
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            Helper.Skills.Nature,
                            AbilityEnum.Intelligence
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            Helper.Skills.Perception,
                            AbilityEnum.Wisdom
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            Helper.Skills.Performance,
                            AbilityEnum.Charisma
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            Helper.Skills.Persuasion,
                            AbilityEnum.Charisma
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            Helper.Skills.Religion,
                            AbilityEnum.Intelligence
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            Helper.Skills.SleightOfHand,
                            AbilityEnum.Dexterity
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            Helper.Skills.Stealth,
                            AbilityEnum.Dexterity
                        ));
                    yield return new TestCaseData(
                        new SkillModel(
                            Helper.Skills.Survival,
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