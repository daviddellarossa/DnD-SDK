using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.NUnit3;
using DnD.Code.Scripts.Abilities;
using DnD.Code.Scripts.Backgrounds;
using DnD.Code.Scripts.Characters;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Helpers.PathHelper;
using DnD.Code.Scripts.Helpers;
using DnD.Code.Scripts.Helpers.NameHelper;
using DnD.Code.Scripts.Languages;
using DnD.Code.Scripts.Species;
using Moq;
using NUnit.Framework;

namespace Tests.Character
{
    [TestFixture]
    public class Builder
    {
        private CharacterStats.Builder _model;
        private CharacterStats _instance;
        private Fixture _fixture;
        
        private Class[] _classes;
        private Background[] _backgrounds;
        private Spex[] _species;
        private Ability[] _abilities;
        private Skill[] _skills;
        private StandardLanguage[] _standardLanguages;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _fixture = new Fixture();
            
            SetupClassCollection();
            SetupBackgroundCollection();
            SetupSpeciesCollection();
            SetupAbilitiesCollection();
            SetupSkillsCollection();
            SetupStandardLanguagesCollection();
            
            void SetupClassCollection()
            {
                _classes = ScriptableObjectHelper.GetAllScriptableObjects<Class>(PathHelper.Classes.ClassesPath);
                if (_classes is null || _classes.Any() == false)
                {
                    Assert.Inconclusive("Class not found.");
                }
            }

            void SetupBackgroundCollection()
            {
                _backgrounds =
                    ScriptableObjectHelper.GetAllScriptableObjects<Background>(PathHelper.Backgrounds.BackgroundsPath);
                if (_backgrounds is null || _backgrounds.Any() == false)
                {
                    Assert.Inconclusive("Background not found.");
                }
            }
            
            void SetupSpeciesCollection()
            {
                _species =
                    ScriptableObjectHelper.GetAllScriptableObjects<Spex>(PathHelper.Species.SpeciesPath);
                if (_species is null || _species.Any() == false)
                {
                    Assert.Inconclusive("Species not found.");
                }
            }
            
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
            
            void SetupStandardLanguagesCollection()
            {
                _standardLanguages =
                    ScriptableObjectHelper.GetAllScriptableObjects<StandardLanguage>(PathHelper.Languages.StandardLanguagesPath);
                if (_standardLanguages is null || _standardLanguages.Any() == false)
                {
                    Assert.Inconclusive("Standard Languages not found.");
                }
            }
        }

        
        [SetUp]
        public void Setup()
        {
            _model = new CharacterStats.Builder();
        }

        [TearDown]
        public void TearDown()
        {
            if (_instance != null)
            {
                // var filePath = AssetDatabase.GetAssetPath(_instance);
                // Object.DestroyImmediate(_instance, true);
                // AssetDatabase.DeleteAsset(filePath);
                // AssetDatabase.Refresh();
            }
        }
        
        [Test, AutoData]
        public void CheckName_Should_Return_True_When_Name_Assigned(string name)
        {
            _model.SetName(name);
            Assert.That(_model.CheckName(), Is.True);
        }
        
        [Test]
        public void CheckName_Should_Return_False_When_Name_Not_Assigned()
        {
            Assert.That(_model.CheckName(), Is.False);
        }
        
        [Test]
        public void CheckClass_Should_Return_True_When_Class_Assigned()
        {
            _model.SetClass(_classes.First());
            Assert.That(_model.CheckClass(), Is.True);
        }
        
        [Test]
        public void CheckClass_Should_Return_False_When_Class_Not_Assigned()
        {
            Assert.That(_model.CheckClass(), Is.False);
        }
        
        [Test]
        [Ignore("At the moment there is no class with no subclasses")]
        public void CheckSubClass_Should_Return_True_When_Class_HasNo_SubClass()
        {
            var @class = _classes.FirstOrDefault(x => x.SubClasses.Count == 0);
            if (@class is null)
            {
                Assert.Inconclusive("Class with no Subclasses not found.");
            }
            
            _model.SetClass(@class);
            Assert.That(_model.CheckSubClass(), Is.True);
        }

        [Test]
        public void CheckSubClass_Should_Return_False_When_Class_Has_SubClasses_And_No_SubClass_Is_Assigned()
        {
            var @class = _classes.FirstOrDefault(x => x.SubClasses.Count > 0);
            if (@class is null)
            {
                Assert.Inconclusive("Class with no Subclasses not found.");
            }
            
            _model.SetClass(@class);
            
            Assert.That(_model.CheckSubClass(), Is.False);
        }
        
