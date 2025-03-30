using System.Collections;
using System.Linq;
using DnD.Code.Scripts.Classes;
using DnD.Code.Scripts.Classes.Barbarian.FeatureProperties;
using DnD.Code.Scripts.Helpers.NameHelper;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Tests.Classes.Barbarian
{
    [TestFixture]
    public class Barbarian
    {
        private static readonly string ClassName = NameHelper.Classes.Barbarian;
        private Class _class;
        
        [SetUp]
        public void Setup()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(Class)}");
            _class =  guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Class>)
                .Single(asset => asset.name == NameHelper.Classes.Barbarian);
        }

        [TestCaseSource(typeof(ClassData), nameof(ClassData.ClassTestCases))]
        public void TestClass(ClassTestModel expected)
        {
            Assert.That(_class, Is.Not.Null, $"Class {expected.Name} was null.");
            Assert.That(_class.DisplayName, Is.EqualTo(expected.DisplayName),  $"{expected.DisplayName}: {nameof(expected.DisplayName)} not equal to {expected.DisplayName}.");
            Assert.That(_class.DisplayDescription, Is.EqualTo(expected.DisplayDescription),  $"{expected.DisplayName}: {nameof(expected.DisplayDescription)} not equal to {expected.DisplayDescription}.");
            Assert.That(_class.PrimaryAbility.name, Is.EqualTo(expected.PrimaryAbility),  $"{expected.DisplayName}: {nameof(expected.PrimaryAbility)} not equal to {expected.PrimaryAbility}.");
            Assert.That(_class.HitPointDie.name, Is.EqualTo(expected.HitPointDie),  $"{expected.DisplayName}: {nameof(expected.HitPointDie)} not equal to {expected.HitPointDie}.");
            Assert.That(_class.NumberOfSkillProficienciesToChoose, Is.EqualTo(expected.NumberOfSkillProficienciesToChoose),  $"{expected.DisplayName}: {nameof(expected.NumberOfSkillProficienciesToChoose)} not equal to {expected.NumberOfSkillProficienciesToChoose}.");

        }
        
        [TestCaseSource(typeof(ClassData), nameof(ClassData.SavingThrowProficienciesTestCases))]
        public void TestSavingThrowProficiencyTestCases(string expected)
        {
            Assert.That(_class.SavingThrowProficiencies.Select(x => x.name), Does.Contain(expected),  $"{_class.DisplayName} SavingThrowProficiencies: {nameof(expected)} not found.");
        }
        
        [TestCaseSource(typeof(ClassData), nameof(ClassData.SkillProficienciesAvailableTestCases))]
        public void TestSkillProficienciesAvailableTestCases(string expected)
        {
            Assert.That(_class.SkillProficienciesAvailable.Select(x => x.name), Does.Contain(expected),  $"{_class.DisplayName} SkillProficienciesAvailable: {nameof(expected)} not found.");
        }
        
        [TestCaseSource(typeof(ClassData), nameof(ClassData.WeaponProficienciesTestCases))]
        public void TestWeaponProficienciesTestCases(string expected)
        {
            Assert.That(_class.WeaponProficiencies.Select(x => x.name), Does.Contain(expected),  $"{_class.DisplayName} WeaponProficiencies: {nameof(expected)} not found.");
        }
        
        [TestCaseSource(typeof(ClassData), nameof(ClassData.ArmourTrainingTestCases))]
        public void TestArmourTrainingTestCases(string expected)
        {
            Assert.That(_class.ArmourTraining.Cast<ScriptableObject>().Select(x => x.name), Does.Contain(expected),  $"{_class.DisplayName} ArmorTraining: {nameof(expected)} not found.");
        }
        
        [TestCaseSource(typeof(ClassData), nameof(ClassData.StartingEquipmentOptionsTestCases))]
        public void TestStartingEquipmentOptionsTestCases(StartingEquipmentTestModel expected)
        {
            var startingEquipmentOption =  _class.StartingEquipmentOptions.SingleOrDefault(option => option.name == expected.Name);
            Assert.That(startingEquipmentOption, Is.Not.Null,  $"StartingEquipmentOption {expected.Name} was null.");

            foreach (var expectedItem in expected.Items)
            {
                var item = startingEquipmentOption.EquipmentsWithAmountList.SingleOrDefault(item => item.Equipment.name == expectedItem.ItemName);
                Assert.That(item, Is.Not.Null,  $"Item {expected.Name} was null.");

                Assert.That(item.Amount, Is.EqualTo(expectedItem.Amount),
                    $"{expectedItem.ItemName}: {nameof(expectedItem.Amount)} not equal to {item.Amount}.");
            }
        }

        [TestCaseSource(typeof(ClassData), nameof(ClassData.LevelsTestCases))]
        public void TestLevelsTestCases(LevelTestModel expected)
        {
            var level = _class.Levels.SingleOrDefault(level => level.name == expected.Name);
            Assert.That(level, Is.Not.Null);
            
            TestLevel(level, expected);
        }
        
        [TestCaseSource(typeof(ClassData), nameof(ClassData.SubClassesTestCases))]
        public void TestSubClassesTestCases(SubClassTestModel expected)
        {
            var subClass = _class.SubClasses.SingleOrDefault(subClass => subClass.name == expected.Name);
            Assert.That(subClass, Is.Not.Null,  $"SubClass {expected.Name} was null.");
            Assert.That(subClass.DisplayName, Is.EqualTo(expected.DisplayName),  $"{expected.DisplayName}: {nameof(expected.DisplayName)} not equal to {expected.DisplayName}.");
            Assert.That(subClass.DisplayDescription, Is.EqualTo(expected.DisplayDescription),  $"{expected.DisplayName}: {nameof(expected.DisplayDescription)} not equal to {expected.DisplayDescription}.");
        }
        
        [TestCaseSource(typeof(ClassData), nameof(ClassData.SubLevelsTestCases))]
        public void TestSubLevelsTestCases(SubLevelTestModel expected)
        {
            var subClass = _class.SubClasses.SingleOrDefault(subClass => subClass.name == expected.SubClassName);
            Assert.That(subClass, Is.Not.Null,  $"SubClass {expected.Name} was null.");
            
            var levels = new[] { subClass.Level03, subClass.Level06, subClass.Level10, subClass.Level14 };
            var level = levels.SingleOrDefault(level => level.name == expected.Name);
            
            TestLevel(level, expected);
        }
        
        private void TestLevel(Level actual, LevelTestModel expected)
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.DisplayName, Is.EqualTo(expected.DisplayName),  $"{expected.LevelNum}.{expected.DisplayName}: {nameof(expected.DisplayName)} not equal to {expected.DisplayName}.");
            Assert.That(actual.DisplayDescription, Is.EqualTo(expected.DisplayDescription),  $"{expected.LevelNum}.{expected.DisplayName}: {nameof(expected.DisplayDescription)} not equal to {expected.DisplayDescription}.");
            Assert.That(actual.LevelNum, Is.EqualTo(expected.LevelNum), $"{expected.LevelNum}.{expected.DisplayName}: {nameof(expected.LevelNum)} not equal to {expected.LevelNum}.");
            Assert.That(actual.ProficiencyBonus, Is.EqualTo(expected.ProficiencyBonus), $"{expected.LevelNum}.{expected.DisplayName}: {nameof(expected.ProficiencyBonus)} not equal to {expected.ProficiencyBonus}.");

            var levelClassFeatureNames = actual.ClassFeatures.Select(x => x.name).ToArray();
            foreach (var classFeature in expected.ClassFeatures)
            {
                Assert.That(levelClassFeatureNames, Does.Contain(classFeature));
            }
            
            Assert.That(actual.ClassFeatureTraits, Is.TypeOf<BarbarianFp>(), $"{expected.LevelNum}.{nameof(actual.ClassFeatureTraits)} is of the wrong type. Expected: {nameof(BarbarianFp)}.");
            
            var classFeatureTraits = (BarbarianFp)actual.ClassFeatureTraits;
            var expectedClassFeatureTraits = (BarbarianClassFeatureTraitsTestModel)expected.ClassFeatureTraits;
            Assert.That(classFeatureTraits.rages, Is.EqualTo(expectedClassFeatureTraits.Rages), $"{expected.LevelNum}.{nameof(actual.ClassFeatureTraits)}.Rages: {nameof(expectedClassFeatureTraits.Rages)} not equal to {expectedClassFeatureTraits.Rages}.");
            Assert.That(classFeatureTraits.rageDamage, Is.EqualTo(expectedClassFeatureTraits.RageDamage), $"{expected.LevelNum}.{nameof(actual.ClassFeatureTraits)}.{nameof(expectedClassFeatureTraits.RageDamage)}: {nameof(expectedClassFeatureTraits.RageDamage)} not equal to {expectedClassFeatureTraits.RageDamage}.");
            Assert.That(classFeatureTraits.weaponMastery, Is.EqualTo(expectedClassFeatureTraits.WeaponMastery), $"{expected.LevelNum}.{nameof(actual.ClassFeatureTraits)}.{nameof(expectedClassFeatureTraits.WeaponMastery)}: {nameof(expectedClassFeatureTraits.WeaponMastery)} not equal to {expectedClassFeatureTraits.WeaponMastery}.");
        }
        
        private class ClassData
        {
            public static IEnumerable ClassTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new ClassTestModel()
                        {
                            Name = NameHelper.Classes.Barbarian,
                            PrimaryAbility = NameHelper.Abilities.Strength,
                            HitPointDie = NameHelper.Dice.D12,
                            NumberOfSkillProficienciesToChoose = 2,
                        });
                }
            }

            public static IEnumerable SavingThrowProficienciesTestCases
            {
                get
                {
                    yield return NameHelper.Abilities.Strength;
                    yield return NameHelper.Abilities.Constitution;
                }
            }
            public static IEnumerable SkillProficienciesAvailableTestCases
            {
                get
                {
                    yield return NameHelper.Skills.AnimalHandling;
                    yield return NameHelper.Skills.Athletics;
                    yield return NameHelper.Skills.Intimidation;
                    yield return NameHelper.Skills.Nature;
                    yield return NameHelper.Skills.Perception;
                    yield return NameHelper.Skills.Survival;
                }
            }
            public static IEnumerable WeaponProficienciesTestCases
            {
                get
                {
                    yield return NameHelper.WeaponTypes.SimpleMeleeWeapon;
                    yield return NameHelper.WeaponTypes.SimpleRangedWeapon;
                    yield return NameHelper.WeaponTypes.MartialMeleeWeapon;
                    yield return NameHelper.WeaponTypes.MartialRangedWeapon;
                }
            }
            public static IEnumerable ArmourTrainingTestCases
            {
                get
                {
                    yield return NameHelper.ArmourType.LightArmour;
                    yield return NameHelper.ArmourType.MediumArmour;
                    yield return NameHelper.ArmourType.Shield;
                }
            }
            public static IEnumerable LevelsTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new LevelTestModel()
                        {
                            Name = $"{ClassName}.{NameHelper.Naming.Level}",
                            LevelNum = 1,
                            ProficiencyBonus = 2,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 2,
                                RageDamage = 2,
                                WeaponMastery = 2
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.Rage,
                                NameHelper.ClassFeatures_Barbarian.UnarmouredDefense,
                                NameHelper.ClassFeatures_Barbarian.WeaponMastery,
                            }
                        });
                    yield return new TestCaseData(
                        new LevelTestModel()
                        {
                            Name = $"{ClassName}.{NameHelper.Naming.Level}",
                            LevelNum = 2,
                            ProficiencyBonus = 2,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 2,
                                RageDamage = 2,
                                WeaponMastery = 2
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.DangerSense,
                                NameHelper.ClassFeatures_Barbarian.RecklessAttack,
                            }
                        });
                    yield return new TestCaseData(
                        new LevelTestModel()
                        {
                            Name = $"{ClassName}.{NameHelper.Naming.Level}",
                            LevelNum = 3,
                            ProficiencyBonus = 2,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 3,
                                RageDamage = 2,
                                WeaponMastery = 2
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.BarbarianSubclass,
                                NameHelper.ClassFeatures_Barbarian.PrimalKnowledge,
                            }
                        });
                    yield return new TestCaseData(
                        new LevelTestModel()
                        {
                            Name = $"{ClassName}.{NameHelper.Naming.Level}",
                            LevelNum = 4,
                            ProficiencyBonus = 2,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 3,
                                RageDamage = 2,
                                WeaponMastery = 3
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.AbilityScoreImprovement,
                            }
                        });
                    yield return new TestCaseData(
                        new LevelTestModel()
                        {
                            Name = $"{ClassName}.{NameHelper.Naming.Level}",
                            LevelNum = 5,
                            ProficiencyBonus = 3,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 3,
                                RageDamage = 2,
                                WeaponMastery = 3
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.ExtraAttack,
                                NameHelper.ClassFeatures_Barbarian.FastMovement,
                            }
                        });
                    yield return new TestCaseData(
                        new LevelTestModel()
                        {
                            Name = $"{ClassName}.{NameHelper.Naming.Level}",
                            LevelNum = 6,
                            ProficiencyBonus = 3,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 4,
                                RageDamage = 2,
                                WeaponMastery = 3
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.SubclassFeature,
                            }
                        });
                    yield return new TestCaseData(
                        new LevelTestModel()
                        {
                            Name = $"{ClassName}.{NameHelper.Naming.Level}",
                            LevelNum = 7,
                            ProficiencyBonus = 3,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 4,
                                RageDamage = 2,
                                WeaponMastery = 3
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.FeralInstinct,
                                NameHelper.ClassFeatures_Barbarian.InstinctivePounce,
                            }
                        });
                    yield return new TestCaseData(
                        new LevelTestModel()
                        {
                            Name = $"{ClassName}.{NameHelper.Naming.Level}",
                            LevelNum = 8,
                            ProficiencyBonus = 3,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 4,
                                RageDamage = 2,
                                WeaponMastery = 3
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.AbilityScoreImprovement,
                            }
                        });
                    yield return new TestCaseData(
                        new LevelTestModel()
                        {
                            Name = $"{ClassName}.{NameHelper.Naming.Level}",
                            LevelNum = 9,
                            ProficiencyBonus = 4,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 4,
                                RageDamage = 3,
                                WeaponMastery = 3
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.BrutalStrike,
                            }
                        });
                    yield return new TestCaseData(
                        new LevelTestModel()
                        {
                            Name = $"{ClassName}.{NameHelper.Naming.Level}",
                            LevelNum = 10,
                            ProficiencyBonus = 4,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 4,
                                RageDamage = 3,
                                WeaponMastery = 4
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.SubclassFeature,
                            }
                        });
                    yield return new TestCaseData(
                        new LevelTestModel()
                        {
                            Name = $"{ClassName}.{NameHelper.Naming.Level}",
                            LevelNum = 11,
                            ProficiencyBonus = 4,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 4,
                                RageDamage = 3,
                                WeaponMastery = 4
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.RelentlessRage,
                            }
                        });
                    yield return new TestCaseData(
                        new LevelTestModel()
                        {
                            Name = $"{ClassName}.{NameHelper.Naming.Level}",
                            LevelNum = 12,
                            ProficiencyBonus = 4,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 5,
                                RageDamage = 3,
                                WeaponMastery = 4
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.AbilityScoreImprovement,
                            }
                        });
                    yield return new TestCaseData(
                        new LevelTestModel()
                        {
                            Name = $"{ClassName}.{NameHelper.Naming.Level}",
                            LevelNum = 13,
                            ProficiencyBonus = 5,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 5,
                                RageDamage = 3,
                                WeaponMastery = 4
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.ImprovedBrutalStrike,
                            }
                        });
                    yield return new TestCaseData(
                        new LevelTestModel()
                        {
                            Name = $"{ClassName}.{NameHelper.Naming.Level}",
                            LevelNum = 14,
                            ProficiencyBonus = 5,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 5,
                                RageDamage = 3,
                                WeaponMastery = 4
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.SubclassFeature,
                            }
                        });
                    yield return new TestCaseData(
                        new LevelTestModel()
                        {
                            Name = $"{ClassName}.{NameHelper.Naming.Level}",
                            LevelNum = 15,
                            ProficiencyBonus = 5,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 5,
                                RageDamage = 3,
                                WeaponMastery = 4
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.PersistentRage,
                            }
                        });
                    yield return new TestCaseData(
                        new LevelTestModel()
                        {
                            Name = $"{ClassName}.{NameHelper.Naming.Level}",
                            LevelNum = 16,
                            ProficiencyBonus = 5,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 5,
                                RageDamage = 4,
                                WeaponMastery = 4
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.AbilityScoreImprovement,
                            }
                        });
                    yield return new TestCaseData(
                        new LevelTestModel()
                        {
                            Name = $"{ClassName}.{NameHelper.Naming.Level}",
                            LevelNum = 17,
                            ProficiencyBonus = 6,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 6,
                                RageDamage = 4,
                                WeaponMastery = 4
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.ImprovedBrutalStrike,
                            }
                        });
                    yield return new TestCaseData(
                        new LevelTestModel()
                        {
                            Name = $"{ClassName}.{NameHelper.Naming.Level}",
                            LevelNum = 18,
                            ProficiencyBonus = 6,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 6,
                                RageDamage = 4,
                                WeaponMastery = 4
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.SubclassFeature,
                            }
                        });
                    yield return new TestCaseData(
                        new LevelTestModel()
                        {
                            Name = $"{ClassName}.{NameHelper.Naming.Level}",
                            LevelNum = 19,
                            ProficiencyBonus = 6,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 6,
                                RageDamage = 4,
                                WeaponMastery = 4
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.EpicBoon,
                            }
                        });
                    yield return new TestCaseData(
                        new LevelTestModel()
                        {
                            Name = $"{ClassName}.{NameHelper.Naming.Level}",
                            LevelNum = 20,
                            ProficiencyBonus = 6,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 6,
                                RageDamage = 4,
                                WeaponMastery = 4
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.PrimalChampion,
                            }
                        });
                }
            }
            public static IEnumerable SubClassesTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new SubClassTestModel()
                        {
                            Name = NameHelper.BarbarianSubClasses.PathOfTheZealot,
                            ClassName = NameHelper.Classes.Barbarian,
                        });
                    yield return new TestCaseData(
                        new SubClassTestModel()
                        {
                            Name = NameHelper.BarbarianSubClasses.PathOfTheWildHeart,
                            ClassName = NameHelper.Classes.Barbarian,
                        });
                    yield return new TestCaseData(
                        new SubClassTestModel()
                        {
                            Name = NameHelper.BarbarianSubClasses.PathOfTheWorldTree,
                            ClassName = NameHelper.Classes.Barbarian,
                        });
                    yield return new TestCaseData(
                        new SubClassTestModel()
                        {
                            Name = NameHelper.BarbarianSubClasses.PathOfTheZealot,
                            ClassName = NameHelper.Classes.Barbarian,
                        });
                }
            }
            public static IEnumerable SubLevelsTestCases
            {
                get
                {
                    // PathOfTheZealot
                    yield return new TestCaseData(
                        new SubLevelTestModel()
                        {
                            SubClassName = NameHelper.BarbarianSubClasses.PathOfTheZealot,
                            Name = $"{ClassName}.{NameHelper.BarbarianSubClasses.PathOfTheZealot}.{NameHelper.Naming.Level}",
                            LevelNum = 3,
                            ProficiencyBonus = 2,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 2,
                                RageDamage = 2,
                                WeaponMastery = 2
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.DivineFury,
                                NameHelper.ClassFeatures_Barbarian.WarriorOfTheGods,
                            }
                        });
                    yield return new TestCaseData(
                        new SubLevelTestModel()
                        {
                            SubClassName = NameHelper.BarbarianSubClasses.PathOfTheZealot,
                            Name = $"{ClassName}.{NameHelper.BarbarianSubClasses.PathOfTheZealot}.{NameHelper.Naming.Level}",
                            LevelNum = 6,
                            ProficiencyBonus = 3,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 4,
                                RageDamage = 2,
                                WeaponMastery = 3
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.FanaticalFocus,
                            }
                        });
                    yield return new TestCaseData(
                        new SubLevelTestModel()
                        {
                            SubClassName = NameHelper.BarbarianSubClasses.PathOfTheZealot,
                            Name = $"{ClassName}.{NameHelper.BarbarianSubClasses.PathOfTheZealot}.{NameHelper.Naming.Level}",
                            LevelNum = 10,
                            ProficiencyBonus = 4,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 4,
                                RageDamage = 3,
                                WeaponMastery = 4
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.ZealousPresence,
                            }
                        });
                    yield return new TestCaseData(
                        new SubLevelTestModel()
                        {
                            SubClassName = NameHelper.BarbarianSubClasses.PathOfTheZealot,
                            Name = $"{ClassName}.{NameHelper.BarbarianSubClasses.PathOfTheZealot}.{NameHelper.Naming.Level}",
                            LevelNum = 14,
                            ProficiencyBonus = 5,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 5,
                                RageDamage = 3,
                                WeaponMastery = 4
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.RageOfTheGods,
                            }
                        });
                    
                    // PathOfTheWildHeart
                    yield return new TestCaseData(
                        new SubLevelTestModel()
                        {
                            SubClassName = NameHelper.BarbarianSubClasses.PathOfTheWildHeart,
                            Name = $"{ClassName}.{NameHelper.BarbarianSubClasses.PathOfTheWildHeart}.{NameHelper.Naming.Level}",
                            LevelNum = 3,
                            ProficiencyBonus = 2,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 2,
                                RageDamage = 2,
                                WeaponMastery = 2
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.AnimalSpeaker,
                                NameHelper.ClassFeatures_Barbarian.RageOfTheWilds,
                            }
                        });
                    yield return new TestCaseData(
                        new SubLevelTestModel()
                        {
                            SubClassName = NameHelper.BarbarianSubClasses.PathOfTheWildHeart,
                            Name = $"{ClassName}.{NameHelper.BarbarianSubClasses.PathOfTheWildHeart}.{NameHelper.Naming.Level}",
                            LevelNum = 6,
                            ProficiencyBonus = 3,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 4,
                                RageDamage = 2,
                                WeaponMastery = 3
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.AspectOfTheWilds,
                            }
                        });
                    yield return new TestCaseData(
                        new SubLevelTestModel()
                        {
                            SubClassName = NameHelper.BarbarianSubClasses.PathOfTheWildHeart,
                            Name = $"{ClassName}.{NameHelper.BarbarianSubClasses.PathOfTheWildHeart}.{NameHelper.Naming.Level}",
                            LevelNum = 10,
                            ProficiencyBonus = 4,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 4,
                                RageDamage = 3,
                                WeaponMastery = 4
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.NatureSpeaker,
                            }
                        });
                    yield return new TestCaseData(
                        new SubLevelTestModel()
                        {
                            SubClassName = NameHelper.BarbarianSubClasses.PathOfTheWildHeart,
                            Name = $"{ClassName}.{NameHelper.BarbarianSubClasses.PathOfTheWildHeart}.{NameHelper.Naming.Level}",
                            LevelNum = 14,
                            ProficiencyBonus = 5,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 5,
                                RageDamage = 3,
                                WeaponMastery = 4
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.PowerOfTheWilds,
                            }
                        });
                    
                    // PathOfTheWorldTree
                    yield return new TestCaseData(
                        new SubLevelTestModel()
                        {
                            SubClassName = NameHelper.BarbarianSubClasses.PathOfTheWorldTree,
                            Name = $"{ClassName}.{NameHelper.BarbarianSubClasses.PathOfTheWorldTree}.{NameHelper.Naming.Level}",
                            LevelNum = 3,
                            ProficiencyBonus = 2,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 2,
                                RageDamage = 2,
                                WeaponMastery = 2
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.VitalityOfTheTree,
                            }
                        });
                    yield return new TestCaseData(
                        new SubLevelTestModel()
                        {
                            SubClassName = NameHelper.BarbarianSubClasses.PathOfTheWorldTree,
                            Name = $"{ClassName}.{NameHelper.BarbarianSubClasses.PathOfTheWorldTree}.{NameHelper.Naming.Level}",
                            LevelNum = 6,
                            ProficiencyBonus = 3,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 4,
                                RageDamage = 2,
                                WeaponMastery = 3
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.BranchesOfTheTree,
                            }
                        });
                    yield return new TestCaseData(
                        new SubLevelTestModel()
                        {
                            SubClassName = NameHelper.BarbarianSubClasses.PathOfTheWorldTree,
                            Name = $"{ClassName}.{NameHelper.BarbarianSubClasses.PathOfTheWorldTree}.{NameHelper.Naming.Level}",
                            LevelNum = 10,
                            ProficiencyBonus = 4,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 4,
                                RageDamage = 3,
                                WeaponMastery = 4
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.BatteringRoots,
                            }
                        });
                    yield return new TestCaseData(
                        new SubLevelTestModel()
                        {
                            SubClassName = NameHelper.BarbarianSubClasses.PathOfTheWorldTree,
                            Name = $"{ClassName}.{NameHelper.BarbarianSubClasses.PathOfTheWorldTree}.{NameHelper.Naming.Level}",
                            LevelNum = 14,
                            ProficiencyBonus = 5,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 5,
                                RageDamage = 3,
                                WeaponMastery = 4
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.TravelAlongTheTree,
                            }
                        });
                    
                    // PathOfTheBerserker
                    yield return new TestCaseData(
                        new SubLevelTestModel()
                        {
                            SubClassName = NameHelper.BarbarianSubClasses.PathOfTheBerserker,
                            Name = $"{ClassName}.{NameHelper.BarbarianSubClasses.PathOfTheBerserker}.{NameHelper.Naming.Level}",
                            LevelNum = 3,
                            ProficiencyBonus = 2,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 2,
                                RageDamage = 2,
                                WeaponMastery = 2
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.Frenzy,
                            }
                        });
                    yield return new TestCaseData(
                        new SubLevelTestModel()
                        {
                            SubClassName = NameHelper.BarbarianSubClasses.PathOfTheBerserker,
                            Name = $"{ClassName}.{NameHelper.BarbarianSubClasses.PathOfTheBerserker}.{NameHelper.Naming.Level}",
                            LevelNum = 6,
                            ProficiencyBonus = 3,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 4,
                                RageDamage = 2,
                                WeaponMastery = 3
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.MindlessRage,
                            }
                        });
                    yield return new TestCaseData(
                        new SubLevelTestModel()
                        {
                            SubClassName = NameHelper.BarbarianSubClasses.PathOfTheBerserker,
                            Name = $"{ClassName}.{NameHelper.BarbarianSubClasses.PathOfTheBerserker}.{NameHelper.Naming.Level}",
                            LevelNum = 10,
                            ProficiencyBonus = 4,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 4,
                                RageDamage = 3,
                                WeaponMastery = 4
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.Retaliation,
                            }
                        });
                    yield return new TestCaseData(
                        new SubLevelTestModel()
                        {
                            SubClassName = NameHelper.BarbarianSubClasses.PathOfTheBerserker,
                            Name = $"{ClassName}.{NameHelper.BarbarianSubClasses.PathOfTheBerserker}.{NameHelper.Naming.Level}",
                            LevelNum = 14,
                            ProficiencyBonus = 5,
                            ClassFeatureTraits = new BarbarianClassFeatureTraitsTestModel()
                            {
                                Rages = 5,
                                RageDamage = 3,
                                WeaponMastery = 4
                            },
                            ClassFeatures = new[]
                            {
                                NameHelper.ClassFeatures_Barbarian.IntimidatingPresence,
                            }
                        });
                }
            }

            public static IEnumerable StartingEquipmentOptionsTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new StartingEquipmentTestModel()
                        {
                            Name = NameHelper.StartingEquipmentOptions.OptionA,
                            Items = new []
                            {
                                new ItemWithAmountTestModel()
                                {
                                    Amount = 1,
                                    ItemName = NameHelper.Weapons_MartialMelee.Greataxe
                                },
                                new ItemWithAmountTestModel()
                                {
                                    Amount = 4,
                                    ItemName = NameHelper.Weapons_SimpleMelee.Handaxe
                                },
                                new ItemWithAmountTestModel()
                                {
                                    Amount = 15,
                                    ItemName = NameHelper.CoinValues.GoldPiece
                                },
                            }
                        });
                    yield return new TestCaseData(
                        new StartingEquipmentTestModel()
                        {
                            Name = NameHelper.StartingEquipmentOptions.OptionB,
                            Items = new []
                            {
                                new ItemWithAmountTestModel()
                                {
                                    Amount = 75,
                                    ItemName = NameHelper.CoinValues.GoldPiece
                                },
                            }
                        });
                }
            }
        }
        
        public class BarbarianClassFeatureTraitsTestModel : IClassFeatureTraitsTestModel
        {
            public int Rages  { get; set; }
            public int RageDamage  { get; set; }
            public int WeaponMastery  { get; set; }
        }
    }
}