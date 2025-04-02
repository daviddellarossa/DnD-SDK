using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using DnD.Code.Scripts;
using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Characters;
using DnD.Code.Scripts.Helpers;
using DnD.Code.Scripts.Helpers.NameHelper;
using DnD.Code.Scripts.Helpers.PathHelper;
using Moq;
using NUnit.Framework;

namespace Tests.Character
{
    [TestFixture]
    public class CharacterStatsUnitTests
    {
        private Fixture _fixture;

        private Ability[] _abilities;
        private Skill[] _skills;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _fixture = new Fixture();
            

            SetupAbilitiesCollection();
            SetupSkillsCollection();

            
                
            void SetupAbilitiesCollection()
            {
                _abilities =
                    ScriptableObjectHelper.GetAllScriptableObjects<Ability>(PathHelper.Abilities.AbilitiesPath);
                if (_abilities is null || _abilities.Any() == false)
                {
                    Assert.Inconclusive("Abilities not found.");
                }
            }
            
            void SetupSkillsCollection()
            {
                _skills =
                    ScriptableObjectHelper.GetAllScriptableObjects<Skill>(PathHelper.Abilities.SkillsPath);
                if (_skills is null || _skills.Any() == false)
                {
                    Assert.Inconclusive("Skills not found.");
                }
            }
        }

        [TestCaseSource(typeof(CharacterStatsTestData), nameof(CharacterStatsTestData.PassivePerceptionTestData))]
        public int PassivePerception_Should_Return_CorrectValues(int wisdowScore, bool hasPerceptionProficiency)
        {
            var model = new CharacterStats();
            
            var skillProficiencies = new Dictionary<string, SkillStats>();

            if (hasPerceptionProficiency)
            {
                skillProficiencies[NameHelper.Skills.Perception] = new SkillStats()
                {
                    Skill = this._skills.Single(x => x.name == NameHelper.Skills.Perception),
                };
            }
            
            var abilityStat = new AbilityStats()
            {
                Score = wisdowScore,
                SkillProficiencies = skillProficiencies,
            };
            
            model.Abilities[NameHelper.Abilities.Wisdom] = abilityStat;

            return model.PassivePerception;
        }
        
        private AbilityStats GetValidAbilityStats(Ability ability)
            =>_fixture
                .Build<AbilityStats>()
                .With(x => x.Score,  _fixture.Create<int>() % 20 + 1)
                .With(x => x.Ability, ability)
                .Without(x => x.SkillProficiencies)
                .Create();
    }

    public class CharacterStatsTestData
    {
        public static IEnumerable PassivePerceptionTestData
        {
            get
            {
                yield return new TestCaseData(2, false).Returns(Constants.BasePassivePerception - 4);
                yield return new TestCaseData(4, false).Returns(Constants.BasePassivePerception - 3);
                yield return new TestCaseData(6, false).Returns(Constants.BasePassivePerception - 2);
                yield return new TestCaseData(8, false).Returns(Constants.BasePassivePerception - 1);
                yield return new TestCaseData(10, false).Returns(Constants.BasePassivePerception);
                yield return new TestCaseData(11, false).Returns(Constants.BasePassivePerception);
                yield return new TestCaseData(12, false).Returns(Constants.BasePassivePerception + 1);
                yield return new TestCaseData(13, false).Returns(Constants.BasePassivePerception + 1);
                yield return new TestCaseData(14, false).Returns(Constants.BasePassivePerception + 2);
                yield return new TestCaseData(16, false).Returns(Constants.BasePassivePerception + 3);
                yield return new TestCaseData(18, false).Returns(Constants.BasePassivePerception + 4);
                yield return new TestCaseData(20, false).Returns(Constants.BasePassivePerception + 5);
                
                yield return new TestCaseData(2, true).Returns(Constants.BasePassivePerception - 2);
                yield return new TestCaseData(4, true).Returns(Constants.BasePassivePerception - 1);
                yield return new TestCaseData(6, true).Returns(Constants.BasePassivePerception);
                yield return new TestCaseData(8, true).Returns(Constants.BasePassivePerception + 1);
                yield return new TestCaseData(10, true).Returns(Constants.BasePassivePerception + 2);
                yield return new TestCaseData(11, true).Returns(Constants.BasePassivePerception + 2);
                yield return new TestCaseData(12, true).Returns(Constants.BasePassivePerception + 3);
                yield return new TestCaseData(13, true).Returns(Constants.BasePassivePerception + 3);
                yield return new TestCaseData(14, true).Returns(Constants.BasePassivePerception + 4);
                yield return new TestCaseData(16, true).Returns(Constants.BasePassivePerception + 5);
                yield return new TestCaseData(18, true).Returns(Constants.BasePassivePerception + 6);
                yield return new TestCaseData(20, true).Returns(Constants.BasePassivePerception + 7);

            }
        }
    }
}