        [Test]
        [Ignore("This test needs two classes with subclasses available. At the moment only one class is available.")]
        public void CheckSubClass_Should_Return_False_When_Class_Has_SubClasses_And_Wrong_SubClass_Is_Assigned()
        {
            var classAssigned = _classes.FirstOrDefault(x => x.SubClasses.Count > 0);
            if (classAssigned is null)
            {
                Assert.Inconclusive("Class with no Subclasses not found.");
            }
            
            _model.SetClass(@classAssigned);
            
            var otherClass = _classes.Skip(1).FirstOrDefault(x => x.SubClasses.Count > 0);
            if (otherClass is null)
            {
                Assert.Inconclusive("Class with no Subclasses not found.");
            }
            
            _model.SetSubClass(otherClass.SubClasses.First());
            
            Assert.That(_model.CheckSubClass(), Is.False);
        }

        [Test]
        public void CheckSubClass_Should_Return_True_When_Class_Has_SubClasses_And_SubClass_Is_Assigned()
        {
            var classAssigned = _classes.FirstOrDefault(x => x.SubClasses.Count > 0);
            if (classAssigned is null)
            {
                Assert.Inconclusive("Class with no Subclasses not found.");
            }
            
            _model.SetClass(@classAssigned);
            
            _model.SetSubClass(classAssigned.SubClasses.First());
            
            Assert.That(_model.CheckSubClass(), Is.True);
        }
        
        [Test]
        public void CheckBackground_Should_Return_True_When_Background_Assigned()
        {
            var background = _backgrounds.FirstOrDefault();
            if (background is null)
            {
                Assert.Inconclusive("Background not found.");
            }
            
            _model.SetBackground(background);
            Assert.That(_model.CheckBackground(), Is.True);
        }
        
        [Test]
        public void CheckBackground_Should_Return_False_When_Background_Not_Assigned()
        {
            Assert.That(_model.CheckBackground(), Is.False);
        }
        
        [Test]
        public void CheckSpex_Should_Return_True_When_Spex_Assigned()
        {
            var spex = _species.FirstOrDefault();
            if (spex is null)
            {
                Assert.Inconclusive("Spex not found.");
            }
            
            _model.SetSpex(spex);
            Assert.That(_model.CheckSpex(), Is.True);
        }
        
        [Test]
        public void CheckSpex_Should_Return_False_When_Spex_Not_Assigned()
        {
            Assert.That(_model.CheckSpex(), Is.False);
        }
        
        [Test]
        public void CheckAbilityStats_Should_Return_True_When_AbilityStats_Assigned()
        {
            foreach (var ability in _abilities)
            {
                _model.SetAbilityStats(this.GetValidAbilityStats(ability));
            }
            
            Assert.That(_model.CheckAbilityStats(),  Is.True);
        }
        
        [TestCaseSource(typeof(CharacterStatsBuilderTestData), nameof(CharacterStatsBuilderTestData.AbilityScoresMinusOneTestCases))]
        public void CheckAbilityStats_Should_Return_False_When_Any_AbilityStats_IsNot_Assigned(IEnumerable<Ability> abilities, Ability excludedAbility)
        {
            foreach (var ability in abilities)
            {
                _model.SetAbilityStats(this.GetValidAbilityStats(ability));
            }
            
            Assert.That(_model.CheckAbilityStats(), Is.False);
        }
        
        [TestCaseSource(typeof(CharacterStatsBuilderTestData), nameof(CharacterStatsBuilderTestData.AbilityScoresMinusOneTestCases))]
        public void CheckAbilityStats_Should_Return_False_When_Any_AbilityStats_Has_Score_0(IEnumerable<Ability> abilities, Ability excludedAbility)
        {
            foreach (var ability in abilities)
            {
                _model.SetAbilityStats(this.GetValidAbilityStats(ability));
            }

            _model.SetAbilityStats(_fixture
                .Build<AbilityStats>()
                .With(x => x.Score, 0)
                .With(x => x.Ability, excludedAbility)
                .Without(x => x.SkillProficiencies)
                .Create());
            
            Assert.That(_model.CheckAbilityStats(), Is.False);
        }

