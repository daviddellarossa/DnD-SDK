using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using DnD.Code.Scripts;
using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Characters;
using DnD.Code.Scripts.Classes;
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
        private Class[] _classes;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _fixture = new Fixture();
            

            SetupAbilitiesCollection();
            SetupSkillsCollection();
            SetupClassCollection();
            
                
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
            
            void SetupClassCollection()
            {
                _classes = ScriptableObjectHelper.GetAllScriptableObjects<Class>(PathHelper.Classes.ClassesPath);
                if (_classes is null || _classes.Any() == false)
                {
                    Assert.Inconclusive("Class not found.");
                }
            }
        }

        [TestCaseSource(typeof(CharacterStatsTestData), nameof(CharacterStatsTestData.PassivePerceptionTestData))]
        public int PassivePerception_Should_Return_CorrectValues(int wisdomScore, bool hasPerceptionProficiency)
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
                Score = wisdomScore,
                SkillProficiencies = skillProficiencies,
            };
            
            model.Abilities[NameHelper.Abilities.Wisdom] = abilityStat;

            return model.PassivePerception;
        }

        [TestCaseSource(typeof(CharacterStatsTestData), nameof(CharacterStatsTestData.MaxHitPointsTestData))]
        public int MaxHitPoints_Should_Return_CorrectValues(Class @class, int level, int constitutionScore)
        {
            var model = new CharacterStatsTestModel();
            
            var abilityStat = new AbilityStats()
            {
                Ability =  this._abilities.Single(x => x.name == NameHelper.Abilities.Constitution),
                Score = constitutionScore,
            };
            
            model.Abilities[NameHelper.Abilities.Constitution] = abilityStat;
            
            model.SetLevel(level);

            model.SetClass(@class);
            
            return model.MaxHitPoints;
        }

        [TestCaseSource(typeof(CharacterStatsTestData), nameof(CharacterStatsTestData.InitiativeTestData))]
        public void Initiative_Should_Equal_DexterityModifier(int dexterityScore)
        {
            var model = new CharacterStats();
            
            var abilityStat = new AbilityStats()
            {
                Ability =  this._abilities.Single(x => x.name == NameHelper.Abilities.Dexterity),
                Score = dexterityScore,
            };
            
            model.Abilities[NameHelper.Abilities.Dexterity] = abilityStat;
            
            Assert.That(model.Initiative, Is.EqualTo(abilityStat.Modifier));
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

        public static IEnumerable MaxHitPointsTestData
        {
            get
            {
                var classes = ScriptableObjectHelper.GetAllScriptableObjects<Class>(PathHelper.Classes.ClassesPath);
                
                yield return new TestCaseData(
                    classes.Single(x => x.name == NameHelper.Classes.Barbarian),
                    1,
                    10).Returns(12);
                
                yield return new TestCaseData(
                    classes.Single(x => x.name == NameHelper.Classes.Barbarian),
                    2,
                    10).Returns(24);
                
                yield return new TestCaseData(
                    classes.Single(x => x.name == NameHelper.Classes.Barbarian),
                    3,
                    10).Returns(36);
                
                yield return new TestCaseData(
                    classes.Single(x => x.name == NameHelper.Classes.Barbarian),
                    4,
                    10).Returns(48);
                
                yield return new TestCaseData(
                    classes.Single(x => x.name == NameHelper.Classes.Barbarian),
                    5,
                    10).Returns(60);
                
                yield return new TestCaseData(
                    classes.Single(x => x.name == NameHelper.Classes.Barbarian),
                    6,
                    10).Returns(72);
                
                yield return new TestCaseData(
                    classes.Single(x => x.name == NameHelper.Classes.Barbarian),
                    7,
                    10).Returns(84);
                
                yield return new TestCaseData(
                    classes.Single(x => x.name == NameHelper.Classes.Barbarian),
                    8,
                    10).Returns(96);
                
                yield return new TestCaseData(
                    classes.Single(x => x.name == NameHelper.Classes.Barbarian),
                    9,
                    10).Returns(108);
                
                
                yield return new TestCaseData(
                    classes.Single(x => x.name == NameHelper.Classes.Barbarian),
                    10,
                    11).Returns(120);
                
                yield return new TestCaseData(
                    classes.Single(x => x.name == NameHelper.Classes.Barbarian),
                    11,
                    12).Returns(133);
                
                yield return new TestCaseData(
                    classes.Single(x => x.name == NameHelper.Classes.Barbarian),
                    12,
                    13).Returns(145);
                
                yield return new TestCaseData(
                    classes.Single(x => x.name == NameHelper.Classes.Barbarian),
                    13,
                    14).Returns(158);
                
                yield return new TestCaseData(
                    classes.Single(x => x.name == NameHelper.Classes.Barbarian),
                    14,
                    15).Returns(170);
                
                yield return new TestCaseData(
                    classes.Single(x => x.name == NameHelper.Classes.Barbarian),
                    15,
                    16).Returns(183);
                
                yield return new TestCaseData(
                    classes.Single(x => x.name == NameHelper.Classes.Barbarian),
                    16,
                    17).Returns(195);
                
                yield return new TestCaseData(
                    classes.Single(x => x.name == NameHelper.Classes.Barbarian),
                    17,
                    18).Returns(208);
                
                yield return new TestCaseData(
                    classes.Single(x => x.name == NameHelper.Classes.Barbarian),
                    18,
                    19).Returns(220);
            }
        }

        public static IEnumerable InitiativeTestData
        {
            get
            {
                yield return new TestCaseData(5);
                yield return new TestCaseData(6);
                yield return new TestCaseData(8);
                yield return new TestCaseData(9);
                yield return new TestCaseData(10);
                yield return new TestCaseData(11);
                yield return new TestCaseData(12);
                yield return new TestCaseData(14);
                yield return new TestCaseData(16);
                yield return new TestCaseData(18);
                yield return new TestCaseData(20);

            }
        }
    }

    public class CharacterStatsTestModel : CharacterStats
    {
        public void SetLevel(int level)
        {
            base.Level = level;
        }

        public void SetClass(Class @class)
        {
            base.Class = @class;
        }
    }
}