        [Test]
        public void CheckSkillProficiencies_Should_Return_False_When_Number_DoesNot_Match()
        {
            var @class = _classes.First();
            
            _model.SetClass(@class);
            
            var skills = @class.SkillProficienciesAvailable.Take(@class.NumberOfSkillProficienciesToChoose + 1).ToArray();
            
            _model.SetSkillProficienciesFromClass(skills);
            
            Assert.That(_model.CheckSkillProficienciesFromClass(), Is.False);
        }
        [Test]
        public void CheckSkillProficiencies_Should_Return_True_When_Skills_From_Class_Are_Assigned()
        {
            var @class = _classes.First();
            
            _model.SetClass(@class);
            
            _model.SetSkillProficienciesFromClass(@class.SkillProficienciesAvailable.Take(2).ToArray());
            
            Assert.That(_model.CheckSkillProficienciesFromClass(), Is.True);
        }
        
        [Test]
        public void CheckSkillProficiencies_Should_Return_False_When_Skills_Not_From_Class_Are_Assigned()
        {
            var @class = _classes.First();
            
            _model.SetClass(@class);
            
            var skills = _skills.Except(@class.SkillProficienciesAvailable).Take(2).ToArray();
            
            _model.SetSkillProficienciesFromClass(skills);
            
            Assert.That(_model.CheckSkillProficienciesFromClass(), Is.False);
        }
        [Test]
        public void CheckSkillProficiencies_Should_Return_False_When_Mixed_Skills_Are_Assigned()
        {
            var @class = _classes.First();
            
            _model.SetClass(@class);
            
            var skills = _skills.Except(@class.SkillProficienciesAvailable).Take(1).Union(@class.SkillProficienciesAvailable.Skip(1)).ToArray();
            
            _model.SetSkillProficienciesFromClass(skills);
            
            Assert.That(_model.CheckSkillProficienciesFromClass(), Is.False);
        }

        [Test]
        public void CheckStartingEquipmentFromClass_Should_Return_false_When_StartingEquipment_Is_Not_Assigned()
        {
            var @class = _classes.First();
            
            _model.SetClass(@class);
            
            Assert.That(_model.CheckStartingEquipmentFromClass(), Is.False);
        }
        
        [Test]
        public void CheckStartingEquipmentFromClass_Should_Return_True_When_StartingEquipment_from_Class_Is_Assigned()
        {
            var @class = _classes.First();
            
            _model.SetClass(@class);
            
            _model.SetStartingEquipmentFromClass(@class.StartingEquipmentOptions.First());
            
            Assert.That(_model.CheckStartingEquipmentFromClass(), Is.True);
        }
        
        [Test]
        [Ignore("This test requires at least two different classes. At the moment, only one class is available.")]
        public void CheckStartingEquipmentFromClass_Should_Return_false_When_StartingEquipment_From_Other_Class_Is_Assigned()
        {
            var assignedClass = _classes.First();
            var otherClass = _classes.Skip(1).First();
            
            _model.SetClass(@assignedClass);
            _model.SetStartingEquipmentFromClass(otherClass.StartingEquipmentOptions.First());
            
            Assert.That(_model.CheckStartingEquipmentFromBackground(), Is.False);
        }

        [Test]
        public void CheckStartingEquipmentFromBackground_Should_Return_false_When_StartingEquipment_Is_Not_Assigned()
        {
            var background = _backgrounds.First();
            
            _model.SetBackground(@background);
            
            Assert.That(_model.CheckStartingEquipmentFromBackground(), Is.False);
        }
        
        [Test]
        public void CheckStartingEquipmentFromBackground_Should_Return_True_When_StartingEquipment_from_Background_Is_Assigned()
        {
            var background = _backgrounds.First();

            _model.SetBackground(background);

            _model.SetStartingEquipmentFromBackground(background.StartingEquipmentOptions.First());
            
            Assert.That(_model.CheckStartingEquipmentFromBackground(), Is.True);
        }

        [Test]
        [Ignore("This test requires at least two different backgrounds. At the moment, only one background is available.")]
        public void CheckStartingEquipmentFromBackground_Should_Return_false_When_StartingEquipment_From_Other_Background_Is_Assigned()
        {
            var assignedBackground = _backgrounds.First();
            var otherBackground = _backgrounds.Skip(1).First();
            
            _model.SetBackground(assignedBackground);
            _model.SetStartingEquipmentFromBackground(otherBackground.StartingEquipmentOptions.First());
            
            Assert.That(_model.CheckStartingEquipmentFromBackground(), Is.False);
        }

        [Test]
        [TestCaseSource(typeof(CharacterStatsBuilderTestData), nameof(CharacterStatsBuilderTestData.CheckLanguagesTestCases))]
        public void CheckLanguages_Should_Return_false_When_Languages_Are_Not_As_Many_As_Expected(CheckLanguagesTestData testData)
        {
            foreach (var languageName in testData.Languages)
            {
                var standardLanguage = this._standardLanguages.SingleOrDefault(x => x.name == languageName);
                if (standardLanguage == null)
                {
                    Assert.Fail($"Language '{languageName}' was not found.");
                }
                _model.SetLanguage(standardLanguage);
            }
            
            Assert.That(_model.CheckLanguages(), Is.EqualTo(testData.ExpectedResult));
        }
        
        [TestCaseSource(typeof(CharacterStatsBuilderTestData), nameof(CharacterStatsBuilderTestData.CheckAllTestCases))]
        public void CheckAll_Should_Return_False_When_Any_Check_Returns_False(CheckAllTestData checkData)
        {
            var modelMock = new Mock<CharacterStats.Builder>(){ CallBase = true, };
            
            modelMock.Setup(x => x.CheckName()).Returns(checkData.CheckNameResult);
            modelMock.Setup(x => x.CheckClass()).Returns(checkData.CheckClassResult);
            modelMock.Setup(x => x.CheckSubClass()).Returns(checkData.CheckSubClassResult);
            modelMock.Setup(x => x.CheckBackground()).Returns(checkData.CheckBackgroundResult);
            modelMock.Setup(x => x.CheckSpex()).Returns(checkData.CheckSpexResult);
            modelMock.Setup(x => x.CheckAbilityStats()).Returns(checkData.CheckAbilityStatsResult);
            modelMock.Setup(x => x.CheckSkillProficienciesFromClass()).Returns(checkData.CheckSkillProficienciesFromClassResult);
            modelMock.Setup(x => x.CheckStartingEquipmentFromClass()).Returns(checkData.CheckStartingEquipmentFromClassResult);
            modelMock.Setup(x => x.CheckStartingEquipmentFromBackground()).Returns(checkData.CheckStartingEquipmentFromBackgroundResult);
            modelMock.Setup(x => x.CheckLanguages()).Returns(checkData.CheckLanguagesResult);

            var model = modelMock.Object;
            
            Assert.That(model.CheckAll(), Is.EqualTo(checkData.ExpectedResult));
        }

        [Test]
        public void Build_Should_Return_Null_When_Any_Check_Fails()
        {
            var modelMock = new Mock<CharacterStats.Builder>() { CallBase = true, };
            modelMock.Setup(x => x.CheckAll()).Returns(false);
            
            var result = modelMock.Object.Build();
            Assert.That(result, Is.Null);
        }
        
        [Test]
        public void CheckAll_Should_Return_True_When_All_Checks_Return_True()
        {
            var modelMock = new Mock<CharacterStats.Builder>(){ CallBase = true, };
            
            modelMock.Setup(x => x.CheckName()).Returns(true);
            modelMock.Setup(x => x.CheckClass()).Returns(true);
            modelMock.Setup(x => x.CheckSubClass()).Returns(true);
            modelMock.Setup(x => x.CheckBackground()).Returns(true);
            modelMock.Setup(x => x.CheckSpex()).Returns(true);
            modelMock.Setup(x => x.CheckAbilityStats()).Returns(true);
            modelMock.Setup(x => x.CheckSkillProficienciesFromClass()).Returns(true);
            modelMock.Setup(x => x.CheckStartingEquipmentFromClass()).Returns(true);
            modelMock.Setup(x => x.CheckStartingEquipmentFromBackground()).Returns(true);
            modelMock.Setup(x => x.CheckLanguages()).Returns(true);

            var model = modelMock.Object;
            
            Assert.That(model.CheckAll(), Is.EqualTo(true));
        }
        
        [Test]
        public void Build_Should_Return_CharacterStats_When_All_Is_As_Expected()
        {
            var characterName = _fixture.Create<string>();
            var @class = _classes.First();
            var subClass = @class.SubClasses.First();
            var background = _backgrounds.First();
            var spex = _species.First();
            var skillProficienciesFromClass = @class.SkillProficienciesAvailable.Take(@class.NumberOfSkillProficienciesToChoose).ToArray();
            var startingEquipmentFromClass = @class.StartingEquipmentOptions.First();
            var startingEquipmentFromBackground = background.StartingEquipmentOptions.First();
            var abilityStats = new List<AbilityStats>();
            var languages = new StandardLanguage[]
            {
                this._standardLanguages.Single(x => x.name == NameHelper.Languages.Common),
                this._standardLanguages.Single(x => x.name == NameHelper.Languages.Draconic),
                this._standardLanguages.Single(x => x.name == NameHelper.Languages.Elvish),
            };
            
            foreach (var ability in _abilities)
            {
                abilityStats.Add(this.GetValidAbilityStats(ability));
            }
            
            var builder = _model
                .SetName(characterName)
                .SetClass(@class)
                .SetSubClass(subClass)
                .SetBackground(background)
                .SetSpex(spex)
                .SetSkillProficienciesFromClass(skillProficienciesFromClass)
                .SetStartingEquipmentFromClass(startingEquipmentFromClass)
                .SetStartingEquipmentFromBackground(startingEquipmentFromBackground);
            
            foreach (var abilityStat in abilityStats)
            {
                builder.SetAbilityStats(abilityStat);
            }
            
            foreach (var language in languages)
            {
                builder.SetLanguage(language);
            }
                
            _instance = builder.Build();
            
            // Assertions
            Assert.That(_instance, Is.Not.Null);
            Assert.That(_instance.CharacterName, Is.EqualTo(characterName));
            Assert.That(_instance.Class, Is.EqualTo(@class));
            Assert.That(_instance.SubClass, Is.EqualTo(subClass));
            Assert.That(_instance.Spex, Is.EqualTo(spex));
            Assert.That(_instance.Background, Is.EqualTo(background));
            Assert.That(_instance.Level, Is.EqualTo(CharacterStats.Builder.DefaultLevel));
            Assert.That(_instance.Xp, Is.EqualTo(CharacterStats.Builder.DefaultXp));
            
            Assert.That(_instance.ArmorTraining, Is.SupersetOf(@class.ArmourTraining));
            Assert.That(_instance.WeaponProficiencies, Is.SupersetOf(@class.WeaponProficiencies));
            Assert.That(_instance.SkillProficiencies, Is.SubsetOf(@class.SkillProficienciesAvailable.Union(background.SkillProficiencies)));
            Assert.That(_instance.Inventory, Is.SupersetOf(@class.StartingEquipmentOptions.Single(x => x.Equals(startingEquipmentFromClass)).EquipmentsWithAmountList));
            Assert.That(_instance.Inventory, Is.SupersetOf(background.StartingEquipmentOptions.Single(x => x.Equals(startingEquipmentFromBackground)).EquipmentsWithAmountList));
            Assert.That(_instance.SavingThrowProficiencies, Is.SupersetOf(@class.SavingThrowProficiencies));
            Assert.That(_instance.ToolProficiencies, Does.Contain(background.ToolProficiency));
            
            Assert.That(_instance.Abilities.Values, Is.EquivalentTo(abilityStats));
            foreach (var ability in abilityStats)
            {
                Assert.That(_instance.Abilities[ability.Ability.name].SavingThrow, Is.EqualTo(@class.SavingThrowProficiencies.Contains(ability.Ability)));
                Assert.That(
                    _instance.Abilities[ability.Ability.name].SkillProficiencies.Select(x =>x.Value.Skill), 
                    Is.EquivalentTo(_instance.SkillProficiencies.Where(x => x.Ability == ability.Ability)));
            };
            
            Assert.That(_instance.Languages, Is.EquivalentTo(languages));
            
            var currentLevel = @class.Levels.Single(lvl => lvl.LevelNum == CharacterStats.Builder.DefaultLevel);
            Assert.That(_instance.ClassFeatures, Is.EquivalentTo(currentLevel.ClassFeatures));
            Assert.That(_instance.ClassFeatureStats, Is.EqualTo(currentLevel.ClassFeatureStats));
        }
        
        private AbilityStats GetValidAbilityStats(Ability ability)
            =>_fixture
                .Build<AbilityStats>()
                .With(x => x.Score,  _fixture.Create<int>() % 20 + 1)
                .With(x => x.Ability, ability)
                .Without(x => x.SkillProficiencies)
                .Create();
    }
    

    public class CheckAllTestData
    {
        public bool CheckNameResult = true;
        public bool CheckClassResult = true;
        public bool CheckSubClassResult = true;
        public bool CheckBackgroundResult = true;
        public bool CheckSpexResult = true;
        public bool CheckAbilityStatsResult = true;
        public bool CheckSkillProficienciesFromClassResult = true;
        public bool CheckStartingEquipmentFromClassResult = true;
        public bool CheckStartingEquipmentFromBackgroundResult = true;
        public bool CheckLanguagesResult = true;
        public bool ExpectedResult = true;
    }

    public class CheckLanguagesTestData
    {
        public List<string> Languages { get; } = new ();
        public bool ExpectedResult = true;
    }

    public class CharacterStatsBuilderTestData
    {
        public static IEnumerable AbilityScoresMinusOneTestCases
        {
            get
            {
                var abilities =
                    ScriptableObjectHelper.GetAllScriptableObjects<Ability>(PathHelper.Abilities.AbilitiesPath);

                foreach (var ability in abilities)
                {
                    yield return new TestCaseData(abilities.Except(new [] { ability }), ability);
                }
            }
        }

        public static IEnumerable CheckAllTestCases
        {
            get
            {
                yield return new CheckAllTestData()
                {
                    CheckBackgroundResult = false,
                    ExpectedResult = false,
                };
                yield return new CheckAllTestData()
                {
                    CheckClassResult = false,
                    ExpectedResult = false,
                };
                yield return new CheckAllTestData()
                {
                    CheckNameResult = false,
                    ExpectedResult = false,
                };
                yield return new CheckAllTestData()
                {
                    CheckSpexResult = false,
                    ExpectedResult = false,
                };
                yield return new CheckAllTestData()
                {
                    CheckStartingEquipmentFromClassResult = false,
                    ExpectedResult = false,
                };
                yield return new CheckAllTestData()
                {
                    CheckAbilityStatsResult = false,
                    ExpectedResult = false,
                };
                yield return new CheckAllTestData()
                {
                    CheckSubClassResult = false,
                    ExpectedResult = false,
                };
                yield return new CheckAllTestData()
                {
                    CheckSkillProficienciesFromClassResult = false,
                    ExpectedResult = false,
                };
                yield return new CheckAllTestData()
                {
                    CheckStartingEquipmentFromBackgroundResult = false,
                    ExpectedResult = false,
                };
                yield return new CheckAllTestData()
                {
                    CheckLanguagesResult = false,
                    ExpectedResult = false,
                };
                yield return new CheckAllTestData() { };
            }
        }

        public static IEnumerable CheckLanguagesTestCases
        {
            get
            {
                yield return new CheckLanguagesTestData()
                {
                    Languages = { NameHelper.Languages.Common },
                    ExpectedResult = false,
                };
                yield return new CheckLanguagesTestData()
                {
                    Languages = { NameHelper.Languages.Draconic },
                    ExpectedResult = false,
                };
                yield return new CheckLanguagesTestData()
                {
                    Languages =
                    {
                        NameHelper.Languages.Common,
                        NameHelper.Languages.Draconic,
                    },
                    ExpectedResult = false,
                };
                yield return new CheckLanguagesTestData()
                {
                    Languages =
                    {
                        NameHelper.Languages.Elvish,
                        NameHelper.Languages.Draconic,
                    },
                    ExpectedResult = true,
                };
                yield return new CheckLanguagesTestData()
                {
                    Languages =
                    {
                        NameHelper.Languages.Common,
                        NameHelper.Languages.Elvish,
                        NameHelper.Languages.Draconic,
                    },
                    ExpectedResult = true,
                };
                yield return new CheckLanguagesTestData()
                {
                    Languages =
                    {
                        NameHelper.Languages.Common,
                        NameHelper.Languages.Elvish,
                        NameHelper.Languages.Draconic,
                        NameHelper.Languages.Gnomish,
                    },
                    ExpectedResult = false,
                };
                
                yield return new CheckLanguagesTestData()
                {
                    Languages =
                    {
                        NameHelper.Languages.Elvish,
                        NameHelper.Languages.Draconic,
                        NameHelper.Languages.Gnomish,
                    },
                    ExpectedResult = false,
                };
                
                yield return new CheckLanguagesTestData()
                {
                    Languages =
                    {
                        NameHelper.Languages.Elvish,
                        NameHelper.Languages.Draconic,
                        NameHelper.Languages.Gnomish,
                        NameHelper.Languages.Goblin,
                    },
                    ExpectedResult = false,
                };
            }
        }
    }
}